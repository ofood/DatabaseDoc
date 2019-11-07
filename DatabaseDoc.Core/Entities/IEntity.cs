namespace DatabaseDoc.Core.Entities
{
    /// <summary>
    /// 数据实体接口定义
    /// 自定义主键类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
    /// <summary>
    /// 数据实体接口定义
    /// 默认Int类型主键
    /// </summary>
    public interface IEntity : IEntity<int>
    {
    }
}
