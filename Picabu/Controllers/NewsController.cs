using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Picabu.Helpers;
using Picabu.Models;
using Picabu.Services;
using Picabu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Picabu.Models.Identity;

namespace Picabu.Controllers
{
    public class NewsController : Controller
    {
        private INewsService newsService;
        private ILikeService likeService;
        private readonly UserManager<AppUser> _userManager;
        public NewsController(INewsService newsService, UserManager<AppUser> userManager, ILikeService likeService)
        {
            this.newsService = newsService;
            this._userManager = userManager;
            this.likeService = likeService;
        }

        public async Task<IActionResult> Index(int id = 0, int page = 1)
        {
            NewsResult newsResult = null;

            if (id == 0)
            {
                newsResult = await newsService.GetNewsAsync(page);
            }
            else
            {
                newsResult = await newsService.GetNewsAsync(id, page);
            }
            var resultCategories = await newsService.GetCategoriesAsync();
            var resultComments = await newsService.GetComentariesAsync();
            var viewModel = new NewsIndexViewModel
            {
                News = newsResult.News,
                Categories = resultCategories,
                CurrentPage = page,
                TotalPages = (int)newsResult.TotalPages,
                CategoryId = id,
                Comment = resultComments
            };
            return View(viewModel);
        } 

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var list = await newsService.GetCategoriesAsync();
            var list2 = await newsService.GetTagsAsync();
            ViewBag.Categories = new SelectList(list, "Id", "Name");
            ViewBag.Tags = new MultiSelectList(list2, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(News news, IFormFile ImageFile, int[] Tags)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    var filepath = await FileUploadHelper.UploadAsync(ImageFile);
                    news.PosterUrl =filepath;
                }
                news.Date = DateTime.Now;
                news.Nickname = User.Identity.Name;
                await newsService.AddNewsAsync(news);
                await newsService.UpdateTagsInNews(news.Id, Tags);
                return RedirectToAction("Index");
            }
            else
            {
                var list = await newsService.GetCategoriesAsync();
                var list2 = await newsService.GetTagsAsync();
                ViewBag.Categories = new SelectList(list, "Id", "Name");
                ViewBag.Tags = new MultiSelectList(list2, "Id", "Name");
                return View(news);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            await newsService.DeleteNewsAsync(Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var news = await newsService.FindNewsByIdAsync(Id);
            var list = await newsService.GetCategoriesAsync();
            var list2 = await newsService.GetTagsAsync();
            ViewBag.Categories = new SelectList(list, "Id", "Name", news.CategoryId);
            ViewBag.Tags = new MultiSelectList(list2, "Id", "Name", news.NewsTags.Select(x => x.TagId));
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Edit(News news, IFormFile ImageFile, int[] Tags)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    var filepath = await FileUploadHelper.UploadAsync(ImageFile);
                    news.PosterUrl = filepath;
                }
                news.Date = DateTime.Now;
                await newsService.EditNewsAsync(news);
                await newsService.UpdateTagsInNews(news.Id, Tags);
                return RedirectToAction("Index");
            }
            else
            {
                var list = await newsService.GetCategoriesAsync();
                var list2 = await newsService.GetTagsAsync();
                ViewBag.Categories = new SelectList(list, "Id", "Name");
                ViewBag.Tags = new MultiSelectList(list2, "Id", "Name");
                return View(news);
            }
        }

        [HttpGet]
        public async Task<string> LikeThis(int id)
        {
            Like like = new Like();
            News art = await newsService.FindNewsByIdAsync(id); 
            AppUser m = await _userManager.GetUserAsync(User); 
            string userId = await _userManager.GetUserIdAsync(m);
            if (User.Identity.IsAuthenticated)
            {
                if (likeService.PutLikeOnPost(art.Id, userId))
                {
                    art.Likes++; 
                    like.NewsId = art.Id;
                    like.UserID = userId;
                    like.LikedDate = DateTime.Now;
                    like.Liked = true; 
                    await newsService.EditNewsAsync(art);
                    await likeService.AddLikeAsync(like);
                }
                else
                { 

                        art.Likes--;
                        await newsService.EditNewsAsync(art);
                        await likeService.DeleteLikeAsync(art); 
                }
            }

            return art.Likes.ToString();
        }

        [HttpPost]
        public async Task<IActionResult>  CommentThis(int id,string content)
        {
            Comment comment = new Comment();
            News art = await newsService.FindNewsByIdAsync(id); 
            if (User.Identity.IsAuthenticated)
            {
                art.CountofComments++;
                comment.Content = content;
                comment.Date = DateTime.Now;
                comment.Name = User.Identity.Name;
                comment.NewsID = art.Id;
                await newsService.AddCommentAsync(comment);
                await newsService.EditNewsAsync(art);
            }
            return RedirectToAction("Index");
        } 
    }
}

