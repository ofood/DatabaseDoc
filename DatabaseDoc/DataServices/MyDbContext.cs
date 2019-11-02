using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;
namespace DatabaseDoc.DataServices
{
  public class MyDbContext
  {
    private static SqlSugarClient sqlSugarClient = null;
    private static readonly object obj = new object();
    private MyDbContext()
    {

    }
    public static SqlSugarClient Init
    {
      get
      {
        if (Init == null)
        {
          lock (obj)
          {
            if (Init == null)
            {
              ConnectionConfig config = new ConnectionConfig()
              {
                ConnectionString = "server=192.168.30.10;uid=headhunter;pwd=headhunterdev;database=HeadHunter",
                DbType = SqlSugar.DbType.SqlServer,//设置数据库类型
                IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
              };
              //用来打印Sql方便你调式    
      
              sqlSugarClient = new SqlSugarClient(config);
              sqlSugarClient.Aop.OnLogExecuting = (sql, pars) =>
              {
                Console.WriteLine(sql + "\r\n" +
                sqlSugarClient.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
              };
            }
          }
        }

        return sqlSugarClient;
      }
    }

  }
}
