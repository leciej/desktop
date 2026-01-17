using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using KlubSportowy.Helper;
using KlubSportowy.Models;
using LiveCharts;
using LiveCharts.Wpf;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media;

namespace KlubSportowy.ViewModels
{
    public class RaportFinansowyViewModel : WorkspaceViewModel
    {
        private KlubSportowyEntities db = new KlubSportowyEntities();
        private CultureInfo polskaKultura = new CultureInfo("pl-PL");

        public RaportFinansowyViewModel()
        {
            base.DisplayName = "Raport Finansowy";
            DataOd = DateTime.Now.AddMonths(-6);
            DataDo = DateTime.Now;
            SeriesCollection = new SeriesCollection();
            Labels = new string[] { };

            ShowReportCommand = new BaseCommand(() => GenerujRaport());
            ExportToPdfCommand = new BaseCommand(() => EksportujDoPdf());
            PrintCommand = new BaseCommand<object>((obj) => Drukuj(obj));
        }

        private DateTime _DataOd;
        public DateTime DataOd { get => _DataOd; set { _DataOd = value; OnPropertyChanged(() => DataOd); } }

        private DateTime _DataDo;
        public DateTime DataDo { get => _DataDo; set { _DataDo = value; OnPropertyChanged(() => DataDo); } }

        private decimal _SumaPrzychodow;
        public decimal SumaPrzychodow { get => _SumaPrzychodow; set { _SumaPrzychodow = value; OnPropertyChanged(() => SumaPrzychodow); } }

        private SeriesCollection _SeriesCollection;
        public SeriesCollection SeriesCollection { get => _SeriesCollection; set { _SeriesCollection = value; OnPropertyChanged(() => SeriesCollection); } }

        private string[] _Labels;
        public string[] Labels { get => _Labels; set { _Labels = value; OnPropertyChanged(() => Labels); } }

        public Func<double, string> YFormatter => value => value.ToString("C", polskaKultura);

        public ICommand ShowReportCommand { get; set; }
        public ICommand ExportToPdfCommand { get; set; }
        public ICommand PrintCommand { get; set; }

        private void GenerujRaport()
        {
            var dane = db.Platnosci
                .Where(p => p.KiedyDodal >= DataOd && p.KiedyDodal <= DataDo)
                .ToList();

            SumaPrzychodow = dane.Sum(p => (decimal?)p.Kwota) ?? 0;

            var pogrupowane = dane
                .GroupBy(p => p.KiedyDodal.Value.ToString("MMMM yyyy", polskaKultura))
                .Select(g => new { Etykieta = g.Key, Wartosc = g.Sum(p => (decimal?)p.Kwota) ?? 0 })
                .ToList();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Przychód",
                    Values = new ChartValues<decimal>(pogrupowane.Select(x => x.Wartosc)),
                    Fill = Brushes.Crimson,
                    DataLabels = true
                }
            };

            Labels = pogrupowane.Select(x => x.Etykieta).ToArray();
        }

        private void EksportujDoPdf()
        {
            string[] naglowki = { "Okres", "Suma Wpłat" };
            var wiersze = Labels.Select((label, index) => new string[] {
                label,
                SeriesCollection[0].Values.Cast<decimal>().ElementAt(index).ToString("C", polskaKultura)
            }).ToList();

            PdfExporter.ExportDataToPdf("RAPORT FINANSOWY KLUBU", naglowki, wiersze);
        }

        private void Drukuj(object viewElement)
        {
            var visual = viewElement as Visual;
            if (visual == null) return;

            PrintDialog printDlg = new PrintDialog();
            if (printDlg.ShowDialog() == true)
            {
                printDlg.PrintVisual(visual, "Raport Finansowy " + DateTime.Now.ToShortDateString());
            }
        }
    }
}