using KlubSportowy.Helper;
using KlubSportowy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;

namespace KlubSportowy.ViewModels
{
    public class DruzynyViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        #region Baza danych
        private Druzyny item;
        private KlubSportowyEntities klubSportowyEntities;
        #endregion

        #region Konstruktor
        public DruzynyViewModel()
        {
            base.DisplayName = "Drużyny";
            item = new Druzyny();
            klubSportowyEntities = new KlubSportowyEntities();

            _SortBy = "Nazwa";
            _SelectedFilterColumn = "Nazwa";
            _FilterText = "";
            _IsAdding = false;

            item.KiedyDodal = DateTime.Now;
            item.CzyAktywny = true;

            Messenger.Default.Register<Trenerzy>(this, trener =>
            {
                if (trener != null)
                {
                    this.TrenerId = trener.TrenerId;
                }
            });
        }
        #endregion

        #region Opcje Wyboru (Statusy i ComboBox)
        public List<string> StatusyOptions => new List<string> { "Tak", "Nie" };

        public string CzyAktywnyWybor
        {
            get => item.CzyAktywny == true ? "Tak" : "Nie";
            set
            {
                item.CzyAktywny = (value == "Tak");
                OnPropertyChanged(() => CzyAktywnyWybor);
            }
        }
        #endregion

        #region Walidacja (IDataErrorInfo)
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case nameof(Nazwa):
                        if (string.IsNullOrWhiteSpace(Nazwa)) result = "Nazwa drużyny jest wymagana!";
                        else if (Nazwa.Length < 3) result = "Nazwa musi mieć co najmniej 3 znaki!";
                        break;

                    case nameof(Kategoria):
                        if (string.IsNullOrWhiteSpace(Kategoria)) result = "Kategoria jest wymagana!";
                        break;

                    case nameof(TrenerId):
                        if (TrenerId == null || TrenerId <= 0) result = "Wybierz trenera z listy!";
                        break;

                    case nameof(KtoDodal):
                        if (string.IsNullOrWhiteSpace(KtoDodal)) result = "Podaj osobę dodającą rekord!";
                        break;

                    case nameof(KiedyDodal):
                        if (KiedyDodal == null) result = "Wybierz datę z kalendarza!";
                        else if (KiedyDodal > DateTime.Now) result = "Data nie może być z przyszłości!";
                        break;

                    case nameof(CzyAktywnyWybor):
                        if (string.IsNullOrEmpty(CzyAktywnyWybor)) result = "Musisz wybrać status!";
                        break;

                    case nameof(Uwagi):
                        if (Uwagi != null && Uwagi.Length > 200) result = "Uwagi są zbyt długie (max 200 zn)!";
                        break;

