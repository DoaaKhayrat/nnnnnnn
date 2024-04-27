using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManager1.Models;
using ProjectManager1.Models.Repositories;
using ProjectManager1.ViewModels;

namespace ProjectManager1.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectManagerRepository<Project> projectRepository;
        private readonly IProjectManagerRepository<Status> statusRepository;
        private readonly IProjectManagerRepository<Models.Task> taskRepository;
        private readonly ProjectManagerDbContext dbContext;

        public ProjectController(IProjectManagerRepository<Project> projectRepository,
                                 IProjectManagerRepository<Status> statusRepository,
                                 IProjectManagerRepository<Models.Task> taskRepository,
                                 ProjectManagerDbContext dbContext)

        {
            this.projectRepository = projectRepository;
            this.statusRepository = statusRepository;
            this.taskRepository = taskRepository;
            this.dbContext = dbContext;
        }
        // GET: ProjectController
        public ActionResult Index()
        {
            var project = projectRepository.List();
            return View(project);
        }

        // GET: ProjectController/Details/5

        public ActionResult Details(int id)
        {
            var project = projectRepository.Find(id);
            return View(project);
        }

        //public async Task<ActionResult> Details(int id)
        //{

        //    var project = await dbContext.Projects
        //                  .Include(p => p.Tasks)
        //                  .Include(s => s.Status)
        //                  .FirstOrDefaultAsync(m => m.Id == id);

        //    //var project = projectRepository.List();
        //    return View(project);
        //}

        //public ActionResult Details(int id)
        //{

        //    var Projects = projectRepository.Find(id);
        //    return View(Projects);
        //}

        // GET: ProjectController/Create
        public ActionResult Create()
        {
            var model = new ProjectStatusVM
            {
                Statuses = statusRepository.List().ToList()
            };
            return View(model);
        }

        // POST: ProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectStatusVM model, Models.Task tasks)
        {
            try
            {
                var status = statusRepository.Find(model.StatusId);
                Project Project = new Project
                {
                    Id = model.ProjectId,
                    Name = model.ProjectName,
                    StartTime = model.StartTime,
                    Status = status,
                    StatusId = model.StatusId,
                    Tasks = model.Tasks,
                };
                projectRepository.Add(Project);
                 return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectController/Edit/5
        public ActionResult Edit(int id)
        {
            var project = projectRepository.Find(id);

            var viewModel = new ProjectStatusVM
            {
                ProjectId = project.Id,
                ProjectName = project.Name,
                StartTime = project.StartTime,
                Statuses = statusRepository.List().ToList(),
                StatusId = project.StatusId,
                Tasks = project.Tasks,

            };
            return View(viewModel);
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectStatusVM viewModel)
        {
            try
            {
                var status = statusRepository.Find(viewModel.StatusId);
                Project project = new Project
                {
                    Id = viewModel.ProjectId,
                    Name= viewModel.ProjectName,
                    StartTime = viewModel.StartTime,
                    StatusId= viewModel.StatusId,
                    Tasks = viewModel.Tasks,
                    Status = status
                };
                projectRepository.Update(viewModel.ProjectId, project);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
