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
    public class LogiOperacjiViewModel : WorkspaceViewModel
    {
        #region BazaDanych
        protected KlubSportowyEntities klubSportowyEntities;
        private LogiOperacji item;
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
        private ObservableCollection<LogiOperacji> _List;
        public ObservableCollection<LogiOperacji> List
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
            var query = klubSportowyEntities.LogiOperacji.AsQueryable();

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (SelectedFilterColumn)
                {
                    case "Operacja":
                        query = query.Where(z => z.Operacja.Contains(FilterText));
                        break;
                    case "Opis":
                        query = query.Where(z => z.Opis.Contains(FilterText));
                        break;
                    case "Uzytkownik ID":
                        query = query.Where(z => z.UzytkownikId.ToString().Contains(FilterText));
                        break;
                }
            }

            switch (SortBy)
            {
                case "Data Operacji":
                    query = query.OrderByDescending(z => z.DataOperacji);
                    break;
                case "Operacja":
                    query = query.OrderBy(z => z.Operacja);
                    break;
                case "Uzytkownik ID":
                    query = query.OrderBy(z => z.UzytkownikId);
                    break;
                default:
                    query = query.OrderByDescending(z => z.DataOperacji);
                    break;
            }

            List = new ObservableCollection<LogiOperacji>(query.ToList());
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
                return new List<string> { "Operacja", "Opis", "Uzytkownik ID" };
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
                return new List<string> { "Data Operacji", "Operacja", "Uzytkownik ID" };
            }
        }
        #endregion

        #region Konstuktor
        public LogiOperacjiViewModel()
        {
            base.DisplayName = "Logi Operacji";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new LogiOperacji();
            _SortBy = "Data Operacji";
            _SelectedFilterColumn = "Operacja";
            _FilterText = "";
        }
        #endregion

        #region Wlasciwosci
        public int? UzytkownikId
        {
            get
            {
                return item.UzytkownikId;
            }
            set
            {
                if (item.UzytkownikId != value)
                {
                    item.UzytkownikId = value;
                    OnPropertyChanged(() => UzytkownikId);
                }
            }
        }
        public string Operacja
        {
            get
            {
                return item.Operacja;
            }
            set
            {
                if (item.Operacja != value)
                {
                    item.Operacja = value;
                    OnPropertyChanged(() => Operacja);
                }
            }
        }
        public string Opis
        {
            get
            {
                return item.Opis;
            }
            set
            {
                if (item.Opis != value)
                {
                    item.Opis = value;
                    OnPropertyChanged(() => Opis);
                }
            }
        }
        public DateTime? DataOperacji
        {
            get
            {
                return item.DataOperacji;
            }
            set
            {
                if (item.DataOperacji != value)
                {
                    item.DataOperacji = value;
                    OnPropertyChanged(() => DataOperacji);
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
            klubSportowyEntities.LogiOperacji.Add(item);
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