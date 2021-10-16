using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewAnalyzer.Api;
using ViewAnalyzer.Api.Models;
using ViewAnalyzer.Models;
using ViewAnalyzer.Wpf.Services;

namespace ViewAnalyzer.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //TODO: разобраться с MVVM

        NavigationManager navigationManager;
        DataFactory dataFactory;

        public List<Study> SelectStudies { get; set; } = new List<Study>();
        public Models.Analyzer SelectAnalyzer { get; set; }

        public MainWindow(NavigationManager navigationManager, DataFactory dataFactory)
        {
            this.navigationManager = navigationManager;
            this.dataFactory = dataFactory;

            InitializeComponent();

            ListAnalyzers.ItemsSource = dataFactory.Create().Global.GetIncludeAnalyzers();
        }

        private async void ListAnalyzers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 1)
                return;

            SelectAnalyzer = e.AddedItems[0] as Models.Analyzer;
            if (SelectAnalyzer != null)
            {
                ListStudy.ItemsSource = SelectAnalyzer.Studies;
            }

            await Refrash();
        }

        private void ListStudy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectStudies.AddRange(e.AddedItems.Cast<Models.Study>());
            SelectStudies.RemoveAll(p => e.RemovedItems.Cast<Models.Study>().Select(p => p.Id).Contains(p.Id));
        }

        public string GetResultMessage(AnalyzerResult result)
        {
            var listStudies = result.Studies;

            var str = new StringBuilder();

            str.AppendLine(result.Patient);
            if (result.Error != null)
            {
                str.AppendLine($"Ошибка: {result.Error}");

                str.AppendLine(result.GetStateText());

                return str.ToString();
            }

            foreach (var study in listStudies)
            {
                var studyResult = result.Result.FirstOrDefault(p => p.Code == study.ServiceCode);

                if (studyResult != null)
                    str.AppendLine($"{study.Name} : {studyResult.Result}");
                else
                    str.AppendLine($"{study.Name} : NoN");
            }

            str.AppendLine(result.GetStateText());

            return str.ToString();
        }



        public async Task Refrash()
        {
            ListResult.Items.Clear();

            using var db = dataFactory.Create();

            var idStop = false;

            var listResults = new List<AnalyzerResult>();

            SelectAnalyzer.Studies.ForEach(p => listResults.AddRange(db.Global.GetResultByStudy(p, SelectAnalyzer)));

            listResults = listResults.GroupBy(p => p.Id)
                .Select(p => p.First()).OrderBy(p => p.CreatedAt).ToList();

            foreach(var result in listResults)
            {

                var stackPanel = new StackPanel
                {
                    Margin = new Thickness(5),
                };

                if (result.ResultState == ResultState.Process)
                {
                    var loaded = new TextBlock
                    {
                        Text = $"Загрузка: {result.Progress}",
                        FontSize = 16,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    stackPanel.Children.Add(loaded);
                    ListResult.Items.Add(stackPanel);

                    await Task.Run(() => WaitForCompletion(result, loaded));

                    await Refrash();

                    return;
                }

                var message = GetResultMessage(result);

                stackPanel.Children.Add(new TextBlock
                {
                    Text = message,
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Center
                });

                if (result.ResultState == ResultState.Expect)
                    AddExpect(stackPanel, result);

                ListResult.Items.Add(stackPanel);
            }
            
        }

        public async Task WaitForCompletion(AnalyzerResult analyzerResult, TextBlock loaded)
        {
            while (true)
            {
                await Task.Delay(1000);

                using var db = dataFactory.Create();

                var result = db.AnalyzerResults.GetById(analyzerResult.Id);

                if (result.ResultState == ResultState.Expect)
                    return;

                Dispatcher.Invoke(() => loaded.Text = $"Загрузка: {result.Progress}");
            }
        }

        private void AddExpect(StackPanel stackPanel, AnalyzerResult analyzerResult)
        {
            var completedButton = new Button
            {
                Content = $"Одобрить",
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            completedButton.Click += (_, _) => CompletedButton_Click(analyzerResult.Id);

            var noCompletedButton = new Button
            {
                Content = $"Отклонить",
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            noCompletedButton.Click += (_, _) => NoCompletedButton_Click(analyzerResult.Id);


            stackPanel.Children.Add(completedButton);
            stackPanel.Children.Add(noCompletedButton);
        }

        private async void NoCompletedButton_Click(long id)
        {
            using var db = dataFactory.Create();

            var analyzerResult = db.AnalyzerResults.GetById(id);

            analyzerResult.ResultState = ResultState.NoCompleted;

            db.AnalyzerResults.Update(analyzerResult);
            db.SaveChanges();

            await Refrash();
        }


        private async void CompletedButton_Click(long id)
        {
            using var db = dataFactory.Create();

            var analyzerResult = db.AnalyzerResults.GetById(id);

            analyzerResult.ResultState = ResultState.Completed;

            db.AnalyzerResults.Update(analyzerResult);
            db.SaveChanges();

            await Refrash();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using var db = dataFactory.Create();

            ErrorMessage.Text = "";

            if (!db.Global.IsActiveAnalyzer(SelectAnalyzer))
            {
                ErrorMessage.Text = "Анализатор занят";
                return;
            }

            await StartAnalyzer(SelectAnalyzer, SelectStudies, NameRes.Text);
            await Refrash();
        }

        private async Task StartAnalyzer(Models.Analyzer selectAnalyzer, List<Study> studies,string patient)
        {

            var processor = dataFactory.GetService<AnalyzerProcessor>();

            var analyzer = new Analyzer<ServiceInfoRequest>
            {
                AnalyzerId = selectAnalyzer.Id,
                Name = selectAnalyzer.Name,
                Patient = patient,
                Services = studies.Select(p => new ServiceInfoRequest
                {
                    ServiceCode = p.ServiceCode,
                    Study = p,
                }).ToList(),
            };

            await Task.Run(() => processor.Start(analyzer));

            Thread.Sleep(1000);
        }
    }
}
