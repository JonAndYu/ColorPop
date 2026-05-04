using ColorPop.Core.Enums;
using ColorPop.Core.Models;

public class BoardRenderer : IBoardRenderer
{
    public void Render(Board board)
    {
        for (int r = 0; r < board.Rows; r++)
        {
            for (int c = 0; c < board.Cols; c++)
            {
                var token = board.GetToken(new Position(r, c));

                Console.Write(Symbol(token));
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private static char Symbol(Token token)
    {
        return token.Color switch
        {
            TokenColor.Empty => '.',
            TokenColor.Blue => 'B',
            TokenColor.Green => 'G',
            TokenColor.Yellow => 'Y',
            TokenColor.Pink => 'P',
            TokenColor.Orange => 'O',
            TokenColor.Joker => 'J',
            _ => '?'
        };
    }
}
