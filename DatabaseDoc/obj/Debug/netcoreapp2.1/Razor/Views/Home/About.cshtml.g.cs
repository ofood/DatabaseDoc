#pragma checksum "C:\Users\PeterH\Desktop\DatabaseDoc\DatabaseDoc\Views\Home\About.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6ea0e4a4d6326d85c8eebfd1afbaff22c917f437"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_About), @"mvc.1.0.view", @"/Views/Home/About.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/About.cshtml", typeof(AspNetCore.Views_Home_About))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\PeterH\Desktop\DatabaseDoc\DatabaseDoc\Views\_ViewImports.cshtml"
using DatabaseDoc;

#line default
#line hidden
#line 2 "C:\Users\PeterH\Desktop\DatabaseDoc\DatabaseDoc\Views\_ViewImports.cshtml"
using DatabaseDoc.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ea0e4a4d6326d85c8eebfd1afbaff22c917f437", @"/Views/Home/About.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"15d09217ceaa0ee4acef7558c77904659158f3ce", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_About : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\PeterH\Desktop\DatabaseDoc\DatabaseDoc\Views\Home\About.cshtml"
  
    ViewData["Title"] = "About";

#line default
#line hidden
            BeginContext(41, 4, true);
            WriteLiteral("<h2>");
            EndContext();
            BeginContext(46, 17, false);
#line 4 "C:\Users\PeterH\Desktop\DatabaseDoc\DatabaseDoc\Views\Home\About.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(63, 11, true);
            WriteLiteral("</h2>\r\n<h3>");
            EndContext();
            BeginContext(75, 19, false);
#line 5 "C:\Users\PeterH\Desktop\DatabaseDoc\DatabaseDoc\Views\Home\About.cshtml"
Write(ViewData["Message"]);

#line default
#line hidden
            EndContext();
            BeginContext(94, 81, true);
            WriteLiteral("</h3>\r\n\r\n<p>Use this area to provide additional information.</p>\r\n<test></test>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591