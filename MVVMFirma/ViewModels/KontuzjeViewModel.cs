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
        private readonly KlubSportowyEntities klubSportowyEntities;
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
            List = new ObservableCollection<Kontuzje>(
                klubSportowyEntities.Kontuzje.ToList()
                );
        }
        #endregion
        #region Konstuktor
        public KontuzjeViewModel()
        {
            base.DisplayName = "Kontuzje";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new Kontuzje();
        }
        #endregion
        #region Wlasciwosci
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
        public string TypKontuzji
        {
            get
            {
                return item.TypKontuzji;
            }
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
            get
            {
                return item.DataZdarzenia;
            }
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
            get
            {
                return item.PrzewidywanyPowrot;
            }
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
            klubSportowyEntities.Kontuzje.Add(item);
            klubSportowyEntities.SaveChanges();
        }
        private void saveAndClose()
        {
            Save();
        }
        #endregion
    }
}
#endregion