using System;

namespace WebAPI.Entities
{
    public partial class Project : Entity
    {
        public string PublicId { get; set; }
        public string Name { get; set; }
        public long ResponsiblePersonId { get; set; }

        public User ResponsiblePerson { get; set; }
        public IList<ProjectTask> ProjectTasks { get; set; }

    }
}
