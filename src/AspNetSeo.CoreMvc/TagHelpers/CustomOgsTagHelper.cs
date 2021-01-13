using System;
using System.Linq;
using AspNetSeo.CoreMvc.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetSeo.CoreMvc.TagHelpers
{
    [HtmlTargetElement("custom-ogs", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class CustomOgsTagHelper : SeoTagHelperBase
    {
        public CustomOgsTagHelper(ISeoHelper seoHelper) 
            : base(seoHelper)
        {
        }

        public override void Process(
            TagHelperContext context, 
            TagHelperOutput output)
        {
            if(!SeoHelper.CustomOgs.Any())
            {
                output.SuppressOutput();
                return;
            }

            output.TagName = null;
            output.Attributes.Clear();
            
            foreach (var custom in SeoHelper.CustomOgs)
            {
                if (custom.Value != null)
                {
                    foreach (var value in custom.Value)
                    {
                        AddCustomOg(output, custom.Key, value);
                        output.Content.AppendHtml(Environment.NewLine);
                    }
                }
            }
        }

        private void AddCustomOg(TagHelperOutput output, string name, string content)
        {
            if (name == null || content == null)
            {
                return;
            }

            var tag = new TagBuilder("meta")
            {
                TagRenderMode = TagRenderMode.SelfClosing
            };

            tag.Attributes["property"] = name;
            tag.Attributes["content"] = content;

            output.Content.AppendHtml(tag);
        }
    }
}
