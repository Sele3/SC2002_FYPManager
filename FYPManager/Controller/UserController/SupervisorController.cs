using FYPManager.Entity;
using FYPManager.Entity.Projects;
using FYPManager.Entity.Users;
using FYPManager.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPManager.Controller.UserController;

public class SupervisorController : BaseUserController
{
    public SupervisorController(FYPMContext context) : base(context) { }

    public void CreateProject(string projectTitle, Supervisor supervisor)
    {
        if (_context.Projects.Any(p => p.Title.Equals(projectTitle)))
            throw new ProjectException($"A project with title '{projectTitle}' already exists.");

        var project = new Project
        {
            Title = projectTitle,
            SupervisorID = supervisor.UserID,
            Supervisor = supervisor
        };
        _context.Projects.Add(project);
        _context.SaveChanges();
    }
}
