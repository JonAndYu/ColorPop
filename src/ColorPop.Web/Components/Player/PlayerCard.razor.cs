using ColorPop.Core.Enums;
using Microsoft.AspNetCore.Components;
using PlayerModel = ColorPop.Core.Models.Player;

namespace ColorPop.Web.Components.Player;

public partial class PlayerCard : ComponentBase
{
    private static readonly ColorDefinition[] _colorDefinitions =
    [
        new(TokenColor.Yellow, "Yellow", "#F1C40F"),
        new(TokenColor.Green, "Green", "#2ECC71"),
        new(TokenColor.Pink, "Pink", "#FF4FB8"),
        new(TokenColor.Orange, "Orange", "#E67E22"),
        new(TokenColor.Blue, "Blue", "#3498DB")
    ];

    [Parameter]
    public PlayerModel? Player { get; set; }

    [Parameter]
    public bool IsInitialized { get; set; } = true;

    private string PlayerName => Player?.Name ?? "User Name";
    private ColorGroup[] ColorGroups => _colorDefinitions
        .Select(color => new ColorGroup(
            color.Label,
            Player?.CapturedColorCounts.GetValueOrDefault(color.TokenColor) ?? 0,
            color.Hex))
        .ToArray();

    private int MaxGroupCount => Math.Max(1, ColorGroups.Max(group => group.Count));
    private string ContentClass => IsInitialized ? "player-card-content" : "player-card-content is-waiting";

    private string GetBarStyle(ColorGroup group)
    {
        var heightPercent = Math.Max(8, group.Count * 100 / MaxGroupCount);

        return $"height: {heightPercent}%; background-color: {group.Color};";
    }

    private sealed record ColorDefinition(TokenColor TokenColor, string Label, string Hex);

    private sealed record ColorGroup(string Label, int Count, string Color);
}
