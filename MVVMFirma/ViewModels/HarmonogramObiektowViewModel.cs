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
            List = new ObservableCollection<HarmonogramObiektow>(
                klubSportowyEntities.HarmonogramObiektow.ToList()
                );
        }
        #endregion
        #region Konstuktor
        public HarmonogramObiektowViewModel()
        {
            base.DisplayName = "Harmonogram Obiektów";
            klubSportowyEntities = new KlubSportowyEntities();
            item = new HarmonogramObiektow();
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
            klubSportowyEntities.HarmonogramObiektow.Add(item);
            klubSportowyEntities.SaveChanges();
        }
        private void saveAndClose()
        {
            Save();
        }
        #endregion
    }
}
