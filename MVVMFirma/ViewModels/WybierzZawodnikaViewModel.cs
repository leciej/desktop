using KlubSportowy.Helper;
using KlubSportowy.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;

namespace KlubSportowy.ViewModels
{
    public class WybierzZawodnikaViewModel : WorkspaceViewModel
    {
        private KlubSportowyEntities db;

        // Lista wszystkich zawodników
        private ObservableCollection<Zawodnicy> _List;
        public ObservableCollection<Zawodnicy> List
        {
            get { if (_List == null) Load(); return _List; }
            set { _List = value; OnPropertyChanged(() => List); }
        }

        // Właściwość trzymająca zaznaczonego w tabeli zawodnika
        private Zawodnicy _SelectedZawodnik;
        public Zawodnicy SelectedZawodnik
        {
            get => _SelectedZawodnik;
            set
            {
                _SelectedZawodnik = value;
                OnPropertyChanged(() => SelectedZawodnik);
            }
        }

        public WybierzZawodnikaViewModel()
        {
            base.DisplayName = "Wybierz Zawodnika";
            db = new KlubSportowyEntities();
        }

        // Komenda przycisku "Wybierz"
        public ICommand WybierzCommand => new BaseCommand(() =>
        {
            if (SelectedZawodnik != null)
            {
                // Wysyłamy obiekt zawodnika do PlatnosciViewModel
                Messenger.Default.Send(SelectedZawodnik);
                // Zamykamy okno
                OnRequestClose();
            }
        });

        private void Load()
        {
            // Ładujemy tylko aktywnych zawodników
            List = new ObservableCollection<Zawodnicy>(db.Zawodnicy.Where(z => z.CzyAktywny == true).ToList());
        }
    }
}