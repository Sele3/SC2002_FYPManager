using Microsoft.IdentityModel.Tokens;

namespace FYPManager.Boundary;

public abstract class BaseBoundary
{
    public string OptionalSuccessMessage { private get; set; } = string.Empty;
    public string OptionalFailureMessage { private get; set; } = string.Empty;

    public void DisplayOptionalHeaderMessage()
    {
        Console.Clear();
        DisplayOptionalSuccessMessage();
        DisplayOptionalFailureMessage();
    }

    private void DisplayOptionalSuccessMessage()
    {
        if (OptionalSuccessMessage.IsNullOrEmpty())
            return;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(OptionalSuccessMessage);
        OptionalSuccessMessage = string.Empty;
        Console.ResetColor();
    }

    private void DisplayOptionalFailureMessage()
    {
        if (OptionalFailureMessage.IsNullOrEmpty())
            return;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(OptionalFailureMessage);
        OptionalFailureMessage = string.Empty;
        Console.ResetColor();
    }
}
