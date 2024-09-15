using Google.Cloud.Vision.V1;
using MemeMuncherAI.Util.Abstraction;

namespace MemeMuncherAI.Util;

public class ImageAnalyzerUtil : IImageAnalyzerUtil
{
    public bool IsMemeImage(ImageAnnotatorClient client, Image image, string file)
    {
        Thread.Sleep(1000);

        var labels = client.DetectLabels(image);
        DisplayResults("Labels", file, labels.Select(l => $"{l.Description} (confidence: {l.Score})"));

        if (labels.Any(label => ContainsMemeKeywords(label.Description)))
            return true;

        var textAnnotations = client.DetectText(image);
        DisplayResults("Text", file, textAnnotations.Select(t => t.Description));

        return textAnnotations.Any(text => text.Description.ToLower().Contains("meme"));
    }

private static bool ContainsMemeKeywords(string text)
{
    string lowerText = text.ToLower();
    var memeKeywords = new List<string>
    {
        "meme", "cartoon", "text", "when you", "that moment when", "me vs.", "starter pack",
        "brace yourselves", "one does not simply", "y u no", "i don't always", "so much win", 
        "what if i told you", "ain't nobody got time for that", "all the things", "bad luck brian", 
        "grumpy cat", "i can haz", "doge", "confession bear", "success kid", "trollface",
        "change my mind", "not sure if", "who would win", "distracted boyfriend",
        "expanding brain", "drake hotline bling", "roll safe", "hide the pain harold",
        "woman yelling at cat", "mocking spongebob", "is this a", "say no more", "kermit sipping tea",
        "how it started", "how it’s going", "two buttons", "is this loss", "galaxy brain",
        "chad", "virgin", "npc", "boomer", "pepe the frog", "arthur's fist", "that's none of my business",
        "so you're telling me", "i should buy a boat", "first world problems", "i don't always",
        "awkward moment seal", "why not both", "you're breathtaking", "nobody:", "surprised pikachu",
        "wojak", "stonks", "giga chad", "big brain", "no one:", "zero context", "can’t believe it’s not",
        "it's free real estate", "aliens guy", "is this a pigeon?", "i fear no man", "head out", "ok boomer",
        
        "invisible bike", "bad luck", "all your base", "i am once again", "and everybody loses their minds",
        "i can’t believe you’ve done this", "this is fine", "some men just want to watch the world burn",
        "they had us in the first half", "i’m the captain now", "you're doing amazing sweetie",
        "don’t talk to me or my son ever again", "not today satan", "draw 25", "uno reverse",
        "the floor is", "but that’s none of my business", "one simply does not", "look at me",
        "the cake is a lie", "nothing to see here", "we live in a society", "press f to pay respects",
        "they don't know", "he protect, he attack", "my disappointment is immeasurable", "and my day is ruined",
        "hello darkness, my old friend", "but wait, there’s more", "clever girl", "i have no idea what i’m doing",
        "what is love?", "cash me ousside", "why you always lying", "hold my beer", "am i a joke to you?",
        "looks like we made it", "if you’re reading this", "they grow up so fast", "rise and shine",
        "in a van down by the river", "you can’t see me", "i’ve made a huge mistake", "you had one job",
        "the most interesting man in the world", "guess i’ll die", "moth and lamp", "still a better love story than",
        "i’m in danger", "this is getting out of hand", "you were the chosen one", "let me in",
        "call an ambulance", "but not for me", "big oof", "visible confusion", "who are you", "i don’t know her",
        "wait, that's illegal", "thanks, i hate it", "oh, you're approaching me?", "this is where the fun begins",
        "this is not a drill", "no one expects the", "another one bites the dust", "you can't handle the truth",
        
        "lol", "omg", "wtf", "bruh", "haha", "lmao", "rofl", "noob", "rekt", "yeet", "salty", "triggered",
        "based", "cringe", "sus", "karen", "simp", "pog", "big mood", "same", "i can't even", "ikr", "smh",
        "thicc", "fam", "bet", "yass", "lit", "slaps roof of", "r/whoosh", "it's a trap", "get in loser",
        "has left the chat", "when bae", "weird flex but ok", "dank", "fire", "no cap", "cheems", "boi",
        "shook", "goals", "bae", "boss move", "dead inside", "wholesome", "small brain", "fren", "big sad",
        
        "the office", "game of thrones", "star wars", "avengers", "spongebob", "pikachu", "rickroll", "rick astley",
        "harry potter", "futurama", "the simpsons", "south park", "shrek", "lord of the rings", "marvel",
        "dc", "batman", "spiderman", "superman", "joker", "iron man", "thanos", "matrix", "keanu reeves",
        "john wick", "bob ross", "pewdiepie", "mr. bean", "walter white", "breaking bad", "saul goodman",
        "it's always sunny", "be like bill", "arthur", "cat meme", "philosoraptor", "ancient aliens",
        "success kid", "awkward penguin", "futurama fry", "evil kermit", "mocking spongebob",
        "tuxedo winnie the pooh", "bernie sanders", "crying jordan", "pepe", "chad", "giga chad"
    };
    return memeKeywords.Any(keyword => lowerText.Contains(keyword));
}

    private static void DisplayResults(string type, string file, IEnumerable<string> results)
    {
        Console.WriteLine($"{type} found in {Path.GetFileName(file)}:");
        foreach (var result in results)
        {
            Console.WriteLine(result);
        }
    }
}