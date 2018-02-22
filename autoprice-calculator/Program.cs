using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;


namespace autoprice_calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Checking the unit tests");
            var ut = new UnitTests();
            ut.CalculateCarValue();

            Console.WriteLine("Unit Tests successfull");
            //give time for the user to read the result
            Console.ReadLine();
        }



    }


    public class Car
    {
        public decimal PurchaseValue { get; set; }
        public int AgeInMonths { get; set; }
        public int NumberOfMiles { get; set; }
        public int NumberOfPreviousOwners { get; set; }
        public int NumberOfCollisions { get; set; }
        
    }

    public class PriceDeterminator
    {
        public decimal DetermineCarPrice(Car car)
        {
            /* *    Each factor should be off of the result of the previous value in the order of
             *        1. AGE
             *        2. MILES
             *        3. PREVIOUS OWNER
             *        4. COLLISION
             *        
             *    E.g., Start with the current value of the car, then adjust for age, take that  
             *    result then adjust for miles, then collision, and finally previous owner. 
             *    Note that if previous owner, had a positive effect, then it should be applied 
             *    AFTER step 4. If a negative effect, then BEFORE step 4.
             */

            decimal price = CalculatePriceWithAge(car);
            price = CalculatePriceWithMiles(car.NumberOfMiles, price);
            price = CalculatePriceWithPreviousOwners(car.NumberOfPreviousOwners, price);
            price = CalculatePriceWithCollisions(car.NumberOfCollisions, price);

            return price;

        }


        /// <summary>
        /// AGE:    Given the number of months of how old the car is, reduce its value one-half (0.5) percent.
        ///After 10 years, it's value cannot be reduced further by age. This is not cumulative.
        /// </summary>
        /// <param name="car">Taking the car object here since the algorithm wants it done in order and we need the starting price</param>
        /// <returns>the price with age factored in</returns>
        decimal CalculatePriceWithAge(Car car)
        {
            int age = car.AgeInMonths;
            decimal price = car.PurchaseValue;

            //10 years * 12 months/year = 120 months
            for(int i = 0; i < 120 && i < age; i++)
            {
                if(age == 0)
                {
                    break;
                }
                age--;
                price =  price - (price * 0.05m);
            }

            return price;
        }

        /// <summary>
        ///  MILES:    For every 1,000 miles on the car, reduce its value by one-fifth of a
        /// percent(0.2). Do not consider remaining miles.After 150,000 miles, it's 
        /// value cannot be reduced further by miles.
        /// </summary>
        /// <param name="NumberOfMiles">The number of miles the car has</param>
        /// <param name="currentResaleValue">because the algorithm computes the values in order</param>
        /// <returns>Car price with miles factored in</returns>
        decimal CalculatePriceWithMiles(int NumberOfMiles, decimal currentResalePrice)
        {
            var price = currentResalePrice;

            for(int i = 0; i <= NumberOfMiles / 1000 && i < 15; i++)
            {
                price = price - (price * 0.002m);
            }

            return price;
        }

        /// <summary>
        /// PREVIOUS OWNER: 
        /// If the car has had more than 2 previous owners, reduce its value  by twenty-five(25) percent.
        /// If the car has had no previous owners, add ten(10) percent of the FINAL car value at the end.
        /// </summary>
        /// <param name="numberOfPreviousOwners">Number of previous owners</param>
        /// <param name="currentResaleValue">because the algorithm computes the values in order</param>
        /// <returns>Car price with previous owners factored in</returns>
        decimal CalculatePriceWithPreviousOwners(int numberOfPreviousOwners, decimal currentResalePrice)
        {
            var price = currentResalePrice;
            if (numberOfPreviousOwners == 0)
            {
                price = price + price * 0.10m;
            } else if (numberOfPreviousOwners >= 2)
            {
                price = price - ( price * 0.25m);
            }
            return price;
        }


        /// <summary>
        /// COLLISION:        For every reported collision the car has been in, remove two (2) 
        /// percent of it's value up to five (5) collisions.             
        /// </summary>
        /// <param name="numberOfCollisions">number of collisions</param>
        /// <param name="currentResaleValue">because the algorithm computes the values in order</param>
        /// <returns>The new price with colllisions factored in</returns>
        decimal CalculatePriceWithCollisions(int numberOfCollisions, decimal currentResaleValue)
        {
            var price = currentResaleValue;
            for(int i = 0; i < numberOfCollisions && i < 5; i++)
            {
                price = price - (price * 0.02m);
            }

            return price;
        }
    }

    public class UnitTests
    {

        public void CalculateCarValue()
        {
            //             Final Price    Purchase Value   Age       Miles    Owners      Collisions
            AssertCarValue(25313.40m,     35000m,          3 * 12,   50000,   1,          1);
            AssertCarValue(19688.20m, 35000m, 3 * 12, 150000, 1, 1);
            AssertCarValue(19688.20m, 35000m, 3 * 12, 250000, 1, 1);
            AssertCarValue(20090.00m, 35000m, 3 * 12, 250000, 1, 0);
            AssertCarValue(21657.02m, 35000m, 3 * 12, 250000, 0, 1);
        }

        private static void AssertCarValue( decimal expectValue, decimal purchaseValue,
                                            int ageInMonths, int numberOfMiles,
                                            int numberOfPreviousOwners, int numberOfCollisions)
        {
            Car car = new Car
            {
                AgeInMonths = ageInMonths,
                NumberOfCollisions = numberOfCollisions,
                NumberOfMiles = numberOfMiles,
                NumberOfPreviousOwners = numberOfPreviousOwners,
                PurchaseValue = purchaseValue
            };
            PriceDeterminator priceDeterminator = new PriceDeterminator();
            var carPrice = priceDeterminator.DetermineCarPrice(car);
            Assert.AreEqual(expectValue, carPrice);
        }
    }
}
