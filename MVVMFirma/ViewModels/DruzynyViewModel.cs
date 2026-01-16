using KlubSportowy.Helper;
using KlubSportowy.Models;
using KlubSportowy.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KlubSportowy.ViewModels
{
    public class DruzynyViewModel : WorkspaceViewModel
    {
        #region Baza danych
        private Druzyny item;
        private KlubSportowyEntities klubSportowyEntities;
        #endregion
        #region Konstuktor
        public DruzynyViewModel()
        {
            base.DisplayName = "Drużyny";
            item = new Druzyny();
            klubSportowyEntities = new KlubSportowyEntities();
        }
        #endregion
        #region Lista
        private ObservableCollection<Druzyny> _List;
        public ObservableCollection<Druzyny> List
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
        public void Load()
        {
            List = new ObservableCollection<Druzyny>(
                klubSportowyEntities.Druzyny.ToList()
                );
            OnPropertyChanged(() => List);
        }
        #endregion
        #region Wlasciwosci
        public string Nazwa
        {
            get
            {
                return item.Nazwa;
            }
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
            get
            {
                return item.Kategoria;
            }
            set
            {
                if (item.Kategoria != value)
                {
                    item.Kategoria = value;
                    OnPropertyChanged(() => Kategoria);
                }
            }
        }
        public int? TrenerId
        {
            get
            {
                return item.TrenerId;
            }
            set
            {
                if (item.TrenerId != value)
                {
                    item.TrenerId = value;
                    OnPropertyChanged(() => TrenerId);
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
        #region Komendy
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
        public void Save()
        {
            item.CzyAktywny = true;
            item.KiedyDodal = DateTime.Now;
            klubSportowyEntities.Druzyny.Add(item);
            klubSportowyEntities.SaveChanges();
        }
        private void saveAndClose()
        {
            Save();
        }
    }
        #endregion
}
