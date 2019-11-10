using DatabaseDoc.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseDoc.Repository.Doc.Entities
{
    public class TableFieldInfo:Entity
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
        public string IsIdentity { get; set; }
        /// <summary>
        /// 是否主键
        /// </summary>
        //public bool IsPK { get; set; }
        /// <summary>
        /// 是否外键
        /// </summary>
        //public bool IsFK { get; set; }
        /// <summary>
        /// 字段描述
        /// </summary>
        public string ColumnDescription { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 是否为空 1：为空 0：不为空
        /// </summary>
        public string IsNullable { get; set; }
        /// <summary>
        /// 占用字节
        /// </summary>
        public string OccupiedByte { get; set; }
        /// <summary>
        /// 小数点位数
        /// </summary>
        public string NumericScale { get; set; }
    }
}
