using FYPManager.Boundary.Services;
using FYPManager.Entity.Users;
using Microsoft.Extensions.Configuration;
using Npoi.Mapper;
using Npoi.Mapper.Attributes;
using NPOI.SS.Formula.Functions;

namespace FYPManager.Entity.Data;

internal class DataInitialiser
{
    private class UserExcelData
    {
        [Column("Name")]
        public string Name { get; set; } = "";

        [Column("Email")]
        public string Email { get; set; } = "";
    }

    private readonly IConfiguration _configuration;
    public DataInitialiser(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SeedData(FYPMContext context)
    {
        SeedStudents(context);
        SeedSupervisors(context);
        SeedCoordinators(context);
    }

    private static void SaveChanges(FYPMContext context)
    {
        try
        {
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while seeding data: {ex.Message}");
        }
    }

    private List<UserExcelData> GetUserList(string excelFile)
    {
        var section = _configuration.GetRequiredSection(excelFile);
        var pathName = section.GetRequiredSection("Path").Value;
        var sheetName = section.GetRequiredSection("SheetName").Value;

        var mapper = new Mapper(pathName);
        return mapper
            .Take<UserExcelData>(sheetName)
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

    private void SeedStudents(FYPMContext context)
    {
        if (context.Students.Any())
            return;

        var userList = GetUserList("StudentExcel");
        var students = MapUsers<Student>(userList);

        context.Students.AddRange(students);
        SaveChanges(context);
    }

    private void SeedSupervisors(FYPMContext context)
    {
        if (context.Supervisors.Any())
            return;

        var userList = GetUserList("SupervisorExcel");
        var supervisors = MapUsers<Supervisor>(userList);

        context.Supervisors.AddRange(supervisors);
        SaveChanges(context);
    }

    private void SeedCoordinators(FYPMContext context)
    {
        if (context.Coordinators.Any())
            return;

        var userList = GetUserList("CoordinatorExcel");
        var coordinators = MapUsers<Coordinator>(userList);

        context.Coordinators.AddRange(coordinators);
        SaveChanges(context);
    }
}