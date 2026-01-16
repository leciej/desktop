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
    public class UzytkownicyViewModel : WorkspaceViewModel
    {
        #region BazaDanych
        protected KlubSportowyEntities klubSportowyEntities;
        private Uzytkownicy item;
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
        private ObservableCollection<Uzytkownicy> _List;
        public ObservableCollection<Uzytkownicy> List
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
            var query = klubSportowyEntities.Uzytkownicy.AsQueryable();

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (SelectedFilterColumn)
                {
                    case "Login":
                        query = query.Where(z => z.Login.Contains(FilterText));
                        break;
                    case "Rola":
                        query = query.Where(z => z.Rola.Contains(FilterText));
                        break;
                    case "Uwagi":
                        query = query.Where(z => z.Uwagi.Contains(FilterText));
                        break;
                }
            }

            switch (SortBy)
            {
                case "Login":
                    query = query.OrderBy(z => z.Login);
                    break;
                case "Rola":
                    query = query.OrderBy(z => z.Rola);
                    break;
                default:
                    query = query.OrderBy(z => z.Login);
                    break;
            }

            List = new ObservableCollection<Uzytkownicy>(query.ToList());
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
                return new List<string> { "Login", "Rola", "Uwagi" };
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
                return new List<string> { "Login", "Rola" };
            }
        }
        #endregion

        #region Konstuktor
        public UzytkownicyViewModel()
        {
            base.DisplayName = "Użytkownicy";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new Uzytkownicy();
            _SortBy = "Login";
            _SelectedFilterColumn = "Login";
            _FilterText = "";
        }
        #endregion

        #region Wlasciwosci
        public string Login
        {
            get
            {
                return item.Login;
            }
            set
            {
                if (item.Login != value)
                {
                    item.Login = value;
                    OnPropertyChanged(() => Login);
                }
            }
        }
        public string Haslo
        {
            get
            {
                return item.Haslo;
            }
            set
            {
                if (item.Haslo != value)
                {
                    item.Haslo = value;
                    OnPropertyChanged(() => Haslo);
                }
            }
        }
        public string Rola
        {
            get
            {
                return item.Rola;
            }
            set
            {
                if (item.Rola != value)
                {
                    item.Rola = value;
                    OnPropertyChanged(() => Rola);
                }
            }
        }
        public bool? CzyAktywny
        {
            get
            {
                return item.CzyAktywny;
            }
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
            get
            {
                return item.KtoDodal;
            }
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
            get
            {
                return item.KiedyDodal;
            }
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
            get
            {
                return item.KtoModyfikowal;
            }
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
            get
            {
                return item.KtoWykasowal;
            }
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
            get
            {
                return item.Uwagi;
            }
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

        #region Komendy Logika
        public void Save()
        {
            klubSportowyEntities.Uzytkownicy.Add(item);
            klubSportowyEntities.SaveChanges();
            Load();
        }
        private void saveAndClose()
        {
            Save();
        }
        #endregion
    }
}