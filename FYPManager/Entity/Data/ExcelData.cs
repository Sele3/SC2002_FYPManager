namespace FYPManager.Entity.Data;
using Npoi.Mapper.Attributes;

public class ExcelData { }

public class ExcelUserData : ExcelData
{
    [Column("Name")]
    public string Name { get; set; } = "";

    [Column("Email")]
    public string Email { get; set; } = "";
}

public class ExcelProjectData : ExcelData
{
    [Column("Supervisor")]
    public string Supervisor { get; set; } = "";

    [Column("Title")]
    public string Title { get; set; } = "";
}
