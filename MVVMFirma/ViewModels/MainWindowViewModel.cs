using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using KlubSportowy.Helper;
using System.Diagnostics;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using KlubSportowy.Views;

namespace KlubSportowy.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private ReadOnlyCollection<CommandViewModel> _Commands;
        private ObservableCollection<WorkspaceViewModel> _Workspaces;
        #endregion

        #region Commands
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_Commands == null)
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _Commands;
            }
        }
        private List<CommandViewModel> CreateCommands()
        {
            Messenger.Default.Register<string>(this, open);

            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    "Druzyny",
                    new BaseCommand(() => this.ShowAllDruzyny())),

                new CommandViewModel(
                    "Frekwencja",
                    new BaseCommand(() => this.ShowAllFrekwencja())),

                new CommandViewModel(
                    "Harmonogram",
                    new BaseCommand(() => this.ShowAllHarmonogram())),

                new CommandViewModel(
                    "Kontuzje",
                    new BaseCommand(() => this.CreateView(new KontuzjeViewModel()))),

                new CommandViewModel(
                    "LogiOperacji",
                    new BaseCommand(() => this.ShowAllLogiOperacji())),

                new CommandViewModel(
                    "Magazyny",
                    new BaseCommand(() => this.ShowAllMagazyny())),

                new CommandViewModel(
                    "Mecze",
                    new BaseCommand(() => this.CreateView(new MeczeViewModel()))),

                new CommandViewModel(
                    "Platnosci",
                    new BaseCommand(() => this.CreateView(new PlatnosciViewModel()))),

                new CommandViewModel(
                    "Rodzice",
                    new BaseCommand(() => this.ShowAllRodzice())),

                new CommandViewModel(
                    "Sprzet",
                    new BaseCommand(() => this.ShowAllSprzet())),

                new CommandViewModel(
                    "Statystyki",
                    new BaseCommand(() => this.ShowAllStatystyki())),

                new CommandViewModel(
                    "Trenerzy",
                    new BaseCommand(() => this.ShowAllTrenerzy())),

                new CommandViewModel(
                    "Treningi",
                    new BaseCommand(() => this.CreateView (new TreningiViewModel()))),

                new CommandViewModel(
                    "Uzytkownicy",
                    new BaseCommand(() => this.ShowAllUzytkownicy())),

                new CommandViewModel(
                    "WypozyczeniaSprzetu",
                    new BaseCommand(() => this.ShowAllWypozyczeniaSprzetu())),

                new CommandViewModel(
                    "Zawodnicy",
                    new BaseCommand(() => this.CreateView(new ZawodnicyViewModel())))
            };
        }
        #endregion

        #region Workspaces
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _Workspaces;
            }
        }
        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }
        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            this.Workspaces.Remove(workspace);
        }

        #endregion

        #region Private Helpers
        private void open(string name)
        {
            if (name == "Trenerzy All")
            {
                var viewModel = new WybierzTreneraViewModel();
                Window window = new Window
                {
                    Title = "Wybierz Trenera",
                    Width = 600,
                    Height = 450,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Content = new WybierzTreneraView(),
                    DataContext = viewModel
                };
                viewModel.RequestClose += (s, e) => window.Close();
                window.ShowDialog();
            }
            else if (name == "OpenFinancialReport")
            {
                var viewModel = new RaportFinansowyViewModel();
                Window reportWindow = new Window
                {
                    Title = "Analiza Finansowa Klubu",
                    Width = 1000,
                    Height = 750,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Content = new RaportFinansowyView(),
                    DataContext = viewModel
                };
                reportWindow.Show();
            }
        }
        private void CreateView(WorkspaceViewModel workspace)
        {
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }
        private void CreateKontuzje()
        {
            KontuzjeViewModel workspace = new KontuzjeViewModel();
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }
        private void CreateMecze()
        {
            MeczeViewModel workspace = new MeczeViewModel();
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }
        private void CreatePlatnosci()
        {
            PlatnosciViewModel workspace = new PlatnosciViewModel();
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }
        private void CreateTreningi()
        {
            TreningiViewModel workspace = new TreningiViewModel();
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }
        private void CreateZawodnicy()
        {
            ZawodnicyViewModel workspace = new ZawodnicyViewModel();
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllDruzyny()
        {
            DruzynyViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is DruzynyViewModel)
                as DruzynyViewModel;
            if (workspace == null)
            {
                workspace = new DruzynyViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllFrekwencja()
        {
            FrekwencjaViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is FrekwencjaViewModel)
                as FrekwencjaViewModel;
            if (workspace == null)
            {
                workspace = new FrekwencjaViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllHarmonogram()
        {
            HarmonogramObiektowViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is HarmonogramObiektowViewModel)
                as HarmonogramObiektowViewModel;
            if (workspace == null)
            {
                workspace = new HarmonogramObiektowViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllLogiOperacji()
        {
            LogiOperacjiViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is LogiOperacjiViewModel)
                as LogiOperacjiViewModel;
            if (workspace == null)
            {
                workspace = new LogiOperacjiViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllMagazyny()
        {
            MagazynyViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is MagazynyViewModel)
                as MagazynyViewModel;
            if (workspace == null)
            {
                workspace = new MagazynyViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllRodzice()
        {
            RodziceViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is RodziceViewModel)
                as RodziceViewModel;
            if (workspace == null)
            {
                workspace = new RodziceViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllSprzet()
        {
            SprzetViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is SprzetViewModel)
                as SprzetViewModel;
            if (workspace == null)
            {
                workspace = new SprzetViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllStatystyki()
        {
            StatystykiViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is StatystykiViewModel)
                as StatystykiViewModel;
            if (workspace == null)
            {
                workspace = new StatystykiViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllTrenerzy()
        {
            TrenerzyViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is TrenerzyViewModel)
                as TrenerzyViewModel;
            if (workspace == null)
            {
                workspace = new TrenerzyViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllUzytkownicy()
        {
            UzytkownicyViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is UzytkownicyViewModel)
                as UzytkownicyViewModel;
            if (workspace == null)
            {
                workspace = new UzytkownicyViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllWypozyczeniaSprzetu()
        {
            WypozyczeniaSprzetuViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is WypozyczeniaSprzetuViewModel)
                as WypozyczeniaSprzetuViewModel;
            if (workspace == null)
            {
                workspace = new WypozyczeniaSprzetuViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }
        #endregion
    }
}