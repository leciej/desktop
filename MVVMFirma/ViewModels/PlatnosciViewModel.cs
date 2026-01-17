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
    public class PlatnosciViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        #region Baza danych
        private Platnosci item;
        private KlubSportowyEntities klubSportowyEntities;
        #endregion

        #region Konstruktor
        public PlatnosciViewModel()
        {
            base.DisplayName = "Płatności";
            klubSportowyEntities = new KlubSportowyEntities();
            ResetForm();

            _SortBy = "Data wpłaty";
            _SelectedFilterColumn = "Status";
            _FilterText = "";
            _IsAdding = false;

            // Rejestracja odbierania zawodnika z okna modalnego
            Messenger.Default.Register<Zawodnicy>(this, zawodnik =>
            {
                if (zawodnik != null)
                {
                    this.ZawodnikId = zawodnik.ZawodnikId;
                }
            });
        }

        private void ResetForm()
        {
            item = new Platnosci();
            item.KiedyDodal = DateTime.Now;
            item.CzyAktywny = true;
            item.Miesiac = DateTime.Now.ToString("yyyy-MM-dd");
        }
        #endregion

        #region Opcje Wyboru
        public List<string> StatusyOptions => new List<string> { "Opłacone", "Nieopłacone", "Oczekujące" };
        public List<string> CzyAktywnyOptions => new List<string> { "Tak", "Nie" };

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
                    case nameof(ZawodnikId):
                        if (ZawodnikId == null || ZawodnikId <= 0) result = "Wybierz zawodnika z listy!";
                        break;

                    case nameof(Kwota):
                        if (Kwota == null || Kwota <= 0) result = "Wpisz kwotę!";
                        break;

                    case nameof(DataWplaty):
                        if (DataWplaty == null) result = "Wybierz datę wpłaty!";
                        break;

                    case nameof(Status):
                        if (string.IsNullOrWhiteSpace(Status)) result = "Wybierz status!";
                        break;
                }
                return result;
            }
        }

        public bool IsValid()
        {
            return string.IsNullOrEmpty(this[nameof(ZawodnikId)]) &&
                   string.IsNullOrEmpty(this[nameof(Kwota)]) &&
                   string.IsNullOrEmpty(this[nameof(DataWplaty)]) &&
                   string.IsNullOrEmpty(this[nameof(Status)]);
        }
        #endregion

        #region Logika Listy i LINQ
        private ObservableCollection<Platnosci> _List;
        public ObservableCollection<Platnosci> List
        {
            get { if (_List == null) Load(); return _List; }
            set { if (_List != value) { _List = value; OnPropertyChanged(() => List); } }
        }

        public void Load()
        {
            klubSportowyEntities = new KlubSportowyEntities();
            var query = klubSportowyEntities.Platnosci.AsQueryable();

            if (!string.IsNullOrEmpty(FilterText))
            {
                switch (SelectedFilterColumn)
                {
                    case "Status": query = query.Where(z => z.Status.Contains(FilterText)); break;
                    case "Uwagi": query = query.Where(z => z.Uwagi.Contains(FilterText)); break;
                }
            }

            switch (SortBy)
            {
                case "Kwota": query = query.OrderBy(z => z.Kwota); break;
                case "Status": query = query.OrderBy(z => z.Status); break;
                default: query = query.OrderByDescending(z => z.Miesiac); break;
            }

            List = new ObservableCollection<Platnosci>(query.ToList());
        }
        #endregion

        #region Komendy
        private bool _IsAdding;
        public bool IsAdding
        {
            get => _IsAdding;
            set { if (_IsAdding != value) { _IsAdding = value; OnPropertyChanged(() => IsAdding); } }
        }

        public ICommand LoadCommand => new BaseCommand(Load);
        public ICommand AddCommand => new BaseCommand(() => IsAdding = true);
        public ICommand SaveAndCloseCommand => new BaseCommand(saveAndClose);

        public ICommand ShowReportCommand => new BaseCommand(() =>
        {
            Messenger.Default.Send("OpenFinancialReport");
        });

        // Komenda otwierająca okno modalne wyboru zawodnika
        public ICommand WybierzZawodnikaCommand => new BaseCommand(() =>
        {
            Messenger.Default.Send("Zawodnicy All");
        });

        private void saveAndClose()
        {
            if (IsValid())
            {
                item.KiedyDodal = DateTime.Now;
                item.KtoDodal = "System";

                klubSportowyEntities.Platnosci.Add(item);
                klubSportowyEntities.SaveChanges();
                Load();

                ResetForm();
                OdswiezPola();
                IsAdding = false;
            }
            else
            {
                System.Windows.MessageBox.Show("Formularz zawiera błędy.");
            }
        }

        private void OdswiezPola()
        {
            OnPropertyChanged(() => ZawodnikId);
            OnPropertyChanged(() => Kwota);
            OnPropertyChanged(() => DataWplaty);
            OnPropertyChanged(() => Status);
            OnPropertyChanged(() => CzyAktywnyWybor);
            OnPropertyChanged(() => Uwagi);
        }
        #endregion

        #region Filtrowanie i Sortowanie
        private string _FilterText;
        public string FilterText { get => _FilterText; set { if (_FilterText != value) { _FilterText = value; OnPropertyChanged(() => FilterText); Load(); } } }

        private string _SelectedFilterColumn;
        public string SelectedFilterColumn { get => _SelectedFilterColumn; set { if (_SelectedFilterColumn != value) { _SelectedFilterColumn = value; OnPropertyChanged(() => SelectedFilterColumn); Load(); } } }

        public List<string> FilterColumnOptions => new List<string> { "Status", "Uwagi" };

        private string _SortBy;
        public string SortBy { get => _SortBy; set { if (_SortBy != value) { _SortBy = value; OnPropertyChanged(() => SortBy); Load(); } } }

        public List<string> SortOptions => new List<string> { "Kwota", "Status" };
        #endregion

        #region Właściwości modelu
        public int? ZawodnikId
        {
            get => item.ZawodnikId;
            set
            {
                if (item.ZawodnikId != value)
                {
                    item.ZawodnikId = value;
                    OnPropertyChanged(() => ZawodnikId);
                }
            }
        }

        public decimal? Kwota { get => item.Kwota; set { item.Kwota = value; OnPropertyChanged(() => Kwota); } }

        public DateTime? DataWplaty
        {
            get
            {
                if (DateTime.TryParse(item.Miesiac, out DateTime tempDate))
                    return tempDate;
                return null;
            }
            set
            {
                item.Miesiac = value?.ToString("yyyy-MM-dd");
                OnPropertyChanged(() => DataWplaty);
            }
        }

        public string Status { get => item.Status; set { item.Status = value; OnPropertyChanged(() => Status); } }
        public bool? CzyAktywny { get => item.CzyAktywny; set { item.CzyAktywny = value; OnPropertyChanged(() => CzyAktywny); } }
        public string KtoDodal { get => item.KtoDodal; set { item.KtoDodal = value; OnPropertyChanged(() => KtoDodal); } }
        public DateTime? KiedyDodal { get => item.KiedyDodal; set { item.KiedyDodal = value; OnPropertyChanged(() => KiedyDodal); } }
        public string Uwagi { get => item.Uwagi; set { item.Uwagi = value; OnPropertyChanged(() => Uwagi); } }
        #endregion
    }
}