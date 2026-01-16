using KlubSportowy.Helper;
using KlubSportowy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel; // Wymagane dla IDataErrorInfo
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KlubSportowy.ViewModels
{
    public class DruzynyViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        #region Baza danych
        private Druzyny item;
        private KlubSportowyEntities klubSportowyEntities;
        #endregion

        #region Konstruktor
        public DruzynyViewModel()
        {
            base.DisplayName = "Drużyny";
            item = new Druzyny();
            klubSportowyEntities = new KlubSportowyEntities();
            _SortBy = "Nazwa";
            _SelectedFilterColumn = "Nazwa";
            _FilterText = "";
            _IsAdding = false; // Formularz domyślnie ukryty
        }
        #endregion

        #region Walidacja (IDataErrorInfo) - Wymóg Lab 5 (8 pól)
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case nameof(Nazwa): // 1
                        if (string.IsNullOrWhiteSpace(Nazwa)) result = "Nazwa drużyny jest wymagana!";
                        else if (Nazwa.Length < 3) result = "Nazwa musi mieć co najmniej 3 znaki!";
                        break;

                    case nameof(Kategoria): // 2
                        if (string.IsNullOrWhiteSpace(Kategoria)) result = "Kategoria jest wymagana!";
                        break;

                    case nameof(TrenerId): // 3
                        if (TrenerId == null || TrenerId <= 0) result = "Wybierz poprawnego trenera (ID > 0)!";
                        break;

                    case nameof(KtoDodal): // 4
                        if (string.IsNullOrWhiteSpace(KtoDodal)) result = "Pole 'Kto dodał' nie może być puste!";
                        break;

                    case nameof(KiedyDodal): // 5
                        if (KiedyDodal == null || KiedyDodal > DateTime.Now) result = "Data dodania nie może być z przyszłości!";
                        break;

                    case nameof(CzyAktywny): // 6
                        if (CzyAktywny == null) result = "Określ, czy drużyna jest aktywna!";
                        break;

                    case nameof(Uwagi): // 7
                        if (Uwagi != null && Uwagi.Length > 200) result = "Uwagi nie mogą przekraczać 200 znaków!";
                        break;

                    case nameof(KtoModyfikowal): // 8
                        if (KtoModyfikowal != null && KtoModyfikowal.Length > 50) result = "Nazwa osoby modyfikującej jest za długa!";
                        break;
                }

                return result;
            }
        }

        public bool IsValid()
        {
            // Sprawdzenie kluczowych pól biznesowych przed zapisem
            return string.IsNullOrEmpty(this[nameof(Nazwa)]) &&
                   string.IsNullOrEmpty(this[nameof(Kategoria)]) &&
                   string.IsNullOrEmpty(this[nameof(TrenerId)]);
        }
        #endregion

        #region Lista
        private ObservableCollection<Druzyny> _List;
        public ObservableCollection<Druzyny> List
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

        public void Load()
        {
            // Odświeżanie kontekstu gwarantuje aktualne dane z bazy (Wymóg Lab 5)
            klubSportowyEntities = new KlubSportowyEntities();
            var query = klubSportowyEntities.Druzyny.AsQueryable();

            // Filtrowanie LINQ
            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (SelectedFilterColumn)
                {
                    case "Nazwa":
                        query = query.Where(z => z.Nazwa.Contains(FilterText));
                        break;
                    case "Kategoria":
                        query = query.Where(z => z.Kategoria.Contains(FilterText));
                        break;
                    case "Uwagi":
                        query = query.Where(z => z.Uwagi.Contains(FilterText));
                        break;
                }
            }

            // Sortowanie LINQ (Warunek konieczny Lab 5)
            switch (SortBy)
            {
                case "Nazwa":
                    query = query.OrderBy(z => z.Nazwa);
                    break;
                case "Kategoria":
                    query = query.OrderBy(z => z.Kategoria);
                    break;
                case "Trener ID":
                    query = query.OrderBy(z => z.TrenerId);
                    break;
                default:
                    query = query.OrderBy(z => z.Nazwa);
                    break;
            }

            List = new ObservableCollection<Druzyny>(query.ToList());
        }
        #endregion

        #region Filtrowanie i Sortowanie - Wlasciwosci
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
                    Load();
                }
            }
        }

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
                    Load();
                }
            }
        }

        public List<string> FilterColumnOptions
        {
            get
            {
                return new List<string> { "Nazwa", "Kategoria", "Uwagi" };
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
            get
            {
                return new List<string> { "Nazwa", "Kategoria", "Trener ID" };
            }
        }
        #endregion

        #region Zarzadzanie formularzem (Logika Dodawania)
        private bool _IsAdding;
        public bool IsAdding
        {
            get => _IsAdding;
            set
            {
                if (_IsAdding != value)
                {
                    _IsAdding = value;
                    OnPropertyChanged(() => IsAdding);
                }
            }
        }
        #endregion

        #region Komendy
        private BaseCommand _LoadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null) _LoadCommand = new BaseCommand(Load);
                return _LoadCommand;
            }
        }

        private BaseCommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null) _AddCommand = new BaseCommand(() => IsAdding = true);
                return _AddCommand;
            }
        }

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
            if (IsValid())
            {
                item.KiedyDodal = DateTime.Now;
                klubSportowyEntities.Druzyny.Add(item);
                klubSportowyEntities.SaveChanges();
                Load(); // Odśwież listę po zapisie
            }
        }

        private void saveAndClose()
        {
            Save();
            if (IsValid())
            {
                IsAdding = false; // Ukryj formularz po udanym zapisie
                item = new Druzyny(); // Reset obiektu do stanu początkowego
            }
        }
        #endregion

        #region Wlasciwosci Modelu
        public string Nazwa
        {
            get { return item.Nazwa; }
            set
            {
                if (item.Nazwa != value)
                {
                    item.Nazwa = value;
                    OnPropertyChanged(() => Nazwa);
                }
            }
        }
        public string Kategoria
        {
            get { return item.Kategoria; }
            set
            {
                if (item.Kategoria != value)
                {
                    item.Kategoria = value;
                    OnPropertyChanged(() => Kategoria);
                }
            }
        }
        public int? TrenerId
        {
            get { return item.TrenerId; }
            set
            {
                if (item.TrenerId != value)
                {
                    item.TrenerId = value;
                    OnPropertyChanged(() => TrenerId);
                }
            }
        }
        public bool? CzyAktywny
        {
            get { return item.CzyAktywny; }
            set
            {
                if (item.CzyAktywny != value)
                {
                    item.CzyAktywny = value;
                    OnPropertyChanged(() => CzyAktywny);
                }
            }
        }
        public string KtoDodal
        {
            get { return item.KtoDodal; }
            set
            {
                if (item.KtoDodal != value)
                {
                    item.KtoDodal = value;
                    OnPropertyChanged(() => KtoDodal);
                }
            }
        }
        public DateTime? KiedyDodal
        {
            get { return item.KiedyDodal; }
            set
            {
                if (item.KiedyDodal != value)
                {
                    item.KiedyDodal = value;
                    OnPropertyChanged(() => KiedyDodal);
                }
            }
        }
        public string KtoModyfikowal
        {
            get { return item.KtoModyfikowal; }
            set
            {
                if (item.KtoModyfikowal != value)
                {
                    item.KtoModyfikowal = value;
                    OnPropertyChanged(() => KtoModyfikowal);
                }
            }
        }
        public string KtoWykasowal
        {
            get { return item.KtoWykasowal; }
            set
            {
                if (item.KtoWykasowal != value)
                {
                    item.KtoWykasowal = value;
                    OnPropertyChanged(() => KtoWykasowal);
                }
            }
        }
        public string Uwagi
        {
            get { return item.Uwagi; }
            set
            {
                if (item.Uwagi != value)
                {
                    item.Uwagi = value;
                    OnPropertyChanged(() => Uwagi);
                }
            }
        }
        #endregion
    }
}