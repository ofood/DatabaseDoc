using DatabaseDoc.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseDoc.Repository.Doc.Entities
{
    public class TableInfo:Entity
    {
        public string TableName { get; set; }
        public string TableDescritpion { get; set; }
        public DateTime CreateTime { get; set; }
        public List<FieldInfo> FieldInfos { get; set; }
    }
}
