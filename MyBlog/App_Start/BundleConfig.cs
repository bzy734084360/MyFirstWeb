using System.Web;
using System.Web.Optimization;

namespace MyBlog
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryvalex").Include(
                        "~/Scripts/blog.validate.extension.js"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //clean_blog
            //CSS
            //bundles.Add(new StyleBundle("~/bundles/cleanblog/bootstrapcss").Include(
            //    "~/Content/clean_blog/dist/css/styles.css"
            //    ));
            //bundles.Add(new StyleBundle("~/bundles/cleanblog/fonts").Include(
            //    "~/Content/clean_blog/dist/css/styles.css"
            //    ));
            var cleanblogcss = new StyleBundle("~/bundles/cleanblog/css").Include(
                "~/Content/clean_blog/dist/css/styles.css", new SampleTransform()
                );
            cleanblogcss.Transforms.Add(new SampleBundleTransform());
            bundles.Add(cleanblogcss);
            //JS   ~/Content/clean_blog/dist/js/*.js 通配字符*
            bundles.Add(new ScriptBundle("~/bundles/cleanblog/bootstrapjs").Include(
                "~/Content/clean_blog/dist/js/bootstrap.bundle.min.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/cleanblog/js").Include(
                "~/Content/clean_blog/dist/js/scripts.js"
                ));

            //使用CDN
            bundles.UseCdn = true;
            var jqueryCDNPath = "https://cdn.bootcss.com/jquery/3.4.1/jquery.min.js";
            var jqueryBundle = new ScriptBundle("~/bundles/cleanblog/jquery", jqueryCDNPath).Include(
                "~/Content/clean_blog/vendor/jquery/jquery.js");
            jqueryBundle.CdnFallbackExpression = "window.jQuery";//设置后 CDN不存在文件则取本地文件
            bundles.Add(jqueryBundle);
            //设置后 自动进行文件大小优化
            BundleTable.EnableOptimizations = true;
        }

        /*
         * 自定义资源转变  在CSS文件中追加样式
         */
        public class SampleTransform : IItemTransform
        {
            public string Process(string includedVirtualPath, string input)
            {
                input += " .test {color:red};";
                return input;
            }
        }
        /*
         * 转换Bundle相应文件的方法
         */
        public class SampleBundleTransform : IBundleTransform
        {
            public void Process(BundleContext context, BundleResponse response)
            {
                response.Content += "/*hello grumpyfish*/";
            }
        }
    }
}
