namespace ProjectManager1.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }
        //public List<Status> Statuses { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
