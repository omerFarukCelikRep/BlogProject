using Entity.Concrete;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITopicService : IService<Topic>
    {
        Task<bool> Add(Topic entity);
        Task<bool> Update(Topic entity);
    }
}
