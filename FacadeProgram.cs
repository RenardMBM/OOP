using System;

namespace Facade
{
    public class HomeTheatreFacade
    {
        private Amplifier _amplifier;
        private CdPlayer _cdPlayer;
        private DvdPlayer _dvdPlayer;
        private PopcornPopper _popcornPopper;
        private Tuner _tuner;
        
        //region HomeTheaterFacade constructions
        public HomeTheatreFacade()
        {
            _amplifier = new Amplifier();
            _cdPlayer = new CdPlayer(_amplifier);
            _dvdPlayer = new DvdPlayer(_amplifier);
            _popcornPopper = new PopcornPopper(_amplifier);
            _tuner = new Tuner(_amplifier);
        }

        public HomeTheatreFacade(Amplifier amplifier)
        {
            _amplifier = amplifier;
            _cdPlayer = new CdPlayer(_amplifier);
            _dvdPlayer = new DvdPlayer(_amplifier);
            _popcornPopper = new PopcornPopper(_amplifier);
            _tuner = new Tuner(_amplifier);
        }
        //endregion
        //region HomeTheaterFacade methods
        public void WatchMovie(string movie)
        {
            _amplifier.On();
            _dvdPlayer.On();
            _popcornPopper.On();
            _amplifier.UpVolume(30);
            _dvdPlayer.SetMovie(movie);
            _popcornPopper.Pop();
            _dvdPlayer.Play();
        }
        public void EndMovie()
        {
            _amplifier.SetVolume(0);
            _dvdPlayer.Off();
            _dvdPlayer.EjectMovie();
            _amplifier.Off();
            _dvdPlayer.Off();
            _popcornPopper.Off();
        }

        public void ListenToCd()
        {
            _amplifier.On();
            _cdPlayer.On();
            _cdPlayer.Play();
            _amplifier.SetVolume(20);
        }

        public void EndCd()
        {
            _cdPlayer.Pause();
            _cdPlayer.Off();
            _amplifier.SetVolume(0);
            _amplifier.Off();
        }

        public void ListenToRadio()
        {
            _amplifier.On();
            _tuner.On();
            _amplifier.SetVolume(1);
            _tuner.SetFm(107.4f);
        }

        public void EndRadio()
        {
            _amplifier.Off();
            _tuner.Off();
        }
        //endregion
    }

    internal class Tuner
    {
        private Amplifier _amplifier;
        private float _am, _fm, _frequency;
        
        public Tuner(Amplifier amplifier)
        {
            _amplifier = amplifier;
        }

        public void On()
        {
            Console.WriteLine("Tuner turns ON");
        }

        public void Off()
        {
            Console.WriteLine("Tuner turns OFF");
        }

        public void SetAm(float am)
        {
            _am = am;
            Console.WriteLine("Tuner set am: " + _am);
        }

        public void SetFm(float fm)
        {
            _fm = fm;
            Console.WriteLine("Tuner set fm: " + _fm);
        }

        public void SetFrequency(float frequency)
        {
            _frequency = frequency;
            Console.WriteLine("Tuner set frequency: " + _frequency + ".");
        }
    }

    internal class PopcornPopper
    {
        private Amplifier _amplifier;

        public PopcornPopper(Amplifier amplifier)
        {
            _amplifier = amplifier;
        }

        public void On()
        {
            Console.WriteLine("Popcorn turns ON");
        }

        public void Off()
        {
            Console.WriteLine("Popcorn turns OFF");
        }

        public void Pop()
        {
            Console.WriteLine("Popcorn popper pop popcorn");
        }
    }

    internal class DvdPlayer
    {
        private Amplifier _amplifier;
        private string _movie;
        private bool _isOn;

        private bool _checkIsOn()
        {
            if (!_isOn)
            {
                Console.WriteLine("DvdPlayer is OFF");
                return false;
            }
            return true;
        }
        private bool _checkIsEmpty()
        {
            if (_movie == null)
            {
                Console.WriteLine("DvdPlayer is empty");
                return false;
            }
            return true;
        }
        private bool _isCorrectVideoAction()
        {
            return _checkIsOn() && _checkIsEmpty();
        }

        public DvdPlayer(Amplifier amp)
        {
            _amplifier = amp;
            _movie = null;
            _isOn = false;
        }

        public void On()
        {
            if (_isOn) return;
            Console.WriteLine("Dvd turns ON");
        }

        public void Off()
        {
            if (_isOn) Console.WriteLine("Dvd turns OFF");
        }
        
        public void EjectMovie()
        {
            if (!_checkIsOn() || !_checkIsEmpty()) return;
            Console.WriteLine("DvdPlayer: ejected \"" + _movie + "\"");
            _movie = null;
        }
        public void SetMovie(string movieName)
        {
            if (!_checkIsOn()) return;
            if (_movie != null)
            {
                EjectMovie();
                _movie = movieName;
                Console.WriteLine("DvdPlayer: set \"" + _movie + "\"");
            }
        }
        public void Play()
        {
            if (!_isCorrectVideoAction()) return;
            Console.WriteLine("DvdPlayer is playing \"" + _movie + "\"");
        }
        public void Pause()
        {
            if (!_isCorrectVideoAction()) return;
            Console.WriteLine("DvdPlayer paused \"" + _movie + "\"");
        }
    }

    internal class CdPlayer
    {
        private Amplifier _amplifier;

        public CdPlayer(Amplifier amplifier)
        {
            _amplifier = amplifier;
        }

        public void On()
        {
            Console.WriteLine("Amplifier turns ON");
        }
        public void Off()
        {
            Console.WriteLine("Amplifier turns OFF");
        }

        public void Play()
        {
            Console.WriteLine("CdPlayer is playing a music");
        }

        public void Pause()
        {
            Console.WriteLine("CdPlayer stop music");
        }
    }

    public class Amplifier
    {
        private uint _volume = 0;

        public void On()
        {
            Console.WriteLine("Amplifier turns ON");
        }

        public void Off()
        {
            Console.WriteLine("Amplifier turns OFF");
        }

        public void SetVolume(uint v)
        {
            _volume = v;
            Console.WriteLine("Amplifier set volume: " + _volume);
        }
        public void UpVolume(uint v)
        {
            _volume += v;
            Console.WriteLine("Amplifier set volume: " + _volume);
        }
        public void DownVolume(uint v)
        {
            if (v >= _volume) _volume = 0;
            else _volume -= v;
            Console.WriteLine("Amplifier set volume: " + _volume);
        }
    }

    public class Screen
    {
        public void Up(){}
        public void Down(){}
    }

    static class FacadeProgram
    {
        public static void Main(String[] args)
        {
            HomeTheatreFacade cinema = new HomeTheatreFacade();
            cinema.WatchMovie("The best FEFU");
            cinema.EndMovie();
            cinema.ListenToCd();
            cinema.EndCd();
            cinema.ListenToRadio();
            cinema.EndRadio();
        }
    }
}

