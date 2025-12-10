using System.Diagnostics;
using ScrubJay.Validation;

namespace ScrubJay.Decka.Sandbox.Iterations.Iteration1;

public class Player
{
    public IEnumerable<PlayState> Play(PlayState playState)
    {
        var game = playState.Board;
        
        // always check if we're done
        if (playState.IsFinished)
            return [playState];

        // this means we have cards to deal
        deal:

        Debug.Assert(game.Deck.Count >= 4);
        var top4 = game.Deck[..4];
        game.Deck.RemoveRange(0, 4);
        game.PileA.Add(top4[0]);
        game.PileB.Add(top4[1]);
        game.PileC.Add(top4[2]);
        game.PileD.Add(top4[3]);

        // now we collide
        throw Ex.NotImplemented();
    }


    private dynamic Collide(Board game)
    {
        var (a,b,c,d) = game;

        throw Ex.NotImplemented();
    }
}