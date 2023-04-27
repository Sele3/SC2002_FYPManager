using FYPManager.Boundary.Services;
using FYPManager.Entity.Projects;
using FYPManager.Entity.Users;
using Microsoft.Extensions.Configuration;
using Npoi.Mapper;


namespace FYPManager.Entity.Data;

public class DataInitialiser
{
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

    private void SeedStudents()
    {
        if (_context.Students.Any())
            return;

        var userList = GetExcelList<ExcelUserData>("StudentExcel");
        var students = MapUsers<Student>(userList);

        _context.Students.AddRange(students);
        _context.SaveChanges();
    }

    private void SeedSupervisors()
    {
        if (_context.Supervisors.Any())
            return;

        var userList = GetExcelList<ExcelUserData>("SupervisorExcel");
        var supervisors = MapUsers<Supervisor>(userList);

        _context.Supervisors.AddRange(supervisors);
        _context.SaveChanges();
    }

    private void SeedCoordinators()
    {
        if (_context.Coordinators.Any())
            return;

        var userList = GetExcelList<ExcelUserData>("CoordinatorExcel");
        var coordinators = MapUsers<Coordinator>(userList);

        _context.Coordinators.AddRange(coordinators);
        _context.SaveChanges();
    }

    private void SeedProjects()
    {
        if (_context.Projects.Any())
            return;

        var projectList = GetExcelList<ExcelProjectData>("ProjectExcel");
        var projects = MapProjects(projectList);

        _context.Projects.AddRange(projects);
        _context.SaveChanges();
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

    private static List<T> MapUsers<T>(List<ExcelUserData> dataList) where T : User, new()
    {
        var users = new List<T>();

        foreach (var data in dataList)
        {
            if (string.IsNullOrWhiteSpace(data.Name) || string.IsNullOrWhiteSpace(data.Email))
                continue;

            users.Add(new()
            {
                UserID = EmailService.GetUserID(data.Email).ToLower(),
                Name = data.Name.Trim(),
                Email = data.Email.ToLower(),
                Password = HashService.Hash("password")
            });
        }

        return users;
    }

    private List<Project> MapProjects(List<ExcelProjectData> dataList)
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
}