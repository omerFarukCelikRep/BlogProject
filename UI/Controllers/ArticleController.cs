using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Abstract;
using Entity.Concrete;
using Entity.Dtos;
using Entity.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ITopicService _topicService;
        private readonly UserManager<AppUser> _userManager;
        private readonly INotyfService _notyfService;

        public ArticleController(IArticleService articleService, UserManager<AppUser> userManager, INotyfService notyfService, ITopicService topicService)
        {
            _articleService = articleService;
            _userManager = userManager;
            _notyfService = notyfService;
            _topicService = topicService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var articleList = await _articleService.GetAll(a => a.Status != Status.Deleted && a.IsPublished);

            var articles = articleList.AsQueryable().Select(a => new ArticleDetailsDto
            {
                Id = a.Id,
                Content = a.Content,
                CreatedDate = a.CreatedDate,
                Image = a.Image,
                LikeCount = a.LikeCount,
                ReadTime = a.ReadTime,
                Title = a.Title,
                WriterName = a.User.Name
            });

            IPagedList<ArticleDetailsDto> pagedList = articles.ToPagedList(page, 10);
            return View("Index",pagedList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            await _articleService.IncreaseReadCount(id);

            var article = await _articleService.GetArticleWithUserDetails(id);
            return View(article);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleDto createArticleDto)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();

                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        createArticleDto.Image = dataStream.ToArray();
                    }

                }
                createArticleDto.User = await _userManager.GetUserAsync(User);

                await _articleService.Add(createArticleDto);

                return RedirectToAction(nameof(UserArticles));
            }

            return View(createArticleDto);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }


            var article = await _articleService.GetByID(id);

            CreateArticleDto articleDto = new CreateArticleDto
            {
                Content = article.Content,
                Image = article.Image,
                Title = article.Title,
                TopicIds = article.ArticleTopics.Select(a => a.TopicId).ToList()
            };

            return View(articleDto);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, EditArticleDto articleDto)
        {
            if (id == Guid.Empty)
            {
                _notyfService.Error("İşlem Gerçekleştirilemedi");
                return RedirectToAction(nameof(UserArticles));
            }

            articleDto.Id = id;

            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();

                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        articleDto.Image = dataStream.ToArray();
                    }
                }

                var article = await _articleService.GetByID(id);

                article.Title = articleDto.Title;
                article.Content = articleDto.Content;

                if (articleDto.Image != null)
                {
                    article.Image = articleDto.Image;
                }

                foreach (var topicId in articleDto.TopicIds)
                {
                    if (article.ArticleTopics.Any(a => a.TopicId == topicId))
                    {
                        continue;
                    }
                    else
                    {
                        article.ArticleTopics.Add(new ArticleTopic
                        {
                            ArticleId = article.Id,
                            TopicId = topicId
                        });
                    }
                }

                await _articleService.Update(article);

                return RedirectToAction(nameof(UserArticles));
            }

            return View(articleDto);
        }

        public async Task<IActionResult> UserArticles()
        {
            var user = await _userManager.GetUserAsync(User);

            var articles = await _articleService.GetAll(a => a.UserId == user.Id);

            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> Publish(Guid id)
        {
            if (id == Guid.Empty)
            {
                _notyfService.Error("İşlem Gerçekleştirilemedi");
                return RedirectToAction(nameof(UserArticles));
            }

            await _articleService.Publish(id);

            return RedirectToAction(nameof(UserArticles));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                _notyfService.Error("İşlem Gerçekleştirilemedi");
                return RedirectToAction(nameof(UserArticles));
            }

            await _articleService.Delete(id);

            return RedirectToAction(nameof(UserArticles));
        }
    }
}
