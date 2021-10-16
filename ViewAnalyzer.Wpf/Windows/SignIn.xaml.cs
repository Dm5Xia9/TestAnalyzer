using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewAnalyzer.Models;
using ViewAnalyzer.Wpf.Services;

namespace ViewAnalyzer.Wpf
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        NavigationManager navigationManager;
        DataFactory dataFactory;
        public SignIn(NavigationManager navigationManager, DataFactory dataFactory)
        {
            this.navigationManager = navigationManager;
            this.dataFactory = dataFactory;

            InitializeComponent();
        }

        public void Authorization(string email, string password)
        {
            using var db = dataFactory.GetService<UserRoleManager>();

            var user = db.Users.GetUser(email, password, "Researcher");

            if (user != null)
            {
                navigationManager.Show<MainWindow>();
                Close();
                return;
            }

            ErrorLabel.Content = "Не верный логин или пароль";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Authorization(Login.Text, Password.Text);
        }
    }
}
