namespace WebAPI.Data.ViewModels.Task
{
    public class TaskListItemModel
    {
        public string PublicId { get; set; }
        public string TaskPersonName { get; set; }
        public long ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
