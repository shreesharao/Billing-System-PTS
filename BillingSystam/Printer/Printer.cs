using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BillingSystem.Utility;


//pdfnet
//using pdftron.PDF;
//using pdftron;
//using pdftron.SDF;
//using pdftron.Common;

using BillingSystem.Entities;

//NReco
using NReco.PdfGenerator;

namespace BillingSystam.Printer
{
   public class Printer
    {
      //  private static pdftron.PDFNetLoader pdfNetLoader = pdftron.PDFNetLoader.Instance();

       public Printer()
        {
          //  PDFNet.Initialize();

         //   string S = Path.GetFullPath(@"packages\PDFNet.6.5.1.31640\lib\net40\PDFNet.dll");
         //   HTML2PDF.SetModulePath(Path.GetFullPath(@"C:\Users\Admin\Documents\Visual Studio 2012\Ramprasad\BillingSystam\packages\PDFNet.6.5.1.31640\lib\net40\PDFNet.dll"));
        }

       public void Print(List<SalesEntity> entity,string fileName)
       {
           try
           {
               string html = BuilTable(entity);

           //    PDFNetPrint(html);

               nreco(html,fileName);


           }
           catch (Exception ex)
           {
               throw;
           }
          

       }

       #region BuilTable


       private string BuilTable(List<SalesEntity> lstentity)
        {
           int totalAmount = 0;
           int i = 0;
           string vat=string.Empty;
           double CGST = 0;
           double SGST = 0;
           string invoiceNumber=string.Empty;
           string deliveryNote=string.Empty;
           string buyer=string.Empty;
           string adress = string.Empty;
           double grandTotal = 0;
           
          // double vatAmount = 0;
           double taxAmount = 0;
           StringBuilder tableLayout = new StringBuilder();
           try
           {
               StringBuilder tableItemAndRate = new StringBuilder();

               tableItemAndRate.Append("<table width ='100%' class='tableItemAndRate'>");
               tableItemAndRate.Append("<tr><td>Sl No.</td><td>Description of goods</td><td>CGST%</td><td>SGST%</td><td>Quantity</td><td>Rate</td><td>Per</td><td>Disc%</td><td style='border-right-style:none;'>Amount</td></tr>");

               foreach (SalesEntity entity in lstentity)
               {
                   totalAmount = totalAmount + (System.Convert.ToInt32(entity.Price) * entity.Count);
                 //  vat = entity.Vat;
                   CGST = Convert.ToDouble(entity.CGST);
                   SGST = Convert.ToDouble(entity.SGST);
                   invoiceNumber = entity.InvoiceNumWithYear;
                   deliveryNote=entity.DelNote;
                   buyer = entity.Buyer;
                   adress = entity.Address;

                   //to display the total count
                   i = i + entity.Count;

                   tableItemAndRate.Append("<tr class='tdNoBorder'>");
                   tableItemAndRate.Append(string.Format("{0}{1}{2}", "<td>", i.ToString(), "</td>"));
                   tableItemAndRate.Append(string.Format("{0}{1}{2}", "<td>", entity.SerialItemName, "</td>"));
                   //tableItemAndRate.Append(string.Format("{0}{1}{2}", "<td>", ((i>1==true)? "":(entity.Vat+"%")), "</td>"));
                   tableItemAndRate.Append(string.Format("{0}{1}{2}", "<td>", ((i > 1 == true) ? "" : (entity.CGST + "%")), "</td>"));
                   tableItemAndRate.Append(string.Format("{0}{1}{2}", "<td>", ((i > 1 == true) ? "" : (entity.SGST + "%")), "</td>"));
                   tableItemAndRate.Append(string.Format("{0}{1}{2}", "<td>", entity.Count, "</td>"));
                   tableItemAndRate.Append(string.Format("{0}{1}{2}", "<td>", entity.Price, "</td>"));
                   tableItemAndRate.Append(string.Format("{0}{1}{2}", "<td>", entity.Price, "</td>"));
                   tableItemAndRate.Append(string.Format("{0}{1}{2}", "<td>", "&nbsp;", "</td>"));
                   tableItemAndRate.Append(string.Format("{0}{1}{2}", "<td style='border-right-style:none;'>", (System.Convert.ToInt32(entity.Price) * entity.Count), "</td>"));
                   tableItemAndRate.Append("</tr>");


               }
               taxAmount = System.Convert.ToDouble(totalAmount) *(System.Convert.ToDouble((CGST+SGST))/100);
               grandTotal = System.Convert.ToDouble(totalAmount) + taxAmount;

               taxAmount = Math.Round(taxAmount, 2);
               grandTotal = Math.Round(grandTotal, 2);

               tableItemAndRate.Append(string.Format("{0}{1}{2}{3}", "<tr><td>&nbsp;</td><td><b>Output Tax @", (CGST + SGST), "%</b></td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td></td><td style='border-right-style:none;'>", taxAmount, "</td></tr>"));
               tableItemAndRate.Append(string.Format("{0}{1}{2}{3}", "<tr><td>&nbsp;</td><td>Total</td><td>&nbsp;</td><td></td><td>", (i), "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td style='border-right-style:none;'>", grandTotal.ToString(), "</td></tr>"));

               tableItemAndRate.Append("</table>");


               StringBuilder tableInvoiceNumAndOthers = new StringBuilder();
               tableInvoiceNumAndOthers.Append("<table width='100%' class='tableInvoiceNumAndOthers'>");
               tableInvoiceNumAndOthers.Append(string.Format("{0}{1}{2}{3}{4}", "<tr><td>Invoice No.<br /><b>", invoiceNumber, "</b></td><td>Dated<br /><b>", DateTime.Now.ToString("dd/MMM/yyyy"), "</b></td></tr>"));
               tableInvoiceNumAndOthers.Append(string.Format("{0}{1}{2}","<tr><td>Delivery Note<br /><b>", deliveryNote, "</b></td><td>Mode/Terms of payment</td></tr>"));
               tableInvoiceNumAndOthers.Append("<tr><td>Supplier&#39;s Ref</td><td>Other References</td></tr>");
               tableInvoiceNumAndOthers.Append("<tr><td>Buyer&#39;s Order No.</td><td>Order Date</td></tr>");
               tableInvoiceNumAndOthers.Append("<tr><td>Dispatch document No</td><td>Delivey Date</td></tr>");
               tableInvoiceNumAndOthers.Append("<tr><td>Dispached through </td><td>Destination</td></tr>");
               tableInvoiceNumAndOthers.Append("<tr><td style='border-bottom-style:none;' colspan='2'>Terms of Delivery</td></tr>");
               tableInvoiceNumAndOthers.Append("</table>");




               StringBuilder tableAmountInWords = new StringBuilder();

               tableAmountInWords.Append("<table width ='100%' class='tableAmountInWords'>");
               tableAmountInWords.Append("<tr><td>Amount chargable(in words)</td><td>&nbsp;</td><td>&nbsp;</td><td style='text-align:right'>E&amp;O.E</td></tr>");
               tableAmountInWords.Append(string.Format("{0}{1}{2}", "<tr><td><b>INR ", NumberToWords(System.Convert.ToInt32(grandTotal)), "</b></td><td>Tax%</td><td>Assessable Value</td><td>Tax Amount</td></tr>"));
               tableAmountInWords.Append("<tr><td>Tax Amount(in words)</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>");
               tableAmountInWords.Append(string.Format("{0}{1}{2}", "<tr><td><b>INR ", NumberToWords(System.Convert.ToInt32(taxAmount)), "</b></td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>"));
               tableAmountInWords.Append("</table>");




               StringBuilder tableBankAndDeclaration = new StringBuilder();

               tableBankAndDeclaration.Append("<table width ='100%' class='tableBankAndDeclaration'>");
               tableBankAndDeclaration.Append("<tr><td>&nbsp;</td><td>&nbsp;</td><td colspan='2'>Company&#39;s Bank Details</td><td>&nbsp;</td></tr>");
               tableBankAndDeclaration.Append("<tr><td>&nbsp;</td><td>&nbsp;</td><td>Bank Name:</td><td><b>Corporation Bank</b></td></tr>");
               tableBankAndDeclaration.Append("<tr><td style='width:200px'>Company&#39;s GSTIN/UAN</td><td><b>:29DZBPS0430P1ZX </b></td><td>A/C No.:</td><td><b>NEFT:560371000106880</b></td></tr>");
               tableBankAndDeclaration.Append("<tr><td style='width:200px'>Company&#39;s PAN</td><td><b>:</b></td><td>Branch &amp; IFS Code:</td><td><b>BH road sagara&amp; CORP0002142</b></td></tr>");
               tableBankAndDeclaration.Append("<tr><td colspan ='2' > Declaration:<br />We declare that this invoice shows the actual price of the gooods described and that all particulars are true and correct.</td><td style='border-left:1px solid black;border-top:1px solid black;text-align:right;border-right-style:none' colspan ='2'> For <b>M/s Pruthvi Total Solutions</b><br /><br />Authorised sigantory</td></tr>");
               tableBankAndDeclaration.Append("</table>");


               

               tableLayout.Append("<html><head>");

               tableLayout.Append("<style>.table1 {border: 1px solid black;border-collapse:collapse;padding:0px}");
               tableLayout.Append(".table1 td{font-size:large}");
               tableLayout.Append(".tableUpper {border-bottom: 1px solid black;border-collapse:collapse;}");
               tableLayout.Append(".tableItemAndRate td{border-right: 1px solid black;border-bottom: 1px solid black;border-collapse:collapse;}");
               tableLayout.Append(".tableInvoiceNumAndOthers td{border-bottom: 1px solid black;border-left: 1px solid black;border-collapse:collapse;}");
               tableLayout.Append(".tableAmountInWords {border-bottom: 0px solid black;border-collapse:collapse;}");
               tableLayout.Append(".tableBankAndDeclaration td{border-right: 0px solid black;border-collapse:collapse;}");
               tableLayout.Append(".tdNoBorder td{border-bottom-style:none;border-collapse:collapse;}");
               tableLayout.Append(".tdWithBorder td{border-bottom: 1px solid black;border-collapse:collapse;}</style>");

               tableLayout.Append("</head><body>");
               tableLayout.Append("<h3 style='text-align:center'>INVOICE</h3>");
               tableLayout.Append("<table id =\"tblMain\" class='table1' width=\"100%\">");

               tableLayout.Append("<tr><td>");
               tableLayout.Append("<table width='100%' class='tableUpper'");
               tableLayout.Append("<tr width=\"100%\"><td style='border-bottom:1px solid black;' width=\"40%\"><b>M/s Pruthvi Total Solutions </b><br />opp MGN Traders new<br/>BH Road Sagara-577401<br/>PH:9901601354,08183229229<br/>GSTIN/UAN:29DZBPS0430P1ZX </td><td width='60%' rowspan ='2'>");

               tableLayout.Append(tableInvoiceNumAndOthers.ToString());

               tableLayout.Append("</td></tr><tr><td><b> Buyer: </b><br>" + buyer + "<br>" + adress + "</td></tr>");
               tableLayout.Append("</table>");

               tableLayout.Append("</td></tr>");

               tableLayout.Append("<tr><td>");
               tableLayout.Append(tableItemAndRate.ToString());
               tableLayout.Append("</td></tr>");

               tableLayout.Append("<tr><td>");
               tableLayout.Append(tableAmountInWords.ToString());
               tableLayout.Append("</td></tr>");

               tableLayout.Append("<tr><td>");
               tableLayout.Append(tableBankAndDeclaration.ToString());
               tableLayout.Append("</td></tr>");

               tableLayout.Append("</table>");
               tableLayout.Append("<h3 style='text-decoration:underline'>TERMS AND CONDITIONS</h3>");
               tableLayout.Append("<h4>");
               tableLayout.Append("1)Goods once sold will not be taken back or exchanged without proper approval.<br />");
               tableLayout.Append("2)Manufacture's warranty only.<br />");
               tableLayout.Append("3)Interest at 24% p.a. will be charged if payment is not received in accordance with the payment particulars of this invoice. <br />");
               tableLayout.Append("4)Warranty on all peripherals/ consumable is as per manufacture's policy.<br />");
               tableLayout.Append("5)No warranty on burnt,physically damaged and track cut items.<br />");
               tableLayout.Append("6)Cheque / d.d. on <span style='text-decoration:underline'>'PRUTHVI TOTAL SOLUTIONS'</span><br />");
               tableLayout.Append("7)All disputes are subject to sagara jurisdiction only. <br />");
               tableLayout.Append("</h4>");
               tableLayout.Append("<h5 style='text-align:center'>This is computer generated invoice</h5>");
               tableLayout.Append("</body></html>");

           }
           catch (Exception)
           {
               
               throw;
           }
            

            return tableLayout.ToString();

        }

