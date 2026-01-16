using KlubSportowy.Helper;
using KlubSportowy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KlubSportowy.ViewModels
{
    public class TrenerzyViewModel : WorkspaceViewModel
    {
        #region BazaDanych
        protected KlubSportowyEntities klubSportowyEntities;
        private Trenerzy item;
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

        private BaseCommand _SaveAndCloseCommand;
        public ICommand SaveAndCloseCommand
        {
            get
            {
                if (_SaveAndCloseCommand == null) _SaveAndCloseCommand = new BaseCommand(saveAndClose);
                return _SaveAndCloseCommand;
            }
        }
        #endregion

        #region Lista
        private ObservableCollection<Trenerzy> _List;
        public ObservableCollection<Trenerzy> List
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

        private void Load()
        {
            var query = klubSportowyEntities.Trenerzy.AsQueryable();

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
                    case "Specjalizacja":
                        query = query.Where(z => z.Specjalizacja.Contains(FilterText));
                        break;
                    case "Telefon":
                        query = query.Where(z => z.Telefon.Contains(FilterText));
                        break;
                }
            }

            switch (SortBy)
            {
                case "Nazwisko":
                    query = query.OrderBy(z => z.Nazwisko);
                    break;
                case "Imie":
                    query = query.OrderBy(z => z.Imie);
                    break;
                case "Specjalizacja":
                    query = query.OrderBy(z => z.Specjalizacja);
                    break;
                default:
                    query = query.OrderBy(z => z.Nazwisko);
                    break;
            }

            List = new ObservableCollection<Trenerzy>(query.ToList());
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
                return new List<string> { "Nazwisko", "Imie", "Specjalizacja", "Telefon" };
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
                return new List<string> { "Nazwisko", "Imie", "Specjalizacja" };
            }
        }
        #endregion

        #region Konstuktor
        public TrenerzyViewModel()
        {
            base.DisplayName = "Trenerzy";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new Trenerzy();
            _SortBy = "Nazwisko";
            _SelectedFilterColumn = "Nazwisko";
            _FilterText = "";
        }
        #endregion

        #region Komendy Logika
        public void Save()
        {
            klubSportowyEntities.Trenerzy.Add(item);
            klubSportowyEntities.SaveChanges();
            Load();
        }

        private void saveAndClose()
        {
            Save();
        }
        #endregion

        #region Wlasciwosci
        public string Imie
        {
            get { return item.Imie; }
            set
            {
                if (item.Imie != value)
                {
                    item.Imie = value;
                    OnPropertyChanged(() => Imie);
                }
            }
        }
        public string Nazwisko
        {
            get { return item.Nazwisko; }
            set
            {
                if (item.Nazwisko != value)
                {
                    item.Nazwisko = value;
                    OnPropertyChanged(() => Nazwisko);
                }
            }
        }
        public string Specjalizacja
        {
            get { return item.Specjalizacja; }
            set
            {
                if (item.Specjalizacja != value)
                {
                    item.Specjalizacja = value;
                    OnPropertyChanged(() => Specjalizacja);
                }
            }
        }
        public string Telefon
        {
            get { return item.Telefon; }
            set
            {
                if (item.Telefon != value)
                {
                    item.Telefon = value;
                    OnPropertyChanged(() => Telefon);
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