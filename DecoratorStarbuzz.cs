using System;

namespace Starbuzz
{
    public interface IBeverage
    {
        public float GetCost();
        public string GetDescription();
    }
    class FlatWhite : IBeverage
    {
        public float GetCost()
        {
            return 179;
        }

        public string GetDescription()
        {
            return "Flat White";
        }
    }

    class Late : IBeverage
    {
        public float GetCost()
        {
            return 159;
        }

        public string GetDescription()
        {
            return "Late";
        }
    }

    class Americano : IBeverage
    {
        public float GetCost()
        {
            return 129;
        }

        public string GetDescription()
        {
            return "Americano";
        }
    }

    class Capuchino : IBeverage
    {
        public float GetCost()
        {
            return 120;
        }

        public string GetDescription()
        {
            return "Capuchino";
        }
    }

    class CheeseRaf : IBeverage
    {
        public float GetCost()
        {
            return 269;
        }

        public string GetDescription()
        {
            return "Cheese raf";
        }
    }

    class Espresso : IBeverage
    {
        public float GetCost()
        {
            return 129;
        }

        public string GetDescription()
        {
            return "Espresso";
        }
    }

    class MatchaLatte : IBeverage
    {
        public float GetCost()
        {
            return 199;
        }

        public string GetDescription()
        {
            return "Matcha latte";
        }
    }

    public interface ICondimentDecorator : IBeverage { }
    public class MilkDecorator : ICondimentDecorator
    {
        IBeverage thisorder;

        public MilkDecorator(IBeverage thisorder)
        {
            this.thisorder = thisorder;
        }

        public float GetCost()
        {
            return thisorder.GetCost() + 20;
        }

        public string GetDescription()
        {
            return thisorder.GetDescription() + " Milk";
        }
    }

    public class MochaDecorator : ICondimentDecorator
    {
        IBeverage thisorder;

        public float GetCost()
        {
            return thisorder.GetCost() + 30;
        }

        public string GetDescription()
        {
            return thisorder.GetDescription() + " Mocha";
        }

        public MochaDecorator(IBeverage thisorder)
        {
            this.thisorder = thisorder;
        }
    }

    public class SoyDecorator : ICondimentDecorator
    {
        IBeverage thisorder;

        public SoyDecorator(IBeverage thisorder)
        {
            this.thisorder = thisorder;
        }

        public float GetCost()
        {
            return thisorder.GetCost() + 10;
        }

        public string GetDescription()
        {
            return thisorder.GetDescription() + " Soy";
        }
    }

    public class WIPDecorator : ICondimentDecorator
    {
        IBeverage thisorder;

        public WIPDecorator(IBeverage thisorder)
        {
            this.thisorder = thisorder;
        }

        public float GetCost()
        {
            return thisorder.GetCost() + 40;
        }

        public string GetDescription()
        {
            return thisorder.GetDescription() + "  WIP";
        }
    }

    class DecoratorStarbuzz
    {
        static void Main(string[] args)
        {
            IBeverage beverage = new MilkDecorator(new SoyDecorator(new MochaDecorator(new WIPDecorator(new Late()))));
            Console.WriteLine(beverage.GetDescription());
            Console.WriteLine(beverage.GetCost());
        }
    }
}