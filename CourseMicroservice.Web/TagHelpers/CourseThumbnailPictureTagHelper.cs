using CourseMicroservice.Web.Options;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CourseMicroservice.Web.TagHelpers
{
    public class CourseThumbnailPictureTagHelper(MicroserviceOption microserviceOption) : TagHelper
    {
        public string? Src { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";

            var blankCourseThumbnailImagePath = "/images/default-course-thumbnail.jpg";

            if (string.IsNullOrEmpty(Src))
            {
                output.Attributes.SetAttribute("src", blankCourseThumbnailImagePath);
            }
            else
            {
                var courseThumbnailImagePath = $"{microserviceOption.File.BaseAddress}/{Src}";


                output.Attributes.SetAttribute("src", courseThumbnailImagePath);
            }


            return base.ProcessAsync(context, output);
        }
    }
}
