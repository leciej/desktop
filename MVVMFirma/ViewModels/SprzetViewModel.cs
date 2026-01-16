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
    public class SprzetViewModel : WorkspaceViewModel
    {
        #region BazaDanych
        protected KlubSportowyEntities klubSportowyEntities;
        private Sprzet item;
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
        private ObservableCollection<Sprzet> _List;
        public ObservableCollection<Sprzet> List
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
            var query = klubSportowyEntities.Sprzet.AsQueryable();

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

            switch (SortBy)
            {
                case "Nazwa":
                    query = query.OrderBy(z => z.Nazwa);
                    break;
                case "Kategoria":
                    query = query.OrderBy(z => z.Kategoria);
                    break;
                case "Ilosc":
                    query = query.OrderBy(z => z.Ilosc);
                    break;
                default:
                    query = query.OrderBy(z => z.Nazwa);
                    break;
            }

            List = new ObservableCollection<Sprzet>(query.ToList());
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
                return new List<string> { "Nazwa", "Kategoria", "Ilosc" };
            }
        }
        #endregion

        #region Konstuktor
        public SprzetViewModel()
        {
            base.DisplayName = "Sprzęt";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new Sprzet();
            _SortBy = "Nazwa";
            _SelectedFilterColumn = "Nazwa";
            _FilterText = "";
        }
        #endregion

        #region Komendy Logika
        public void Save()
        {
            klubSportowyEntities.Sprzet.Add(item);
            klubSportowyEntities.SaveChanges();
            Load();
        }

        private void saveAndClose()
        {
            Save();
        }
        #endregion

        #region Wlasciwosci
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

        public int? Ilosc
        {
            get { return item.Ilosc; }
            set
            {
                if (item.Ilosc != value)
                {
                    item.Ilosc = value;
                    OnPropertyChanged(() => Ilosc);
                }
            }
        }

        public int? MagazynId
        {
            get { return item.MagazynId; }
            set
            {
                if (item.MagazynId != value)
                {
                    item.MagazynId = value;
                    OnPropertyChanged(() => MagazynId);
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