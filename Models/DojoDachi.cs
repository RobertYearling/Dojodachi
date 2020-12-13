using System;
using System.Collections.Generic;

namespace DojoDachi.Models
{
    public class Dachi
    {
        public int Happiness;
        public int Fullness;
        public int Energy;
        public int Meals;
        public Dachi()
        {
            // Your Dojodachi should start with 20 happiness, 20 fullness, 50 energy, and 3 meals.
            Happiness = 20;
            Fullness = 20;
            Energy = 50;
            Meals = 3;
        }
    }
}