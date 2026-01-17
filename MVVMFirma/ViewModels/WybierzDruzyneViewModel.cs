using KlubSportowy.Helper;
using KlubSportowy.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;

namespace KlubSportowy.ViewModels
{
    public class WybierzDruzyneViewModel : WorkspaceViewModel
    {
        #region Baza Danych
        private KlubSportowyEntities db;
        #endregion

        #region Właściwości
        private ObservableCollection<Druzyny> _List;
        public ObservableCollection<Druzyny> List
        {
            get { if (_List == null) Load(); return _List; }
            set { if (_List != value) { _List = value; OnPropertyChanged(() => List); } }
        }

        private Druzyny _SelectedDruzyna;
        public Druzyny SelectedDruzyna
        {
            get => _SelectedDruzyna;
            set { _SelectedDruzyna = value; OnPropertyChanged(() => SelectedDruzyna); }
        }
        #endregion

        #region Konstruktor
        public WybierzDruzyneViewModel()
        {
            base.DisplayName = "Wybierz Drużynę";
            db = new KlubSportowyEntities();
        }
        #endregion

        #region Komendy
        public ICommand WybierzCommand => new BaseCommand(Wybierz);

        private void Wybierz()
        {
            if (SelectedDruzyna != null)
            {
                // Wysyłamy wybrany obiekt drużyny przez Messenger
                Messenger.Default.Send(SelectedDruzyna);
                // Zamykamy okno
                OnRequestClose();
            }
        }

        private void Load()
        {
            // Ładujemy tylko aktywne drużyny
            var query = db.Druzyny.Where(d => d.CzyAktywny == true);
            List = new ObservableCollection<Druzyny>(query.ToList());
        }
        #endregion
    }
}