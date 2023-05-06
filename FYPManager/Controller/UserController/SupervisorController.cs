using FYPManager.Entity;
using FYPManager.Entity.Projects;
using FYPManager.Entity.Requests;
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
        ValidateTitleDoesNotExist(projectTitle);

        var project = new Project
        {
            Title = projectTitle,
            SupervisorID = supervisor.UserID,
            Supervisor = supervisor
        };
        _context.Projects.Add(project);
        _context.SaveChanges();
    }

    public List<Project> GetProjectsBy(Supervisor supervisor)
        => _context.Projects.Where(p => p.SupervisorID == supervisor.UserID).ToList();

    public void UpdateProjectTitle(int projectID, string newTitle)
    {
        ValidateProjectID(projectID);
        ValidateTitleDoesNotExist(newTitle);
        
        var project = _context.Projects
            .FirstOrDefault(p => p.ProjectID == projectID);

        project!.Title = newTitle;
        _context.SaveChanges();
    }

    public int GetNewPendingRequestCount(string supervisorID)
    {
        var supervisor = GetUser<Supervisor>(supervisorID);
        var pendingRequestCount = _context
            .TitleChangeRequests
            .Where(r => r.RequestToSupervisorID.Equals(supervisorID)
            && r.RequestStatus == RequestStatus.Pending
            && !r.IsSeen)
            .Count();

        return pendingRequestCount;
    }

    private void ValidateProjectID(int projectID)
    {
        if (!_context.Projects.Any(p => p.ProjectID == projectID))
            throw new ProjectException($"Project with ID '{projectID}' does not exist.");
    }

    private void ValidateTitleDoesNotExist(string title)
    {
        if (_context.Projects.Any(p => p.Title.Equals(title)))
            throw new ProjectException($"A project with title '{title}' already exists.");
    }
}
