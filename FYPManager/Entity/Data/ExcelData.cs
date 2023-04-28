namespace FYPManager.Entity.Data;
using Npoi.Mapper.Attributes;

/// <summary>
/// Base class for storing data from Excel files.
/// </summary>
public abstract class ExcelData { }

/// <summary>
/// Class for storing user data from Excel files.
/// </summary>
public class ExcelUserData : ExcelData
{
    [Column("Name")]
    public string Name { get; set; } = "";

    [Column("Email")]
    public string Email { get; set; } = "";
}

/// <summary>
/// Class for storing project data from Excel files.
/// </summary>
public class ExcelProjectData : ExcelData
{
    [Column("Supervisor")]
    public string Supervisor { get; set; } = "";

    [Column("Title")]
    public string Title { get; set; } = "";
}