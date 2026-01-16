using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using KlubSportowy.Helper; // Tu masz BaseCommand
using KlubSportowy.Models; // Tu masz KlubSportowyEntities i Trenerzy
using GalaSoft.MvvmLight.Messaging;

namespace KlubSportowy.ViewModels
{
    public class WybierzTreneraViewModel : WorkspaceViewModel
    {
        #region Baza danych
        private KlubSportowyEntities klubSportowyEntities;
        #endregion

        #region Właściwości
        private ObservableCollection<Trenerzy> _List;
        public ObservableCollection<Trenerzy> List
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

        private Trenerzy _WybranyTrener;
        public Trenerzy WybranyTrener
        {
            get => _WybranyTrener;
            set
            {
                if (_WybranyTrener != value)
                {
                    _WybranyTrener = value;
                    OnPropertyChanged(() => WybranyTrener);
                }
            }
        }

        // Filtrowanie (Widoczne w Twoim XAML WybierzTreneraView)
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
                    Load(); // Automatyczne odświeżanie przy pisaniu
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

        public List<string> FilterColumnOptions => new List<string> { "Nazwisko", "Imię", "Specjalizacja" };

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

        public List<string> SortOptions => new List<string> { "Nazwisko", "Specjalizacja" };
        #endregion

        #region Konstruktor
        public WybierzTreneraViewModel()
        {
            base.DisplayName = "Wybierz Trenera";
            klubSportowyEntities = new KlubSportowyEntities();
            _SelectedFilterColumn = "Nazwisko";
            _SortBy = "Nazwisko";
        }
        #endregion

        #region Metody pomocnicze i Komendy
        public void Load()
        {
            // Odświeżamy kontekst bazy danych
            klubSportowyEntities = new KlubSportowyEntities();
            var query = klubSportowyEntities.Trenerzy.AsQueryable();

            // Logika filtrowania
            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (SelectedFilterColumn)
                {
                    case "Nazwisko":
                        query = query.Where(t => t.Nazwisko.Contains(FilterText));
                        break;
                    case "Imię":
                        query = query.Where(t => t.Imie.Contains(FilterText));
                        break;
                    case "Specjalizacja":
                        query = query.Where(t => t.Specjalizacja.Contains(FilterText));
                        break;
                }
            }

            // Logika sortowania
            switch (SortBy)
            {
                case "Nazwisko":
                    query = query.OrderBy(t => t.Nazwisko);
                    break;
                case "Specjalizacja":
                    query = query.OrderBy(t => t.Specjalizacja);
                    break;
            }

            List = new ObservableCollection<Trenerzy>(query.ToList());
        }

        // Komenda do przycisku wyboru (wyślij i zamknij)
        public ICommand WybierzIZamknijCommand => new BaseCommand(() =>
        {
            if (WybranyTrener != null)
            {
                // Wysyłamy obiekt trenera do DruzynyViewModel
                Messenger.Default.Send(WybranyTrener);

                // Zamykamy okno modalne wywołując zdarzenie z WorkspaceViewModel
                OnRequestClose();
            }
            else
            {
                System.Windows.MessageBox.Show("Proszę wybrać trenera z listy.");
            }
        });
        #endregion
    }
}