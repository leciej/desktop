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
    public class RodziceViewModel : WorkspaceViewModel
    {
        #region BazaDanych
        protected KlubSportowyEntities klubSportowyEntities;
        private Rodzice item;
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
        private ObservableCollection<Rodzice> _List;
        public ObservableCollection<Rodzice> List
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
            var query = klubSportowyEntities.Rodzice.AsQueryable();

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
                    case "Telefon":
                        query = query.Where(z => z.Telefon.Contains(FilterText));
                        break;
                    case "Uwagi":
                        query = query.Where(z => z.Uwagi.Contains(FilterText));
                        break;
                    case "Zawodnik ID":
                        query = query.Where(z => z.ZawodnikId.ToString().Contains(FilterText));
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
                case "Telefon":
                    query = query.OrderBy(z => z.Telefon);
                    break;
                default:
                    query = query.OrderBy(z => z.Nazwisko);
                    break;
            }

            List = new ObservableCollection<Rodzice>(query.ToList());
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
                return new List<string> { "Nazwisko", "Imie", "Telefon", "Uwagi", "Zawodnik ID" };
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
                return new List<string> { "Nazwisko", "Imie", "Telefon" };
            }
        }
        #endregion

        #region Konstuktor
        public RodziceViewModel()
        {
            base.DisplayName = "Rodzice";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new Rodzice();
            _SortBy = "Nazwisko";
            _SelectedFilterColumn = "Nazwisko";
            _FilterText = "";
        }
        #endregion

        #region Wlasciwosci
        public int? ZawodnikId
        {
            get { return item.ZawodnikId; }
            set
            {
                if (item.ZawodnikId != value)
                {
                    item.ZawodnikId = value;
                    OnPropertyChanged(() => ZawodnikId);
                }
            }
        }
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

        #region Komendy Logika
        public void Save()
        {
            klubSportowyEntities.Rodzice.Add(item);
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