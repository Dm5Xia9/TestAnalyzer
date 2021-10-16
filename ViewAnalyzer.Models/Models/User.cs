using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewAnalyzer.Core.Data;

namespace ViewAnalyzer.Models
{
    public class User : BaseRecord
    {
        public string Email { get; set; }

        //плохое решение хранить не закодированный пароль
        public string Password { get; set; }

        public long RoleId { get; set; }
        public Role Role { get; set; }

    }
}
