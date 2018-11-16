using Akka.Actor;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ReactiveStock.ActorModel;
using ReactiveStock.ActorModel.Actors.UI;
using ReactiveStock.ActorModel.Messages;
using System.Windows.Input;

namespace ReactiveStock.ViewModel
{
    public class StockToggleButtonViewModel : ViewModelBase
    {
        private string _buttonText;

        public string StockSymbol { get; set; }

        private ICommand _command;
        public ICommand ToggleCommand
        {
            get
            {
                if(_command == null)
                    _command = new RelayCommand(() => StockToggleButtonActoRef.Tell(new FlipToggleMessage()));

                return _command;
            }
        }

        public IActorRef StockToggleButtonActoRef { get; private set; }

        public string ButtonText
        {
            get { return _buttonText; }
            set { Set(() => ButtonText, ref _buttonText, value); }
        }

        public StockToggleButtonViewModel(IActorRef stocksCoordinatorRef, string stockSymbol)
        {
            StockSymbol = stockSymbol;
            StockToggleButtonActoRef = ActorSystemReference.ActorSystem
                .ActorOf(Props.Create(() => new StockToggleButtonActor(stocksCoordinatorRef, this, stockSymbol)));

            //ToggleCommand = new RelayCommand(() => StockToggleButtonActoRef.Tell(new FlipToggleMessage()));

            UpdateButtonTextToOff();
        }

        public void UpdateButtonTextToOff()
        {
            ButtonText = ConstructButtonText(false);
        }

        public void UpdateButtonTextToOn()
        {
            ButtonText = ConstructButtonText(true);
        }

        private string ConstructButtonText(bool isToggledOn)
        {
            return string.Format("{0} {1}", StockSymbol, isToggledOn ? "(on)" : "(off)");
        }
    }
}
