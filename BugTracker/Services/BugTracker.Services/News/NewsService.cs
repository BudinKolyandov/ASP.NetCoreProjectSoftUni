namespace BugTracker.Services.News
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTracker.Data;
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;
    using BugTracker.Web.ViewModels.News;
    using Microsoft.EntityFrameworkCore;

    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext context;

        public NewsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public object CompanyCheck(string id)
        {
            var companyUser = this.context.CompaniesUsers.Where(x => x.UserId == id).FirstOrDefault();
            if (companyUser == null)
            {
                return null;
            }

            return companyUser.UserId;
        }

        public async Task<int> CreateNews(string userId, CreateNewsInputModel model)
        {
            var news = new News
            {
                Headline = model.Headline,
                Body = model.Body,
                DatePosted = DateTime.UtcNow,
                ProjectId = model.ProjectId,
                PosterId = userId,
            };

            this.context.News.Add(news);
            await this.context.SaveChangesAsync();
            var project = this.context.Projects.Where(x => x.Id == model.ProjectId).First();
            var companies = this.context.CompaniesUsers.Where(x => x.CompanyId == project.CompanyId);
            List<string> ids = new List<string>();
            foreach (var company in companies)
            {
                if (ids.Contains(company.UserId))
                {
                    continue;
                }

                ids.Add(company.UserId);
            }

            foreach (var id in ids)
            {
                var newsUser = new UserNews
                {
                    NewsId = news.Id,
                    UserId = id,
                    Seen = false,
                };
                await this.context.UsersNews.AddAsync(newsUser);
            }

            await this.context.SaveChangesAsync();
            return news.Id;
        }

        public IEnumerable<T> GetAllForUser<T>(string userId, int? count = null)
        {
            var user = this.context.Users.Where(x => x.Id == userId).First();
            if (user == null)
            {
                return null;
            }

            var newsUsers = this.context.UsersNews.Where(x => x.UserId == userId);
            List<int> newsIds = new List<int>();
            List<string> userIds = new List<string>();
            foreach (var news in newsUsers)
            {
                if (!news.Seen)
                {
                    newsIds.Add(news.NewsId);
                }
            }

            IQueryable<News> query = this.context.News
                .Where(x => newsIds.Contains(x.Id));
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task<int> SeenChange(int newsId, string id)
        {
            var userNews = this.context.UsersNews.Where(x => x.UserId == id && x.NewsId == newsId).FirstOrDefault();
            userNews.Seen = true;
            this.context.UsersNews.Update(userNews);
            await this.context.SaveChangesAsync();
            return userNews.NewsId;
        }
    }
}
