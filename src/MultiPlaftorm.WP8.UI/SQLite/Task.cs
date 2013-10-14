using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlatform.Shared.SQLite
{
    [Table("Tasks")]
    public class Task
    {
        [PrimaryKey]
        public string IdTask { get; set; }

        [MaxLength(255)] 
        public string Name { get; set; }
    }
}
