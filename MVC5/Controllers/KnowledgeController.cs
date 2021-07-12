using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Controllers
{
    public class KnowledgeController : Controller
    {
        private List<int> _list;
        /// <summary>
        /// yield 关键字学习
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            _list = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                _list.Add(i);
            }
            //申明属性，过滤器(使用yield)
            StringBuilder yieldValue = new StringBuilder();
            foreach (var item in GetAllEvenNumberYield())
            {
                yieldValue.Append("|" + item);
            }
            StringBuilder listValue = new StringBuilder();
            foreach (var item in GetAllEvenNumber())
            {
                listValue.Append("|" + item);
            }
            ViewBag.yieldValue = yieldValue.ToString();
            ViewBag.listValue = listValue.ToString();
            return View();
        }

        private IEnumerable<int> GetAllEvenNumber()
        {
            List<int> result = new List<int>();
            foreach (var item in _list)
            {
                if (item % 2 == 0)
                {
                    result.Add(item);
                }
            }
            return result;
        }
        private IEnumerable<int> GetAllEvenNumberYield()
        {
            foreach (var item in _list)
            {
                if (item == 17)
                {
                    yield break;
                }
                if (item % 2 == 0)
                {
                    yield return item;
                }
            }
            yield break;
        }
    }
}