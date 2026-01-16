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
    public class StatystykiViewModel : WorkspaceViewModel
    {
        #region BazaDanych
        protected KlubSportowyEntities klubSportowyEntities;
        private StatystykiMeczowe item;
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
        private ObservableCollection<StatystykiMeczowe> _List;
        public ObservableCollection<StatystykiMeczowe> List
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
            var query = klubSportowyEntities.StatystykiMeczowe.AsQueryable();

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (SelectedFilterColumn)
                {
                    case "Mecz ID":
                        query = query.Where(z => z.MeczId.ToString().Contains(FilterText));
                        break;
                    case "Zawodnik ID":
                        query = query.Where(z => z.ZawodnikId.ToString().Contains(FilterText));
                        break;
                    case "Uwagi":
                        query = query.Where(z => z.Uwagi.Contains(FilterText));
                        break;
                }
            }

            switch (SortBy)
            {
                case "Gole":
                    query = query.OrderByDescending(z => z.Gole);
                    break;
                case "Asysty":
                    query = query.OrderByDescending(z => z.Asysty);
                    break;
                case "Czas Gry":
                    query = query.OrderByDescending(z => z.CzasGry);
                    break;
                case "Mecz ID":
                    query = query.OrderBy(z => z.MeczId);
                    break;
                default:
                    query = query.OrderBy(z => z.MeczId);
                    break;
            }

            List = new ObservableCollection<StatystykiMeczowe>(query.ToList());
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
                return new List<string> { "Mecz ID", "Zawodnik ID", "Uwagi" };
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
                return new List<string> { "Gole", "Asysty", "Czas Gry", "Mecz ID" };
            }
        }
        #endregion

        #region Konstuktor
        public StatystykiViewModel()
        {
            base.DisplayName = "Statystyki";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new StatystykiMeczowe();
            _SortBy = "Mecz ID";
            _SelectedFilterColumn = "Mecz ID";
            _FilterText = "";
        }
        #endregion

        #region Komendy Logika
        public void Save()
        {
            klubSportowyEntities.StatystykiMeczowe.Add(item);
            klubSportowyEntities.SaveChanges();
            Load();
        }

        private void saveAndClose()
        {
            Save();
        }
        #endregion

        #region Wlasciwosci
        public int? MeczId
        {
            get { return item.MeczId; }
            set
            {
                if (item.MeczId != value)
                {
                    item.MeczId = value;
                    OnPropertyChanged(() => MeczId);
                }
            }
        }
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
        public int? Gole
        {
            get { return item.Gole; }
            set
            {
                if (item.Gole != value)
                {
                    item.Gole = value;
                    OnPropertyChanged(() => Gole);
                }
            }
        }
        public int? Asysty
        {
            get { return item.Asysty; }
            set
            {
                if (item.Asysty != value)
                {
                    item.Asysty = value;
                    OnPropertyChanged(() => Asysty);
                }
            }
        }
        public int? CzasGry
        {
            get { return item.CzasGry; }
            set
            {
                if (item.CzasGry != value)
                {
                    item.CzasGry = value;
                    OnPropertyChanged(() => CzasGry);
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