using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    interface IObserver
    {
        void Update(float temperature, float humidity, float pressure);
        void Display();
    }
    
    interface ISubject
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
    }

    class WeatherData : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
 
        public void RegisterObserver(IObserver observer)
        {
            _observers.Add(observer);
        }
 
        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }
        public void NotifyObservers()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update(GetTemperature(), GetHumidity(), GetPressure());
            }
        }
 
        public float GetTemperature()
        {
            return 0;
        }
 
        public float GetHumidity()
        {
            return 0;
        }
 
        public float GetPressure()
        {
            return 0;
        }
 
        public void MeasurementsChanged()
        {
            NotifyObservers();
        }
 
    }
    
    class CurrentConditionsDisplay : IObserver
    {
        private float _temperature;
        private float _humidity;
        private float _pressure;
 
        public CurrentConditionsDisplay(WeatherData weatherData)
        {
            weatherData.RegisterObserver(this);
        }
        public void Update(float temperature, float humidity, float pressure)
        {
            this._temperature = temperature;
            this._humidity = humidity;
            this._pressure = pressure;
            this.Display();
        }
 
        public void Display()
        {
            Console.WriteLine("Temperature: " + _temperature + " ˚C");
            Console.WriteLine("Humidity: " + _humidity + " %");
            Console.WriteLine("Pressure: " + _pressure + " Pa");
        }
    }
 
    class StatisticsDisplay : IObserver
    {
        private float _temperatureSum;
        private float _humiditySum;
        private float _pressureSum;

        private int _total;
        
        public StatisticsDisplay(WeatherData weatherData)
        {
            weatherData.RegisterObserver(this);
            _total = 0; 
            _temperatureSum = 0; 
            _humiditySum = 0;
            _pressureSum = 0;
        }
        public void Update(float temperature, float humidity, float pressure)
        {
            this._temperatureSum += temperature;
            this._humiditySum += humidity;
            this._pressureSum += pressure;
            ++this._total; 
            
            this.Display();
        }
 
        public void Display()
        {
            Console.WriteLine("Mean Temperature: " + _temperatureSum / _total + " ˚C");
            Console.WriteLine("Mean Humidity: " + _humiditySum / _total + " %");
            Console.WriteLine("Mean Pressure: " + _pressureSum / _total + " Pa");
        }
    }
 
    class ForecastDisplay : IObserver
    {
        private float _temperature;
        private float _humidity;
        private float _pressure;
        
        private float _temperatureOld;
        private float _humidityOld;
        private float _pressureOld;
 
        public ForecastDisplay(WeatherData weatherData)
        {
            weatherData.RegisterObserver(this);
            this._temperature = 10;
            this._humidity = 10;
            this._pressure = 10;
        }
 
        public void Update(float temperature, float humidity, float pressure)
        {
            this._temperatureOld = this._temperature;
            this._humidityOld = this._humidity;
            this._pressureOld = this._pressure;
            
            this._temperature = temperature;
            this._humidity = humidity;
            this._pressure = pressure;
            this.Display();
        }
        public void Display()
        {
            Console.WriteLine("Forecast Temperature: " + _temperature + (_temperature - _temperatureOld) + " ˚C");
            Console.WriteLine("Forecast Humidity: " + _humidity + (_humidity - _humidityOld)+ " %");
            Console.WriteLine("Forecast Pressure: " + _pressure + (_pressure - _pressureOld) + " Pa");
        }
    }
 
    class ObserverProgram
    {
        static void Main(string[] args)
        {
            var weatherData = new WeatherData();
 
            var currentConditionsDisplay = new CurrentConditionsDisplay(weatherData);
            var statisticsDisplay = new StatisticsDisplay(weatherData);
            var forecastDisplay = new ForecastDisplay(weatherData);
            
            weatherData.RegisterObserver(currentConditionsDisplay);
            weatherData.RegisterObserver(statisticsDisplay);
            weatherData.RegisterObserver(forecastDisplay);
            
            weatherData.MeasurementsChanged();
        }
    }
}