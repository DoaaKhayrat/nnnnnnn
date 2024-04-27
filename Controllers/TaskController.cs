using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager1.Models.Repositories;
using ProjectManager1.Models;
using ProjectManager1.ViewModels;

namespace ProjectManager1.Controllers
{
    public class TaskController : Controller
    {
        private readonly IProjectManagerRepository<Models.Task> taskRepository;
        private readonly IProjectManagerRepository<Project> projectRepository;
        private readonly IProjectManagerRepository<Status> statusRepository;
        private readonly ProjectManagerDbContext dbContext;

        public TaskController(IProjectManagerRepository<Models.Task> taskRepository,
                              IProjectManagerRepository<Project> projectRepository,
                              IProjectManagerRepository<Status> statusRepository,
                              ProjectManagerDbContext dbContext)

        {
            this.taskRepository = taskRepository;
            this.projectRepository = projectRepository;
            this.statusRepository = statusRepository;
            this.dbContext = dbContext;
        }

        // GET: TaskController
        public ActionResult Index()
        {
            var task = taskRepository.List();
            return View(task);
        }

        // GET: TaskController/Details/5
        public ActionResult Details(int id)
        {
            var task = taskRepository.Find(id);
            return View(task);
        }

        // GET: TaskController/Create
        public ActionResult Create()
        {
            var model = new TaskProjectStatusVM
            {
                Statuses = statusRepository.List().ToList(),
                Projects = projectRepository.List().ToList()
            };
            return View(model);
        }

        // POST: TaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskProjectStatusVM model)
        {
            try
            {
                var status = statusRepository.Find(model.StatusId);
                var projects = projectRepository.Find(model.ProjectId);
                Models.Task Task = new Models.Task
                {
                    Id = model.TaskId,
                    Name = model.TaskName,
                    StartDate = model.StartDate,
                    DueDate = model.dueDate,
                    ProjectId = model.ProjectId,
                    Project = projects,
                    StatusId = model.StatusId,
                    Status = status,
                };
                taskRepository.Add(Task);
                //return RedirectToAction(nameof(Index));

                return RedirectToAction("Details", "Project", new {id= model.ProjectId });
                //return View(Task);
                //return RedirectToAction("Details"+model.ProjectId, "Project");

            }
            catch
            {
                return View();
            }
        }

        // GET: TaskController/Edit/5
        public ActionResult Edit(int id)
        {
            var task = taskRepository.Find(id);
            var viewModel = new TaskProjectStatusVM
            {
                TaskId = task.Id,
                TaskName = task.Name,
                StartDate = task.StartDate,
                dueDate = task.DueDate,
                ProjectId = task.ProjectId,
                Projects = projectRepository.List().ToList(),
                StatusId = task.StatusId,
                Statuses = statusRepository.List().ToList(),
               // ProjectName = task.Project.Name,
                //StatusName = task.Status.Name,
                
                //ProjectName = task.Project.Name,
              //  StatusName = task.Status.Name,
            };
            return View(viewModel);
        }

        // POST: TaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskProjectStatusVM viewModel)
        {
            try
            {
                //var project = projectRepository.Find(viewModel.ProjectId);
               // var status = statusRepository.Find(viewModel.StatusId);
                Models.Task task = new Models.Task
                {
                    Id = viewModel.TaskId,
                    Name = viewModel.TaskName,
                    StartDate = viewModel.StartDate,
                    DueDate = viewModel.dueDate,
                    ProjectId = viewModel.ProjectId,
                  //  Project = project,
                  //  Status = status,
                    StatusId = viewModel.StatusId,
                    
                    
                };
                taskRepository.Update(viewModel.TaskId, task);
                return RedirectToAction("Details", "Project", new { id = viewModel.ProjectId });
            }
            catch
            {
                return View();
            }
        }

        // GET: TaskController/Delete/5
        public ActionResult Delete(int id)
        {
            var task = taskRepository.Find(id);
            return View(task);
        }

        // POST: TaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                taskRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
