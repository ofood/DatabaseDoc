using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatabaseDoc.Domain;
using DatabaseDoc.Domain.Doc;
using Microsoft.Extensions.Logging;
using DatabaseDoc.WebApp.Models;
using DatabaseDoc.Domain.Doc.Dtos;

namespace DatabaseDoc.WebApp.Controllers
{
    public class IndexController : Controller
    {
        private readonly IDocService _docService;
        private readonly ILogger _logger;
        public IndexController(IDocService docService,ILogger<IndexController> logger)
        {
            _docService = docService;
            _logger = logger;
        }
         
        public async Task<IActionResult> Index(SearchConditionParams query)
        {
            try
            {

                ViewBag.keywords = query.KeyWords?.Trim();
                ViewBag.IsNull = query.ExistNull;
                int counts = await _docService.GetRecordCounts(query);
                var tables = await _docService.GetTables(query);
                foreach (var tb in tables)
                {
                    tb.FieldInfos = await _docService.GetFields(tb.Id);
                }
                PagerOption pageOptions = new PagerOption()
                {
                    ItemCount = counts,
                    PageSize = query.PageSize, //5
                    PageIndex = query.PageIndex,
                    CountNum = 5,
                    Url = Request.Path.Value,
                    Query = Request.Query,
                };
                ViewBag.Option = pageOptions;
                ViewBag.TotalCount = counts;
                return View(tables);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return View();
        }
    }
}