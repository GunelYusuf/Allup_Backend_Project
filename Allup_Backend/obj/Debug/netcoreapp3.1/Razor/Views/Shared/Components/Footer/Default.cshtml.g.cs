#pragma checksum "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Views/Shared/Components/Footer/Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "589765dd0284fb5fe69a18adf4c8dfc6c87f14fb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Allup_Backend.Views.Shared.Components.Footer.Views_Shared_Components_Footer_Default), @"mvc.1.0.view", @"/Views/Shared/Components/Footer/Default.cshtml")]
namespace Allup_Backend.Views.Shared.Components.Footer
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"589765dd0284fb5fe69a18adf4c8dfc6c87f14fb", @"/Views/Shared/Components/Footer/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2992f23ae0e6e6f3f885ca4686f37f1880c75a31", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_Footer_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Footer>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/images/payment.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("payment"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(@"
<section class=""footer-area bg_cover"" style=""background-image: url(/assets/images/bg-footer.jpg)"">
    <div class=""footer-widget pt-30 pb-70"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-lg-4"">
                    <div class=""footer-contact mt-50"">
                        <h4 class=""footer-title"">Contact Details</h4>

                        <ul>
                            <li><i class=""fas fa-map-marker-alt""></i> 45 Grand Central Terminal New York,NY 1017 United State USA</li>
                            <li><i class=""fas fa-phone""></i> <a href=""tell:123-456-789"">");
#nullable restore
#line 13 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Views/Shared/Components/Footer/Default.cshtml"
                                                                                   Write(Model.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\n                            <li><i class=\"fas fa-envelope\"></i><a href=\"mailto://email@yourwebsitename.com\">");
#nullable restore
#line 14 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Views/Shared/Components/Footer/Default.cshtml"
                                                                                                       Write(Model.EmailAddress);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a></li>
                            <li><i class=""far fa-clock""></i> Mon-Sat 9:00pm - 5:00pm Sun:Closed</li>
                        </ul>
                    </div> <!-- footer contact -->
                </div>
                <div class=""col-lg-8"">
                    <div class=""footer-link-wrapper d-flex flex-wrap justify-content-between"">
                        <div class=""footer-link mt-50"">
                            <h4 class=""footer-title"">Information</h4>

                            <ul class=""link"">
                                <li><a href=""#"">Delivery</a></li>
                                <li><a href=""#"">Legal Notice</a></li>
                                <li><a href=""about.html"">About us</a></li>
                                <li><a href=""#"">Secure payment</a></li>
                                <li><a href=""contact.html"">Contact us</a></li>
                            </ul>
                        </div> <!-- footer link -->
                        <div class=""footer-link mt-50""");
            WriteLiteral(@">
                            <h4 class=""footer-title"">Customer</h4>

                            <ul class=""link"">
                                <li><a href=""shop-4-column.html"">Prices drop</a></li>
                                <li><a href=""shop-4-column.html"">New Product</a></li>
                                <li><a href=""shop-3-column.html"">Best Sales</a></li>
                                <li><a href=""#"">Sitemap</a></li>
                                <li><a href=""login.html"">Login</a></li>
                            </ul>
                        </div> <!-- footer link -->
                        <div class=""footer-link mt-50"">
                            <h4 class=""footer-title"">About Us</h4>

                            <ul class=""link"">
                                <li><a href=""#"">About Our Shop</a></li>
                                <li><a href=""#"">Secure Shopping </a></li>
                                <li><a href=""#"">Delivery infomation </a></li>
                                <l");
            WriteLiteral(@"i><a href=""#"">Store Locations </a></li>
                                <li><a href=""#"">Affiliates </a></li>
                            </ul>
                        </div> <!-- footer link -->
                        <div class=""footer-link mt-50"">
                            <h4 class=""footer-title"">My account</h4>

                            <ul class=""link"">
                                <li><a href=""#"">Personal info</a></li>
                                <li><a href=""#"">Order</a></li>
                                <li><a href=""#"">Credit Slips</a></li>
                                <li><a href=""#"">Address</a></li>
                            </ul>
                        </div> <!-- footer link -->
                    </div> <!-- footer link wrapper -->
                </div>
            </div> <!-- row -->
        </div> <!-- container -->
    </div> <!-- footer widget -->

    <div class=""footer-copyright"">
        <div class=""container"">
            <div class=""footer-copyright-payment text-c");
            WriteLiteral("enter d-lg-flex justify-content-between align-items-center\">\n                <div class=\"copyright-text\">\n                    <p>Copyright 2020 &copy; <a href=\"https://hasthemes.com/\">");
#nullable restore
#line 74 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Views/Shared/Components/Footer/Default.cshtml"
                                                                         Write(Model.PoweredName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a> . All Rights Reserved</p>\n                </div>\n                <div class=\"payment\">\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "589765dd0284fb5fe69a18adf4c8dfc6c87f14fb9220", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                </div>\n            </div> <!-- footer copyright payment -->\n        </div> <!-- container -->\n    </div> <!-- footer copyright -->\n</section>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Footer> Html { get; private set; }
    }
}
#pragma warning restore 1591