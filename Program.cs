using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

namespace ConsoleApp3
{
    class Program
    {
        class FraudReport : ReportBuilder
        {
            public override List<Report> Reports
            {
                get => new List<Report>()
                {
                    new Report("/Report Project2/FraudTemplate")
                    {
                        Parameters = new List<ReportParameter>()
                        {
                            new ReportParameter( "loannumber", "11113" )
                        }
                    }
                };
            }
        }

        static void Main(string[] args)
        {
            new FraudReport().Render();
        }
    }
}
