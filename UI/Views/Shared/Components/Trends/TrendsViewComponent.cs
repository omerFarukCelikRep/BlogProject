using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UI.Views.Shared.Components.Trends
{
    [ViewComponent(Name ="Trends")]
    public class TrendsViewComponent : ViewComponent
    {
        private readonly IArticleService _articleService;

        public TrendsViewComponent(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var trendArticleList = await _articleService.GetMostReadFiveArticles();

            return View(trendArticleList);
        }
    }
}
