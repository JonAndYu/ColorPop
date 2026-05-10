using Microsoft.AspNetCore.Components;

namespace ColorPop.Web.Pages;

public partial class Index : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    private string _gameCode = string.Empty;

    private void PracticeButton_OnClick()
    {
        NavigationManager.NavigateTo("game");
    }
}
