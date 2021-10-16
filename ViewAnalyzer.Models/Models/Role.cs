using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewAnalyzer.Core.Data;

namespace ViewAnalyzer.Models
{
    public class Role : BaseRecord
    {
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
