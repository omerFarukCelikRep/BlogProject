using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UI.Views.Shared.Components.LastArticles
{
    public class LastArticlesViewComponent : ViewComponent
    {
        private readonly IArticleService _articleService;

        public LastArticlesViewComponent(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = await _articleService.GetLastTenArticles();

            return View(articles);
        }
    }
}
