using System;

namespace StrategyDucks
{
    #region Quack
    interface IQuackStrategy
    {
        void quack();
    }

    class NormalQuack : IQuackStrategy
    {
        public void quack()
        {
            Console.WriteLine("quack");
        }
    }

    class SquiqQuack : IQuackStrategy
    {
        public void quack()
        {
            Console.WriteLine("squiq");
        }
    }

    class SilentQuack : IQuackStrategy
    {
        public void quack()
        {
        }
    }
    #endregion
 
    #region Swim
    interface ISwimStrategy
    {
        void swim();
    }

    class NormalSwim: ISwimStrategy
    {
        public void swim()
        {
            Console.WriteLine("angry swimming splashes");
        }
    }

    class CuteSwim: ISwimStrategy
    {
        public void swim()
        {
            Console.WriteLine("cute swimming splashes");
        }
    }

    class NoSwim: ISwimStrategy
    {
        public void swim()
        {
        }
    }
    #endregion
    
    #region Display

    interface IDisplay
    {
        void display();
    }

    class NoDisplay : IDisplay
    {
        public void display()
        {
            
        }
    }

    class MallaradDisplay : IDisplay
    {
        public void display()
        {
            Console.WriteLine("Mallard goes meee");
        }
    }

    class RedheadDisplay : IDisplay
    {
        public void display()
        {
            Console.WriteLine("Redhead goes mooo");
        }
    }
    #endregion
    
    #region Fly

    interface IFly
    {
        void fly();
    }

    class NoFly : IFly
    {
        public void fly() { }
    }

    class HighFly : IFly
    {
        public void fly()
        {
            Console.WriteLine("Very high flight");
        }
    }

    class LowFly : IFly
    {
        public void fly()
        {
            Console.WriteLine("Low flight");
        }
    }
    #endregion
    
    class Duck
    {
        private IQuackStrategy quackStrategy;
        private ISwimStrategy swimStrategy;
        private IDisplay displayStrategy;
        private IFly flyStrategy;

        public Duck(IQuackStrategy quackStrategy, ISwimStrategy swimStrategy, IDisplay displayStrategy,
            IFly flyStrategy)
        {
            this.quackStrategy = quackStrategy;
            this.swimStrategy = swimStrategy;
            this.displayStrategy = displayStrategy;
            this.flyStrategy = flyStrategy;
        }

        public void Quack()
        {
            quackStrategy.quack();
        }

        public void Swim()
        {
            swimStrategy.swim();
        }

        public void Display()
        {
            displayStrategy.display();
        }

        public void Fly()
        {
            flyStrategy.fly();
        }
    }

    class Program
    {
        static void TestDuck(Duck duck)
        {
            duck.Quack();
            duck.Swim();
            duck.Display();
            duck.Fly();
        }

        static void Main(string[] args)
        {
            Duck marlladDuck = new Duck(new NormalQuack(), new NormalSwim(), new MallaradDisplay(), new LowFly());
            Duck silentDuck = new Duck(new SilentQuack(), new NormalSwim(), new NoDisplay(), new NoFly());
            Duck rubberDuck = new Duck(new SquiqQuack(), new NormalSwim(), new RedheadDisplay(), new HighFly());
            Program.TestDuck(marlladDuck);
            Program.TestDuck(silentDuck);
            Program.TestDuck(rubberDuck);
        }
    }
}