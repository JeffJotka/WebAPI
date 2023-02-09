namespace WebAPI.Data.ViewModels.Task
{
    public class TaskFilterModel
    {
        public long UserId { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public long TaskPersonId { get; set; }
        public long ProjectId { get; set; }
    }
}
