using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.Dtos;
using Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ITopicRepository _topicRepository;

        public ArticleManager(IArticleRepository articleRepository, ITopicRepository topicRepository)
        {
            _articleRepository = articleRepository;
            _topicRepository = topicRepository;
        }

        public async Task<bool> Add(CreateArticleDto createArticleDto)
        {
            Article article = new Article
            {
                Content = createArticleDto.Content,
                Image = createArticleDto.Image,
                IsPublished = false,
                ReadingCount = 0,
                LikeCount = 0,
                ReadTime = CalculateReadTime(createArticleDto.Content),
                Title = createArticleDto.Title,
                User = createArticleDto.User
            };

            await _articleRepository.Add(article);

            foreach (var topicId in createArticleDto.TopicIds)
            {
                var topic = await _topicRepository.GetByID(topicId);

                article.ArticleTopics.Add(new ArticleTopic
                {
                    Article = article,
                    Topic = topic
                });
            }

            return await _articleRepository.Update(article);

        }

        public async Task<bool> Any(Expression<Func<Article, bool>> expression)
        {
            return await _articleRepository.Any(expression);
        }

        public async Task<bool> Delete(Article entity)
        {
            return await _articleRepository.Delete(entity);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _articleRepository.Delete(id);
        }

        public async Task<Article> Get(Expression<Func<Article, bool>> expression)
        {
            return await _articleRepository.Get(expression);
        }

        public async Task<List<Article>> GetAll()
        {
            return await _articleRepository.GetAll();
        }

        public async Task<List<Article>> GetAll(Expression<Func<Article, bool>> expression)
        {
            return await _articleRepository.GetAll(expression);
        }

        public async Task<Article> GetByID(Guid id)
        {
            return await _articleRepository.GetByID(id);
        }

        public async Task<bool> Update(Article entity)
        {
            entity.ReadTime = CalculateReadTime(entity.Content);
            return await _articleRepository.Update(entity);
        }

        public async Task<bool> Publish(Guid id)
        {
            var article = await _articleRepository.GetByID(id);

            article.IsPublished = true;

            return await _articleRepository.Update(article);
        }

        public async Task<List<ArticleDetailsDto>> GetMostReadFiveArticles()
        {
            var articleList = await _articleRepository.GetAll(a => a.IsPublished);
            articleList = articleList.OrderByDescending(a => a.ReadingCount)
                                     .Take(5)
                                     .ToList();

            //TODO: Yayınlama Tarihi eklenecek
            List<ArticleDetailsDto> trends = articleList.Select(a => new ArticleDetailsDto
            {
                Id = a.Id,
                Content = a.Content.Length > 200 ? a.Content.Substring(0, 200) : a.Content,
                CreatedDate = a.CreatedDate,
                Image = a.Image,
                LikeCount = a.LikeCount,
                ReadTime = a.ReadTime,
                Title = a.Title,
                WriterName = a.User.Name
            }).ToList();

            return trends;
        }

        public async Task<ArticleWithUserDetailDto> GetArticleWithUserDetails(Guid id)
        {
            var article = await _articleRepository.GetByID(id);

            ArticleWithUserDetailDto articleWithUserDetails = new ArticleWithUserDetailDto
            {
                Content = article.Content,
                CreatedDate = article.CreatedDate,
                Image = article.Image,
                LikeCount = article.LikeCount,
                ReadTime = article.ReadTime,
                ReadingCount = article.ReadingCount,
                Title = article.Title,
                Topics = article.ArticleTopics.Select(a => a.Topic).Select(a => a.Name).ToList(),
                WriterName = article.User.Name
            };

            return articleWithUserDetails;
        }

        public async Task<List<ArticleDetailsDto>> GetLastTenArticles()
        {
            var articleList = await _articleRepository.GetAll(a => a.Status != Status.Deleted);

            List<ArticleDetailsDto> articles = articleList.OrderByDescending(a => a.CreatedDate).Take(10).Select(a => new ArticleDetailsDto
            {
                Id = a.Id,
                Content = a.Content.Length > 350 ? a.Content.Substring(0,350) : a.Content,
                CreatedDate = a.CreatedDate,
                Image = a.Image,
                LikeCount = a.LikeCount,
                ReadTime = a.ReadTime,
                Title = a.Title,
                WriterName = a.User.Name
            }).ToList();

            return articles;
        }

        public async Task IncreaseReadCount(Guid id)
        {
            var article = await _articleRepository.GetByID(id);

            article.ReadingCount++;

            await _articleRepository.Update(article);
        }

        private int CalculateReadTime(string text)
        {
            List<char> specialCharacters = new List<char>() { ' ', ',', '.', '@', '(', ')', '"' };

            List<char> letters = new List<char>();

            foreach (char item in text)
            {
                if (specialCharacters.Contains(item))
                {
                    continue;
                }
                letters.Add(item);
            }

            int readTime = letters.Count / 150;

            return readTime;
        }
    }
}
