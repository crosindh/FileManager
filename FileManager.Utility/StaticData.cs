using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Utility
{
    public class StaticData
    {


        public List<string> Deparment_list = new List<string>()

        {
            "Governor Secretariat / House" ,"Chief Minister","Services General Admin. Coord Deptt.","Finance",
            "Investment Department","Planning & Development","Excise & Taxation","Board of Revenue","Home","Law & Parliamentary Affairs",
            "Agriculture Supply & Prices","Food","Livestock & Fisheries", "Cooperation","Irrigation","Energy,Mines & Minerals Development","Industries & Commerce",
            "Labour & Human Resources","Works & Services", "School Education", "Transport and Mass Transit", "Environment, Forest & Wildlife",
            "Local Government & HTP","Housing & Town Planning","Katchi Abadies","Public Health Engineering and RDD","Health","Sports & Youth Affairs",
            "Information & amp; Archives", "Minorities Affairs"," Culture, Tourism and Antiquities Department", "Information Technology", "College Education",
            "Universities and Boards","Dept of Empowerment - Persons Disabilities",  "Population Welfare",  "Women Development",  "Rehabilitation",
            "Social Welfare","Auqaf, Relgious Affairs & Zakat","Human Rights","Inter Provincial Coordination","Provincial Assembly","Provincial Ombudsman",
            "Ombudsman for Protection against woman harassment at workplace","AG Sindh","Federal Govt","Sindh Revenue Board"
        };


        public List<string> Type_List = new List<string>()

        {
            "Project" ,"Scheme","PC-I","FileType-2"
        };
        public List<string> Section_List = new List<string>()

        {
            "admin1" ,"admin2","general","foreigntranning","legal","audit"
        };

        public List<string> StatusList = new List<string>()
        { "Pending", "Urgent", "Seen", "Objection"
        };

        public List<string> ForwardToList = new List<string>
        { "S&T", "development", "general", "admin1", "admin2", "Health", "F.Aid","F&T","L&F","socialsector","PP&H","coordination",
           "socoord" ,"audit","legal","W&D","SP&PR","C.Eco","education","communication","transport","agriculture","industries"
        };

        public const string Status1 = "Pending";
        public const string Status2 = "Seen";
        public const string Status3 = "Urgent";
        public const string Status4 = "Objection";

        public const string AdminRole = "Admin";
        public const string SecretaryRole = "Secretary";

        public const string DataEntryUser = "R&I";

        public const string StaticNavigationArrow = " " + "=>" + " ";
    }
}
