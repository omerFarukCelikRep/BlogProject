using Entity.Concrete;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IArticleService : IService<Article>
    {
        Task<bool> Add(CreateArticleDto createArticleDto);
        Task<bool> Update(Article entity);
        Task<List<ArticleDetailsDto>> GetMostReadFiveArticles();
        Task<bool> Publish(Guid id);
        Task<ArticleWithUserDetailDto> GetArticleWithUserDetails(Guid id);
        Task<List<ArticleDetailsDto>> GetLastTenArticles();
        Task IncreaseReadCount(Guid id);
    }
}
