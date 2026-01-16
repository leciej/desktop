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
    public class HarmonogramObiektowViewModel : WorkspaceViewModel
    {
        #region BazaDanych
        protected KlubSportowyEntities klubSportowyEntities;
        private HarmonogramObiektow item;
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
        private ObservableCollection<HarmonogramObiektow> _List;
        public ObservableCollection<HarmonogramObiektow> List
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
            var query = klubSportowyEntities.HarmonogramObiektow.AsQueryable();

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (SelectedFilterColumn)
                {
                    case "Obiekt":
                        query = query.Where(z => z.Obiekt.Contains(FilterText));
                        break;
                    case "Druzyna ID":
                        query = query.Where(z => z.DruzynaId.ToString().Contains(FilterText));
                        break;
                }
            }

            switch (SortBy)
            {
                case "Obiekt":
                    query = query.OrderBy(z => z.Obiekt);
                    break;
                case "Data":
                    query = query.OrderBy(z => z.Data);
                    break;
                case "Godzina Od":
                    query = query.OrderBy(z => z.GodzinaOd);
                    break;
                default:
                    query = query.OrderBy(z => z.Data);
                    break;
            }

            List = new ObservableCollection<HarmonogramObiektow>(query.ToList());
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
                return new List<string> { "Obiekt", "Druzyna ID" };
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
                return new List<string> { "Obiekt", "Data", "Godzina Od" };
            }
        }
        #endregion

        #region Konstuktor
        public HarmonogramObiektowViewModel()
        {
            base.DisplayName = "Harmonogram Obiektów";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new HarmonogramObiektow();
            _SortBy = "Data";
            _SelectedFilterColumn = "Obiekt";
            _FilterText = "";
        }
        #endregion

        #region Wlasciwosci
        public string Obiekt
        {
            get
            {
                return item.Obiekt;
            }
            set
            {
                if (item.Obiekt != value)
                {
                    item.Obiekt = value;
                    OnPropertyChanged(() => Obiekt);
                }
            }
        }
        public DateTime? Data
        {
            get
            {
                return item.Data;
            }
            set
            {
                if (item.Data != value)
                {
                    item.Data = value;
                    OnPropertyChanged(() => Data);
                }
            }
        }
        public TimeSpan? GodzinaOd
        {
            get
            {
                return item.GodzinaOd;
            }
            set
            {
                if (item.GodzinaOd != value)
                {
                    item.GodzinaOd = value;
                    OnPropertyChanged(() => GodzinaOd);
                }
            }
        }
        public TimeSpan? GodzinaDo
        {
            get
            {
                return item.GodzinaDo;
            }
            set
            {
                if (item.GodzinaDo != value)
                {
                    item.GodzinaDo = value;
                    OnPropertyChanged(() => GodzinaDo);
                }
            }
        }
        public int? DruzynaId
        {
            get
            {
                return item.DruzynaId;
            }
            set
            {
                if (item.DruzynaId != value)
                {
                    item.DruzynaId = value;
                    OnPropertyChanged(() => DruzynaId);
                }
            }
        }
        #endregion

        #region Komendy Logika
        public void Save()
        {
            klubSportowyEntities.HarmonogramObiektow.Add(item);
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