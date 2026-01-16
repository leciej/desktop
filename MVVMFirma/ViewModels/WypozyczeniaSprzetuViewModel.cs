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
    public class WypozyczeniaSprzetuViewModel : WorkspaceViewModel
    {
        #region BazaDanych
        protected KlubSportowyEntities klubSportowyEntities;
        private WypozyczeniaSprzetu item;
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
        private ObservableCollection<WypozyczeniaSprzetu> _List;
        public ObservableCollection<WypozyczeniaSprzetu> List
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
            var query = klubSportowyEntities.WypozyczeniaSprzetu.AsQueryable();

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (SelectedFilterColumn)
                {
                    case "Sprzęt ID":
                        query = query.Where(z => z.SprzetId.ToString().Contains(FilterText));
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
                case "Data Wydania":
                    query = query.OrderByDescending(z => z.DataWydania);
                    break;
                case "Data Zwrotu":
                    query = query.OrderByDescending(z => z.DataZwrotu);
                    break;
                case "Sprzęt ID":
                    query = query.OrderBy(z => z.SprzetId);
                    break;
                default:
                    query = query.OrderByDescending(z => z.DataWydania);
                    break;
            }

            List = new ObservableCollection<WypozyczeniaSprzetu>(query.ToList());
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
                return new List<string> { "Sprzęt ID", "Zawodnik ID", "Uwagi" };
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
                return new List<string> { "Data Wydania", "Data Zwrotu", "Sprzęt ID" };
            }
        }
        #endregion

        #region Konstuktor
        public WypozyczeniaSprzetuViewModel()
        {
            base.DisplayName = "Wypożyczenia Sprzętu";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new WypozyczeniaSprzetu();
            _SortBy = "Data Wydania";
            _SelectedFilterColumn = "Sprzęt ID";
            _FilterText = "";
        }
        #endregion

        #region Wlasciwosci
        public int? SprzetId
        {
            get { return item.SprzetId; }
            set
            {
                if (item.SprzetId != value)
                {
                    item.SprzetId = value;
                    OnPropertyChanged(() => SprzetId);
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
        public DateTime? DataWydania
        {
            get { return item.DataWydania; }
            set
            {
                if (item.DataWydania != value)
                {
                    item.DataWydania = value;
                    OnPropertyChanged(() => DataWydania);
                }
            }
        }
        public DateTime? DataZwrotu
        {
            get { return item.DataZwrotu; }
            set
            {
                if (item.DataZwrotu != value)
                {
                    item.DataZwrotu = value;
                    OnPropertyChanged(() => DataZwrotu);
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
            klubSportowyEntities.WypozyczeniaSprzetu.Add(item);
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