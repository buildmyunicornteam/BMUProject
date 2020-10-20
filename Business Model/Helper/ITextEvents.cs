using System;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Business_Model.Helper
{
    public class ITextEvents : PdfPageEventHelper
    {
        //private DAL.AccountContext db = new DAL.AccountContext();
        public Decimal ProgressValue = 0.00m;

        public ITextEvents(Decimal ProgressValue)
        {
            this.ProgressValue = ProgressValue;


        }
        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;

        #region Fields
        private string _header;
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion

        public override void OnOpenDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 40);
                footerTemplate = cb.CreateTemplate(50, 30);
            }
            catch (DocumentException de)
            {
                string errror = de.ToString();
            }
            catch (System.IO.IOException ioe)
            {
                string errror = ioe.ToString();
            }

        }

        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            //var Account = (Account)HttpContext.Current.Session["access"];
            //if (Account == null)
           // {
             //   var sql = @"SELECT TOP 1 * FROM account a JOIN employees e 
             //        ON a.employee_id = e.id inner join Role r on r.id = a.role_id  WHERE a.employee_id = {0}";
              //  Account = db.Accounts.SqlQuery(sql, employee_id).FirstOrDefault<Account>();
           // }
            base.OnEndPage(writer, document);
            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font SmallFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 17f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            var logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Content/images/logo.png"));
            
            logo.SetAbsolutePosition(-10, -10);

            Phrase heading1 = new Phrase("Get the idea out of my head", baseFontBig);
            heading1.Font.SetColor(60, 60, 60);
            //Phrase heading2 = new Phrase(Account.name.ToString(), baseFontNormal);
            // Phrase EmployeeName = new Phrase("[Name]: "+Account.name.ToString()+"", baseFontNormal);
            Phrase EmployeeID = new Phrase("Empty01", baseFontNormal);
            Phrase EmployeeManger = new Phrase("Empty02", baseFontNormal);
            Phrase EmployeeEmail = new Phrase("Empty 03", baseFontNormal);


            PdfPTable pdfTab = new PdfPTable(3);

            PdfPCell Celllogo = new PdfPCell(logo);
            Celllogo.PaddingTop = -15;
            Celllogo.PaddingLeft = -25;
            PdfPCell CellHeading1 = new PdfPCell(heading1);
            CellHeading1.PaddingTop = 10;
            PdfPCell CellEmployee = new PdfPCell();

            //  CellEmployeeNumber.AddElement(multiple) ;

            // CellEmployeeNumber.AddElement(EmployeeID);
            // CellEmployeeNumber.AddElement(EmployeeManger);
            //CellEmployeeNumber.AddElement(EmployeeEmail);

            string text = "Page " + writer.PageNumber + " of ";
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 8);
                cb.SetTextMatrix(document.PageSize.GetRight(100), document.PageSize.GetBottom(30));
                cb.ShowText(text);
                cb.EndText();
                float len = bf.GetWidthPoint(text, 8);
                cb.AddTemplate(footerTemplate, document.PageSize.GetRight(100) + len, document.PageSize.GetBottom(30));
            }
            string footertext = "Date:-" + DateTime.Now;
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 8);
                cb.SetTextMatrix(document.PageSize.GetLeft(40), document.PageSize.GetBottom(30));
                cb.ShowText(footertext);
                cb.EndText();
                float len = bf.GetWidthPoint(footertext, 8);
                cb.AddTemplate(footerTemplate, document.PageSize.GetLeft(40) + len, document.PageSize.GetBottom(30));
            }
            string footertext2 = "";
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 8);
                cb.SetTextMatrix(document.PageSize.GetLeft(43), document.PageSize.GetBottom(18));
                cb.ShowText(footertext2);
                cb.EndText();
                float len = bf.GetWidthPoint(footertext, 8);
                cb.AddTemplate(footerTemplate, document.PageSize.GetLeft(40) + len, document.PageSize.GetBottom(30));
            }
            PdfPCell pdfCellEmpty = new PdfPCell();
            PdfPCell pdfHeaderCellDate = new PdfPCell(new Phrase("" + "", baseFontNormal));
            //  PdfPCell pdfHeaderCellDate = new PdfPCell(new Phrase("Date:" +PrintTime.ToShortDateString(), baseFontBig));
            //PdfPCell pdfHeaderCellh2 = new PdfPCell(heading2);
            PdfPCell pdfbarcode = new PdfPCell();
            PdfPCell pdfHeaderCellTime = new PdfPCell(new Phrase("Fill up (%) = " + this.ProgressValue, baseFontNormal));
            //PdfPCell pdfHeaderCellTime = new PdfPCell(new Phrase("TIME:" + string.Format("{0:t}", DateTime.Now), baseFontBig));


            Celllogo.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfHeaderCellDate.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfHeaderCellDate.PaddingBottom = -39;
            CellHeading1.HorizontalAlignment = Element.ALIGN_CENTER;
            //pdfHeaderCellh2.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfHeaderCellTime.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfHeaderCellTime.PaddingBottom = -39;

            Celllogo.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfHeaderCellDate.VerticalAlignment = Element.ALIGN_MIDDLE;
            CellHeading1.VerticalAlignment = Element.ALIGN_MIDDLE;
            //pdfHeaderCellh2.VerticalAlignment = Element.ALIGN_MIDDLE;
            CellEmployee.VerticalAlignment = Element.ALIGN_MIDDLE;
            CellEmployee.HorizontalAlignment = Element.ALIGN_RIGHT;

            pdfHeaderCellTime.VerticalAlignment = Element.ALIGN_MIDDLE;

            Celllogo.Border = 0;
            pdfHeaderCellDate.Border = 0;
            CellHeading1.Border = 0;
            CellEmployee.Border = 0;
            // CellEmployeeNumber.Border = 0;


            //pdfHeaderCellh2.Border = 0;
            pdfHeaderCellTime.Border = 0;
            pdfCellEmpty.Border = 0;
            pdfHeaderCellDate.FixedHeight = 18f;


            pdfTab.AddCell(Celllogo);
            pdfTab.AddCell(CellHeading1);
            pdfTab.AddCell(CellEmployee);

            pdfTab.CompleteRow();
            pdfTab.AddCell(pdfCellEmpty);
            pdfTab.AddCell(pdfCellEmpty);
            pdfTab.AddCell(pdfCellEmpty);
            pdfTab.CompleteRow();

            pdfTab.AddCell(pdfHeaderCellDate);
            pdfTab.AddCell(pdfCellEmpty);
            pdfTab.AddCell(pdfHeaderCellTime);

            pdfTab.TotalWidth = document.PageSize.Width - 80f;
            pdfTab.WidthPercentage = 70;
            //pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;    

            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
            //set pdfContent value

            //Move the pointer and draw line to separate header section from rest of page
            cb.MoveTo(40, document.PageSize.Height - 100);
            cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 100);
            cb.Stroke();

            //Move the pointer and draw line to separate footer section from rest of page
            cb.MoveTo(40, document.PageSize.GetBottom(50));
            cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(50));
            cb.Stroke();
        }

        public override void OnCloseDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnCloseDocument(writer, document);

            headerTemplate.BeginText();
            headerTemplate.SetFontAndSize(bf, 8);
            headerTemplate.SetTextMatrix(0, 0);
            headerTemplate.ShowText((writer.PageNumber - 1).ToString());
            headerTemplate.EndText();

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 8);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber - 1).ToString());
            footerTemplate.EndText();
        }
    }
}