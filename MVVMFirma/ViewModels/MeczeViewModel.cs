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
    public class MeczeViewModel : WorkspaceViewModel
    {
        #region BazaDanych
        protected KlubSportowyEntities klubSportowyEntities;
        private Mecze item;
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
        private ObservableCollection<Mecze> _List;
        public ObservableCollection<Mecze> List
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
            var query = klubSportowyEntities.Mecze.AsQueryable();

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (SelectedFilterColumn)
                {
                    case "Przeciwnik":
                        query = query.Where(z => z.Przeciwnik.Contains(FilterText));
                        break;
                    case "Wynik":
                        query = query.Where(z => z.Wynik.Contains(FilterText));
                        break;
                    case "Uwagi":
                        query = query.Where(z => z.Uwagi.Contains(FilterText));
                        break;
                }
            }

            switch (SortBy)
            {
                case "Przeciwnik":
                    query = query.OrderBy(z => z.Przeciwnik);
                    break;
                case "Data Meczu":
                    query = query.OrderBy(z => z.DataMeczu);
                    break;
                case "Wynik":
                    query = query.OrderBy(z => z.Wynik);
                    break;
                default:
                    query = query.OrderByDescending(z => z.DataMeczu);
                    break;
            }

            List = new ObservableCollection<Mecze>(query.ToList());
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
                return new List<string> { "Przeciwnik", "Wynik", "Uwagi" };
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
                return new List<string> { "Przeciwnik", "Data Meczu", "Wynik" };
            }
        }
        #endregion

        #region Konstruktor
        public MeczeViewModel()
        {
            base.DisplayName = "Mecze";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new Mecze();
            _SortBy = "Data Meczu";
            _SelectedFilterColumn = "Przeciwnik";
            _FilterText = "";
        }
        #endregion

        #region Komendy Logika
        public void Save()
        {
            klubSportowyEntities.Mecze.Add(item);
            klubSportowyEntities.SaveChanges();
            Load();
        }

        private void saveAndClose()
        {
            Save();
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

        public string Przeciwnik
        {
            get { return item.Przeciwnik; }
            set
            {
                if (item.Przeciwnik != value)
                {
                    item.Przeciwnik = value;
                    OnPropertyChanged(() => Przeciwnik);
                }
            }
        }

        public DateTime? DataMeczu
        {
            get { return item.DataMeczu; }
            set
            {
                if (item.DataMeczu != value)
                {
                    item.DataMeczu = value;
                    OnPropertyChanged(() => DataMeczu);
                }
            }
        }

        public string Wynik
        {
            get { return item.Wynik; }
            set
            {
                if (item.Wynik != value)
                {
                    item.Wynik = value;
                    OnPropertyChanged(() => Wynik);
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