                    case nameof(KtoModyfikowal):
                        if (KtoModyfikowal != null && KtoModyfikowal.Length > 50) result = "Nazwa edytora jest za długa!";
                        break;
                }
                return result;
            }
        }

        public bool IsValid()
        {
            return string.IsNullOrEmpty(this[nameof(Nazwa)]) &&
                   string.IsNullOrEmpty(this[nameof(Kategoria)]) &&
                   string.IsNullOrEmpty(this[nameof(TrenerId)]) &&
                   string.IsNullOrEmpty(this[nameof(KtoDodal)]) &&
                   string.IsNullOrEmpty(this[nameof(KiedyDodal)]) &&
                   string.IsNullOrEmpty(this[nameof(CzyAktywnyWybor)]) &&
                   string.IsNullOrEmpty(this[nameof(Uwagi)]) &&
                   string.IsNullOrEmpty(this[nameof(KtoModyfikowal)]);
        }
        #endregion

        #region Logika Listy i LINQ
        private ObservableCollection<Druzyny> _List;
        public ObservableCollection<Druzyny> List
        {
            get { if (_List == null) Load(); return _List; }
            set { if (_List != value) { _List = value; OnPropertyChanged(() => List); } }
        }

        public void Load()
        {
            klubSportowyEntities = new KlubSportowyEntities();
            var query = klubSportowyEntities.Druzyny.AsQueryable();

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (SelectedFilterColumn)
                {
                    case "Nazwa": query = query.Where(z => z.Nazwa.Contains(FilterText)); break;
                    case "Kategoria": query = query.Where(z => z.Kategoria.Contains(FilterText)); break;
                    case "Uwagi": query = query.Where(z => z.Uwagi.Contains(FilterText)); break;
                }
            }

            switch (SortBy)
            {
                case "Nazwa": query = query.OrderBy(z => z.Nazwa); break;
                case "Kategoria": query = query.OrderBy(z => z.Kategoria); break;
                case "Trener ID": query = query.OrderBy(z => z.TrenerId); break;
                default: query = query.OrderBy(z => z.Nazwa); break;
            }

            List = new ObservableCollection<Druzyny>(query.ToList());
        }
        #endregion

        #region Komendy i Zarządzanie Formularzem
        private bool _IsAdding;
        public bool IsAdding
        {
            get => _IsAdding;
            set { if (_IsAdding != value) { _IsAdding = value; OnPropertyChanged(() => IsAdding); } }
        }

        public ICommand LoadCommand => new BaseCommand(Load);
        public ICommand AddCommand => new BaseCommand(() => IsAdding = true);
        public ICommand SaveAndCloseCommand => new BaseCommand(saveAndClose);

        // DODANA KOMENDA RAPORTU (Aby przycisk w Generic.xaml zaczął działać)
        public ICommand ShowReportCommand => new BaseCommand(() =>
        {
            Messenger.Default.Send("OpenFinancialReport");
        });

        public ICommand WybierzTreneraCommand => new BaseCommand(() =>
        {
            Messenger.Default.Send("Trenerzy All");
        });

        private void saveAndClose()
        {
            if (IsValid())
            {
                klubSportowyEntities.Druzyny.Add(item);
                klubSportowyEntities.SaveChanges();
                Load();
                IsAdding = false;

                item = new Druzyny { KiedyDodal = DateTime.Now, CzyAktywny = true };

                OnPropertyChanged(() => Nazwa);
                OnPropertyChanged(() => Kategoria);
                OnPropertyChanged(() => TrenerId);
                OnPropertyChanged(() => KtoDodal);
                OnPropertyChanged(() => KiedyDodal);
                OnPropertyChanged(() => Uwagi);
                OnPropertyChanged(() => KtoModyfikowal);
                OnPropertyChanged(() => CzyAktywnyWybor);
            }
            else
            {
                System.Windows.MessageBox.Show("Formularz zawiera błędy. Popraw pola zaznaczone na czerwono.");
            }
        }
        #endregion

        #region Filtrowanie i Sortowanie
        private string _FilterText;
        public string FilterText { get => _FilterText; set { if (_FilterText != value) { _FilterText = value; OnPropertyChanged(() => FilterText); Load(); } } }

        private string _SelectedFilterColumn;
        public string SelectedFilterColumn { get => _SelectedFilterColumn; set { if (_SelectedFilterColumn != value) { _SelectedFilterColumn = value; OnPropertyChanged(() => SelectedFilterColumn); Load(); } } }

        public List<string> FilterColumnOptions => new List<string> { "Nazwa", "Kategoria", "Uwagi" };

        private string _SortBy;
        public string SortBy { get => _SortBy; set { if (_SortBy != value) { _SortBy = value; OnPropertyChanged(() => SortBy); Load(); } } }

        public List<string> SortOptions => new List<string> { "Nazwa", "Kategoria", "Trener ID" };
        #endregion

        #region Właściwości modelu (Binding do formularza)
        public string Nazwa { get => item.Nazwa; set { item.Nazwa = value; OnPropertyChanged(() => Nazwa); } }
        public string Kategoria { get => item.Kategoria; set { item.Kategoria = value; OnPropertyChanged(() => Kategoria); } }
        public int? TrenerId { get => item.TrenerId; set { item.TrenerId = value; OnPropertyChanged(() => TrenerId); } }
        public string KtoDodal { get => item.KtoDodal; set { item.KtoDodal = value; OnPropertyChanged(() => KtoDodal); } }
        public DateTime? KiedyDodal { get => item.KiedyDodal; set { item.KiedyDodal = value; OnPropertyChanged(() => KiedyDodal); } }
        public string KtoModyfikowal { get => item.KtoModyfikowal; set { item.KtoModyfikowal = value; OnPropertyChanged(() => KtoModyfikowal); } }
        public string Uwagi { get => item.Uwagi; set { item.Uwagi = value; OnPropertyChanged(() => Uwagi); } }
        #endregion
    }
}