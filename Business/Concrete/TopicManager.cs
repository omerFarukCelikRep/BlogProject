using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TopicManager : ITopicService
    {
        private readonly ITopicRepository _topicRepository;

        public TopicManager(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<bool> Add(Topic entity)
        {
            return await _topicRepository.Add(entity);
        }

        public async Task<bool> Any(Expression<Func<Topic, bool>> expression)
        {
            return await _topicRepository.Any(expression);
        }

        public async Task<bool> Delete(Topic entity)
        {
            return await _topicRepository.Delete(entity);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _topicRepository.Delete(id);
        }

        public async Task<Topic> Get(Expression<Func<Topic, bool>> expression)
        {
            return await _topicRepository.Get(expression);
        }

        public async Task<List<Topic>> GetAll()
        {
            return await _topicRepository.GetAll();
        }

        public async Task<List<Topic>> GetAll(Expression<Func<Topic, bool>> expression)
        {
            return await _topicRepository.GetAll(expression);
        }

        public async Task<Topic> GetByID(Guid id)
        {
            return await _topicRepository.GetByID(id);
        }

        public async Task<bool> Update(Topic entity)
        {
            return await _topicRepository.Update(entity);
        }
    }
}
