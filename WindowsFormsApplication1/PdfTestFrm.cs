﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Pdf;
using Aspose.Pdf.Generator;
using Aspose.Pdf.InteractiveFeatures.Annotations;
using Aspose.Pdf.Text;
using Cell = Aspose.Pdf.Cell;
using Color = System.Drawing.Color;
using PageSize = Aspose.Pdf.Generator.PageSize;
using Row = Aspose.Pdf.Row;

namespace WindowsFormsApplication1
{
    public partial class PdfTestFrm : Form
    {
        public PdfTestFrm()
        {
            InitializeComponent();

            Aspose.Pdf.License pdflicense = new Aspose.Pdf.License();
            pdflicense.SetLicense(@"C:\work\Aspose\Aspose Licence\Aspose.Pdf.lic");
            pdflicense.Embedded = true;

            //CorPdf(); 
            LinePdf();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CorPdf();

            LinePdf();
        }


        private void LinePdf()
        {
            // Create Document instance
            Document doc = new Document();

            var page = doc.Pages.Add();
            page.SetPageSize(PageSize.LetterWidth, PageSize.LetterHeight);
            //            page.PageInfo.IsLandscape = true;
            page.PageInfo.Margin.Top = 5;
            page.PageInfo.Margin.Left = 5;
            page.PageInfo.Margin.Bottom = 5;
            page.PageInfo.Margin.Right = 5;


            //Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(1, PageSize.LetterHeight - 10);
            //// Add graph object to paragraphs collection of page instance
            //page.Paragraphs.Add(graph);
            //// Create Rectangle instance
            //Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(new float[] { 1, 1, 1, PageSize.LetterHeight });
            //line.GraphInfo.LineWidth = 0.5f;
            //// Specify fill color for Graph object
            ////line.GraphInfo.DashArray = new int[] { 0, 1, 0 };
            ////line.GraphInfo.DashPhase = 1;
            //// Add rectangle object to shapes collection of Graph object
            //graph.Shapes.Add(line); 

            // Create first line object starting from Lower-Left to Top-Right corner of page
            var graph = new Aspose.Pdf.Drawing.Graph((float)page.PageInfo.Width, (float)page.PageInfo.Height);
            // var line = new Aspose.Pdf.Drawing.Line(new float[] { (float)page.Rect.LLX, 0, (float)page.PageInfo.Width, (float)page.Rect.URY });

            //var line = new Aspose.Pdf.Drawing.Line(new float[] { (float)page.Rect.LLX, 0, (float)page.Rect.LLX, (float)page.Rect.URY }); 

            // left line
            var line2 = new Aspose.Pdf.Drawing.Line(new float[] { (float)page.Rect.LLX, 10, (float)page.Rect.LLX, (float)page.Rect.URY });

            // top line
            var line3 = new Aspose.Pdf.Drawing.Line(new float[] { (float)page.Rect.LLX, (float)page.Rect.URY, (float)page.PageInfo.Width - 10, (float)page.Rect.URY });

            // right line 
            var line4 = new Aspose.Pdf.Drawing.Line(new float[] { (float)page.Rect.URX - 10, 10, (float)page.Rect.URX - 10, (float)page.Rect.URY });
            //  var line4 = new Aspose.Pdf.Drawing.Line(new float[] { (float)page.PageInfo.Width - 10, 0, (float)page.Rect.URX - 10, (float)page.Rect.URY });

            // bottom line 
            var line5 = new Aspose.Pdf.Drawing.Line(new float[] { (float)page.Rect.LLX, 10, (float)page.Rect.URX - 10, 10 });


            // Draw line from Top-Left corner of page to Bottom-Right corner of page
            //Aspose.Pdf.Drawing.Line line2 = new Aspose.Pdf.Drawing.Line(new float[] { 0, (float)page.Rect.URY, (float)page.PageInfo.Width, (float)page.Rect.LLX }); 
            // Aspose.Pdf.Drawing.Line line2 = new Aspose.Pdf.Drawing.Line(new float[] { 0, (float)page.Rect.URY, (float)page.PageInfo.Width, (float)page.Rect.LLX });

            //graph.Shapes.Add((line));
            graph.Shapes.Add((line2));
            graph.Shapes.Add((line3));
            graph.Shapes.Add((line4));
            graph.Shapes.Add((line5));

            page.Paragraphs.Add(graph);

            // save the PDF file  
            var tick = DateTime.Now.Ticks;
            doc.Save($"C:\\work\\temp\\reprot_{tick}.pdf");

        }

