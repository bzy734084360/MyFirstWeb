using BlogModel;
using BlogRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogBusinessLogic
{
    public class BlogManager
    {
        private BlogPostRepository repository = new BlogPostRepository();
        public List<BlogPost> GetAllPosts()
        {
            return repository.GetAll();
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