       #endregion


       #region PDFNetPrint
       
        //    private void PDFNetPrint(string html)
        //{

        //    try
        //    {
        //        using (PDFDoc doc = new PDFDoc())
        //        {
        //            HTML2PDF converter = new HTML2PDF();
        //            string output_path = @"D:\invoice files\pdfnet";

        //             Add html data
        //            converter.InsertFromHtmlString(html);
        //             Note, InsertFromHtmlString can be mixed with the other Insert methods.

        //            if (converter.Convert(doc))
        //                doc.Save(output_path + "_04.pdf", SDFDoc.SaveOptions.e_linearized);

        //        }
        //    }
        //    catch (PDFNetException e)
        //    {
        //        throw e;
        //    }

        //}

       #endregion


       #region NReco
private void nreco(string content,string fileName)
        {
            try
            {
                string htmlContent = content;
                string directory = @"D:\BillingSystem\Invoice Files";

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string pdfPath = directory + "\\" + fileName + ".pdf";
                //varhtmlContent = String.Format(html);
                var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);
                File.WriteAllBytes(pdfPath, pdfBytes);
                System.Diagnostics.Process.Start(pdfPath);
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
#endregion


       #region number to words
       ///<summary>
/// Method to convert from number to words
///</summary>
///<param name="number"></param>
///<returns></returns>



private string NumberToWords(int number)
        {

                if (number == 0)
                return"zero";

                if (number < 0)
                return"minus " + NumberToWords(Math.Abs(number));

                string words = "";

                if ((number / 1000000) > 0)
                            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
                            }

                if ((number / 1000) > 0)
                            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
                            }

                if ((number / 100) > 0)
                            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
                            }

                if (number > 0)
                            {
                if (words != "")
                words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                words += unitsMap[number];
                else
                                {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                words += "-" + unitsMap[number % 10];
                                }
                            }


                return words;
        }
        #endregion
    }
}
