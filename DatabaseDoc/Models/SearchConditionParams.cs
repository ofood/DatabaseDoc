using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseDoc.Models
{
    public class SearchConditionParams
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
        public string KeyWords { get; set; }
        public int ExistNull { get; set; }
    }
}
