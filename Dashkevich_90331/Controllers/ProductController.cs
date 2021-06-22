using Dashkevich_90331.Extensions;
using Dashkevich_90331.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLabsV06.DAL.Entities;
using Dashkevich_90331.Extensions;
using WebLabsV06.DAL.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Dashkevich_90331.Controllers
{
    public class ProductController : Controller
    {
        //public List<Feed> _feeds;
        //List<FeedGroup> _feedGroups;
        ApplicationDbContext _context;
        int _pageSize;
        private ILogger _logger;

        public ProductController(ApplicationDbContext context, ILogger<ProductController> logger)
        {
            _pageSize = 3;
            _context = context;
            _logger = logger;
        }

        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]
        public IActionResult Index(int? group, int pageNo = 1)
        {
            var groupMame = group.HasValue ? _context.FeedGroups.Find(group.Value)?.GroupName : "all groups";

            _logger.LogInformation($"info: group={group}, page={pageNo}");
            
            var feedsFiltered = _context.Feeds.Where(d => !group.HasValue || d.FeedGroupId == group.Value);
            // Поместить список групп во ViewData
            ViewData["Groups"] = _context.FeedGroups;
            // Получить id текущей группы и поместить в ViewData
            ViewData["CurrentGroup"] = group ?? 0;

            var model = ListViewModel<Feed>.GetModel(feedsFiltered, pageNo, _pageSize);
            if (Request.IsAjaxRequest())
                return PartialView("_listpartial", model);
            else
                return View(model);
        }

        /// <summary>
        /// Инициализация списков
        /// </summary>
        //private void SetupData()
        //{
        //    _feedGroups = new List<FeedGroup>
        //    {
        //        new FeedGroup {FeedGroupId=1, GroupName="Сухой корм для кошек"},
        //        new FeedGroup {FeedGroupId=2, GroupName="Сухой корм для собак"},
        //        new FeedGroup {FeedGroupId=3, GroupName="Лакомства"},
        //        new FeedGroup {FeedGroupId=4, GroupName="Мокрый корм для кошек"},
        //        new FeedGroup {FeedGroupId=5, GroupName="Консервы для кошек"},
        //        new FeedGroup {FeedGroupId=6, GroupName="Консервы для собак"}
        //    };
        //    _feeds = new List<Feed>
        //    {
        //        new Feed {FeedId = 1, FeedName="Sanabelle", Description="Для кожи и шерсти", Weight =2.2, FeedGroupId=1, Image="Cat_001.png" },
        //        new Feed {FeedId = 2, FeedName="Royal Canin", Description="Контроль веса", Weight =1.4, FeedGroupId=1, Image="Cat_002.png" },
        //        new Feed {FeedId = 3, FeedName="Dreamies", Description="Подушечки с говядиной", Weight =0.03, FeedGroupId=3, Image="Goody_001.jpg" },
        //        new Feed {FeedId = 4, FeedName="Integra", Description="Консерва для собак", Weight =0.15, FeedGroupId=6, Image="CannedFood_001.jpg" },
        //        new Feed {FeedId = 5, FeedName="Happy Dog", Description="Для молодых собак", Weight =14.0, FeedGroupId=2, Image="Dog_001.jpg" }
        //    };
        //}
    }
}
