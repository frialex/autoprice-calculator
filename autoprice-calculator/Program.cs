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
            ut.CalculateCarValue()

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
            throw new NotImplementedException("Implement here!");
        }


        /// <summary>
        /// AGE:    Given the number of months of how old the car is, reduce its value one-half (0.5) percent.
        ///After 10 years, it's value cannot be reduced further by age. This is not cumulative.
        /// </summary>
        /// <param name="numMonthsOld">Age of the car in months</param>
        /// <returns></returns>
        float CalculatePriceWithAge(int numMonthsOld)
        {

            return 42;
        }

        /// <summary>
        ///  MILES:    For every 1,000 miles on the car, reduce its value by one-fifth of a
        /// percent(0.2). Do not consider remaining miles.After 150,000 miles, it's 
        /// value cannot be reduced further by miles.
        /// </summary>
        /// <param name="miles">The number of miles the car has</param>
        /// <returns></returns>
        float CalculatePriceWithMiles(int miles)
        {
            return 42;
        }

        /// <summary>
        ///  PREVIOUS OWNER:    If the car has had more than 2 previous owners, reduce its value 
        /// by twenty-five(25) percent.If the car has had no previous
        /// owners, add ten(10) percent of the FINAL car value at the end.
        /// </summary>
        /// <param name="ownerNames"></param>
        /// <returns></returns>
        float CalculatePriceWithPreviousOwners(List<string> ownerNames)
        {
            return 42;
        }


        /// <summary>
        /// COLLISION:        For every reported collision the car has been in, remove two (2) 
        /// percent of it's value up to five (5) collisions.             
        /// </summary>
        /// <param name="collisions"></param>
        /// <returns></returns>
        float CalculatePriceWithCollisions(List<Collision> collisions)
        {
            return 42;
        }
    }

    public class UnitTests
    {

        public void CalculateCarValue()
        {
            AssertCarValue(25313.40m, 35000m, 3 * 12, 50000, 1, 1);
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
