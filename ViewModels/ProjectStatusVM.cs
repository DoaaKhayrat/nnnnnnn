using Microsoft.EntityFrameworkCore;
using ProjectManager1.Models;

namespace ProjectManager1.ViewModels
{
    [Keyless]
    public class ProjectStatusVM
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartTime { get; set; }
        public int StatusId { get; set; }
      //  public string StatusName { get; set; }

        public List<Status> Statuses { get; set; }
        public ICollection<Models.Task> Tasks { get; set; }
    }
}
