using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Reporting.WinForms;

namespace ConsoleApp3
{
    public abstract class ReportBuilder
    {
        public abstract List<Report> Reports { get; set; }
        public class Report
        {
            public Report(string path)
            {
                Path = path;
            }
            public string Path { get; set; }
            public List<ReportParameter> Parameters { get; set; }
        }

        private ServerReport _serverReport;
        public ReportBuilder()
        {
            var viewer = new ReportViewer();
            viewer.ServerReport.ReportServerUrl = new Uri("http://sql01/ReportServer");
            var credentials =
                System.Net.CredentialCache.DefaultCredentials;
            var rsCredentials =
                viewer.ServerReport.ReportServerCredentials;
            rsCredentials.NetworkCredentials = credentials;
            viewer.ProcessingMode = ProcessingMode.Remote;
            _serverReport = viewer.ServerReport;
        }

        public void Render()
        {
            foreach (var report in Reports)
            {
                _serverReport.ReportPath = report.Path;
                _serverReport.SetParameters(report.Parameters);
                var mybytes = _serverReport.Render("WORD");
                using (var fs = File.Create($"{report.Path.Split('/').LastOrDefault()}.doc"))
                    fs.Write(mybytes, 0, mybytes.Length);
                
            }
        }
        
    }
}