        private void CorPdf()
        {
            Document pdfDocument = new Document();

            var pdfPage = pdfDocument.Pages.Add();
            pdfPage.SetPageSize(PageSize.LetterWidth, PageSize.LetterHeight);
            pdfPage.PageInfo.IsLandscape = true;
            pdfPage.PageInfo.Margin.Top = 30;
            pdfPage.PageInfo.Margin.Left = 10;
            pdfPage.PageInfo.Margin.Bottom = 20;
            pdfPage.PageInfo.Margin.Right = 10;

            Aspose.Pdf.Text.TextState headTableBoldTextState = new Aspose.Pdf.Text.TextState("Arial", true, false);
            headTableBoldTextState.FontSize = 6;
            Aspose.Pdf.Text.TextState headTableNormalTextState = new Aspose.Pdf.Text.TextState("Arial", false, false);
            headTableNormalTextState.FontSize = 6;

            // header table 
            HeaderTable(pdfPage, headTableNormalTextState);

            // footer part 
            FootTable(pdfPage, headTableNormalTextState);

            Aspose.Pdf.Text.TextState reportInfoTableBoldTextState = new Aspose.Pdf.Text.TextState("Arial", true, false);
            reportInfoTableBoldTextState.FontSize = 12;
            Aspose.Pdf.Text.TextState reportInfoTableNormalTextState = new Aspose.Pdf.Text.TextState("Arial", false, false);
            reportInfoTableNormalTextState.FontSize = 10;

            // report info part  
            ReportInfoTable(pdfPage, reportInfoTableNormalTextState);

            //Samples and Results text   
            SampleAndResult(pdfPage, reportInfoTableBoldTextState);

            //Comments text  
            CommentsTable(pdfPage, reportInfoTableBoldTextState, reportInfoTableNormalTextState);

            //TTO Certification text  
            TTOCertificateTable(pdfPage, reportInfoTableBoldTextState, reportInfoTableNormalTextState);

            //Signature Statement text  
            SignatureTable(pdfPage, reportInfoTableBoldTextState, reportInfoTableNormalTextState);

            //Attachments 
            AttachmentsTable(pdfPage, reportInfoTableBoldTextState, reportInfoTableNormalTextState);



            // save the PDF file  
            var tick = DateTime.Now.Ticks;
            pdfDocument.Save($"C:\\work\\temp\\reprot_{tick}.pdf");
        }

        private static void HeaderTable(Page pdfPage, TextState headTableNormalTextState)
        {
            Aspose.Pdf.Table headerTable = new Aspose.Pdf.Table();

            pdfPage.Header = new Aspose.Pdf.HeaderFooter();
            pdfPage.Header.Paragraphs.Add(headerTable);
            //page1.Paragraphs.Add(headerTable);

            headerTable.ColumnWidths = "33.3% 33.3% 33.3%";
            headerTable.DefaultCellPadding = new Aspose.Pdf.MarginInfo(2, 3, 2, 3);
            headerTable.Broken = Aspose.Pdf.TableBroken.None;
            headerTable.Margin.Top = 5f;
            headerTable.SetColumnTextState(0, headTableNormalTextState);
            headerTable.SetColumnTextState(1, headTableNormalTextState);
            headerTable.SetColumnTextState(2, headTableNormalTextState);

            Aspose.Pdf.Row row = headerTable.Rows.Add();
            row.Cells.Add("City Of Grand Rapids");
            row.Cells.Add("Copy Of Record");
            row.Cells.Add("VALLEY CITY PLATING, INC");
        }

        private static void FootTable(Page pdfPage, TextState headTableNormalTextState)
        {
            Aspose.Pdf.Table footerTable = new Aspose.Pdf.Table();
            pdfPage.Footer = new Aspose.Pdf.HeaderFooter();
            pdfPage.Footer.Paragraphs.Add(footerTable);

            footerTable.ColumnWidths = "33.3% 33.3% 33.3%";
            footerTable.DefaultCellPadding = new Aspose.Pdf.MarginInfo(2, 3, 2, 3);
            footerTable.Broken = Aspose.Pdf.TableBroken.None;
            footerTable.Margin.Bottom = -5f;
            footerTable.SetColumnTextState(0, headTableNormalTextState);
            footerTable.SetColumnTextState(1, headTableNormalTextState);
            footerTable.SetColumnTextState(2, headTableNormalTextState);

            TextFragment text = new TextFragment("Page: ($p of $P )");
            text.TextState.FontSize = 8;

            var row = footerTable.Rows.Add();
            row.Cells.Add("1st QUART PCR");
            var cell = row.Cells.Add("");
            cell.Paragraphs.Add(text);
            row.Cells.Add("APRIL 15 2016 12:15 PM");

        }

