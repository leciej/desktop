using KlubSportowy.Helper;
using KlubSportowy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;

namespace KlubSportowy.ViewModels
{
    public class ZawodnicyViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        #region Baza Danych
        protected KlubSportowyEntities klubSportowyEntities;
        private Zawodnicy item;
        #endregion

        #region Konstruktor
        public ZawodnicyViewModel()
        {
            base.DisplayName = "Zawodnicy";
            klubSportowyEntities = new KlubSportowyEntities();
            ResetForm();

            _SortBy = "Nazwisko";
            _SelectedFilterColumn = "Nazwisko";
            _FilterText = "";
            _IsAdding = false;
        }

        private void ResetForm()
        {
            item = new Zawodnicy();
            item.KiedyDodal = DateTime.Now;
            item.CzyAktywny = true;
            item.DataUrodzenia = DateTime.Now.AddYears(-15); // Domyślnie 15 lat wstecz
        }
        #endregion

        #region Walidacja (IDataErrorInfo)
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case nameof(Imie):
                        if (string.IsNullOrWhiteSpace(Imie)) result = "Imię jest wymagane!";
                        break;
                    case nameof(Nazwisko):
                        if (string.IsNullOrWhiteSpace(Nazwisko)) result = "Nazwisko jest wymagane!";
                        break;
                    case nameof(DruzynaId):
                        if (DruzynaId == null || DruzynaId <= 0) result = "Podaj ID drużyny!";
                        break;
                }
                return result;
            }
        }

        public bool IsValid()
        {
            return string.IsNullOrEmpty(this[nameof(Imie)]) &&
                   string.IsNullOrEmpty(this[nameof(Nazwisko)]) &&
                   string.IsNullOrEmpty(this[nameof(DruzynaId)]);
        }
        #endregion

        #region Opcje Wyboru
        public List<string> CzyAktywnyOptions => new List<string> { "Tak", "Nie" };

        public string CzyAktywnyWybor
        {
            get => item.CzyAktywny == true ? "Tak" : "Nie";
            set
            {
                item.CzyAktywny = (value == "Tak");
                OnPropertyChanged(() => CzyAktywnyWybor);
            }
        }
        #endregion

        #region Logika Listy
        private ObservableCollection<Zawodnicy> _List;
        public ObservableCollection<Zawodnicy> List
        {
            get { if (_List == null) Load(); return _List; }
            set { if (_List != value) { _List = value; OnPropertyChanged(() => List); } }
        }

        public void Load()
        {
            klubSportowyEntities = new KlubSportowyEntities();
            var query = klubSportowyEntities.Zawodnicy.AsQueryable();

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (SelectedFilterColumn)
                {
                    case "Nazwisko": query = query.Where(z => z.Nazwisko.Contains(FilterText)); break;
                    case "Imie": query = query.Where(z => z.Imie.Contains(FilterText)); break;
                    case "Pozycja": query = query.Where(z => z.Pozycja.Contains(FilterText)); break;
                }
            }

            switch (SortBy)
            {
                case "Nazwisko": query = query.OrderBy(z => z.Nazwisko); break;
                case "Imie": query = query.OrderBy(z => z.Imie); break;
                case "Pozycja": query = query.OrderBy(z => z.Pozycja); break;
                default: query = query.OrderBy(z => z.Nazwisko); break;
            }

            List = new ObservableCollection<Zawodnicy>(query.ToList());
        }
        #endregion

        #region Komendy
        private bool _IsAdding;
        public bool IsAdding
        {
            get => _IsAdding;
            set { if (_IsAdding != value) { _IsAdding = value; OnPropertyChanged(() => IsAdding); } }
        }

        public ICommand LoadCommand => new BaseCommand(Load);
        public ICommand SaveAndCloseCommand => new BaseCommand(saveAndClose);
        
        public ICommand ShowReportCommand => new BaseCommand(() =>
        {
            Messenger.Default.Send("OpenFinancialReport");
        });

        private void saveAndClose()
        {
            if (IsValid())
            {
                item.KiedyDodal = DateTime.Now;
                item.KtoDodal = "System";

                klubSportowyEntities.Zawodnicy.Add(item);
                klubSportowyEntities.SaveChanges();
                Load();

                ResetForm();
                OdswiezPola();
                IsAdding = false;
            }
            else
            {
                System.Windows.MessageBox.Show("Uzupełnij wymagane pola (Imię, Nazwisko, Drużyna).");
            }
        }

        private void OdswiezPola()
        {
            OnPropertyChanged(() => Imie);
            OnPropertyChanged(() => Nazwisko);
            OnPropertyChanged(() => DataUrodzenia);
            OnPropertyChanged(() => Pozycja);
            OnPropertyChanged(() => DruzynaId);
            OnPropertyChanged(() => CzyAktywnyWybor);
            OnPropertyChanged(() => Uwagi);
        }
        #endregion

        #region Filtrowanie i Sortowanie - Właściwości
        private string _FilterText;
        public string FilterText { get => _FilterText; set { if (_FilterText != value) { _FilterText = value; OnPropertyChanged(() => FilterText); Load(); } } }

        private string _SelectedFilterColumn;
        public string SelectedFilterColumn { get => _SelectedFilterColumn; set { if (_SelectedFilterColumn != value) { _SelectedFilterColumn = value; OnPropertyChanged(() => SelectedFilterColumn); Load(); } } }

        public List<string> FilterColumnOptions => new List<string> { "Nazwisko", "Imie", "Pozycja" };

        private string _SortBy;
        public string SortBy { get => _SortBy; set { if (_SortBy != value) { _SortBy = value; OnPropertyChanged(() => SortBy); Load(); } } }

        public List<string> SortOptions => new List<string> { "Nazwisko", "Imie", "Pozycja" };
        #endregion

        #region Właściwości modelu
        public string Imie { get => item.Imie; set { item.Imie = value; OnPropertyChanged(() => Imie); } }
        public string Nazwisko { get => item.Nazwisko; set { item.Nazwisko = value; OnPropertyChanged(() => Nazwisko); } }
        public DateTime? DataUrodzenia { get => item.DataUrodzenia; set { item.DataUrodzenia = value; OnPropertyChanged(() => DataUrodzenia); } }
        public string Pozycja { get => item.Pozycja; set { item.Pozycja = value; OnPropertyChanged(() => Pozycja); } }
        public int? DruzynaId { get => item.DruzynaId; set { item.DruzynaId = value; OnPropertyChanged(() => DruzynaId); } }
        public string Uwagi { get => item.Uwagi; set { item.Uwagi = value; OnPropertyChanged(() => Uwagi); } }
        #endregion
    }
}