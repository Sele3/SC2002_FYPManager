using FYPManager.Boundary.Services;
using FYPManager.Entity.Users;
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


    public static void SeedData(FYPMContext context)
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

    private static void SeedStudents(FYPMContext context)
    {
        if (context.Students.Any())
            return;

        var mapper = new Mapper("./Data/student list.xlsx");
        var studentList = mapper.Take<UserExcelData>("Sheet1").ToList();

        var students = MapUsers<Student>(studentList.Select(s => s.Value).ToList());
        context.Students.AddRange(students);

        SaveChanges(context);
    }

    private static void SeedSupervisors(FYPMContext context)
    {
        if (context.Supervisors.Any())
            return;

        var mapper = new Mapper("./Data/faculty_list.xlsx");
        var supervisorList = mapper.Take<UserExcelData>("30Aug2022_FYP Examiner List").ToList();

        var supervisors = MapUsers<Supervisor>(supervisorList.Select(s => s.Value).ToList());
        context.Supervisors.AddRange(supervisors);

        SaveChanges(context);
    }

    private static void SeedCoordinators(FYPMContext context)
    {
        if (context.Coordinators.Any())
            return;

        var mapper = new Mapper("./Data/FYP coordinator.xlsx");
        var coordinatorList = mapper.Take<UserExcelData>("30Aug2022_FYP Examiner List").ToList();

        var coordinators = MapUsers<Coordinator>(coordinatorList.Select(s => s.Value).ToList());
        context.Coordinators.AddRange(coordinators);

        SaveChanges(context);
    }
}