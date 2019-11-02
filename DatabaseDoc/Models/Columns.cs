using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseDoc.Models
{
    /// <summary>
    /// 数据库文档数据结构
    /// </summary>
    public class Columns
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string FieldType { get; set; }
        /// <summary>
        /// 字段最大长度
        /// </summary>
        public int TypeLength { get; set; }
        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsIdentity { get; set; }
        /// <summary>
        /// 字段描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 是否为空 1：为空 0：不为空
        /// </summary>
        public bool IsNull { get; set; }

        public string OccupiedByte { get; set; }
        public string DecimalNumber { get; set; }
    }
}
