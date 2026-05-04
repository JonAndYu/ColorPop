using System.ComponentModel.DataAnnotations;
using ColorPop.Core.Enums;
using ColorPop.Core.Models;
using ColorPop.Core.Rules;
using FluentAssertions;

namespace ColorPop.Tests.Rules;

public class MoveValidatorTests
{
    private readonly MoveValidator _sut = new();

    private GameState CreateState(GameStatus status = GameStatus.InProgress)
    {
        var board = CreateBoard(5, TokenColor.Blue);

        var players = new List<Player>
        {
            new Player(1, "P1", new HashSet<TokenColor>()),
            new Player(2, "P2", new HashSet<TokenColor>())
        };

        return new GameState(board, players, 0, status, 0);
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_WhenGameNotInProgress()
    {
        var state = CreateState(GameStatus.Finished);
        var move = new Move(1, new Position(0, 0));

        _sut.IsValid(state, move).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_WhenNotPlayersTurn()
    {
        var state = CreateState();
        var move = new Move(playerId: 2, new Position(0, 0));

        _sut.IsValid(state, move).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_WhenOutOfBounds()
    {
        var state = CreateState();
        var move = new Move(1, new Position(999, 999));

        _sut.IsValid(state, move).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_WhenCellIsEmpty()
    {
        var board = CreateEmptyBoard(5);

        var state = new GameState(
            board,
            new List<Player> { new Player(1, "P1", new HashSet<TokenColor>()) },
            0,
            GameStatus.InProgress);

        var move = new Move(1, new Position(0, 0));

        _sut.IsValid(state, move).Should().BeFalse();
    }

    [Fact]
    public void IsValid_ShouldReturnTrue_WhenMoveIsValid()
    {
        var state = CreateState();
        var move = new Move(1, new Position(0, 0));

        _sut.IsValid(state, move).Should().BeTrue();
    }

    [Fact]
    public void Validate_ShouldReturnErrorMessage_WhenInvalid()
    {
        var state = CreateState(GameStatus.Finished);
        var move = new Move(1, new Position(0, 0));

        var result = _sut.Validate(state, move);

        result.Should().NotBeNull();
        result!.ErrorMessage.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void Validate_ShouldReturnSuccess_WhenValid()
    {
        var state = CreateState();
        var move = new Move(1, new Position(0, 0));

        var result = _sut.Validate(state, move);

        result.Should().Be(ValidationResult.Success);
    }

    /// <summary>
    /// Creates a board filled entirely with the same token color.
    /// Useful for deterministic test setups.
    /// </summary>
    public static Board CreateBoard(int size, TokenColor color)
    {
        var grid = new Token[size, size];

        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                grid[r, c] = new Token(color);
            }
        }

        return new Board(grid);
    }

    /// <summary>
    /// Creates a board filled with Empty tokens.
    /// </summary>
    public static Board CreateEmptyBoard(int size)
    {
        var grid = new Token[size, size];

        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                grid[r, c] = Token.Empty;
            }
        }

        return new Board(grid);
    }
}
