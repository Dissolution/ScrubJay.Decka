using ScrubJay.Decka.Sandbox.Iterations.StackBased;
using ScrubJay.Decka.Sandbox.Iterations.StackBased.Display;
using ScrubJay.Randomization;
using ScrubJay.Randomization.Seeding;

var starting = Standard52.Default.Cards.DeepClone();

var seed = RandSeed.Known();
var rand = new RomuDuoJrPrng(seed);
rand.Shuffle(starting);

string? str = TextBuilder.Build(tb =>
    tb.Delimit('|', starting, static (t, card) => t.Write(card.ToString(DisplayFormat.Unicode))));
Console.WriteLine($"""
    Seed: {seed}
    Starting Deck: {str}
    """);
    
    