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

    /// <summary>
    /// The constructor for the DataInitialiser class. 
    /// It takes in an <see cref="IConfiguration"/> and <see cref="FYPMContext"/> object.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration"/> object that contains the configuration settings for the application.</param>
    /// <param name="context">The <see cref="FYPMContext"/> object that represents the database context.</param>
    public DataInitialiser(IConfiguration configuration, FYPMContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    /// <summary>
    /// Seed the data into the database. 
    /// This method calls the other seeding methods for students, supervisors, coordinators, and projects.
    /// </summary>
    public void SeedData()
    {
        SeedStudents();
        SeedSupervisors();
        SeedCoordinators();
        SeedProjects();
    }

    /// <summary>
    /// Seed the students data into the database.
    /// </summary>
    private void SeedStudents()
    {
        if (_context.Students.Any())
            return;

        var userList = GetExcelList<ExcelUserData>("StudentExcel");
        var students = MapUsers<Student>(userList);

        _context.Students.AddRange(students);
        _context.SaveChanges();
    }

    /// <summary>
    /// Seed the supervisors data into the database.
    /// </summary>
    private void SeedSupervisors()
    {
        if (_context.Supervisors.Any())
            return;

        var userList = GetExcelList<ExcelUserData>("SupervisorExcel");
        var supervisors = MapUsers<Supervisor>(userList);

        _context.Supervisors.AddRange(supervisors);
        _context.SaveChanges();
    }

    /// <summary>
    /// Seed the coordinators data into the database.
    /// </summary>
    private void SeedCoordinators()
    {
        if (_context.Coordinators.Any())
            return;

        var userList = GetExcelList<ExcelUserData>("CoordinatorExcel");
        var coordinators = MapUsers<Coordinator>(userList);

        _context.Coordinators.AddRange(coordinators);
        _context.SaveChanges();
    }

    /// <summary>
    /// Seed the projects data into the database.
    /// </summary>
    private void SeedProjects()
    {
        if (_context.Projects.Any())
            return;

        var projectList = GetExcelList<ExcelProjectData>("ProjectExcel");
        var projects = MapProjects(projectList);

        _context.Projects.AddRange(projects);
        _context.SaveChanges();
    }

    /// <summary>
    /// Get the list of data from the excel file. 
    /// </summary>
    /// <typeparam name="T">The type of data to retrieve from the excel file. It must derive from the ExcelData class.</typeparam>
    /// <param name="excelFile">The name of the section in the appsettings.json file that contains the Path and SheetName values for the specified excel file.</param>
    /// <returns>A list of data retrieved from the specified excel file.</returns>
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

    /// <summary>
    /// Maps the list of <see cref="ExcelUserData"/> to a list of <see cref="User"/> objects.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="User"/> object to create. It must derive from the <see cref="User"/> class.</typeparam>
    /// <param name="dataList">The list of <see cref="ExcelUserData"/> to map.</param>
    /// <returns>A list of <see cref="User"/> objects created from the specified list of <see cref="ExcelUserData"/>.</returns>
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

    /// <summary>
    /// Maps the list of <see cref="ExcelProjectData"/> to a list of <see cref="Project"/> objects.
    /// </summary>
    /// <param name="dataList">The list of <see cref="ExcelProjectData"/> to map.</param>
    /// <returns>A list of mapped <see cref="Project"/> objects.</returns>
    /// <exception cref="Exception">Thrown when the <see cref="Supervisor"/> for a project is not found in the database.</exception>
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