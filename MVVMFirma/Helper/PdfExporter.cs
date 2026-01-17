using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using System.Windows;

namespace KlubSportowy.Helper
{
    public static class PdfExporter
    {
        public static void ExportDataToPdf(string title, string[] headers, List<string[]> rows)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF File |*.pdf";
            sfd.FileName = title + "_" + DateTime.Now.ToString("yyyyMMdd");

            if (sfd.ShowDialog() == true)
            {
                try
                {
                    using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();

                        Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                        Paragraph p = new Paragraph(title, titleFont);
                        p.Alignment = Element.ALIGN_CENTER;
                        p.SpacingAfter = 20;
                        pdfDoc.Add(p);

                        PdfPTable table = new PdfPTable(headers.Length);
                        table.WidthPercentage = 100;

                        foreach (string header in headers)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(header, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                            cell.BackgroundColor = new BaseColor(184, 0, 6);
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.Padding = 5;
                            table.AddCell(cell);
                        }

                        foreach (string[] row in rows)
                        {
                            foreach (string cellData in row)
                            {
                                table.AddCell(new Phrase(cellData ?? "", FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                            }
                        }

                        pdfDoc.Add(table);
                        pdfDoc.Close();
                        stream.Close();
                    }
                    MessageBox.Show("Raport PDF został wygenerowany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Wystąpił błąd: " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}