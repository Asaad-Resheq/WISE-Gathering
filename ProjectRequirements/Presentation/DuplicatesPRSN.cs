using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using ProjectRequirements.Models;
using System.Windows;
using System.Windows.Media;

namespace ProjectRequirements.Presentation
{
    public class DuplicatesPRSN
    {

        // class-level variables to hold some flow document common properties ...
        double lineheight = 25.0;
        double Fontsizenormal = 12;
        double FontsizeLarge = 14;
        double FontsizeextraLarge = 22;
        SolidColorBrush BorderColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#404040"));
        SolidColorBrush backColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#efedf0"));

        public void generateDocument(FlowDocument flowdocument, string projectname,string compnayname,string keyword,string reqText, double percentage,List<Requirement> requirements)
        {
            // variable to hold properties related to table 
            double pt = 2; // the thickness of table left margin
            double brdrt = 0.2; // the thickness of table border..




            // reset flow document blocks
            flowdocument.Blocks.Clear();


            // date...
            string formattedtext = string.Format("{0} : {1}","Date",DateTime.Now.ToString("yyyy/MM/dd"));
            Paragraph p = new Paragraph(new Run(formattedtext));
            p.FontSize = FontsizeextraLarge;
            p.TextAlignment = TextAlignment.Center;
            flowdocument.Blocks.Add(p);

            //projectname | company 
            formattedtext = string.Format("{0} | {1}", projectname, compnayname);
            p = new Paragraph(new Run(formattedtext));
            p.FontSize = FontsizeextraLarge;
            p.TextAlignment = TextAlignment.Center;
            flowdocument.Blocks.Add(p);

            //keyword
            formattedtext = string.Format("{0} : {1}", "Keyword", keyword);
            p = new Paragraph(new Run(formattedtext));
            p.FontSize = FontsizeLarge;
            flowdocument.Blocks.Add(p);

            // requirement
            /*
            formattedtext = string.Format("{0} : {1}", "Requirement", reqText);
            p = new Paragraph(new Run(formattedtext));
            p.FontSize = FontsizeLarge;
            flowdocument.Blocks.Add(p);
            */

            // percentage
            double perc = percentage * 100.0;
            string percdisplay = string.Format("{0} {1}",perc.ToString(),"%");
            formattedtext = string.Format("{0} : {1}", "Duplicate Percentage", percdisplay);
            p = new Paragraph(new Run(formattedtext));
            p.FontSize = FontsizeLarge;
            flowdocument.Blocks.Add(p);

            // list of the duplicate requirements
          
            Table etable = new Table();
            etable.TextAlignment = TextAlignment.Center;
            etable.FontSize = Fontsizenormal;
            etable.Margin = new Thickness(0);
            etable.CellSpacing = 0;
            // add table columns....
            etable.Columns.Add(new TableColumn() { Width = new GridLength(1, GridUnitType.Star) });   
            etable.RowGroups.Add(new TableRowGroup());


            // adding the table header row
            etable.RowGroups[0].Rows.Add(new TableRow());
            TableRow Row = etable.RowGroups[0].Rows[etable.RowGroups[0].Rows.Count - 1];// last added row

            formattedtext = "Stakeholders:";
            Row.Cells.Add(new TableCell(new Paragraph(new Run(formattedtext))) {Background=backColor,  FontWeight = FontWeights.Bold, LineHeight = lineheight, Padding = new Thickness(pt, 0, 0, 0), BorderBrush = BorderColor, BorderThickness = new Thickness(brdrt) });


            // adding the stickholders data...
            var distinctstickholders = requirements
            .Select(x => x.Stickholder).Distinct();

           List<Stickholder> stickholders= distinctstickholders.ToList();
           foreach (var item in stickholders)
           {
               // adding a row foreach  stickholder
               etable.RowGroups[0].Rows.Add(new TableRow());
               Row = etable.RowGroups[0].Rows[etable.RowGroups[0].Rows.Count - 1];// last added row
               Row.Cells.Add(new TableCell(new Paragraph(new Run(item.StickholderName))) { FontWeight = FontWeights.Regular, LineHeight = lineheight, Padding = new Thickness(pt, 0, 0, 0), BorderBrush = BorderColor, BorderThickness = new Thickness(brdrt) });
           }

           flowdocument.Blocks.Add(etable);



            // the requirements table...
           etable = new Table();
           etable.TextAlignment = TextAlignment.Center;
           etable.FontSize = Fontsizenormal;
           etable.Margin = new Thickness(0);
           etable.CellSpacing = 0;
           // add table columns....
           etable.Columns.Add(new TableColumn() { Width = new GridLength(1, GridUnitType.Star) });
           etable.Columns.Add(new TableColumn() { Width = new GridLength(2, GridUnitType.Star) });
           etable.RowGroups.Add(new TableRowGroup());

           // adding the table header row
           etable.RowGroups[0].Rows.Add(new TableRow());
           Row = etable.RowGroups[0].Rows[etable.RowGroups[0].Rows.Count - 1];// last added row

           formattedtext = "Details:";
           Row.Cells.Add(new TableCell(new Paragraph(new Run(formattedtext))) { ColumnSpan=2,Background=backColor,  FontWeight = FontWeights.Bold, LineHeight = lineheight, Padding = new Thickness(pt, 0, 0, 0), BorderBrush = BorderColor, BorderThickness = new Thickness(brdrt) });

            // adding each stickholder requirements...
           foreach (var item in stickholders)
           {
                // find the stickholder requirements...
              var stickholderrequirements=  requirements.Where(x => x.StickholderID == item.ID).ToList();
              string stickholderrequiremetnsdisplayedtext = string.Empty;
              foreach (var reqitem in stickholderrequirements)
              {
                  if (stickholderrequiremetnsdisplayedtext.Length == 0) // no items added yet .. so we just append the new item
                  {
                      stickholderrequiremetnsdisplayedtext += reqitem.ReqText;
                  }
                  else // there is items so we append new line and then the new item
                  {
                      stickholderrequiremetnsdisplayedtext += Environment.NewLine;
                      stickholderrequiremetnsdisplayedtext += reqitem.ReqText;
                  }
              }


              etable.RowGroups[0].Rows.Add(new TableRow());
              Row = etable.RowGroups[0].Rows[etable.RowGroups[0].Rows.Count - 1];// last added row
              Row.Cells.Add(new TableCell(new Paragraph(new Run(item.StickholderName))) { FontWeight = FontWeights.Regular, LineHeight = lineheight, Padding = new Thickness(pt, 0, 0, 0), BorderBrush = BorderColor, BorderThickness = new Thickness(brdrt) });
              Row.Cells.Add(new TableCell(new Paragraph(new Run(stickholderrequiremetnsdisplayedtext))) { FontWeight = FontWeights.Regular, LineHeight = lineheight, Padding = new Thickness(pt, 0, 0, 0), BorderBrush = BorderColor, BorderThickness = new Thickness(brdrt) });
           }




           flowdocument.Blocks.Add(etable);


        }




    }
}
