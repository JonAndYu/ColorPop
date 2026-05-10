using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using IndexPage = ColorPop.Web.Pages.Index;

namespace ColorPop.Tests.Web.Pages;

public class IndexTests : BunitContext
{
    public IndexTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        Services.AddMudServices();
    }

    [Fact]
    public void Index_ShouldRenderLandingPageContent()
    {
        // Arrange / Act
        var component = Render<IndexPage>();

        // Assert
        component.Markup.Should().Contain("ColorPop");
        component.Markup.Should().Contain("Create Game");
        component.Markup.Should().Contain("Join Game");
        component.Markup.Should().Contain("Code");
        component.Markup.Should().Contain("Practice");
    }

    [Fact]
    public void Index_ShouldRenderGameCodeInput()
    {
        // Arrange / Act
        var component = Render<IndexPage>();

        // Assert
        var input = component.Find("input[placeholder='Enter code']");
        input.Should().NotBeNull();
    }

    [Fact]
    public void PracticeButton_OnClick_ShouldNavigateToGamePage()
    {
        // Arrange
        var component = Render<IndexPage>();
        var navigationManager = Services.GetRequiredService<NavigationManager>();

        // Act
        component.FindAll("button")
            .Single(button => button.TextContent.Contains("Practice"))
            .Click();

        // Assert
        navigationManager.Uri.Should().EndWith("/game");
    }
}
