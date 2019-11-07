using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseDoc.Core.Entities
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public class Entity:IEntity
    {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int Id { get; set; }
    }
}
