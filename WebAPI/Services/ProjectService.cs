using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebAPI.Data.ViewModels.Project;
using WebAPI.DatabaseContext;
using WebAPI.Entities;
using WebAPI.Helper;
using System.Collections.Generic;


namespace WebAPI.Services
{
    public class ProjectService : BaseService
    {
        public ProjectService(WebApiDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
        #region GetList()
        public List<Project> GetList(FilterModel filter, Pager pager, long userId)
        {

            var predicate = PredicateBuilder.New<Project>(true);

            if (!String.IsNullOrEmpty(filter.Name))
            {
                predicate.And(p => p.Name.Contains(filter.Name));
            }

            return Context.Projects
                .Include(p => p.ResponsiblePerson)
                .AsNoTracking()
                .AsExpandable()
                .Where(predicate)
                .ToList();
        }
        #endregion

        #region GetItem()
        public Project GetItem(Guid id)
        {
            return Context.Projects
                .SingleOrDefault(n => n.PublicId == id.ToString());
        }
        #endregion
    }
}
