using System;
using System.Collections.Generic;
using System.Text;

using PersonPosition.Report;

using CrystalDecisions.Shared;

namespace PersonPosition.Model
{
    public class ReportStatistic_Duty : ReportBasic
    {
        public ReportStatistic_Duty(string MainTitle, string LeftSubTitle, string RightSubTitle,string FirstClass,string SecondClass,string ThirdClass) : base(MainTitle, LeftSubTitle, RightSubTitle)
        {
            ParameterField PFMainTitle = new ParameterField();
            ParameterField PFLeftSubTitle = new ParameterField();
            ParameterField PFRightSubTitle = new ParameterField();
            PFMainTitle.ParameterFieldName = "FirstClass";
            PFLeftSubTitle.ParameterFieldName = "SecondClass";
            PFRightSubTitle.ParameterFieldName = "ThirdClass";

            ParameterDiscreteValue DVMainTitle = new ParameterDiscreteValue();
            ParameterDiscreteValue DVLeftSubTitle = new ParameterDiscreteValue();
            ParameterDiscreteValue DVRightSubTitle = new ParameterDiscreteValue();
            DVMainTitle.Value = FirstClass;
            DVLeftSubTitle.Value = SecondClass;
            DVRightSubTitle.Value = ThirdClass;

            PFMainTitle.CurrentValues.Add(DVMainTitle);
            PFLeftSubTitle.CurrentValues.Add(DVLeftSubTitle);
            PFRightSubTitle.CurrentValues.Add(DVRightSubTitle);

            base.PFields.Add(PFMainTitle);
            base.PFields.Add(PFLeftSubTitle);
            base.PFields.Add(PFRightSubTitle);

            base.Report = new Statistic_Duty();
            base.DataSetReport = new DataSetReport();
            base.Report.SetDataSource(DataSetReport);
        }
    }
}
