#pragma checksum "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Views/Shared/Components/CompanyServices/Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "83f33c9e1857c1aa66fa8599323b24ea45ba9295"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Allup_Backend.Views.Shared.Components.CompanyServices.Views_Shared_Components_CompanyServices_Default), @"mvc.1.0.view", @"/Views/Shared/Components/CompanyServices/Default.cshtml")]
namespace Allup_Backend.Views.Shared.Components.CompanyServices
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 2 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Views/_ViewImports.cshtml"
using Allup_Backend.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Views/_ViewImports.cshtml"
using Allup_Backend.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"83f33c9e1857c1aa66fa8599323b24ea45ba9295", @"/Views/Shared/Components/CompanyServices/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2992f23ae0e6e6f3f885ca4686f37f1880c75a31", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_CompanyServices_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CompanyServicesVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("brand"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("Icon"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n<div class=\"brand-area pt-80\">\n    <div class=\"container-fluid custom-container\">\n        <div class=\"row brand-active\">\n\n");
#nullable restore
#line 7 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Views/Shared/Components/CompanyServices/Default.cshtml"
             foreach (var item in Model.CompanySliders)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"col-lg-2\">\n                    <div class=\"single-brand\">\n                        <a href=\"#\">\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "83f33c9e1857c1aa66fa8599323b24ea45ba92954570", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 379, "~/assets/images/brand/", 379, 22, true);
#nullable restore
#line 12 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Views/Shared/Components/CompanyServices/Default.cshtml"
AddHtmlAttributeValue("", 401, item.ImageUrl, 401, 14, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                        </a>\n                    </div> <!-- single brand -->\n                </div>\n");
#nullable restore
#line 16 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Views/Shared/Components/CompanyServices/Default.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </div> <!-- row -->\n    </div> <!-- container -->\n</div>\n\n<section class=\"features-banner-area pt-80 pb-100\">\n    <div class=\"container-fluid custom-container\">\n        <div class=\"features-banner-wrapper d-flex flex-wrap\">\n");
#nullable restore
#line 25 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Views/Shared/Components/CompanyServices/Default.cshtml"
             foreach (var item in Model.ServicesSliders)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"single-features-banner d-flex\">\n                    <div class=\"banner-icon\">\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "83f33c9e1857c1aa66fa8599323b24ea45ba92957161", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 989, "~/assets/images/banner-icon/", 989, 28, true);
#nullable restore
#line 29 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Views/Shared/Components/CompanyServices/Default.cshtml"
AddHtmlAttributeValue("", 1017, item.ImageUrl, 1017, 14, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                    </div>\n                    <div class=\"banner-content media-body\">\n                        <h3 class=\"banner-title\">");
#nullable restore
#line 32 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Views/Shared/Components/CompanyServices/Default.cshtml"
                                            Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\n                        <p>");
#nullable restore
#line 33 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Views/Shared/Components/CompanyServices/Default.cshtml"
                      Write(item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\n                    </div>\n                </div> <!-- single features banner -->\n");
#nullable restore
#line 36 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Views/Shared/Components/CompanyServices/Default.cshtml"

            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n        </div> <!-- features banner wrapper -->\n    </div> <!-- container -->\n</section>\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CompanyServicesVM> Html { get; private set; }
    }
}
#pragma warning restore 1591