using MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            var ablums = GetAlbums();
            return View(ablums);
        }
        [Authorize]
        public ActionResult Buy(int id)
        {
            var album = GetAlbums().Single(a => a.AlbumId == id);
            return View(album);
        }

        private static List<Album> GetAlbums()
        {
            var abums = new List<Album>
            {
                new Album{ AlbumId=1,Title="第一个物品",Price=8.99M},
                new Album{ AlbumId=2,Title="第二个物品",Price=9.99M},
                new Album{ AlbumId=3,Title="第三个物品",Price=10.99M},
                new Album{ AlbumId=4,Title="第四个物品",Price=11.99M},
            };
            return abums;
        }
    }
}