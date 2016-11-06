using System;
using System.Collections.Generic;
using System.Text;

using PersonPosition.Report;

using CrystalDecisions.Shared;

namespace PersonPosition.Model
{
    public class ReportAnalysics_Collect : ReportBasic
    {
        public ReportAnalysics_Collect(string MainTitle, string LeftSubTitle, string RightSubTitle) : base(MainTitle, LeftSubTitle, RightSubTitle)
        {
            base.Report = new Analysics_Collect();
            base.DataSetReport = new DataSetReport();
            base.Report.SetDataSource(base.DataSetReport);
        }
    }
}
