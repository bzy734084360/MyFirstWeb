using BlogModel;
using BlogRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogBusinessLogic
{
    public class BlogPostManager
    {
        private BlogPostRepository repository = new BlogPostRepository();
        public List<BlogPost> GetAllPosts()
        {
            return repository.GetAll();
        }
        public List<BlogPost> GetTop5()
        {
            return repository.GetTop5();
        }
        public BlogPost GetPostById(int id)
        {
            return repository.GetById(id);
        }
        public void UpdatePost(BlogPost post)
        {
            repository.Update(post);
        }
        public void Insert(BlogPost post)
        {
            repository.Insert(post);
        }
        public void Delete(BlogPost post)
        {
            repository.Delete(post);
        }
    }
}
