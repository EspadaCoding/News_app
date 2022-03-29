using Picabu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picabu.Services
{
    public class LikeService : ILikeService
    {
        private readonly NewsDbContext context;

        public LikeService(NewsDbContext context)
        {
            this.context = context;
        }
        public async Task AddLikeAsync(Like like)
        {
            context.Like.Add(like);
            await context.SaveChangesAsync();
        }

        public async Task DeleteLikeAsync(News news)
        {
            var like = context.Like.Where(x => x.NewsId == news.Id).FirstOrDefault();  
            if (like != null)
            {
                context.Like.Remove(like);
            }
            await context.SaveChangesAsync();
        }

        public async Task EditLikeAsync(Like like)
        {
            context.Like.Update(like);
            await context.SaveChangesAsync();
        }

        public bool PutLikeOnPost(int newsId, string userId)
        {

           Like l = context.Like.FirstOrDefault(x => x.NewsId == newsId && x.UserID == userId);
            if(l == null)
            {
               return   true; 
            }
            else
            {
                return   false;
            }

           
        }
    }
}
