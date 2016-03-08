using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SherwoodParkAppSelector
{

    public partial class Form1 : Form
    {
        /*
        //Code for using SQL, was used during the 2016 Open Data Day Hackathon for speed, but not deployable as is
        private SqlConnection DBConnection;
        private SqlDataReader DBDataReader;
        private SqlCommand DBRead;
        */
        private Park[] ParkList;
        private String[] ParsedData;
        private String Data;

        public Form1()
        {
            InitializeComponent();

            /*
            //Code for using SQL, was used during the 2016 Open Data Day Hackathon for speed, but not deployable as is
            DBConnection = new SqlConnection("Server=KONAMIWA\\SQLEXPRESS;Database=SherwoodParkParks;Trusted_Connection=Yes");
            DBConnection.Open();
            DBRead = new SqlCommand("SELECT Park_Name, Golf, Baseball, Campground, Rugby, Football, Tennis, Playground FROM dbo.[Parks Massaged No Spatial]", DBConnection);
            DBDataReader = DBRead.ExecuteReader();
            ParkList = new Park[183];

            for (int row = 0; row < 183; row++)
            {
                DBDataReader.Read();
                ParkList[row] = new Park(DBDataReader.GetString(0), DBDataReader.GetString(1), DBDataReader.GetString(2), DBDataReader.GetString(3), DBDataReader.GetString(4), DBDataReader.GetString(5), DBDataReader.GetString(6), DBDataReader.GetString(7));
            }
            */

            int offset = 0;
            
            Data = System.IO.File.ReadAllText(".\\Parks Massaged No Spatial.csv");
            //Windows newline with carriage return
            //Data = Data.Replace("\r\n", ",");
            //Posix newline without carriage return
            Data = Data.Replace("\n", ",");
            ParsedData = Data.Split(',');
            ParkList = new Park[(ParsedData.Length/12)-1];
            
            for (int row = 0; row < (ParsedData.Length/12)-1; row++)
            {
                offset = ((row * 12) + 12);
                ParkList[row] = new Park(ParsedData[offset], ParsedData[offset + 1], ParsedData[offset + 2], ParsedData[offset + 3], ParsedData[offset + 4], ParsedData[offset + 5], ParsedData[offset + 6], ParsedData[offset + 7], ParsedData[offset + 8], ParsedData[offset + 9], ParsedData[offset + 10], ParsedData[offset + 11]);
            }
        }


        private void Update_Click(object sender, EventArgs e)
        {
            //Clear the Results and reset the count and ranks
            Results.Clear();
            int count = 0;
            for (int row = 0; row < ParkList.Length; row++)
            {
                ParkList[row].Rank = 0;
            }

            //Set the goal for success
            if (BaseballCheck.Checked == true)
            {
                count++;
            }
            if (BMX_SkateCheck.Checked == true)
            {
                count++;
            }
            if (CampgroundCheck.Checked == true)
            {
                count++;
            }
            if (DayUseCheck.Checked == true)
            {
                count++;
            }
            if (FootballCheck.Checked == true)
            {
                count++;
            }
            if (GolfCheck.Checked == true)
            {
                count++;
            }
            if (PlaygroundCheck.Checked == true)
            {
                count++;
            }
            if (RugbyCheck.Checked == true)
            {
                count++;
            }
            if (SoccerCheck.Checked == true)
            {
                count++;
            }
            if (TennisCheck.Checked == true)
            {
                count++;
            }
            if (VolleyballCheck.Checked == true)
            {
                count++;
            }


            //Rank the parks for matches
            if (count > 0)
                {
                for (int row = 0; row < ParkList.Length; row++)
                {
                    if ((BaseballCheck.Checked == true) && (ParkList[row].Baseball == "YES"))
                    {
                        ParkList[row].Rank++;
                    }
                    if ((BMX_SkateCheck.Checked == true) && (ParkList[row].BMX_Skate == "YES"))
                    {
                        ParkList[row].Rank++;
                    }
                    if ((CampgroundCheck.Checked == true) && (ParkList[row].Campground == "YES"))
                    {
                        ParkList[row].Rank++;
                    }
                    if ((DayUseCheck.Checked == true) && (ParkList[row].Day_Use == "YES"))
                    {
                        ParkList[row].Rank++;
                    }
                    if ((FootballCheck.Checked == true) && (ParkList[row].Football == "YES"))
                    {
                        ParkList[row].Rank++;
                    }
                    if ((GolfCheck.Checked == true) && (ParkList[row].Golf == "YES"))
                    {
                        ParkList[row].Rank++;
                    }
                    if ((PlaygroundCheck.Checked == true) && (ParkList[row].Playground == "YES"))
                    {
                        ParkList[row].Rank++;
                    }
                    if ((RugbyCheck.Checked == true) && (ParkList[row].Rugby == "YES"))
                    {
                        ParkList[row].Rank++;
                    }
                    if ((SoccerCheck.Checked == true) && (ParkList[row].Soccer == "YES"))
                    {
                        ParkList[row].Rank++;
                    }
                    if ((TennisCheck.Checked == true) && (ParkList[row].Tennis == "YES"))
                    {
                        ParkList[row].Rank++;
                    }
                    if ((VolleyballCheck.Checked == true) && (ParkList[row].Volleyball == "YES"))
                    {
                        ParkList[row].Rank++;
                    }
                }

                //Fill out Results Box
                for (int row = 0; row < ParkList.Length; row++)
                {
                    if (count == ParkList[row].Rank)
                    {
                        Results.AppendText(ParkList[row].Name + "\n");
                    }
                }
            }
        }
            

        public class Park
        {
            public String Name;
            public String Baseball;
            public String BMX_Skate;
            public String Campground;
            public String Day_Use;
            public String Football;
            public String Golf;
            public String Playground;
            public String Rugby;
            public String Soccer;
            public String Tennis;
            public String Volleyball;
            public int Rank;

            public Park(String _Name, String _Baseball, String _BMX_Skate, String _Campground, String _Day_Use, String _Football, String _Golf, String _Playground, String _Rugby, String _Soccer, String _Tennis, String _Volleyball)
            {
                Name = _Name;
                Baseball = _Baseball;
                BMX_Skate = _BMX_Skate;
                Campground = _Campground;
                Day_Use = _Day_Use;
                Football = _Football;
                Golf = _Golf;
                Playground = _Playground;
                Rugby = _Rugby;
                Soccer = _Soccer;
                Tennis = _Tennis;
                Volleyball = _Volleyball;
                Rank = 0;
            }
        }

    }
}
