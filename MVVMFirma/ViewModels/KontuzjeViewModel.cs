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
    public class KontuzjeViewModel : WorkspaceViewModel
    {
        #region BazaDanych
        protected KlubSportowyEntities klubSportowyEntities;
        private Kontuzje item;
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
        private ObservableCollection<Kontuzje> _List;
        public ObservableCollection<Kontuzje> List
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
            var query = klubSportowyEntities.Kontuzje.AsQueryable();

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (SelectedFilterColumn)
                {
                    case "Typ Kontuzji":
                        query = query.Where(z => z.TypKontuzji.Contains(FilterText));
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
                case "Typ Kontuzji":
                    query = query.OrderBy(z => z.TypKontuzji);
                    break;
                case "Data Zdarzenia":
                    query = query.OrderBy(z => z.DataZdarzenia);
                    break;
                case "Przewidywany Powrót":
                    query = query.OrderBy(z => z.PrzewidywanyPowrot);
                    break;
                default:
                    query = query.OrderByDescending(z => z.DataZdarzenia);
                    break;
            }

            List = new ObservableCollection<Kontuzje>(query.ToList());
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
                return new List<string> { "Typ Kontuzji", "Uwagi", "Zawodnik ID" };
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
                return new List<string> { "Data Zdarzenia", "Typ Kontuzji", "Przewidywany Powrót" };
            }
        }
        #endregion

        #region Konstruktor
        public KontuzjeViewModel()
        {
            base.DisplayName = "Kontuzje";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new Kontuzje();
            _SortBy = "Data Zdarzenia";
            _SelectedFilterColumn = "Typ Kontuzji";
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
        public string TypKontuzji
        {
            get { return item.TypKontuzji; }
            set
            {
                if (item.TypKontuzji != value)
                {
                    item.TypKontuzji = value;
                    OnPropertyChanged(() => TypKontuzji);
                }
            }
        }
        public DateTime? DataZdarzenia
        {
            get { return item.DataZdarzenia; }
            set
            {
                if (item.DataZdarzenia != value)
                {
                    item.DataZdarzenia = value;
                    OnPropertyChanged(() => DataZdarzenia);
                }
            }
        }
        public DateTime? PrzewidywanyPowrot
        {
            get { return item.PrzewidywanyPowrot; }
            set
            {
                if (item.PrzewidywanyPowrot != value)
                {
                    item.PrzewidywanyPowrot = value;
                    OnPropertyChanged(() => PrzewidywanyPowrot);
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
            klubSportowyEntities.Kontuzje.Add(item);
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