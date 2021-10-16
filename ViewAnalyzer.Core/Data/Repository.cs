using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewAnalyzer.Core.Data
{
    //репозиторий на коленке
    public class Repository<TModel> where TModel : class, IEntity, new()
    {
        DbContext dbContext;
        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected DbSet<TModel> Table()
        {
            return dbContext.Set<TModel>();
        }

        public TModel GetById(long id)
        {
            return Table().FirstOrDefault(p => p.Id == id);
        }

        public List<TModel> GetAllItem()
        {
            return Table().ToList();
        }

        public void Update(TModel model)
        {
            Table().Update(model);
        }
        public void Insert(TModel model)
        {
            Table().Add(model);
        }

        public List<TModel> GetMany(IEnumerable<long> ids)
        {
            return Table().Where(p => ids.Contains(p.Id)).ToList();
        }
    }

    public class Repository
    {
        DbContext dbContext;
        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected DbSet<TModel> Table<TModel>() where TModel: class, IEntity, new()
        {
            return dbContext.Set<TModel>();
        }

        public TModel GetById<TModel>(long id) where TModel : class, IEntity, new()
        {
            return Table<TModel>().FirstOrDefault(p => p.Id == id);
        }

        public List<TModel> GetAllItem<TModel>() where TModel : class, IEntity, new()
        {
            return Table<TModel>().ToList();
        }

        public void Update<TModel>(TModel model) where TModel : class, IEntity, new()
        {
            Table<TModel>().Update(model);
        }
        public void Insert<TModel>(TModel model) where TModel : class, IEntity, new()
        {
            Table<TModel>().Add(model);
        }
    }


}