        private static void ReportInfoTable(Page pdfPage, TextState reportInfoTableNormalTextState)
        {
            var reportInfoTable = new Aspose.Pdf.Table();

            //var tableOrder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.Top | Aspose.Pdf.BorderSide.Bottom, 1F);
            //reportInfoTable.Border = tableOrder;
            //reportInfoTable.BackgroundColor = Aspose.Pdf.Color.LightGray; 

            reportInfoTable.Margin.Top = 20f;

            pdfPage.Paragraphs.Add(reportInfoTable);
            reportInfoTable.ColumnWidths = "35% 30% 35%";
            reportInfoTable.DefaultCellPadding = new Aspose.Pdf.MarginInfo(2, 3, 2, 3);
            reportInfoTable.Broken = Aspose.Pdf.TableBroken.None;

            reportInfoTable.SetColumnTextState(0, reportInfoTableNormalTextState);
            reportInfoTable.SetColumnTextState(1, reportInfoTableNormalTextState);
            reportInfoTable.SetColumnTextState(2, reportInfoTableNormalTextState);

            var row = reportInfoTable.Rows.Add();

            row.Cells.Add("Report: 1st Quarter PCR");
            row.Cells.Add("");
            row.Cells.Add("Period:January 1, 2015 - March 31, 2016");

            // empty row
            row = reportInfoTable.Rows.Add();
            row.MinRowHeight = 30;

            row = reportInfoTable.Rows.Add();
            row.Cells.Add("Industy Name:Valley City Plating, Inc");
            row.Cells.Add("");
            row.Cells.Add("Submitted Date:April 15,2015 12:15 PM");

            row = reportInfoTable.Rows.Add();
            row.Cells.Add("Industy Number: 40");
            row.Cells.Add("");
            row.Cells.Add("Submitted By: Divid Pelletier");

            row = reportInfoTable.Rows.Add();
            row.Cells.Add("Address: 3353 N.W. 51 Street \n\r Grand Rapids, MI 02334");
            row.Cells.Add("");
            row.Cells.Add("Title: Enrionment Compliance Manager");
        }

        private static void AttachmentsTable(Page pdfPage, TextState reportInfoTableBoldTextState, TextState reportInfoTableNormalTextState)
        {
            var attachmentsTable = new Aspose.Pdf.Table();
            attachmentsTable.DefaultCellPadding = new Aspose.Pdf.MarginInfo(2, 3, 2, 3);
            pdfPage.Paragraphs.Add(attachmentsTable);

            attachmentsTable.Margin.Top = 20;
            attachmentsTable.ColumnWidths = "33% 33% 33%";
            var row = attachmentsTable.Rows.Add();

            var cell = row.Cells.Add("Attachments", reportInfoTableBoldTextState);
            cell.ColSpan = 3;

            row = attachmentsTable.Rows.Add();
            cell = row.Cells.Add("These files are also part of the Copy Of Records.", reportInfoTableNormalTextState);
            cell.ColSpan = 3;

            row = attachmentsTable.Rows.Add();
            row.Cells.Add("Original File Name", reportInfoTableBoldTextState);
            row.Cells.Add("System Generated Unqiue File Name", reportInfoTableBoldTextState);
            row.Cells.Add("Attachment Type", reportInfoTableBoldTextState);

            for (int i = 1; i < 5; i++)
            {
                row = attachmentsTable.Rows.Add();
                row.Cells.Add("Lab Analysis Report.pdf");
                row.Cells.Add("Lab Analysis Report1.pdf");
                row.Cells.Add("Lab Analysis Report");
            }
        }

        private static void SignatureTable(Page pdfPage, TextState reportInfoTableBoldTextState, TextState reportInfoTableNormalTextState)
        {
            var signatureStatmentTable = new Aspose.Pdf.Table();
            signatureStatmentTable.DefaultCellPadding = new Aspose.Pdf.MarginInfo(2, 3, 2, 3);
            pdfPage.Paragraphs.Add(signatureStatmentTable);

            signatureStatmentTable.Margin.Top = 20;
            signatureStatmentTable.ColumnWidths = "96%";
            var row = signatureStatmentTable.Rows.Add();

            row.Cells.Add("Signature Statement", reportInfoTableBoldTextState);
            row = signatureStatmentTable.Rows.Add();
            row.Cells.Add("I certifiy.....", reportInfoTableNormalTextState);
        }

