using BlogModel;
using BlogRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogBusinessLogic
{
    /// <summary>
    /// 博客-授权用户业务类
    /// </summary>
    public class BlogClientManager : IManagerBase<BlogClient>
    {
        private BlogClientRepository repository = new BlogClientRepository();

        public int AddEntity(BlogClient entity)
        {
            return repository.AddEntity(entity);
        }

        public int UpdateEntity(BlogClient entity)
        {
            return repository.AddEntity(entity);
        }

        public int DeleteEntity(string id)
        {
            return repository.DeleteEntity(id);
        }

        public BlogClient QueryEntity(string id)
        {
            return repository.QueryEntity(id);
        }

        public List<BlogClient> QueryList()
        {
            return repository.QueryList();
        }
    }
}
