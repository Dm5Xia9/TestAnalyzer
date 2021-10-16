using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewAnalyzer.Core.Data;
using ViewAnalyzer.Models.Repositories;

namespace ViewAnalyzer.Models
{
    public class Database : AbstractDatabase
    {
        public Database(AppDbContext dbContext): base(dbContext)
        {
            //TODO: Scope сервис в wpf?
            Analyzers = new Repository<Analyzer>(dbContext);
            AnalyzerResults = new Repository<AnalyzerResult>(dbContext);
            Studies = new Repository<Study>(dbContext);

            Global = new GlobalRepository(dbContext);
        }

        //Чтобы не тратить время на создание отдельного репозитория под каждую таблицу
        public GlobalRepository Global { get; set; }

        public Repository<Analyzer> Analyzers { get; set; }
        public Repository<AnalyzerResult> AnalyzerResults { get; set; }
        public Repository<Study> Studies { get; set; }
    }

    public class UserRoleManager : AbstractDatabase
    {
        public UserRoleManager(AppDbContext dbContext) : base(dbContext)
        {
            Users = new UserRepository(dbContext);
            Roles = new Repository<Role>(dbContext);
        }

        public UserRepository Users { get; set; }
        public Repository<Role> Roles { get; set; }

    }

    public abstract class AbstractDatabase : IDisposable
    {
        AppDbContext dbContext;
        public bool IsDisposed { get; private set; } = false;

        protected AbstractDatabase(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public virtual void Dispose()
        {
            if (!IsDisposed && dbContext != null)
            {
                lock (this)
                {
                    if (dbContext == null || IsDisposed)
                        return;

                    try
                    {
                        IsDisposed = true;

                        if (dbContext != null)
                            dbContext.Dispose();
                    }
                    finally
                    {
                        dbContext = null;
                        IsDisposed = true;
                    }
                }
            }
        }
    }
}
