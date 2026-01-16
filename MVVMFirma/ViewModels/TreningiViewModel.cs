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
            List = new ObservableCollection<Treningi>(
                klubSportowyEntities.Treningi.ToList()
                );
        }
        #endregion
        #region Konstuktor
        public TreningiViewModel()
        {
            base.DisplayName = "Treningi";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new Treningi();
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
            klubSportowyEntities.Treningi.Add(item);
            klubSportowyEntities.SaveChanges();
        }
        private void saveAndClose()
        {
            Save();
        }
        #endregion
        #region Wlasciwosci
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
        public DateTime? DataTreningu
        {
            get
            {
                return item.DataTreningu;
            }
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
            get
            {
                return item.Lokalizacja;
            }
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
            get
            {
                return item.TypTreningu;
            }
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
