# SC2002 FYPManager

## Introduction

FYP Manager is a project that I worked on during my free time to enhance my C# programming skills and gain experience with Entity Framework Core. The project is based on the assignment project for SC2002 - Object Oriented Programming.

The application follows the **Boundary-Control-Entity (BCE)** architecture, which is a design pattern that separates the components of the system into three main layers: Boundary, Control, and Entity. This architecture promotes a clear separation of concerns and modular design, making it easier to maintain and extend the application. 

It also includes unit tests that ensure the functionality of its key features. These tests follow the Arrange-Act-Assert (AAA) pattern, which provides a structured approach to organizing test cases. The AAA pattern consists of three main steps: Arrange, Act, and Assert.

The primary goal of the FYP Manager is to provide a platform for managing and organizing final-year projects (FYPs) for students. It allows users to perform various tasks related to FYPs, including creating, updating, and deleting project information.

**Software engineering design principles applied include**:
- Singleton: To configure settings classes
- Factory: To handle the corresponding user boundary class based on the user input
- Strategy: To manage the filter and order display options

## Features

- User-friendly interface for students, supervisors and coordinators.

### Students

- View available/allocated projects
- Send project allocation/deallocation/title change requests
- View personal request history

### Supervisors

- View/create new projects
- Manage student requests related to their projects
- View request history

### Coordinators

- Perform same the functionalites as that of a Supervisor
- Manage student deallocation and supervisor change requests
- Generate overall system report 
- View all projects/requests in the system

## Technologies

- C# programming language
- Entity Framework Core (EF Core)
- .NET Core

## Screenshots 

<img src="https://github.com/Sele3/SC2002_FYPManager/assets/96132790/000222eb-16ce-441c-9443-20769cfcd58a" />
</br>
<img src="https://github.com/Sele3/SC2002_FYPManager/assets/96132790/f4ab0329-fa74-4b3f-8748-579adeb4e88c" />
</br>
<img src="https://github.com/Sele3/SC2002_FYPManager/assets/96132790/b8eeff04-02fd-4078-aa0c-c07f361b7a1f" width=500 />

---
Developed by Sayhong 
