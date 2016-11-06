using System;
using System.Collections.Generic;
using System.Text;

using PersonPosition.Report;

using CrystalDecisions.Shared;

namespace PersonPosition.Model
{
    public class ReportAnalysics_Duty : ReportBasic
    {
        public ReportAnalysics_Duty(string MainTitle, string LeftSubTitle, string RightSubTitle, string Name, string PID, int CardID, string Department, string WorkType, string AnalysicsTime, string AnalysicsKey, string AnalysicsText, string MapViewTitle1, string MapViewColumn1, string MapViewRow1, string MapViewTitle2, string MapViewColumn2, string MapViewRow2)
            : base(MainTitle, LeftSubTitle, RightSubTitle)
        {
            ParameterField PFName = new ParameterField();
            ParameterField PFPID = new ParameterField();
            ParameterField PFCardID = new ParameterField();
            ParameterField PFDepartment = new ParameterField();
            ParameterField PFWorkType = new ParameterField();
            ParameterField PFAnalysicsTime = new ParameterField();
            ParameterField PFAnalysicsKey = new ParameterField();
            ParameterField PFAnalysicsText = new ParameterField();
            ParameterField PFMapViewTitle1 = new ParameterField();
            ParameterField PFMapViewColumn1 = new ParameterField();
            ParameterField PFMapViewRow1 = new ParameterField();
            ParameterField PFMapViewTitle2 = new ParameterField();
            ParameterField PFMapViewColumn2 = new ParameterField();
            ParameterField PFMapViewRow2 = new ParameterField();
            PFName.ParameterFieldName = "Name";
            PFPID.ParameterFieldName = "PID";
            PFCardID.ParameterFieldName = "CardID";
            PFDepartment.ParameterFieldName = "Department";
            PFWorkType.ParameterFieldName = "WorkType";
            PFAnalysicsTime.ParameterFieldName = "AnalysicsTime";
            PFAnalysicsKey.ParameterFieldName = "AnalysicsKey";
            PFAnalysicsText.ParameterFieldName = "AnalysicsText";
            PFMapViewTitle1.ParameterFieldName = "MapViewTitle1";
            PFMapViewColumn1.ParameterFieldName = "MapViewColumn1";
            PFMapViewRow1.ParameterFieldName = "MapViewRow1";
            PFMapViewTitle2.ParameterFieldName = "MapViewTitle2";
            PFMapViewColumn2.ParameterFieldName = "MapViewColumn2";
            PFMapViewRow2.ParameterFieldName = "MapViewRow2";
            ParameterDiscreteValue DVName = new ParameterDiscreteValue();
            ParameterDiscreteValue DVPID = new ParameterDiscreteValue();
            ParameterDiscreteValue DVCardID = new ParameterDiscreteValue();
            ParameterDiscreteValue DVDepartment = new ParameterDiscreteValue();
            ParameterDiscreteValue DVWorkType = new ParameterDiscreteValue();
            ParameterDiscreteValue DVAnalysicsTime = new ParameterDiscreteValue();
            ParameterDiscreteValue DVAnalysicsKey = new ParameterDiscreteValue();
            ParameterDiscreteValue DVAnalysicsText = new ParameterDiscreteValue();
            ParameterDiscreteValue DVMapViewTitle1 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVMapViewColumn1 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVMapViewRow1 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVMapViewTitle2 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVMapViewColumn2 = new ParameterDiscreteValue();
            ParameterDiscreteValue DVMapViewRow2 = new ParameterDiscreteValue();
            DVName.Value = Name;
            DVPID.Value = PID;
            DVCardID.Value = CardID;
            DVDepartment.Value = Department;
            DVWorkType.Value = WorkType;
            DVAnalysicsTime.Value = AnalysicsTime;
            DVAnalysicsKey.Value = AnalysicsKey;
            DVAnalysicsText.Value = AnalysicsText;
            DVMapViewTitle1.Value = MapViewTitle1;
            DVMapViewColumn1.Value = MapViewColumn1;
            DVMapViewRow1.Value = MapViewRow1;
            DVMapViewTitle2.Value = MapViewTitle2;
            DVMapViewColumn2.Value = MapViewColumn2;
            DVMapViewRow2.Value = MapViewRow2;

            PFName.CurrentValues.Add(DVName);
            PFPID.CurrentValues.Add(DVPID);
            PFCardID.CurrentValues.Add(DVCardID);
            PFDepartment.CurrentValues.Add(DVDepartment);
            PFWorkType.CurrentValues.Add(DVWorkType);
            PFAnalysicsTime.CurrentValues.Add(DVAnalysicsTime);
            PFAnalysicsKey.CurrentValues.Add(DVAnalysicsKey);
            PFAnalysicsText.CurrentValues.Add(DVAnalysicsText);
            PFMapViewTitle1.CurrentValues.Add(DVMapViewTitle1);
            PFMapViewColumn1.CurrentValues.Add(DVMapViewColumn1);
            PFMapViewRow1.CurrentValues.Add(DVMapViewRow1);
            PFMapViewTitle2.CurrentValues.Add(DVMapViewTitle2);
            PFMapViewColumn2.CurrentValues.Add(DVMapViewColumn2);
            PFMapViewRow2.CurrentValues.Add(DVMapViewRow2);

            PFields.Add(PFName);
            PFields.Add(PFPID);
            PFields.Add(PFCardID);
            PFields.Add(PFDepartment);
            PFields.Add(PFWorkType);
            PFields.Add(PFAnalysicsTime);
            PFields.Add(PFAnalysicsKey);
            PFields.Add(PFAnalysicsText);
            PFields.Add(PFMapViewTitle1);
            PFields.Add(PFMapViewColumn1);
            PFields.Add(PFMapViewRow1);
            PFields.Add(PFMapViewTitle2);
            PFields.Add(PFMapViewColumn2);
            PFields.Add(PFMapViewRow2);

            base.Report = new Analysics_Duty();
            base.DataSetReport = new DataSetReport();
            base.Report.SetDataSource(DataSetReport);
        }
    }
}
