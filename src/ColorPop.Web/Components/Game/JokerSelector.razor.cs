using ColorPop.Core.Enums;
using Microsoft.AspNetCore.Components;

namespace ColorPop.Web.Components.Game;

public partial class JokerSelector : ComponentBase
{
    [Parameter]
    public TokenColor? SelectedColor { get; set; }

    [Parameter]
    public EventCallback<TokenColor?> SelectedColorChanged { get; set; }

    private readonly JokerOption[] _options =
    [
        new("Yellow", TokenColor.Yellow, "#F1C40F"),
        new("Green", TokenColor.Green, "#2ECC71"),
        new("Pink", TokenColor.Pink, "#FF4FB8"),
        new("Orange", TokenColor.Orange, "#E67E22"),
        new("Blue", TokenColor.Blue, "#3498DB")
    ];

    private async Task SelectColor(TokenColor color)
    {
        var nextColor = SelectedColor == color ? (TokenColor?)null : color;
        await SelectedColorChanged.InvokeAsync(nextColor);
    }

    private static string GetOptionStyle(JokerOption option) =>
        $"--joker-option-color: {option.Hex};";

    private sealed record JokerOption(string Label, TokenColor Color, string Hex);
}
