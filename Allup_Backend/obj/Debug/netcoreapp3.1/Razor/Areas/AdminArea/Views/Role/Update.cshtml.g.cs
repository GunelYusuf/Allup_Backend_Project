#pragma checksum "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Areas/AdminArea/Views/Role/Update.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3f589fdfbab9fa9767a5c5c3c3e94c41eec85fbc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Allup_Backend.Areas.AdminArea.Views.Role.Areas_AdminArea_Views_Role_Update), @"mvc.1.0.view", @"/Areas/AdminArea/Views/Role/Update.cshtml")]
namespace Allup_Backend.Areas.AdminArea.Views.Role
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
#line 2 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Areas/AdminArea/Views/_ViewImports.cshtml"
using Allup_Backend.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Areas/AdminArea/Views/Role/Update.cshtml"
using Allup_Backend.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3f589fdfbab9fa9767a5c5c3c3e94c41eec85fbc", @"/Areas/AdminArea/Views/Role/Update.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0af7e474ce19ce969e48313aba6c3f6d08aca23f", @"/Areas/AdminArea/Views/_ViewImports.cshtml")]
    public class Areas_AdminArea_Views_Role_Update : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UpdateRoleVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n\n<div class=\"container\">\n    <div class=\"row\">\n        <div class=\"col-md-6 grid-margin stretch-card\">\n            <div class=\"card\">\n                <div class=\"card-body\">\n                    <h4 class=\"card-title\">");
#nullable restore
#line 10 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Areas/AdminArea/Views/Role/Update.cshtml"
                                      Write(Model.User.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\n                    <p class=\"card-description\"> Create Role</p>\n\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3f589fdfbab9fa9767a5c5c3c3e94c41eec85fbc4273", async() => {
                WriteLiteral("\n");
#nullable restore
#line 14 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Areas/AdminArea/Views/Role/Update.cshtml"
                         foreach (var role in Model.Roles)
                        {

#line default
#line hidden
#nullable disable
                WriteLiteral("                            <input ");
#nullable restore
#line 16 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Areas/AdminArea/Views/Role/Update.cshtml"
                               Write(Model.UserRoles.Contains(role.Name) ? "checked =\"checked\"" : "");

#line default
#line hidden
#nullable disable
                WriteLiteral(" type=\"checkbox\" name=\"roles\" value=\"");
#nullable restore
#line 16 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Areas/AdminArea/Views/Role/Update.cshtml"
                                                                                                                                       Write(role.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral("\" /> ");
#nullable restore
#line 16 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Areas/AdminArea/Views/Role/Update.cshtml"
                                                                                                                                                      Write(role.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <br />\n");
#nullable restore
#line 17 "/Users/gunelyusubova/Projects/Allup_Backend/Allup_Backend/Areas/AdminArea/Views/Role/Update.cshtml"
                        }

#line default
#line hidden
#nullable disable
                WriteLiteral("                        <button type=\"submit\">Change</button>\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                </div>\n            </div>\n        </div>\n    </div>\n</div>\n\n\n\n\n\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UpdateRoleVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
