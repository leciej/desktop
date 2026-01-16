using KlubSportowy.Helper;
using KlubSportowy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq; // Wymagane dla LINQ
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KlubSportowy.ViewModels
{
    public class ZawodnicyViewModel : WorkspaceViewModel
    {
        #region BazaDanych
        protected KlubSportowyEntities klubSportowyEntities;
        private Zawodnicy item;
        #endregion

        #region Command
        private BaseCommand _LoadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null) _LoadCommand = new BaseCommand(Load);
                return _LoadCommand;
            }
        }
        #endregion

        #region Lista
        private ObservableCollection<Zawodnicy> _List;
        public ObservableCollection<Zawodnicy> List
        {
            get
            {
                if (_List == null) Load();
                return _List;
            }
            set
            {
                if (_List != value)
                {
                    _List = value;
                    OnPropertyChanged(() => List);
                }
            }
        }

        // METODA LOAD - Laboratorium 5 (Dynamiczne Filtrowanie i Sortowanie LINQ)
        private void Load()
        {
            var query = klubSportowyEntities.Zawodnicy.AsQueryable();

            // 1. DYNAMICZNE FILTROWANIE (LINQ - wybór kolumny i frazy)
            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (SelectedFilterColumn)
                {
                    case "Nazwisko":
                        query = query.Where(z => z.Nazwisko.Contains(FilterText));
                        break;
                    case "Imie":
                        query = query.Where(z => z.Imie.Contains(FilterText));
                        break;
                    case "Pozycja":
                        query = query.Where(z => z.Pozycja.Contains(FilterText));
                        break;
                    case "Uwagi":
                        query = query.Where(z => z.Uwagi.Contains(FilterText));
                        break;
                }
            }

            // 2. SORTOWANIE (LINQ)
            switch (SortBy)
            {
                case "Nazwisko":
                    query = query.OrderBy(z => z.Nazwisko);
                    break;
                case "Imie":
                    query = query.OrderBy(z => z.Imie);
                    break;
                case "Data Urodzenia":
                    query = query.OrderBy(z => z.DataUrodzenia);
                    break;
                case "Pozycja":
                    query = query.OrderBy(z => z.Pozycja);
                    break;
                default:
                    query = query.OrderBy(z => z.Nazwisko);
                    break;
            }

            List = new ObservableCollection<Zawodnicy>(query.ToList());
        }
        #endregion

        #region Filtrowanie i Sortowanie - Wlasciwosci

        // Tekst wpisywany w wyszukiwarke
        private string _FilterText;
        public string FilterText
        {
            get => _FilterText;
            set
            {
                if (_FilterText != value)
                {
                    _FilterText = value;
                    OnPropertyChanged(() => FilterText);
                    Load(); // Odswiezanie na zywo przy pisaniu
                }
            }
        }

        // Wybrana kolumna z listy rozwijalnej "Szukaj w:"
        private string _SelectedFilterColumn;
        public string SelectedFilterColumn
        {
            get => _SelectedFilterColumn;
            set
            {
                if (_SelectedFilterColumn != value)
                {
                    _SelectedFilterColumn = value;
                    OnPropertyChanged(() => SelectedFilterColumn);
                    Load(); // Odswiezanie po zmianie kolumny
                }
            }
        }

        // Lista dostepnych kolumn do filtrowania
        public List<string> FilterColumnOptions
        {
            get
            {
                return new List<string> { "Nazwisko", "Imie", "Pozycja", "Uwagi" };
            }
        }

        private string _SortBy;
        public string SortBy
        {
            get => _SortBy;
            set
            {
                if (_SortBy != value)
                {
                    _SortBy = value;
                    OnPropertyChanged(() => SortBy);
                    Load();
                }
            }
        }

        public List<string> SortOptions
        {
            get { return new List<string> { "Nazwisko", "Imie", "Data Urodzenia", "Pozycja" }; }
        }
        #endregion

        #region Konstruktor
        public ZawodnicyViewModel()
        {
            base.DisplayName = "Zawodnicy";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new Zawodnicy();

            // Inicjalizacja domyslnych wartosci
            _SortBy = "Nazwisko";
            _SelectedFilterColumn = "Nazwisko";
            _FilterText = "";
        }
        #endregion

        #region Komendy
        private BaseCommand _SaveAndCloseCommand;
        public ICommand SaveAndCloseCommand
        {
            get
            {
                if (_SaveAndCloseCommand == null) _SaveAndCloseCommand = new BaseCommand(saveAndClose);
                return _SaveAndCloseCommand;
            }
        }
        public void Save()
        {
            klubSportowyEntities.Zawodnicy.Add(item);
            klubSportowyEntities.SaveChanges();
            Load();
        }
        private void saveAndClose() { Save(); }
        #endregion

        #region Wlasciwosci (Pelna lista)
        public string Imie
        {
            get { return item.Imie; }
            set { if (item.Imie != value) { item.Imie = value; OnPropertyChanged(() => Imie); } }
        }
        public string Nazwisko
        {
            get { return item.Nazwisko; }
            set { if (item.Nazwisko != value) { item.Nazwisko = value; OnPropertyChanged(() => Nazwisko); } }
        }
        public DateTime? DataUrodzenia
        {
            get { return item.DataUrodzenia; }
            set { if (item.DataUrodzenia != value) { item.DataUrodzenia = value; OnPropertyChanged(() => DataUrodzenia); } }
        }
        public string Pozycja
        {
            get { return item.Pozycja; }
            set { if (item.Pozycja != value) { item.Pozycja = value; OnPropertyChanged(() => Pozycja); } }
        }
        public int? DruzynaId
        {
            get { return item.DruzynaId; }
            set { if (item.DruzynaId != value) { item.DruzynaId = value; OnPropertyChanged(() => DruzynaId); } }
        }
        public bool? CzyAktywny
        {
            get { return item.CzyAktywny; }
            set { if (item.CzyAktywny != value) { item.CzyAktywny = value; OnPropertyChanged(() => CzyAktywny); } }
        }
        public string KtoDodal
        {
            get { return item.KtoDodal; }
            set { if (item.KtoDodal != value) { item.KtoDodal = value; OnPropertyChanged(() => KtoDodal); } }
        }
        public DateTime? KiedyDodal
        {
            get { return item.KiedyDodal; }
            set { if (item.KiedyDodal != value) { item.KiedyDodal = value; OnPropertyChanged(() => KiedyDodal); } }
        }
        public string KtoModyfikowal
        {
            get { return item.KtoModyfikowal; }
            set { if (item.KtoModyfikowal != value) { item.KtoModyfikowal = value; OnPropertyChanged(() => KtoModyfikowal); } }
        }
        public string KtoWykasowal
        {
            get { return item.KtoWykasowal; }
            set { if (item.KtoWykasowal != value) { item.KtoWykasowal = value; OnPropertyChanged(() => KtoWykasowal); } }
        }
        public string Uwagi
        {
            get { return item.Uwagi; }
            set { if (item.Uwagi != value) { item.Uwagi = value; OnPropertyChanged(() => Uwagi); } }
        }
        #endregion
    }
}