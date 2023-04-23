using FYPManager.Boundary.Services;
using FYPManager.Entity.Projects;
using FYPManager.Entity.Users;
using Microsoft.Extensions.Configuration;
using Npoi.Mapper;
using Npoi.Mapper.Attributes;
using NPOI.SS.Formula.Functions;

namespace FYPManager.Entity.Data;

internal class DataInitialiser
{
    private class ExcelData { }
    private class UserExcelData : ExcelData
    {
        [Column("Name")]
        public string Name { get; set; } = "";

        [Column("Email")]
        public string Email { get; set; } = "";
    }

    private class ProjectExcelData : ExcelData
    {
        [Column("Supervisor")]
        public string Supervisor { get; set; } = "";

        [Column("Title")]
        public string Title { get; set; } = "";
    }

    private readonly IConfiguration _configuration;
    private readonly FYPMContext _context;
    public DataInitialiser(IConfiguration configuration, FYPMContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public void SeedData()
    {
        SeedStudents();
        SeedSupervisors();
        SeedCoordinators();
        SeedProjects();
    }

    private List<T> GetExcelList<T>(string excelFile) where T : ExcelData
    {
        var section = _configuration.GetRequiredSection(excelFile);
        var pathName = section.GetRequiredSection("Path").Value;
        var sheetName = section.GetRequiredSection("SheetName").Value;

        var mapper = new Mapper(pathName);
        return mapper
            .Take<T>(sheetName)
            .Select(s => s.Value)
            .ToList();
    }

    private static List<T> MapUsers<T>(List<UserExcelData> dataList) where T : User, new()
    {
        var users = new List<T>();

        foreach (var data in dataList)
        {
            if (string.IsNullOrWhiteSpace(data.Name) || string.IsNullOrWhiteSpace(data.Email))
                continue;

            users.Add(new()
            {  
                UserID = EmailService.GetUserID(data.Email).ToLower(),
                Name = data.Name,
                Email = data.Email.ToLower(),
                Password = HashService.Hash("password") 
            });
        }

        return users;
    }

    private void SeedStudents()
    {
        if (_context.Students.Any())
            return;

        var userList = GetExcelList<UserExcelData>("StudentExcel");
        var students = MapUsers<Student>(userList);

        _context.Students.AddRange(students);
        _context.SaveChanges();
    }

    private void SeedSupervisors()
    {
        if (_context.Supervisors.Any())
            return;

        var userList = GetExcelList<UserExcelData>("SupervisorExcel");
        var supervisors = MapUsers<Supervisor>(userList);

        _context.Supervisors.AddRange(supervisors);
        _context.SaveChanges();
    }

    private void SeedCoordinators()
    {
        if (_context.Coordinators.Any())
            return;

        var userList = GetExcelList<UserExcelData>("CoordinatorExcel");
        var coordinators = MapUsers<Coordinator>(userList);

        _context.Coordinators.AddRange(coordinators);
        _context.SaveChanges();
    }

    private List<Project> MapProjects(List<ProjectExcelData> dataList)
    {
        var projects = new List<Project>();
        foreach (var data in dataList)
        {
            if (string.IsNullOrWhiteSpace(data.Supervisor) || string.IsNullOrWhiteSpace(data.Title))
                continue;

            var supervisor = _context.Supervisors.FirstOrDefault(s => s.Name.Equals(data.Supervisor));
            
            projects.Add(new()
            {
                Title = data.Title,
                Supervisor = supervisor ?? throw new Exception("Supervisor not found")
            });
        }
        return projects;
    }

    private void SeedProjects()
    {
        if (_context.Projects.Any())
            return;
        
        var projectList = GetExcelList<ProjectExcelData>("ProjectExcel");
        var projects = MapProjects(projectList);

        _context.Projects.AddRange(projects);
        _context.SaveChanges();
    }
}