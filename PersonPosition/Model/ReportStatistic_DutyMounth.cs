﻿using System;
using System.Collections.Generic;
using System.Text;

using PersonPosition.Report;

using CrystalDecisions.Shared;

namespace PersonPosition.Model
{
    public class ReportStatistic_DutyMounth : ReportBasic
    {
        public ReportStatistic_DutyMounth(string MainTitle, string LeftSubTitle, string RightSubTitle,string DN1,string DN2,string DN3,string DN4,string DN5,string DN6,string DN7,string DN8,string DN9,string DN10,string DN11,string DN12,string DN13,string DN14,string DN15,string DN16,string DN17,string DN18,string DN19,string DN20,string DN21,string DN22,string DN23,string DN24,string DN25,string DN26,string DN27,string DN28,string DN29,string DN30,string DN31)
            : base(MainTitle, LeftSubTitle, RightSubTitle)
        {
            ParameterField PDN1 = new ParameterField();
            ParameterField PDN2 = new ParameterField();
            ParameterField PDN3 = new ParameterField();
            ParameterField PDN4 = new ParameterField();
            ParameterField PDN5 = new ParameterField();
            ParameterField PDN6 = new ParameterField();
            ParameterField PDN7 = new ParameterField();
            ParameterField PDN8 = new ParameterField();
            ParameterField PDN9 = new ParameterField();
            ParameterField PDN10 = new ParameterField();
            ParameterField PDN11 = new ParameterField();
            ParameterField PDN12 = new ParameterField();
            ParameterField PDN13 = new ParameterField();
            ParameterField PDN14 = new ParameterField();
            ParameterField PDN15 = new ParameterField();
            ParameterField PDN16 = new ParameterField();
            ParameterField PDN17 = new ParameterField();
            ParameterField PDN18 = new ParameterField();
            ParameterField PDN19 = new ParameterField();
            ParameterField PDN20 = new ParameterField();
            ParameterField PDN21 = new ParameterField();
            ParameterField PDN22 = new ParameterField();
            ParameterField PDN23 = new ParameterField();
            ParameterField PDN24 = new ParameterField();
            ParameterField PDN25 = new ParameterField();
            ParameterField PDN26 = new ParameterField();
            ParameterField PDN27 = new ParameterField();
            ParameterField PDN28 = new ParameterField();
            ParameterField PDN29 = new ParameterField();
            ParameterField PDN30 = new ParameterField();
            ParameterField PDN31 = new ParameterField();
            PDN1.ParameterFieldName = "DN1";
            PDN2.ParameterFieldName = "DN2";
            PDN3.ParameterFieldName = "DN3";
            PDN4.ParameterFieldName = "DN4";
            PDN5.ParameterFieldName = "DN5";
            PDN6.ParameterFieldName = "DN6";
            PDN7.ParameterFieldName = "DN7";
            PDN8.ParameterFieldName = "DN8";
            PDN9.ParameterFieldName = "DN9";
            PDN10.ParameterFieldName = "DN10";
            PDN11.ParameterFieldName = "DN11";
            PDN12.ParameterFieldName = "DN12";
            PDN13.ParameterFieldName = "DN13";
            PDN14.ParameterFieldName = "DN14";
            PDN15.ParameterFieldName = "DN15";
            PDN16.ParameterFieldName = "DN16";
            PDN17.ParameterFieldName = "DN17";
            PDN18.ParameterFieldName = "DN18";
            PDN19.ParameterFieldName = "DN19";
            PDN20.ParameterFieldName = "DN20";
            PDN21.ParameterFieldName = "DN21";
            PDN22.ParameterFieldName = "DN22";
            PDN23.ParameterFieldName = "DN23";
            PDN24.ParameterFieldName = "DN24";
            PDN25.ParameterFieldName = "DN25";
            PDN26.ParameterFieldName = "DN26";
            PDN27.ParameterFieldName = "DN27";
            PDN28.ParameterFieldName = "DN28";
            PDN29.ParameterFieldName = "DN29";
            PDN30.ParameterFieldName = "DN30";
            PDN31.ParameterFieldName = "DN31";
            ParameterDiscreteValue DVDN1 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN2 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN3 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN4 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN5 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN6 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN7 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN8 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN9 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN10 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN11 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN12 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN13 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN14 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN15 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN16 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN17 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN18 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN19 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN20 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN21 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN22 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN23 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN24 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN25 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN26 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN27 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN28 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN29 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN30 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDN31 = new ParameterDiscreteValue();
            DVDN1.Value = DN1;
            DVDN2.Value = DN2;
            DVDN3.Value = DN3;
            DVDN4.Value = DN4;
            DVDN5.Value = DN5;
            DVDN6.Value = DN6;
            DVDN7.Value = DN7;
            DVDN8.Value = DN8;
            DVDN9.Value = DN9;
            DVDN10.Value = DN10;
            DVDN11.Value = DN11;
            DVDN12.Value = DN12;
            DVDN13.Value = DN13;
            DVDN14.Value = DN14;
            DVDN15.Value = DN15;
            DVDN16.Value = DN16;
            DVDN17.Value = DN17;
            DVDN18.Value = DN18;
            DVDN19.Value = DN19;
            DVDN20.Value = DN20;
            DVDN21.Value = DN21;
            DVDN22.Value = DN22;
            DVDN23.Value = DN23;
            DVDN24.Value = DN24;
            DVDN25.Value = DN25;
            DVDN26.Value = DN26;
            DVDN27.Value = DN27;
            DVDN28.Value = DN28;
            DVDN29.Value = DN29;
            DVDN30.Value = DN30;
            DVDN31.Value = DN31;
            PDN1.CurrentValues.Add(DVDN1);
            PDN2.CurrentValues.Add(DVDN2);
            PDN3.CurrentValues.Add(DVDN3);
            PDN4.CurrentValues.Add(DVDN4);
            PDN5.CurrentValues.Add(DVDN5);
            PDN6.CurrentValues.Add(DVDN6);
            PDN7.CurrentValues.Add(DVDN7);
            PDN8.CurrentValues.Add(DVDN8);
            PDN9.CurrentValues.Add(DVDN9);
            PDN10.CurrentValues.Add(DVDN10);
            PDN11.CurrentValues.Add(DVDN11);
            PDN12.CurrentValues.Add(DVDN12);
            PDN13.CurrentValues.Add(DVDN13);
            PDN14.CurrentValues.Add(DVDN14);
            PDN15.CurrentValues.Add(DVDN15);
            PDN16.CurrentValues.Add(DVDN16);
            PDN17.CurrentValues.Add(DVDN17);
            PDN18.CurrentValues.Add(DVDN18);
            PDN19.CurrentValues.Add(DVDN19);
            PDN20.CurrentValues.Add(DVDN20);
            PDN21.CurrentValues.Add(DVDN21);
            PDN22.CurrentValues.Add(DVDN22);
            PDN23.CurrentValues.Add(DVDN23);
            PDN24.CurrentValues.Add(DVDN24);
            PDN25.CurrentValues.Add(DVDN25);
            PDN26.CurrentValues.Add(DVDN26);
            PDN27.CurrentValues.Add(DVDN27);
            PDN28.CurrentValues.Add(DVDN28);
            PDN29.CurrentValues.Add(DVDN29);
            PDN30.CurrentValues.Add(DVDN30);
            PDN31.CurrentValues.Add(DVDN31);
            PFields.Add(PDN1);
            PFields.Add(PDN2);
            PFields.Add(PDN3);
            PFields.Add(PDN4);
            PFields.Add(PDN5);
            PFields.Add(PDN6);
            PFields.Add(PDN7);
            PFields.Add(PDN8);
            PFields.Add(PDN9);
            PFields.Add(PDN10);
            PFields.Add(PDN11);
            PFields.Add(PDN12);
            PFields.Add(PDN13);
            PFields.Add(PDN14);
            PFields.Add(PDN15);
            PFields.Add(PDN16);
            PFields.Add(PDN17);
            PFields.Add(PDN18);
            PFields.Add(PDN19);
            PFields.Add(PDN20);
            PFields.Add(PDN21);
            PFields.Add(PDN22);
            PFields.Add(PDN23);
            PFields.Add(PDN24);
            PFields.Add(PDN25);
            PFields.Add(PDN26);
            PFields.Add(PDN27);
            PFields.Add(PDN28);
            PFields.Add(PDN29);
            PFields.Add(PDN30);
            PFields.Add(PDN31);
            base.Report = new Statistic_DutyMounth();
            base.DataSetReport = new DataSetReport();
            base.Report.SetDataSource(base.DataSetReport);
        }
    }
}
