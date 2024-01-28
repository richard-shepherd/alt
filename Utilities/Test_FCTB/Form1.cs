using FastColoredTextBoxNS;
using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Test_FCTB
{
    public partial class Form1 : Form
    {
        //styles
        Style BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        Style RedStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        Style MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);

        public Form1()
        {
            InitializeComponent();

            //generate 200,000 lines of HTML
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 50000; i++)
            {
                sb.AppendLine($"<li line-number={i} id=\"ctl00_TopNavBar_AQL\">");
                sb.AppendLine("<a id=\"ctl00_TopNavBar_ArticleQuestion\" class=\"fly highlight\" href=\"#_comments\">Ask a Question about this article</a></li>");
                sb.AppendLine("<li class=\"heading\">Quick Answers</li>");
                sb.AppendLine("<li><a id=\"ctl00_TopNavBar_QAAsk\" class=\"fly\" href=\"/Questions/ask.aspx\">Ask a Question</a></li>");
            }

            //assign to FastColoredTextBox
            fctb.Text = sb.ToString();
            fctb.IsChanged = false;
            fctb.ClearUndo();
            //set delay interval (10 ms)
            fctb.DelayedEventsInterval = 10;
        }

        private void fctb_VisibleRangeChangedDelayed(object sender, EventArgs e)
        {
            //highlight only visible area of text
            HTMLSyntaxHighlight(fctb.VisibleRange);
        }

        private void HTMLSyntaxHighlight(Range range)
        {
            label1.Text = $"{range.FromLine}-{range.ToLine}";
            //clear style of changed range
            //range.ClearStyle(BlueStyle, MaroonStyle, RedStyle);
            //tag brackets highlighting
            range.SetStyle(BlueStyle, @"<|/>|</|>");
            //tag name
            range.SetStyle(MaroonStyle, @"<(?<range>[!\w]+)");
            //end of tag
            range.SetStyle(MaroonStyle, @"</(?<range>\w+)>");
            //attributes
            range.SetStyle(RedStyle, @"(?<range>\S+?)='[^']*'|(?<range>\S+)=""[^""]*""|(?<range>\S+)=\S+");
            //attribute values
            range.SetStyle(BlueStyle, @"\S+?=(?<range>'[^']*')|\S+=(?<range>""[^""]*"")|\S+=(?<range>\S+)");
        }
    }
}
