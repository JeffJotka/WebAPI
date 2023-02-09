using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.ViewModels.Project;
using WebAPI.Entities;
using WebAPI.Extensions;
using WebAPI.Helper;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Area("Project")]
    [Route("api/admin/project")]
    public class ProjectController : Controller
    {
        protected IMapper Mapper { get; }
        protected ProjectService Projects { get; }

        private readonly ILogger<ProjectController> _logger;

        public ProjectController(IMapper mapper, ProjectService projectService, ILogger<ProjectController> logger)
        {
            Mapper = mapper;
            Projects = projectService;
            _logger = logger;   
        }


        #region GetList()
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Pagination<Resource<ListItemModel>>> GetList([FromQuery] FilterModel filter, [FromQuery] Pager pager)
        {

            var items = Projects.GetList(filter, pager, User.Id());

            var result = Mapper.Map<IEnumerable<ListItemModel>>(items).Select((p, i) => new Resource<ListItemModel>(p));
            return new Pagination<Resource<ListItemModel>>(result, pager.TotalRows);
        }
        #endregion

        #region GetResource()
        private Resource<FormModel> GetResource(Project entity)
        {
            return new Resource<FormModel>(Mapper.Map<FormModel>(entity));
        }
        #endregion

        #region Fetch()
        [HttpGet("{publicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<Resource<FormModel>> Fetch(Guid publicId)
        {
            var entity = Projects.GetItem(publicId);

            if (entity == null)
                return NotFound();

            return GetResource(entity);
        }
        #endregion

        #region Create()
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult Create([FromBody] FormModel model)
        {
            var entity = Mapper.Map<Project>(model);

            entity.PublicId = Guid.NewGuid().ToString();

            Projects.Create(entity);

            return CreatedAtAction(nameof(Fetch), new { publicId = entity.PublicId }, GetResource(entity));
        }
        #endregion

        #region Update()
        [HttpPut("{publicId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public ActionResult Update(Guid publicId, [FromBody] FormModel model)
        {
            var entity = Projects.GetItem(publicId);

            if (entity == null)
                return NotFound();

            Mapper.Map(model, entity);

            Projects.Update(entity);

            return Accepted();
        }
        #endregion

        #region Delete()
        [HttpDelete("{publicId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult Remove(Guid publicId)
        {
            var entity = Projects.GetItem(publicId);

            if (entity == null)
                return NotFound();

            Projects.Remove(entity);

            return Accepted();
        }
        #endregion
    }
}
