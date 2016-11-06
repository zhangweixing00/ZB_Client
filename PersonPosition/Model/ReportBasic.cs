using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace PersonPosition.Model
{
    public abstract class ReportBasic
    {
        private string MainTitleParameterName = "MainTitle";
        private string LeftSubTitleParameterName = "LeftSubTitle";
        private string RightSubTitleParameterName = "RightSubTitle";

        //报表模板
        public ReportClass Report;
        //报表参数域
        public ParameterFields PFields;
        //报表数据集
        public DataSetReport DataSetReport;

        public ReportBasic(string MainTitle, string LeftSubTitle, string RightSubTitle)
        {
            this.PFields = new ParameterFields();

            ParameterField PFMainTitle = new ParameterField();
            ParameterField PFLeftSubTitle = new ParameterField();
            ParameterField PFRightSubTitle = new ParameterField();
            PFMainTitle.ParameterFieldName = MainTitleParameterName;
            PFLeftSubTitle.ParameterFieldName = LeftSubTitleParameterName;
            PFRightSubTitle.ParameterFieldName = RightSubTitleParameterName;

            ParameterDiscreteValue DVMainTitle = new ParameterDiscreteValue();
            ParameterDiscreteValue DVLeftSubTitle = new ParameterDiscreteValue();
            ParameterDiscreteValue DVRightSubTitle = new ParameterDiscreteValue();
            DVMainTitle.Value = MainTitle;
            DVLeftSubTitle.Value = LeftSubTitle;
            DVRightSubTitle.Value = RightSubTitle;

            PFMainTitle.CurrentValues.Add(DVMainTitle);
            PFLeftSubTitle.CurrentValues.Add(DVLeftSubTitle);
            PFRightSubTitle.CurrentValues.Add(DVRightSubTitle);

            PFields.Add(PFMainTitle);
            PFields.Add(PFLeftSubTitle);
            PFields.Add(PFRightSubTitle);
        }


    }
}
