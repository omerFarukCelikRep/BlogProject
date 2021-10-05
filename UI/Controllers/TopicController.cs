using Business.Abstract;
using Entity.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public async Task<IActionResult> Index()
        {
            var topicList = await _topicService.GetAll(a => a.Status != Status.Deleted);
            return View(topicList);
        }

        public async Task<IActionResult> GetAll()
        {
            var topicList = await _topicService.GetAll(a => a.Status != Status.Deleted);

            return Json(topicList);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
