using System.Data.Common;

namespace DatabaseDoc.Core.DbProvider
{
    public interface IDbManager
    {
        DbConnection CreateConnection();
    }
}
