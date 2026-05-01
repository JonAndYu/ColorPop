using ColorPop.Core.Enums;

namespace ColorPop.Core.Models;

/// <summary>
/// Represents a single cell value on the board.
/// A token has a color and no behavior.
/// </summary>
/// <remarks>
/// Token is immutable and acts as a value object.
/// It does not contain game logic.
/// </remarks>
public readonly record struct Token
{
    /// <summary>
    /// The color of this token.
    /// </summary>
    public TokenColor Color { get; }

    /// <summary>
    /// Creates a new token with the specified color.
    /// </summary>
    public Token(TokenColor color)
    {
        Color = color;
    }

    /// <summary>
    /// True if this token represents an empty cell.
    /// </summary>
    public bool IsEmpty => Color == TokenColor.Empty;

    /// <summary>
    /// True if this token is a joker.
    /// </summary>
    public bool IsJoker => Color == TokenColor.Joker;

    /// <summary>
    /// Creates a convenience empty token.
    /// </summary>
    public static Token Empty => new(TokenColor.Empty);
}
