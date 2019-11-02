using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseDoc.Models
{
    public class TableInfo
    {
        public string TableName { get; set; }
        public string TableDescritpion { get; set; }
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public List<Columns> Columns { get; set; }
    }
}
