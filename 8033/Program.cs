using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8033
{
    // Base class for all animals
    public class Animal
    {
        protected int food; // Daily food in kilos

        public Animal()
        {
            this.food = 1; // Generic animal eats 1 kilo
        }

        // Method to check if the animal is hungry
        public virtual bool IsHungry()
        {
            return false; // Default: not hungry
        }

        public int GetFood()
        {
            return food;
        }
    }

    // Lion class
    public class Lion : Animal
    {
        public Lion()
        {
            this.food = 2 * base.food; // Lions eat twice as much as a generic animal
        }

        // Override IsHungry
        public override bool IsHungry()
        {
            Random rnd = new Random();
            return rnd.Next(0, 2) == 0; // Lions have 50% chance to be hungry
        }
    }

    // Duck class
    public class Duck : Animal
    {
        public Duck()
        {
            this.food = base.food; // Ducks eat the same as a generic animal
        }

        // Override IsHungry
        public override bool IsHungry()
        {
            return false; // Ducks are not hungry
        }
    }

    // Panda class
    public class Panda : Animal
    {
        public Panda()
        {
            this.food = base.food + 1; // Pandas eat 1 kilo more than a generic animal
        }

        // Override IsHungry
        public override bool IsHungry()
        {
            return false; // Pandas are not hungry
        }
    }

    // Cage class
    public class Cage
    {
        private Animal[] animals; // Non-null array of animals

        public Cage(Animal[] animals)
        {
            this.animals = animals;
        }

        // Calculate the total vegetarian food needed in the cage
        public int TotalVegetarianFood()
        {
            int total = 0;
            foreach (Animal animal in animals)
            {
                // Account for all non-Lions
                if (!(animal is Lion))
                {
                    total += animal.GetFood();
                }
            }
            return total;
        }

        // Check if the cage has a mix of Lions and non-Lions
        public bool IsMix()
        {
            bool hasLion = false;
            bool hasNonLion = false;

            foreach (Animal animal in animals)
            {
                if (animal is Lion)
                    hasLion = true;
                else
                    hasNonLion = true;

                if (hasLion && hasNonLion)
                    return true; // Mixed cage detected
            }

            return false; // No mix of Lions and non-Lions
        }

        // Getter for animals
        public Animal[] GetAnimals()
        {
            return animals;
        }
    }

    // Zoo class
    public class Zoo
    {
        private Cage[] cages; // Array of cages

        public Zoo(Cage[] cages)
        {
            this.cages = cages;
        }

        // Return an array of all hungry lions in the zoo
        public Lion[] GetHungryLions()
        {
            Lion[] hungryLions = new Lion[100]; // Fixed size array as per question
            int index = 0;

            foreach (Cage cage in cages)
            {
                foreach (Animal animal in cage.GetAnimals())
                {
                    // Use downcasting to check and cast to Lion
                    if (animal is Lion)
                    {
                        Lion lion = (Lion)animal; // Explicit cast
                        if (lion.IsHungry())
                        {
                            hungryLions[index++] = lion;
                        }
                    }
                }
            }

            return hungryLions; // Return the fixed array (no trimming)
        }
    }

    // Example usage
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create animals
            Animal[] cage1Animals = { new Lion(), new Lion() }; // All Lions
            Animal[] cage2Animals = { new Duck(), new Panda() };  // Ducks and Pandas
            Animal[] cage3Animals = { new Lion(), new Duck() };  // Mixed cage (Invalid)

            // Create cages
            Cage cage1 = new Cage(cage1Animals);
            Cage cage2 = new Cage(cage2Animals);
            Cage cage3 = new Cage(cage3Animals);

            // Create zoo
            Zoo zoo = new Zoo(new Cage[] { cage1, cage2, cage3 });

            // Test TotalVegetarianFood for a cage
            Console.WriteLine($"Total Vegetarian Food for Cage 2: {cage2.TotalVegetarianFood()} kg");

            // Test IsMix for Cage 3
            Console.WriteLine($"Is Cage 3 mixed? {cage3.IsMix()}");

            // Test GetHungryLions
            Lion[] hungryLions = zoo.GetHungryLions();
            int count = 0;
            foreach (Animal animal in hungryLions)
            {
                if (animal == null) break;
                count++;
            }

            Console.WriteLine($"Number of hungry lions in the zoo: {count}");
        }
    }


}