        private static void TTOCertificateTable(Page pdfPage, TextState reportInfoTableBoldTextState, TextState reportInfoTableNormalTextState)
        {
            var ttoTextTable = new Aspose.Pdf.Table();
            ttoTextTable.DefaultCellPadding = new Aspose.Pdf.MarginInfo(2, 3, 2, 3);
            pdfPage.Paragraphs.Add(ttoTextTable);

            ttoTextTable.Margin.Top = 20;
            ttoTextTable.ColumnWidths = "96%";
            var row = ttoTextTable.Rows.Add();

            row.Cells.Add("TTO Certification", reportInfoTableBoldTextState);
            row = ttoTextTable.Rows.Add();
            row.Cells.Add(
                          "Based on my inquiry of the person or persons directly responsible for managing compliance with the pretreatment standard for total toxic organics (T.T.O.), I certify that to the best of my knowledge and belief, no dumping of concentrated toxic organics into the wastewater has occurred since the filing of the last analysis report.  I further certify that this company is implementing the solvent management plan submitted to the Control Authority.",
                          reportInfoTableNormalTextState);
        }

        private static void CommentsTable(Page pdfPage, TextState reportInfoTableBoldTextState, TextState reportInfoTableNormalTextState)
        {
            var commentsTextTable = new Aspose.Pdf.Table();
            commentsTextTable.DefaultCellPadding = new Aspose.Pdf.MarginInfo(2, 3, 2, 3);
            pdfPage.Paragraphs.Add(commentsTextTable);

            commentsTextTable.Margin.Top = 20;
            commentsTextTable.ColumnWidths = "96%";
            var row = commentsTextTable.Rows.Add();
            row.Cells.Add("Comments", reportInfoTableBoldTextState);
            row = commentsTextTable.Rows.Add();
            row.Cells.Add("User entered comments here....", reportInfoTableNormalTextState);
        }

        private static void SampleAndResult(Page pdfPage, TextState reportInfoTableBoldTextState)
        {
            var sampleResultsTextTable = new Aspose.Pdf.Table();
            pdfPage.Paragraphs.Add(sampleResultsTextTable);

            sampleResultsTextTable.Margin.Top = 20;

            sampleResultsTextTable.ColumnWidths = "100%";
            var row = sampleResultsTextTable.Rows.Add();
            // Need to be bold
            row.Cells.Add("Samples and Results", reportInfoTableBoldTextState);

            var tableOrder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, 0.1F);

            //Samples and Results table  
            var sampleResultsTable = new Aspose.Pdf.Table();

            sampleResultsTable.Border = tableOrder;
            sampleResultsTable.DefaultCellBorder = tableOrder;
            sampleResultsTable.DefaultCellPadding = new Aspose.Pdf.MarginInfo(3, 3, 3, 3);

            pdfPage.Paragraphs.Add(sampleResultsTable);

            sampleResultsTable.ColumnWidths = "6% 16.2% 8.3% 5.5% 9% 9% 7.3% 7.8% 8% 5.3% 9% 7.4%";

            // Monitoring Point row 
            row = sampleResultsTable.Rows.Add();
            row.BackgroundColor = Aspose.Pdf.Color.LightGray;
            row.Border = tableOrder;
            row.Cells.Add("");
            var cell = row.Cells.Add("Monitoring Point:40");
            cell.ColSpan = 11;

            row = sampleResultsTable.Rows.Add();
            row.BackgroundColor = Aspose.Pdf.Color.LightGray;

            row.Cells.Add("Month");
            row.Cells.Add("Parameter");
            row.Cells.Add("Result");
            row.Cells.Add("MDL");
            row.Cells.Add("Sample Start");
            row.Cells.Add("Sample End");
            row.Cells.Add("Collection Method");
            row.Cells.Add("Lab Sample ID");
            row.Cells.Add("Analyss Method");
            row.Cells.Add("EPA Method");
            row.Cells.Add("Analyss Date");
            row.Cells.Add("Flow");

            //// Simulate data
            for (int i = 1; i < 50; i++)
            {
                row = sampleResultsTable.Rows.Add();
                row.Cells.Add("January");
                row.Cells.Add("Arenic");
                row.Cells.Add("0.92 mg/L");
                row.Cells.Add("126.012");
                row.Cells.Add("1/14/2016 8:00 AM");
                row.Cells.Add("1/15/2016 8:00 AM");
                row.Cells.Add("Comp");
                row.Cells.Add("1245677");
                row.Cells.Add("200.7");
                row.Cells.Add("Yes");
                row.Cells.Add("1/20/2016 08:00 AM");
                row.Cells.Add("0.05 mgd");
            }
        }
    }
}
