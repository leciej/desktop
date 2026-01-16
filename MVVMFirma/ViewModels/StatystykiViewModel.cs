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
            List = new ObservableCollection<StatystykiMeczowe>(
                klubSportowyEntities.StatystykiMeczowe.ToList()
                );
        }
        #endregion
        #region Konstuktor
        public StatystykiViewModel()
        {
            base.DisplayName = "Statystyki";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new StatystykiMeczowe();
        }
        #endregion
        #region Komendy
        private BaseCommand _SaveAndCloseCommand;
        public ICommand SaveAndCloseCommand
        {
            get
            {
                if (_SaveAndCloseCommand == null) _SaveAndCloseCommand = new BaseCommand(saveAndClose);
                return _SaveAndCloseCommand;
            }
        }
        public void Save()
        {
            klubSportowyEntities.StatystykiMeczowe.Add(item);
            klubSportowyEntities.SaveChanges();
        }
        private void saveAndClose()
        {
            Save();
        }
        #endregion
        #region Wlasciwosci
        public int? MeczId
        {
            get
            {
                return item.MeczId;
            }
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
            get
            {
                return item.ZawodnikId;
            }
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
            get
            {
                return item.Gole;
            }
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
            get
            {
                return item.Asysty;
            }
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
            get
            {
                return item.CzasGry;
            }
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
    }
}
