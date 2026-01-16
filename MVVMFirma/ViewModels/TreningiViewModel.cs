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
    public class TreningiViewModel : WorkspaceViewModel
    {
        #region BazaDanych
        protected KlubSportowyEntities klubSportowyEntities;
        private Treningi item;
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
        private ObservableCollection<Treningi> _List;
        public ObservableCollection<Treningi> List
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
            var query = klubSportowyEntities.Treningi.AsQueryable();

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (SelectedFilterColumn)
                {
                    case "Typ Treningu":
                        query = query.Where(z => z.TypTreningu.Contains(FilterText));
                        break;
                    case "Lokalizacja":
                        query = query.Where(z => z.Lokalizacja.Contains(FilterText));
                        break;
                    case "Uwagi":
                        query = query.Where(z => z.Uwagi.Contains(FilterText));
                        break;
                }
            }

            switch (SortBy)
            {
                case "Data Treningu":
                    query = query.OrderByDescending(z => z.DataTreningu);
                    break;
                case "Lokalizacja":
                    query = query.OrderBy(z => z.Lokalizacja);
                    break;
                case "Typ Treningu":
                    query = query.OrderBy(z => z.TypTreningu);
                    break;
                default:
                    query = query.OrderByDescending(z => z.DataTreningu);
                    break;
            }

            List = new ObservableCollection<Treningi>(query.ToList());
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
                return new List<string> { "Typ Treningu", "Lokalizacja", "Uwagi" };
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
                return new List<string> { "Data Treningu", "Lokalizacja", "Typ Treningu" };
            }
        }
        #endregion

        #region Konstuktor
        public TreningiViewModel()
        {
            base.DisplayName = "Treningi";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new Treningi();
            _SortBy = "Data Treningu";
            _SelectedFilterColumn = "Typ Treningu";
            _FilterText = "";
        }
        #endregion

        #region Wlasciwosci
        public int? DruzynaId
        {
            get { return item.DruzynaId; }
            set
            {
                if (item.DruzynaId != value)
                {
                    item.DruzynaId = value;
                    OnPropertyChanged(() => DruzynaId);
                }
            }
        }
        public DateTime? DataTreningu
        {
            get { return item.DataTreningu; }
            set
            {
                if (item.DataTreningu != value)
                {
                    item.DataTreningu = value;
                    OnPropertyChanged(() => DataTreningu);
                }
            }
        }
        public string Lokalizacja
        {
            get { return item.Lokalizacja; }
            set
            {
                if (item.Lokalizacja != value)
                {
                    item.Lokalizacja = value;
                    OnPropertyChanged(() => Lokalizacja);
                }
            }
        }
        public string TypTreningu
        {
            get { return item.TypTreningu; }
            set
            {
                if (item.TypTreningu != value)
                {
                    item.TypTreningu = value;
                    OnPropertyChanged(() => TypTreningu);
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
            klubSportowyEntities.Treningi.Add(item);
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