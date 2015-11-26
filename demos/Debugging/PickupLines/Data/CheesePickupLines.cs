using System;

namespace PickupLines.Data
{
    static class CheesePickupLines
    {
        static readonly string[] PickupLines = 
        {
            "Are you lost ma'am? Because heaven is a long way from here.",
            "Hello, I'm a thief, and I'm here to steal your heart.",
            "If girls were boogers, I'd pick you first.",
            "If I could rearrange the alphabet, I'd put U and I together.",
            "Is your name summer? 'Cause you're HOT!",
            "Is there an airport nearby or is that just my heart taking off?",
            "Wouldn't we look cute on a wedding cake together?",
            "Your daddy must have been a baker, because you've got a nice set of buns.",
            "I feel like Richard Gere, I'm standing next to you, the Pretty Woman.",
            "Do you believe in love at first sight, or should I walk by again?",
            "So, do you like fat guys with no money?",
            "So, are you going to give me your phone number, or am I going to have to stalk you?",
        };

        static Random RNG = new Random();

        static public string GetPickupLine()
        {
            return PickupLines[RNG.Next(PickupLines.Length)];
        }
    }
}
