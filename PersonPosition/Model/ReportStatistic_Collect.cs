using System;
using System.Collections.Generic;
using System.Text;

using PersonPosition.Report;

using CrystalDecisions.Shared;

namespace PersonPosition.Model
{
    public class ReportStatistic_Collect : ReportBasic
    {
        public ReportStatistic_Collect(string MainTitle, string LeftSubTitle, string RightSubTitle) : base(MainTitle, LeftSubTitle, RightSubTitle)
        {
            base.Report = new Statistic_Collect();
            base.DataSetReport = new DataSetReport();
            base.Report.SetDataSource(base.DataSetReport);
        }
    }
}
