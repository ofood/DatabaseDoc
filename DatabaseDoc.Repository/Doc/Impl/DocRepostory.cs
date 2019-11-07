using DatabaseDoc.Core;
using DatabaseDoc.Repository.Doc.Entities;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace DatabaseDoc.Repository.Doc
{
    public class DocRepostory:IDocRepostory
    {
        private readonly DapperDbContext _dapperDbContext;
        public DocRepostory(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }

        public Task<bool> AddAsync(TableInfo entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 查询表字段信息
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<FieldInfo>> GetFieldInfosAsync(int tableId)
        {
            string sql = @"SELECT 
    FieldName = a.name,
    IsIdentity = case when COLUMNPROPERTY(a.id, a.name,'IsIdentity')= 1 then '√'else '' end,
    FieldType = b.name,
    OccupiedByte = a.length,
    TypeLength = COLUMNPROPERTY(a.id, a.name, 'PRECISION'),
    DecimalNumber = isnull(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0),
    'IsNull' = case when a.isnullable = 1 then '√'else '' end,
    DefaultValue = isnull(e.text, ''),
    ColumnDescription = isnull(g.[value], '')
FROM
    syscolumns a
left join
    systypes b
on
    a.xusertype = b.xusertype
left join
    syscomments e
on
    a.cdefault = e.id
left join


    sys.extended_properties g
on
    a.id = g.major_id and a.colid = g.minor_id
where
    a.id = @tableid
order by
    a.id,a.colorder";
            return await _dapperDbContext.QueryAsync<FieldInfo>(sql,new { tableid=tableId});
        }
        public async Task<IEnumerable<TableInfo>> GetAllAsync()
        {
            return await _dapperDbContext.QueryAsync<TableInfo>("");
        }
        public async Task<int> GetCountAsync(string keywords = null, int existnull = 0)
        {
            string commonSql = @";with FieldDescNullObj as 
(
select distinct e.id
from sysobjects e inner join syscolumns c on e.id=c.id and e.xtype='U'
left join sys.extended_properties d on c.id=d.major_id and c.colid=d.minor_id
left join sys.extended_properties f on f.minor_id=0 and f.major_id=c.id
where d.value is  null or f.value is  null
)";
            return  await _dapperDbContext.QueryFirstOrDefaultAsync<int>(string.Format(@"{0} 
select count(1) from  sysobjects a {1} 
where a.xtype = 'U' and(@keywords is null or a.name like '' + @keywords + '%')", commonSql, existnull == 1 ? "inner join FieldDescNullObj c on c.id=a.id" : ""), new { keywords = keywords });
        }
        public async Task<IEnumerable<TableInfo>> GetAllAsync(int pageindex, int pagesize, string keywords = null, int existnull = 0)
        {

            string commonSql = @";with FieldDescNullObj as 
(
select distinct e.id
from sysobjects e inner join syscolumns c on e.id=c.id and e.xtype='U'
left join sys.extended_properties d on c.id=d.major_id and c.colid=d.minor_id
left join sys.extended_properties f on f.minor_id=0 and f.major_id=c.id
where d.value is  null or f.value is  null
)";
            string sql = string.Format(@"{0}

select tab.* from (
select ROW_NUMBER() over(order by a.crdate desc) as num, a.name as TableName, 
a.id, a.crdate as CreateTime,b.value as TableDescritpion from
  sysobjects a {1}
  left join sys.extended_properties b on a.id=b.major_id and b.minor_id=0
where a.xtype = 'U' and (@keywords is null or a.name like ''+@keywords+'%' or a.id in(select id from syscolumns where name like ''+@keywords+'%'))
) tab where tab.num between @startNum and @endNum", commonSql, existnull == 1 ? "inner join FieldDescNullObj c on c.id=a.id" : "");
            
            return await _dapperDbContext.QueryAsync<TableInfo>(sql, new { 
            keywords=keywords,
                startNum= pagesize * (pageindex - 1) + 1,
                endNum= pagesize * pageindex
            });
        }
        public Task<TableInfo> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TableInfo entity)
        {
            throw new NotImplementedException();
        }
    }
}
