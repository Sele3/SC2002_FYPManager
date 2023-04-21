using FYPManager.Boundary.Services;
using FYPManager.Entity.Users;
using Npoi.Mapper;
using Npoi.Mapper.Attributes;
using NPOI.SS.Formula.Functions;
using System.Reflection;

namespace FYPManager.Entity.Data;

internal class DataInitialiser
{
    private class UserData
    {
        [Column("Name")]
        public string Name { get; set; } = "";

        [Column("Email")]
        public string Email { get; set; } = "";
    }


    public static void SeedData(FYPMContext context)
    {
        SeedStudents(context);
    }

    private static Student MapStudent(UserData data)
    {
        return new Student
        {
            UserID = EmailService.GetUserID(data.Email),
            Name = data.Name,
            Email = data.Email,
            Password = HashService.Hash("password")
        };
    }

    private static void SeedStudents(FYPMContext context)
    {
        if (context.Students.Any())
            return;

        var mapper = new Mapper("./Data/student list.xlsx");
        var studentList = mapper.Take<UserData>("Sheet1").ToList();

        var students = studentList.Select(s => MapStudent(s.Value)).ToList();
        context.Students.AddRange(students);

        try
        {
            context.SaveChanges();
        } catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while seeding data: {ex.Message}");
        }
    }
}
