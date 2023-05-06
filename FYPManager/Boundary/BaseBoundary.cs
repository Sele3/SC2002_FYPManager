using FYPManager.Boundary.Services.ConsoleDisplay;
using Microsoft.IdentityModel.Tokens;

namespace FYPManager.Boundary;

public abstract class BaseBoundary
{
    /// <summary>
    /// 
    /// </summary>
    public string OptionalSuccessMessage { private get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string OptionalFailureMessage { private get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string OptionalMessage { private get; set; } = string.Empty;

    public void DisplayOptionalHeaderMessage()
    {
        Console.Clear();
        DisplayOptionalSuccessMessage();
        DisplayOptionalFailureMessage();
        DisplayOptionalMessage();
    }

    private void DisplayOptionalSuccessMessage()
    {
        if (OptionalSuccessMessage.IsNullOrEmpty())
            return;

        MenuDisplayService.DisplayColoredText(OptionalSuccessMessage, ConsoleColor.Green);
        OptionalSuccessMessage = string.Empty;
    }

    private void DisplayOptionalFailureMessage()
    {
        if (OptionalFailureMessage.IsNullOrEmpty())
            return;

        MenuDisplayService.DisplayColoredText(OptionalFailureMessage, ConsoleColor.Red);
        OptionalFailureMessage = string.Empty;
    }

    private void DisplayOptionalMessage()
    {
        if (OptionalMessage.IsNullOrEmpty())
            return;

        MenuDisplayService.DisplayColoredText(OptionalMessage, ConsoleColor.Yellow);
        OptionalMessage = string.Empty;
    }
}
