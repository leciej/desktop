using KlubSportowy.Helper;
using KlubSportowy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KlubSportowy.ViewModels.Abstract
{
    public abstract class WszystkieViewModel<T> : WorkspaceViewModel
    {
        #region BazaDanych
        protected readonly KlubSportowyEntities klubSportowyEntities;
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
        private ObservableCollection<T> _List;
        public ObservableCollection<T> List
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
        public abstract void Load();
        #endregion
        #region Konstuktor
        public WszystkieViewModel()
        {
            klubSportowyEntities = new KlubSportowyEntities();
        }
        #endregion
    }
}
