using Microsoft.AspNetCore.Components;

namespace ColorPop.Web.Components.Player;

public partial class PlayerCard : ComponentBase
{
    [Parameter]
    public bool IsInitialized { get; set; } = true;

    private readonly ColorGroup[] _colorGroups =
    [
        new("Yellow", 8, "#F1C40F"),
        new("Green", 3, "#2ECC71"),
        new("Pink", 12, "#FF4FB8"),
        new("Orange", 6, "#E67E22"),
        new("Blue", 10, "#3498DB")
    ];

    private int MaxGroupCount => Math.Max(1, _colorGroups.Max(group => group.Count));
    private string ContentClass => IsInitialized ? "player-card-content" : "player-card-content is-waiting";

    private string GetBarStyle(ColorGroup group)
    {
        var heightPercent = Math.Max(8, group.Count * 100 / MaxGroupCount);

        return $"height: {heightPercent}%; background-color: {group.Color};";
    }

    private sealed record ColorGroup(string Label, int Count, string Color);
}
