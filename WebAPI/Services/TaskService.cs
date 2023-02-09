using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.ViewModels.Task;
using WebAPI.DatabaseContext;
using WebAPI.Entities;
using WebAPI.Helper;

namespace WebAPI.Services
{
    public class TaskService : BaseService
    {
        #region TaskService
        public TaskService(WebApiDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
        #endregion

        #region GetList()
        public List<ProjectTask> GetList(TaskFilterModel filter, Pager pager, Guid publicId)
        {
            var predicate = PredicateBuilder.New<ProjectTask>(true);

            predicate.Or(p => p.Project.PublicId == publicId.ToString());

            if (!String.IsNullOrEmpty(filter.Name))
            {
                predicate.And(p => p.Name.Contains(filter.Name));
            }
       
            return Context.ProjectTasks
                .Include(p => p.Project.ResponsiblePerson)
                .Include(p => p.TaskPerson)
                .AsNoTracking()
                .AsExpandable()
                .Where(predicate)
                .ToList();
        }
        #endregion

        #region GetItem()
        public ProjectTask GetItem(Guid id)
        {
            return Context.ProjectTasks
                .Include(p => p.Project)
                .ThenInclude(p => p.ResponsiblePerson)
                .SingleOrDefault(p => p.PublicId == id.ToString());
        }
        #endregion

        #region GetListForUser()
        public List<ProjectTask> GetListForUser(Pager pager, TaskFilterModel filter, long taskPersonId)
        {
            var predicate = PredicateBuilder.New<ProjectTask>(false);

            predicate.And(a => a.TaskPersonId == taskPersonId);

            if (!String.IsNullOrEmpty(filter.Name))
            {
                predicate.And(p => p.Name.Contains(filter.Name));
            }

            return Context.ProjectTasks
                .AsNoTracking()
                .AsExpandable()
                .Where(predicate)
                .ToList();
        }
        #endregion
    }
}

