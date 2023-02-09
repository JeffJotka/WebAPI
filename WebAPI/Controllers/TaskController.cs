using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.ViewModels.Task;
using WebAPI.Entities;
using WebAPI.Extensions;
using WebAPI.Helper;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Area("Project-task")]
    [Route("api/admin/project/task")]
    public class TaskController : Controller
    {
        protected IMapper Mapper { get; }
        protected TaskService TaskService { get; }
        protected ProjectService ProjectService { get; }

        public TaskController(IMapper mapper, TaskService taskService, ProjectService projectService)
        {
            Mapper = mapper;
            TaskService = taskService;
            ProjectService = projectService;
        }

        #region GetResource()
        private Resource<TaskFormModel> GetResource(ProjectTask entity)
        {
            return new Resource<TaskFormModel>(Mapper.Map<TaskFormModel>(entity));
        }
        #endregion

        #region GetList()
        [HttpGet("{projectPublicId}/list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<Pagination<Resource<TaskListItemModel>>> GetList([FromQuery] TaskFilterModel filter, [FromQuery] Pager pager, Guid projectPublicId)
        {
            var entity = ProjectService.GetItem(projectPublicId);

            if (entity == null)
                return NotFound();

            var items = TaskService.GetList(filter, pager, projectPublicId);

            var result = Mapper.Map<IEnumerable<TaskListItemModel>>(items).Select((p, i) => new Resource<TaskListItemModel>(p));

            return new Pagination<Resource<TaskListItemModel>>(result, pager.TotalRows);
        }
        #endregion

        #region Create()
        [HttpPost("{projectPublicId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult Create([FromBody] TaskFormModel model, Guid projectPublicId)
        {
            var project = ProjectService.GetItem(projectPublicId);

            if (project == null)
                return NotFound();

            var entity = Mapper.Map<ProjectTask>(model);

            entity.PublicId = Guid.NewGuid().ToString();

            TaskService.Create(entity);

            return CreatedAtAction(nameof(Fetch), new { publicId = entity.PublicId }, GetResource(entity));
        }
        #endregion

        #region Fetch()
        [HttpGet("{publicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<Resource<TaskFormModel>> Fetch(Guid publicId)
        {
            var entity = TaskService.GetItem(publicId);

            if (entity == null)
                return NotFound();

            return GetResource(entity);
        }
        #endregion

        #region Update()
        [HttpPut("{publicId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public ActionResult Update(Guid publicId, [FromBody] TaskFormModel model)
        {
            var entity = TaskService.GetItem(publicId);

            if (entity == null)
                return NotFound();

            Mapper.Map(model, entity);

            TaskService.Update(entity);

            return Accepted();
        }
        #endregion

        #region Delete()
        [HttpDelete("{publicId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public ActionResult Remove(Guid publicId)
        {
            var entity = TaskService.GetItem(publicId);

            if (entity == null)
                return NotFound();

            TaskService.Remove(entity);

            return Accepted();
        }
        #endregion

        #region GetListForUser()
        [HttpGet("user-tasks")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Pagination<Resource<TaskListItemModel>>> GetListForUser([FromQuery] Pager pager, [FromQuery] TaskFilterModel filter)
        {
            var item = TaskService.GetListForUser(pager, filter, User.Id());

            var result = Mapper.Map<IEnumerable<TaskListItemModel>>(item).Select((p, i) => new Resource<TaskListItemModel>(p));

            return new Pagination<Resource<TaskListItemModel>>(result, pager.TotalRows);
        }
        #endregion
    }
}
