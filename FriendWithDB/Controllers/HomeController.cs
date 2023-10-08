using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FriendWithDB.Models;
using FriendsWithDatabase.Services;
using FriendWithDB.Services;
using Hangfire;

namespace FriendsWithDatabase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFriendsService _service;
        public HomeController(ILogger<HomeController> logger, IFriendsService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Friend> friends = await _service.GetAll();
            
            RecurringJob.AddOrUpdate("recurringjob", () =>
                        UpdateEveryMinuteHangFire()
                , Cron.Minutely());   
            return View(friends);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Friend friend = await _service.Get(id);
            if (friend != null)
            {
                return View(friend);
            }
            else
            {
                return View("NotFound");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Friend friend)
        {
            if (!ModelState.IsValid)
            {
                return View(friend);
            }
            else
            {
                await _service.Add(friend);
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            Friend friend = await _service.Get(id);
            if (friend == null)
            {
                return View("NotFound");
            }
            else
            {
                var res = new Friend()
                {
                    Id = id,
                    Name = friend.Name,
                    ImageUrl = friend.ImageUrl,
                };
                return View(res);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Friend friend)
        {
            if (!ModelState.IsValid)
            {
                return View(friend);
            }
            else
            {
                await _service.Edit(friend);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            Friend friend = await _service.Get(id);
            if (friend == null)
                return View("NotFound");
            else
                return View(friend);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            if (!ModelState.IsValid)
            {
                return View("NotFound");
            }
            else
            {
                await _service.Delete(id);
                return RedirectToAction("Index");
            }

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task UpdateEveryMinuteHangFire()
        {
            List<Friend> friends = await _service.GetAll();
            foreach (var item in friends)
            {
                _logger.LogInformation(item.Name);
                item.Name = ChangeToUpper.ChangeNameToUpper(item.Name);
                await _service.Edit(item);
            }
        }

    }
}