using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ColorPop.Web.Components.Player;

public partial class PlayerCard : ComponentBase
{
    private readonly string[] _labels = ["Red", "Blue", "Green", "Purple"];

    private readonly List<ChartSeries> _series =
    [
        new()
        {
            Name = "Score",
            Data = [8, 3, 12, 6]
        }
    ];
}
