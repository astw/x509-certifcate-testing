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

            NestedTable();
            CorPdf();
            LinePdf();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CorPdf();

            LinePdf();
        }


        private void NestedTable()
        {
            //Instantiate Document object
            Aspose.Pdf.Document pdf = new Aspose.Pdf.Document();
            //Create a page in the PDF
            Aspose.Pdf.Page page = pdf.Pages.Add();

            //Create a table
            Aspose.Pdf.Table tab1 = new Aspose.Pdf.Table();

            //Add the table into the paragraphs collection of page
            page.Paragraphs.Add(tab1);

            //Set the column widths of the table
            tab1.ColumnWidths = "100 200";

            //Set the default cell border using BorderInfo instance
            tab1.DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All);

            //Add a row into the table
            Aspose.Pdf.Row row1 = tab1.Rows.Add();

            //Add 1st cell in the row
            row1.Cells.Add("left cell");

            //Add 2nd cell in the row
            Aspose.Pdf.Cell cell2 = row1.Cells.Add();

            //Create a table to be nested with the reference of 2nd cell in the row
            Aspose.Pdf.Table tab2 = new Aspose.Pdf.Table();

            //Add the nested table into the paragraphs collection of the 2nd cell
            cell2.Paragraphs.Add(tab2);

            //Set the column widths of the nested table
            tab2.ColumnWidths = "100 100";

            //Create 1st row in the nested table
            Aspose.Pdf.Row row21 = tab2.Rows.Add();

            //Create 1st cell in the 1st row of the nested table
            Aspose.Pdf.Cell cell21 = row21.Cells.Add("top cell");

            //Set the column span of the 1st cell (in the 1st row of the nested table) to 2
            cell21.ColSpan = 2;

            //Create 2nd row in the nested table
            Aspose.Pdf.Row row22 = tab2.Rows.Add();

            //Create 1st cell in the 2nd row of the nested table
            row22.Cells.Add("left bottom cell");

            //Create 2nd cell in the 2nd row of the nested table
            row22.Cells.Add("right bottom cell");


            var tick = DateTime.Now.Ticks;
            pdf.Save($"C:\\work\\temp\\reprot_nested_{tick}.pdf");

        }

        private void LinePdf()
        {
            // Create Document instance
            Document doc = new Document();

            var page = doc.Pages.Add();
            page.SetPageSize(PageSize.LetterWidth, PageSize.LetterHeight);
            page.PageInfo.IsLandscape = true;
            page.PageInfo.Margin.Top = 5;
            page.PageInfo.Margin.Left = 5;
            page.PageInfo.Margin.Bottom = 5;
            page.PageInfo.Margin.Right = 5;


            var graph = new Aspose.Pdf.Drawing.Graph((float)page.PageInfo.Width, (float)page.PageInfo.Height);

            // left line
            var line2 = new Aspose.Pdf.Drawing.Line(new float[] { (float)page.Rect.LLX, (float)(page.PageInfo.Height - page.PageInfo.Width + 10), (float)page.Rect.LLX, (float)page.Rect.URY });

            // top line
            var line3 = new Aspose.Pdf.Drawing.Line(new float[] { (float)page.Rect.LLX, (float)page.Rect.URY, (float)page.PageInfo.Height - 10, (float)page.Rect.URY });

            // right line 
            var line4 = new Aspose.Pdf.Drawing.Line(new float[] { (float)page.MediaBox.URY - 10, (float)(page.PageInfo.Height - page.PageInfo.Width + 10), (float)page.MediaBox.URY - 10, (float)page.Rect.URY });

            // bottom line 
            var line5 = new Aspose.Pdf.Drawing.Line(new float[] { (float)page.Rect.LLX, (float)(page.PageInfo.Height - page.PageInfo.Width + 10), (float)page.MediaBox.URY - 10, (float)(page.PageInfo.Height - page.PageInfo.Width + 10) });

            graph.Shapes.Add((line2));
            graph.Shapes.Add((line3));
            graph.Shapes.Add((line4));
            graph.Shapes.Add((line5));

            page.Paragraphs.Add(graph);

            // save the PDF file  
            var tick = DateTime.Now.Ticks;
            doc.Save($"C:\\work\\temp\\reprot_line_{tick}.pdf");

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
            HeaderTable(pdfPage);

            // footer part 
            FootTable(pdfPage, headTableNormalTextState);

            Aspose.Pdf.Text.TextState reportInfoTableBoldTextState = new Aspose.Pdf.Text.TextState("Arial", true, false);
            reportInfoTableBoldTextState.FontSize = 12;
            Aspose.Pdf.Text.TextState reportInfoTableNormalTextState = new Aspose.Pdf.Text.TextState("Arial", false, false);
            reportInfoTableNormalTextState.FontSize = 10;

            // report info part  
            ReportInfoTable(pdfPage, reportInfoTableNormalTextState, reportInfoTableBoldTextState);

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

        private static void HeaderTable(Page pdfPage)
        {
            Aspose.Pdf.Table headerTable = new Aspose.Pdf.Table();

            pdfPage.Header = new Aspose.Pdf.HeaderFooter();
            pdfPage.Header.Paragraphs.Add(headerTable);
            //page1.Paragraphs.Add(headerTable);

            Aspose.Pdf.Text.TextState headTableNormalTextState = new Aspose.Pdf.Text.TextState("Arial", false, false);
            headTableNormalTextState.FontSize = 6;
            headTableNormalTextState.FontStyle = FontStyles.Bold;
            headTableNormalTextState.ForegroundColor = Aspose.Pdf.Color.Gray;

            headerTable.ColumnWidths = "33.3% 33.3% 33.3%";
            headerTable.DefaultCellPadding = new Aspose.Pdf.MarginInfo(2, 3, 2, 3);
            headerTable.Broken = Aspose.Pdf.TableBroken.None;
            headerTable.Margin.Top = 5f;
            headerTable.SetColumnTextState(0, headTableNormalTextState);
            headerTable.SetColumnTextState(1, headTableNormalTextState);
            headerTable.SetColumnTextState(2, headTableNormalTextState);

            Row row = headerTable.Rows.Add();
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

        private static void ReportInfoTable(Page pdfPage, TextState reportInfoTableNormalTextState, TextState reportInfoTableBoldTextState)
        {
            var reportInfoTable = new Aspose.Pdf.Table();

            //var tableOrder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.Top | Aspose.Pdf.BorderSide.Bottom, 1F);
            //reportInfoTable.Border = tableOrder;
            //reportInfoTable.BackgroundColor = Aspose.Pdf.Color.LightGray; 

            reportInfoTable.Margin.Top = 20f;

            pdfPage.Paragraphs.Add(reportInfoTable);
            reportInfoTable.ColumnWidths = "40% 20% 40%";
            reportInfoTable.DefaultCellPadding = new Aspose.Pdf.MarginInfo(2, 3, 2, 3);
            reportInfoTable.Broken = Aspose.Pdf.TableBroken.None;


            //TODO
            //reportInfoTable.DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, 0.1F);


            reportInfoTable.SetColumnTextState(0, reportInfoTableNormalTextState);
            reportInfoTable.SetColumnTextState(1, reportInfoTableNormalTextState);
            reportInfoTable.SetColumnTextState(2, reportInfoTableNormalTextState);

            var row = reportInfoTable.Rows.Add();
            //var cell = row.Cells.Add("Report: 1st Quarter PCR");
            var cell = row.Cells.Add("");
            var reportNameInnerTable = new Aspose.Pdf.Table();
            var innerRow = reportNameInnerTable.Rows.Add();
            reportNameInnerTable.ColumnWidths = "15% 85%";
            innerRow.Cells.Add("Report:", reportInfoTableBoldTextState);
            innerRow.Cells.Add("1st Quarter PCR");
            cell.Paragraphs.Add(reportNameInnerTable);


            row.Cells.Add("");
            //row.Cells.Add("Period:January 1, 2015 - March 31, 2016"); 
            cell = row.Cells.Add();

            // nested table 
            var innerTable = new Aspose.Pdf.Table();
            innerRow = innerTable.Rows.Add();
            innerTable.ColumnWidths = "15% 85%";
            innerRow.Cells.Add("Period:", reportInfoTableBoldTextState);
            innerRow.Cells.Add("January 1, 2015 - March 31, 2016");

            cell.Paragraphs.Add(innerTable);


            // empty row
            row = reportInfoTable.Rows.Add();
            row.MinRowHeight = 30;

            row = reportInfoTable.Rows.Add();
            //row.Cells.Add("Industy Name:Valley City Plating, Inc");
            cell = row.Cells.Add("");
            //TODO

            var industryNameTable = new Aspose.Pdf.Table();
            innerRow = industryNameTable.Rows.Add();
            industryNameTable.ColumnWidths = "30% 70%";

            innerRow.Cells.Add("Industy Name:", reportInfoTableBoldTextState);
            innerRow.Cells.Add("Valley City Plating, Inc");

            cell.Paragraphs.Add(industryNameTable);

            row.Cells.Add("");
            //row.Cells.Add("Submitted Date:April 15,2015 12:15 PM"); 

            cell = row.Cells.Add();
            var submittedDateTable = new Aspose.Pdf.Table();
            innerRow = submittedDateTable.Rows.Add();
            submittedDateTable.ColumnWidths = "35% 65%";
            innerRow.Cells.Add("Submitted Date:", reportInfoTableBoldTextState);
            innerRow.Cells.Add("April 15,2015 12:15 PM");

            cell.Paragraphs.Add(submittedDateTable);


            row = reportInfoTable.Rows.Add();
            //row.Cells.Add("Industy Number: 40"); 
            cell = row.Cells.Add();
            var industryNumberTable = new Aspose.Pdf.Table();
            innerRow = industryNumberTable.Rows.Add();
            industryNumberTable.ColumnWidths = "35% 65%";
            innerRow.Cells.Add("Industy Number:", reportInfoTableBoldTextState);
            innerRow.Cells.Add("40");

            cell.Paragraphs.Add(industryNumberTable);


            row.Cells.Add("");
            //row.Cells.Add("Submitted By: Divid Pelletier");
            cell = row.Cells.Add();
            var submittedByTable = new Aspose.Pdf.Table();
            innerRow = submittedByTable.Rows.Add();
            submittedByTable.ColumnWidths = "30% 70%";
            innerRow.Cells.Add("Submitted By:", reportInfoTableBoldTextState);
            innerRow.Cells.Add("Divid Pelletier");

            cell.Paragraphs.Add(submittedByTable);

            row = reportInfoTable.Rows.Add();
            // row.Cells.Add("Address: 3353 N.W. 51 Street \n\r Grand Rapids, MI 02334");
            cell = row.Cells.Add("");
            var addressTable = new Aspose.Pdf.Table();
            innerRow = addressTable.Rows.Add();
            addressTable.ColumnWidths = "20% 80%";
            innerRow.Cells.Add("Address:", reportInfoTableBoldTextState);
            innerRow.Cells.Add("3353 N.W. 51 Street Grand Rapids, MI 02334");

            cell.Paragraphs.Add(addressTable);

            row.Cells.Add("");
            //row.Cells.Add("Title: Enrionment Compliance Manager");
            cell = row.Cells.Add();
            var titleTable = new Aspose.Pdf.Table();
            innerRow = titleTable.Rows.Add();
            titleTable.ColumnWidths = "12% 88%";
            innerRow.Cells.Add("Title:", reportInfoTableBoldTextState);
            innerRow.Cells.Add("Enrionment Compliance Manager");

            cell.Paragraphs.Add(titleTable);
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
