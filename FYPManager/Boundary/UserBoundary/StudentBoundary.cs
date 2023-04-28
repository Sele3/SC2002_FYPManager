﻿using FYPManager.Boundary.Services;
using FYPManager.Controller.UserController;
using FYPManager.Exceptions;
using System.Configuration;

namespace FYPManager.Boundary.UserBoundary;

public class StudentBoundary : BaseUserBoundary
{
    private readonly StudentController _studentController;

    public StudentBoundary(StudentController studentController)
    {
        _studentController = studentController;
    }

    private static void DisplayMenu() => Console.WriteLine(
        $"{GetWelcomeText()}" +
        $"---------- Student FYP Menu ----------\n" +
        $"-- PROJECTS\n" +
        $"1. View all available projects\n" +
        $"2. View my allocated project\n" +
        $"-- REQUESTS\n" +
        $"3. Request a project allocation\n" +
        $"4. Request a project title change\n" +
        $"5. Request a project deregistration\n" +
        $"6. View my request history\n" +
        $"-- SETTINGS\n" +
        $"7. Change password\n" +
        $"Please select an option:");

    public override void Run()
    {
        while (true)
        {
            try
            {
                DisplayMenu();

                var idx = NumberHandler.ReadInt(7);
                
                switch (idx)
                {
                    case 0:
                        Console.WriteLine("Logging out ...");
                        return;

                    case 1:
                        ViewAllAvailableProjects();
                        break;

                    //case 2:
                    //    ViewAllocatedProject();
                    //    break;
                    //case 3:
                    //    RequestProjectAllocation();
                    //    break;
                    //case 4:
                    //    RequestProjectTitleChange();
                    //    break;
                    //case 5:
                    //    RequestProjectDeregistration();
                    //    break;
                    //case 6:
                    //    ViewRequestHistory();
                    //    break;

                    case 7:
                        ChangePassword();
                        break;
                }
            }
            catch (CustomException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private void ViewAllAvailableProjects()
    {
        var availableProjects = _studentController.GetAllAvailableProjects();
        Console.WriteLine("Available Projects:");
        availableProjects.ForEach(p =>
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine(p);
            Console.WriteLine(new string('-', 50));
        });
    }

    private void ChangePassword()
    {
        var newPassword = GetNewPassword();
        _studentController.ChangePassword(newPassword);
    }
}
