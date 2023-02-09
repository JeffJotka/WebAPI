namespace WebAPI.Data.ViewModels.Task
{
    public class TaskFormModel
    {
        public string PublicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public long ProjectId { get; set; }
        public long TaskPersonId { get; set; }

    }
}
