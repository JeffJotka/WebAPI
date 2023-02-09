using System.Data.Entity.Core.Metadata.Edm;
using System.Net.NetworkInformation;
using WebAPI.Helper;
using WebAPI.Extensions;


namespace WebAPI.Entities
{
    public class ProjectTask : Entity
    {
        public string PublicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public long TaskPersonId { get; set; }
        public long ProjectId { get; set; }

        public Project Project { get; set; }
        public User TaskPerson { get; set; }

    }
}
