using System;
using System.Collections.Generic;

namespace PickupLines.Data
{
    public static class PickupResponses
    {
        static readonly Random Rng = new Random();

        static readonly List<Tuple<string,bool>> Replies = new List<Tuple<string,bool>>
        {
            Tuple.Create("WHATEVER!:", false),
            Tuple.Create("Ummmm, I'm engaged", false),
            Tuple.Create("Get a tan, nerd.", false),
            Tuple.Create("I don't date guys that speak Klingon", false),
            Tuple.Create("I'm just here to hang out with my friends", false),
            Tuple.Create("That's the best line I've ever heard!", true),
            Tuple.Create("Here's my number: 555-1212", false),
            Tuple.Create("Sorry I'm moving.  To Australia", false),
            Tuple.Create("Why don't you message me on my MySpace page", false),
            Tuple.Create("What did you just say to me???", false),
            Tuple.Create("No thanks, but my friend over there might be interested.", false),
            Tuple.Create("Does that line actually work for you?", false),
            Tuple.Create("You should think about working out a bit", false),
            Tuple.Create("It's not you, it's me", false),
            Tuple.Create("Maybe later, I have to meet my probation officer", false),
            Tuple.Create("That is the most insulting thing I've ever heard!", false),
            Tuple.Create("Nice video game shirt. Loser.", false),
            Tuple.Create("OMG You're like, my dad's age!", false),
            Tuple.Create("I thought they banned you from this place!", false),
        };

        static readonly string[] ExceptionText = new[]
        {
             "Take a shower, creep",
             "Move out of your mom's basement.",
        };

        public static Tuple<string,bool> TryLine(string line)
        {
            if ((Rng.Next(20) % 10) != 0)
            {
                return Replies[Rng.Next(Replies.Count)];
            }

            throw new DrinkInYourFaceException(
                    ExceptionText[Rng.Next(ExceptionText.Length)]);
        }
    }
}
