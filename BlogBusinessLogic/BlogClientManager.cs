using BlogModel;
using BlogRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogBusinessLogic
{
    public class BlogClientManager
    {
        private BlogClientRepository repository = new BlogClientRepository();

        public BlogClient GetPostById(int id)
        {
            return repository.GetById(id);
        }
        public void UpdatePost(BlogClient entity)
        {
            repository.Update(entity);
        }
        public void Insert(BlogClient entity)
        {
            repository.Insert(entity);
        }
        public void Delete(string id)
        {
            repository.Delete(id);
        }
    }
}
