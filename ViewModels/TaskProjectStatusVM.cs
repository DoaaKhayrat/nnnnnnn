using Microsoft.EntityFrameworkCore;
using ProjectManager1.Models;

namespace ProjectManager1.ViewModels
{
    [Keyless]
    public class TaskProjectStatusVM
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime dueDate { get; set; }
        public int ProjectId { get; set; }
        public List<Project> Projects { get; set; }
        public int StatusId { get; set; }
        public List<Status> Statuses { get; set; }
        public string ProjectName { get; set; }
        public string StatusName { get; set; }

    }
}
