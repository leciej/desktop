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
    public class PlatnosciViewModel : WorkspaceViewModel
    {
        #region BazaDanych
        protected KlubSportowyEntities klubSportowyEntities;
        private Platnosci item;
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
        private ObservableCollection<Platnosci> _List;
        public ObservableCollection<Platnosci> List
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
            var query = klubSportowyEntities.Platnosci.AsQueryable();

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (SelectedFilterColumn)
                {
                    case "Miesiąc":
                        query = query.Where(z => z.Miesiac.Contains(FilterText));
                        break;
                    case "Status":
                        query = query.Where(z => z.Status.Contains(FilterText));
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
                case "Kwota":
                    query = query.OrderBy(z => z.Kwota);
                    break;
                case "Miesiąc":
                    query = query.OrderBy(z => z.Miesiac);
                    break;
                case "Status":
                    query = query.OrderBy(z => z.Status);
                    break;
                default:
                    query = query.OrderByDescending(z => z.Miesiac);
                    break;
            }

            List = new ObservableCollection<Platnosci>(query.ToList());
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
                return new List<string> { "Miesiąc", "Status", "Zawodnik ID", "Uwagi" };
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
                return new List<string> { "Kwota", "Miesiąc", "Status" };
            }
        }
        #endregion

        #region Konstuktor
        public PlatnosciViewModel()
        {
            base.DisplayName = "Płatności";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new Platnosci();
            _SortBy = "Miesiąc";
            _SelectedFilterColumn = "Status";
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
        public decimal? Kwota
        {
            get { return item.Kwota; }
            set
            {
                if (item.Kwota != value)
                {
                    item.Kwota = value;
                    OnPropertyChanged(() => Kwota);
                }
            }
        }
        public string Miesiac
        {
            get { return item.Miesiac; }
            set
            {
                if (item.Miesiac != value)
                {
                    item.Miesiac = value;
                    OnPropertyChanged(() => Miesiac);
                }
            }
        }
        public string Status
        {
            get { return item.Status; }
            set
            {
                if (item.Status != value)
                {
                    item.Status = value;
                    OnPropertyChanged(() => Status);
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
            klubSportowyEntities.Platnosci.Add(item);
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