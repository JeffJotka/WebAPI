using AutoMapper;
using WebAPI.Entities;

namespace WebAPI.Data.Models
{
    public class AutoMapperProfile : Profile
    {
        #region AutoMapperProfile()
        public AutoMapperProfile() : this("Admin.Project")
        {
        }

        protected AutoMapperProfile(string profileName)
            : base(profileName)
        {
            ProjectModels();
            TaskModels();
        }
        #endregion

        #region ProjectModels()
        protected void ProjectModels()
        {
            // ListItemModel
            CreateMap<Project, ViewModels.Project.ListItemModel>()
                .ForMember(p => p.ResponsiblePersonName, p => p.MapFrom(a => $"{a.ResponsiblePerson.FirstName} {a.ResponsiblePerson.LastName}"));

            //FormModel
            CreateMap<Project, ViewModels.Project.FormModel>()
            .ReverseMap();
        }
        #endregion

        #region TaskModels()
        protected void TaskModels()
        {
            CreateMap<ProjectTask, ViewModels.Task.TaskListItemModel>()
                .ForMember(p => p.TaskPersonName, p => p.MapFrom(a => $"{a.TaskPerson.FirstName} {a.TaskPerson.LastName}"));

            //FormModel
            CreateMap<ProjectTask, ViewModels.Task.TaskFormModel>()
                .ReverseMap();

        }
        #endregion
    }
}
