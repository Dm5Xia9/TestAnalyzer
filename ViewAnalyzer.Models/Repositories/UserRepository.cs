using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewAnalyzer.Core.Data;

namespace ViewAnalyzer.Models.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public User GetUser(string email, string password, string role)
        {
            return Table().Include(p => p.Role).FirstOrDefault(p => p.Email == email && p.Password == password && p.Role.Name == role);
        }

    }
}
