using ScrubJay.Decka.Sandbox.Iterations.SB10;
using ScrubJay.Randomization;
using ScrubJay.Randomization.Seeding;
using ScrubJay.Text.Building;

var starting = Decks.Standard52.Cards.DeepClone();

var seed = RandSeed.Known();
var rand = new RomuDuoJrPrng(seed);
rand.Shuffle(starting);

string? str = TextBuilder.Build(tb =>
    tb.Delimit('|', starting, static (t, card) => t.Write(card.ToString(DisplayFormat.Unicode))));
Console.WriteLine($"""
    Seed: {seed}
    Starting Deck: {str}
    """);

var board = new AcesUpBoard()
{
    Deck = new(starting),
};
var log = new BoardLog();


top:

if (board.Deck.Count == 0)
    goto fin;

// deal
board.Move(PileIndex.Deck, PileIndex.PileA, MoveReason.Deal);
board.Move(PileIndex.Deck, PileIndex.PileB, MoveReason.Deal);
board.Move(PileIndex.Deck, PileIndex.PileC, MoveReason.Deal);
board.Move(PileIndex.Deck, PileIndex.PileD, MoveReason.Deal);

    
    
    
    
    
    fin: