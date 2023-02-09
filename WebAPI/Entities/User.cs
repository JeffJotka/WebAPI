namespace WebAPI.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public  IList<Project> Projects { get; set; }
        public IList<ProjectTask> ProjectTasks { get; set; }
    }
}
