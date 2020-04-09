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

    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext context;

        public NewsService(ApplicationDbContext context)
        {
            this.context = context;
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
            await this.context.News.AddAsync(news);
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

            var companies = this.context.CompaniesUsers.Where(x => x.UserId == user.Id);
            List<string> ids = new List<string>();
            foreach (var company in companies)
            {
                ids.Add(company.CompanyId);
            }

            var projects = this.context.Projects.Where(x => ids.Contains(x.CompanyId));

            foreach (var project in projects)
            {
                ids.Add(project.Id);
            }

            IQueryable<News> query = this.context.News
                .Where(x => ids.Contains(x.ProjectId));
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
