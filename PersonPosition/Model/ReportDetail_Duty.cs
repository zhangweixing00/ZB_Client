using System;
using System.Collections.Generic;
using System.Text;

using PersonPosition.Report;

using CrystalDecisions.Shared;

namespace PersonPosition.Model
{
    public class ReportDetail_Duty : ReportBasic
    {
        public ReportDetail_Duty(string MainTitle, string LeftSubTitle, string RightSubTitle,string Class1,string Class2,string Class3,string Class4) : base(MainTitle, LeftSubTitle, RightSubTitle)
        {
            ParameterField PClass1 = new ParameterField();
            ParameterField PClass2 = new ParameterField();
            ParameterField PClass3 = new ParameterField();
            ParameterField PClass4 = new ParameterField();
            PClass1.ParameterFieldName = "Class1";
            PClass2.ParameterFieldName = "Class2";
            PClass3.ParameterFieldName = "Class3";
            PClass4.ParameterFieldName = "Class4";
            ParameterDiscreteValue DVClass1 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVClass2 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVClass3 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVClass4 = new ParameterDiscreteValue();
            DVClass1.Value = Class1;
            DVClass2.Value = Class2;
            DVClass3.Value = Class3;
            DVClass4.Value = Class4;

            PClass1.CurrentValues.Add(DVClass1);
            PClass2.CurrentValues.Add(DVClass2);
            PClass3.CurrentValues.Add(DVClass3);
            PClass4.CurrentValues.Add(DVClass4);

            PFields.Add(PClass1);
            PFields.Add(PClass2);
            PFields.Add(PClass3);
            PFields.Add(PClass4);

            base.Report = new Detail_Duty();
            base.DataSetReport = new DataSetReport();
            base.Report.SetDataSource(DataSetReport);
        }
    }
}
