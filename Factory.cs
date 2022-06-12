using System;

namespace PizzaIngredientFactory
{
    public interface IPizzaIngredientsFactory
    {
        public string DoughCreate();
        public string SauceCreate();
        public string CheeseCreate();
        public string SausageCreate();
        public string[] VeggiesCreate();
    }

    public class NYPizzaIngridientFactory : IPizzaIngredientsFactory
    {
        public string DoughCreate()
        {
            return "Crispy crust";
        }

        public string SauceCreate()
        {
            return "Sweet and sour sauce";
        }

        public string CheeseCreate()
        {
            return "Dry mozzarella";
        }

        public string SausageCreate()
        {
            return "Pepperoni sausages";
        }

        public string[] VeggiesCreate()
        {
            string[] veggies = { "Сhilli", "Tomatoes", "Garlic", "Oregano", "Basil" };
            return veggies;
        }
    }

    public class ChicagoPizzaIngredientsFactory : IPizzaIngredientsFactory
    {
        public string DoughCreate()
        {
            return "Crispy crust";
        }

        public string SauceCreate()
        {
            return "Tomato sauce";
        }

        public string CheeseCreate()
        {
            return "Skim mozzarella and Parmesan";
        }

        public string SausageCreate()
        {
            return "Pepperoni and Italian sausages";
        }

        public string[] VeggiesCreate()
        {
            string[] veggies = { "Tomatoes" };
            return veggies;
        }
    }

    public abstract class Pizza
    {
        public string Name;
        public string Dough;
        public string Sauce;
        public string Cheese;
        public string Sausage;
        public string[] Veggies;


        public abstract void Prepare();

        public void Bake()
        {
        }

        public void Cut()
        {
        }

        public void Box()
        {
        }

    }

    public class CheesePizza : Pizza
    {
        private IPizzaIngredientsFactory ingredientFactory;

        public CheesePizza(IPizzaIngredientsFactory ingredientFactory)
        {
            this.ingredientFactory = ingredientFactory;
        }

        public override void Prepare()
        {
            this.Dough = ingredientFactory.DoughCreate();
            this.Sauce = ingredientFactory.SauceCreate();
            this.Cheese = ingredientFactory.CheeseCreate();
        }
    }

    public class PepperoniPizza : Pizza
    {
        private IPizzaIngredientsFactory ingredientFactory;

        public PepperoniPizza(IPizzaIngredientsFactory ingredientFactory)
        {
            this.ingredientFactory = ingredientFactory;
        }

        public override void Prepare()
        {
            this.Dough = ingredientFactory.DoughCreate();
            this.Sauce = ingredientFactory.SauceCreate();
            this.Cheese = ingredientFactory.CheeseCreate();
            this.Sausage = ingredientFactory.SausageCreate();
        }
    }

    class GreekPizza : Pizza
    {
        private IPizzaIngredientsFactory ingredientFactory;

        public GreekPizza(IPizzaIngredientsFactory ingredientFactory)
        {
            this.ingredientFactory = ingredientFactory;
        }

        public override void Prepare()
        {
            this.Dough = ingredientFactory.DoughCreate();
            this.Sauce = ingredientFactory.SauceCreate();
            this.Cheese = ingredientFactory.CheeseCreate();
            this.Sausage = ingredientFactory.SausageCreate();
            this.Veggies = ingredientFactory.VeggiesCreate();
        }
    }

    public abstract class PizzaStore {
        public abstract Pizza CreatePizza(String pizzaName);

        Pizza OrderPizza(string pizzaName){
            Pizza pizza = CreatePizza(pizzaName);
            pizza.Prepare();
            pizza.Bake();
            pizza.Box();
            return pizza;
        }
    }

    public class NyPizzaStore : PizzaStore
    {
        public override Pizza CreatePizza(string pizzaName)
        {
            Pizza pizza = null;
            IPizzaIngredientsFactory ingredientFactory = new NYPizzaIngridientFactory();

            if (pizzaName.Equals("cheese"))
            {
                pizza = new CheesePizza(ingredientFactory);
            }
            else if (pizzaName.Equals("greek"))
            {
                pizza = new GreekPizza(ingredientFactory);
            }
            else if (pizzaName.Equals("pepperoni"))
            {
                pizza = new PepperoniPizza(ingredientFactory);
            }

            return pizza;
        }
    }

    public class ChicagoPizzaStore : PizzaStore
    {
        public override Pizza CreatePizza(string pizzaName)
        {
            Pizza pizza = null;
            IPizzaIngredientsFactory ingredientFactory = new ChicagoPizzaIngredientsFactory();

            if (pizzaName.Equals("cheese"))
            {
                pizza = new CheesePizza(ingredientFactory);
            }
            else if (pizzaName.Equals("greek"))
            {
                pizza = new GreekPizza(ingredientFactory);
            }
            else if (pizzaName.Equals("pepperoni"))
            {
                pizza = new PepperoniPizza(ingredientFactory);
            }

            return pizza;
        }
    }


    internal class Factory
    {
        public static void Main(string[] args)
        {
        }
    }
}