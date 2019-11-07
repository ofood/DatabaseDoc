using DatabaseDoc.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseDoc.Core
{
    /// <summary>
    /// 仓储
    /// 协调领域和数据映射层，利用类似于集合的接口来访问领域对象
    /// </summary>
    public interface IRepository<T> where T:Entity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

        Task<bool> AddAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(T entity);
    }
}
