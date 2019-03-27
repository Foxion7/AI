using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.Fuzzylogic.FuzzyTerms;


namespace SteeringCS.Fuzzylogic
{
    //this class might be better of as a serie of unit tests.
    //that way we could actually check if it does what we want.
    //I'm not going to write those tho.
    class FuzzyLogicExample
    {
        public void mainExample()
        {
            FuzzyModule Module = new FuzzyModule();

            //creating the variable that maintains hunger
            FuzzyVariable hungerVar = Module.CreateFLV("Hunger");

            //creating the set that translates to being Full
            //a hunger score from 0 to 10 returns 1, then at 11 it returns 0.9, at 15 it returns 0.5 and at 20 it returns 0
            FzSet FullSet = hungerVar.AddLeftShoulderSet("Full", 0, 10, 20);

            //creating the set that translates to being hungry
            //a hunger score at 10 returns 0, at 20 it returns 1, at 30 it retusn 0 again. 
            FzSet HungrySet = hungerVar.AddTriangularSet("Hungry", 10, 20, 30);

            //creating the set that translates to being starving
            //a hunger score at 20 it retusn zero, at 30 and higher it returns 1. 
            FzSet StarvingSet = hungerVar.AddTriangularSet("Starving", 20, 30, 100);

            //creating the GrabFoodDesirebility Variable
            FuzzyVariable grabFoodVar = Module.CreateFLV("GrabFood");

            //creating the set that translates to being Full
            //a grabfood score from 0 to 10 returns 1, then at 11 it returns 0.9, at 15 it returns 0.5 and at 20 it returns 0
            FzSet no = grabFoodVar.AddLeftShoulderSet("no", 0, 10, 20);

            //creating the set that translates to being hungry
            //a grabfood score at 10 returns 0, at 20 it returns 1, at 30 it retusn 0 again. 
            FzSet maybe = grabFoodVar.AddTriangularSet("maybe", 10, 20, 30);

            //creating the set that translates to being starving
            //a grabfood score at 20 it retusn zero, at 30 and higher it returns 1. 
            FzSet very = grabFoodVar.AddTriangularSet("very", 20, 30, 100);

            
            //creating the rules
            //if I'm full I won't get food, If I'm fairly full, I might get food
            Module.addRule(FullSet, no);
            Module.addRule(new FzFairly(FullSet), maybe);

            //If I'm Hungry I might get food, If I'm very Hungry I will get food
            Module.addRule(HungrySet, maybe);
            Module.addRule(new FzVery(HungrySet), very);

            //if I'm starving I will get food
            Module.addRule(StarvingSet, very);

            //so you Fuzzify all the value in the input set.
            //In this case only the Hunger set matters but if there where more sets you'd call this function multiple times
            Module.Fuzzify("Hunger", 35);

            //and Defuzzify the output set
            double DoIWantToGrabFood = Module.DeFuzzify("GrabFood");
            Console.WriteLine(DoIWantToGrabFood);
        }
    }
}
