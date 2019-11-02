using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseDoc.DataServices;
using DatabaseDoc.Models;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace DatabaseDoc.Controllers
{
    public class DocController : Controller
    {
        private SqlSugarClient _sugarClient = null;
        private string ConnectionStr = ConfigHelper.Configuration.GetSection("ConnectionStrings:SqlServer").Value;
        public DocController()
        {
            ConnectionConfig config = new ConnectionConfig()
            {
                ConnectionString = ConnectionStr,
                DbType = SqlSugar.DbType.SqlServer,//设置数据库类型
                IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
            };
            _sugarClient = new SqlSugarClient(config);
        }
        public IActionResult Index(int pageindex = 1, int pagesize = 10, string keywords = null)
        {
            ViewBag.keywords = keywords?.Trim();
            int counts = 0;
            var tables = GetTables(out counts, pageindex, pagesize, keywords);
            PagerOption pageOptions = new PagerOption()
            {
                ItemCount = counts,
                PageSize = pagesize, //5
                PageIndex = pageindex,
                CountNum = 5,
                Url = Request.Path.Value,
                Query = Request.Query
            };
            ViewBag.Option = pageOptions;
            return View(tables);
        }
        private List<TableInfo> GetTables(out int totals, int pageindex, int pagesize, string keywords = null)
        {
            try
            {

                var prams = new SugarParameter[]
                {
          new SugarParameter("@keywords",keywords),
          new SugarParameter("@startNum",pagesize*(pageindex-1)+1),
          new SugarParameter("@endNum",pagesize*pageindex)
                };
                var prams1 = new SugarParameter[]
                {
          new SugarParameter("@keywords",keywords)
                };
                var tables = _sugarClient.Ado.SqlQuery<TableInfo>(@"select tab.* from (
select ROW_NUMBER() over(order by a.crdate desc) as num, a.name as TableName, 
a.id, a.crdate as CreateTime,b.value as TableDescritpion from
  sysobjects a left join sys.extended_properties b on a.id=b.major_id and b.minor_id=0
where a.xtype = 'U' and (@keywords is null or a.name like ''+@keywords+'%' or a.id in(select id from syscolumns where name like ''+@keywords+'%'))
) tab where tab.num between @startNum and @endNum", prams);
                totals = (int)_sugarClient.Ado.GetScalar(@"select count(1) from  sysobjects where xtype = 'U' and(@keywords is null or name like '' + @keywords + '%')", prams1);
                foreach (var tb in tables)
                {
                    tb.Columns = GetColums(tb.Id);
                }
                return tables;
            }
            catch (Exception ex)
            {
                totals = 0;
                return null;
            }
        }
        private List<Columns> GetColums(int tableid)
        {
            try
            {
                var prams = new SugarParameter[]
                {
          new SugarParameter("@tableid",tableid)
                };
                string sql = @"SELECT 
    FieldName = a.name,
    IsIdentity = case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end,
    FieldType = b.name,
    OccupiedByte = a.length,
    TypeLength = COLUMNPROPERTY(a.id,a.name,'PRECISION'),
    DecimalNumber= isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0),
    'IsNull' = case when a.isnullable=1 then '√'else '' end,
    DefaultValue = isnull(e.text,''),
    ColumnDescription = isnull(g.[value],'')
FROM 
    syscolumns a
left join 
    systypes b 
on 
    a.xusertype=b.xusertype
left join 
    syscomments e 
on 
    a.cdefault=e.id
left join 
    
    sys.extended_properties g 
on 
    a.id=g.major_id and a.colid=g.minor_id  
where 
    a.id=@tableid 
order by 
    a.id,a.colorder";
                return _sugarClient.Ado.SqlQuery<Columns>(sql, prams);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}