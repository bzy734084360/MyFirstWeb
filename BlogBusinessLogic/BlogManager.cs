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
        private MyBlogRepository repository = new MyBlogRepository();
        public List<Post> GetAllPosts()
        {
            return repository.GetAll();
        }
        public Post GetPostById(int id)
        {
            return repository.GetById(id);
        }
    }
}
