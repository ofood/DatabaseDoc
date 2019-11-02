using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseDoc.Models
{
  /// <summary>
  /// 分页组件
  /// Query：主要还是用来接收url参数

///CountNum：显示的页面个数

///PageIndex：当前页

///PageSize：每页数据条数

///ItemCount：总数据条数

///TotalPage：总页数

///Url：分页请求的地址
  /// </summary>
  public class PagerOption
  {
    public int PageIndex { get; set; }
    public int PageSize { get; set; }

    public int CountNum { get; set; }
    public int ItemCount { get; set; }
    public int TotalPage
    {
      get
      {
        return ItemCount % PageSize > 0 ? (ItemCount / PageSize + 1) : ItemCount / PageSize;
      }
    }
    public string Url { get; set; }

    public IQueryCollection Query { get; set; }
  }
}
