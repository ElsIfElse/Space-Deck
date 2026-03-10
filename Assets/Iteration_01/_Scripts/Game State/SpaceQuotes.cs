using System.Collections.Generic;
using UnityEngine;

public class SpaceQuotes
{
    List<string> Quotes;

    public SpaceQuotes()
    {
        Quotes = new()
        {
            "\"\"Space doesn't forgive. Neither does standing still.\"",
            "\"Every dead star was once the brightest thing in the sky.\"",
            "\"The void has swallowed greater ships than yours.\"",
            "\"Drift long enough and even the dark becomes familiar.\"",
            "\"Gravity pulls everything down. Rising is the exception.\"",
            "\"No signal. No rescue. Only forward.\"",
            "\"The universe expands. So can you.\"",
            "\"Even supernovas are just stars that refused to die quietly.\"",
            "\"Lost in the dark again. Good. You know this place.\"",
            "\"The wreckage floats. So do you.\"",
            "\"Distance is just failure with patience.\"",
            "\"Stars collapse before they're reborn. So it goes.\"",
            "\"The cosmos has seen empires fall. It will see you rise.\"",
            "\"Black holes consume everything except the will to try.\"",
            "\"You are stardust that learned to lose and try again.\"",
            "\"Even light takes time to reach its destination.\"",
            "\"The silence between stars is not emptiness. It's patience.\"",
            "\"Orbit long enough and you'll find your way back.\"",
            "\"Comets burn up. They also keep going.\"",
            "\"The universe has no record of your last defeat.\"",
            "\"Every crash landing is still a landing.\"",
            "\"You've drifted before. You've found your way before.\"",
            "\"The dark between galaxies is vast. So is your stubbornness.\"",
            "\"Nebulas are just destruction that became something beautiful.\"",
            "\"Another run. Another chance to make the void fear you.\""
        };
    }

    public string RandomQuote()
    {
        return Quotes[Random.Range(0, Quotes.Count)];
    }

}