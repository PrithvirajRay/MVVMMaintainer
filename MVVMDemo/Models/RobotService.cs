//RobotService class is the model we use to provide the functionalities to the ViewModel, as required by the triggers of the buttons.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;




namespace MVVMDemo.Models
{
    class RobotService
    {
        //MySqlConnection ObjMySqlConnection;
        //We initialise a connection to the MySQL server connection and a datareader to read the data that we get from the server databases, and a command object, along with a static list for the robots.
        MySqlConnection con = null;
        MySqlCommand ObjMySqlCommand;
        private static List<Robot> ObjRobotsList;
        MySqlDataReader reader = null;

        //Constructor RobotService
        public RobotService()
        {   //Connection String to the Maintainer Instance server connection at port 3308
            String str = "datasource=127.0.0.1;port=3308;database=sys;userid=root;password=ndrm;";
            con = new MySqlConnection(str);

            //Opening the connection
            con.Open();
            Console.WriteLine("MySQL DB Connected");
            //MySqlDataReader reader = null;
            




            //ObjMySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnect"].ConnectionString);
            
            //We assign the connection and type fpr the command object.
            ObjMySqlCommand = new MySqlCommand();
            ObjMySqlCommand.Connection = con;
            ObjMySqlCommand.CommandType = CommandType.Text;

            //ObjRobotsList = new List<Robot>()
            // {
            //     new Robot{SerialNo=10001,Parameter1=3.0,Parameter2=2.0,Parameter3=4.0,Parameter4=5.0,Parameter5=1.0}
            //  };
        }

        //To get the lists of robots compiled everytime
        public List<Robot> GetAll()
        {
            /*String str = @"datasource=127.0.0.1;port=3306;database=sql_inventory;userid=root;password=ndrm;";
            MySqlConnection con = null;
            con = new MySqlConnection(str);
            con.Open();
            Console.WriteLine("MySQL DB Connected");*/
            //MySqlDataReader reader = null;
            //MySqlDataReader reader = null;

            List<Robot> ObjRobotsList = new List<Robot>();
            try
            {   
                ObjMySqlCommand.Parameters.Clear();
                //ObjMySqlCommand.CommandText = "udp_SelectAllRobots";
                //MySqlCommand cmd1 = new MySqlCommand(ObjMySqlCommand.CommandText, con);

                //Command text for SQL Query
                String cmdText1 = "SELECT * FROM robotsmaintainer";
                MySqlCommand cmd1 = new MySqlCommand(cmdText1, con);
                // Executing the SQL Query
                reader = cmd1.ExecuteReader();
                Console.WriteLine("Command1 called");
                //ObjMySqlConnection.Open();
                //reader = ObjMySqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    Robot ObjRobot = null;
                    while (reader.Read())
                    {   //Getting each of the column values from the database
                        ObjRobot = new Robot();
                        ObjRobot.SerialNo = reader.GetInt32(0);
                        ObjRobot.Parameter1 = reader.GetDouble(1);
                        ObjRobot.Parameter2 = reader.GetDouble(2);
                        ObjRobot.Parameter3 = reader.GetDouble(3);
                        ObjRobot.Parameter4 = reader.GetDouble(4);
                        ObjRobot.Parameter5 = reader.GetDouble(5);

                        ObjRobotsList.Add(ObjRobot);
                    }
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {

                throw ex;
            }
            return ObjRobotsList;
        }

        // To add a new robot
        public bool Add(Robot objNewRobot)
        {
            /*String str = @"datasource=127.0.0.1;port=3306;database=sql_inventory;userid=root;password=ndrm;";
            MySqlConnection con = null;
            con = new MySqlConnection(str);
            con.Open();
            Console.WriteLine("MySQL DB Connected");*/
            Console.WriteLine("ADD");
            bool IsAdded = false;
            //MySqlDataReader reader = null;

            try
            {
                ObjMySqlCommand.Parameters.Clear();
                //ObjMySqlCommand.CommandText = "udp_InsertRobots";
                //String cmdText1 = "udp_InsertRobots";
                String cmdText1 = "INSERT INTO robotsmaintainer VALUES("+objNewRobot.SerialNo.ToString()+","+objNewRobot.Parameter1.ToString() + "," +objNewRobot.Parameter2.ToString() + "," +objNewRobot.Parameter3.ToString() + "," +objNewRobot.Parameter4.ToString() + "," +objNewRobot.Parameter5.ToString() + ");";
                MySqlCommand cmd1 = new MySqlCommand(cmdText1, con);
                reader = cmd1.ExecuteReader();
                
                ObjMySqlCommand.Parameters.AddWithValue("SerialNo", objNewRobot.SerialNo);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter1", objNewRobot.Parameter1);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter2", objNewRobot.Parameter2);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter3", objNewRobot.Parameter3);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter4", objNewRobot.Parameter4);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter5", objNewRobot.Parameter5);
                
                
                //ObjMySqlConnection.Open();
                int NoOfRowsAffected = ObjMySqlCommand.ExecuteNonQuery();
                IsAdded = NoOfRowsAffected > 0;
                reader.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
           }
            //Age must be between 21 and 58

            //ObjRobotsList.Add(objNewRobot);
            return IsAdded;
        }

        //To update robot parameters
        public bool Update(Robot objRobotToUpdate)
        {
            /*String str = @"datasource=127.0.0.1;port=3306;database=sql_inventory;userid=root;password=ndrm;";
            MySqlConnection con = null;
            con = new MySqlConnection(str);
            con.Open();
            Console.WriteLine("MySQL DB Connected");*/
            bool IsUpdated = false;
            //MySqlDataReader reader = null;

            try
            {
                ObjMySqlCommand.Parameters.Clear();
                //ObjMySqlCommand.CommandText = "udp_UpdateRobots";
                //String cmdText1 = "udp_UpdateRobots";
                String cmdText1 = "UPDATE robotsmaintainer SET Parameter1="+ objRobotToUpdate.Parameter1+ ",Parameter2=" + objRobotToUpdate.Parameter2 + ",Parameter3=" + objRobotToUpdate.Parameter3 + ",Parameter4=" + objRobotToUpdate.Parameter4 + ",Parameter5=" + objRobotToUpdate.Parameter5 + " WHERE SerialNo="+ objRobotToUpdate.SerialNo+";";
                MySqlCommand cmd1 = new MySqlCommand(cmdText1, con);
                reader = cmd1.ExecuteReader();
                
                ObjMySqlCommand.Parameters.AddWithValue("SerialNo", objRobotToUpdate.SerialNo);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter1", objRobotToUpdate.Parameter1);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter2", objRobotToUpdate.Parameter2);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter3", objRobotToUpdate.Parameter3);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter4", objRobotToUpdate.Parameter4);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter5", objRobotToUpdate.Parameter5);
                
                //ObjMySqlConnection.Open();
                int NoOfRowsAffected = ObjMySqlCommand.ExecuteNonQuery();
                IsUpdated = NoOfRowsAffected > 0;
                reader.Close();
            }
            catch (MySqlException ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
            }
            //for (int index=0;index<ObjRobotsList.Count;index++)
            // {
            //if (ObjRobotsList[index].SerialNo == objRobotToUpdate.SerialNo)
            // {
            //   ObjRobotsList[index].Parameter1 = objRobotToUpdate.Parameter1;
            //   ObjRobotsList[index].Parameter2 = objRobotToUpdate.Parameter2;
            //   ObjRobotsList[index].Parameter3 = objRobotToUpdate.Parameter3;
            //  ObjRobotsList[index].Parameter4 = objRobotToUpdate.Parameter4;
            //  ObjRobotsList[index].Parameter5 = objRobotToUpdate.Parameter5;
            //   IsUpdated = true;
            //   break;

            // }
            //}
            return IsUpdated;
        }

        
        //To delete a robot from the database
        public bool Delete(int serialNo)
        {
            /*String str = @"datasource=127.0.0.1;port=3306;database=sql_inventory;userid=root;password=ndrm;";
            MySqlConnection con = null;
            con = new MySqlConnection(str);
            con.Open();
            Console.WriteLine("MySQL DB Connected");*/
           //MySqlDataReader reader = null;

            bool IsDeleted = false;
            try
            {
                ObjMySqlCommand.Parameters.Clear();
                //ObjMySqlCommand.CommandText = "udp_DeleteRobots";
                String cmdText1 = "DELETE FROM robotsmaintainer WHERE SerialNo="+serialNo+';';
                MySqlCommand cmd1 = new MySqlCommand(cmdText1, con);
                reader = cmd1.ExecuteReader();
                ObjMySqlCommand.Parameters.AddWithValue("SerialNo", serialNo);


                //ObjMySqlConnection.Open();
                int NoOfRowsAffected = ObjMySqlCommand.ExecuteNonQuery();
                IsDeleted = NoOfRowsAffected > 0;
                reader.Close();
            }
            catch (MySqlException ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
            }
            //for (int index=0; index < ObjRobotsList.Count; index++)
            //{
            // if (ObjRobotsList[index].SerialNo == serialNo)
            //  {
            //      ObjRobotsList.RemoveAt(index);
            //     IsDeleted = true;
            //      break;
            //  }

            //}
            return IsDeleted;
        }


        //To search a specific robot from the database
        public Robot Search(int serialNo)
        {
            /*String str = @"datasource=127.0.0.1;port=3306;database=sql_inventory;userid=root;password=ndrm;";
            MySqlConnection con = null;
            con = new MySqlConnection(str);
            con.Open();
            Console.WriteLine("MySQL DB Connected");*/
            Robot ObjRobot = null;
            //MySqlDataReader reader = null;
            try
            {
                ObjMySqlCommand.Parameters.Clear();
                //ObjMySqlCommand.CommandText = "udp_SelectRobotsBySerialNo";
                String cmdText1 = "SELECT * FROM robotsmaintainer WHERE SerialNo="+serialNo+";";
                MySqlCommand cmd1 = new MySqlCommand(cmdText1, con);
                reader = cmd1.ExecuteReader();

                ObjMySqlCommand.Parameters.AddWithValue("SerialNo", serialNo);


                //ObjMySqlConnection.Open();

                //reader = ObjMySqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    //Robot ObjRobot = null;
                    reader.Read();
                    ObjRobot = new Robot();
                    ObjRobot.SerialNo = reader.GetInt32(0);
                    ObjRobot.Parameter1 = reader.GetDouble(1);
                    ObjRobot.Parameter2 = reader.GetDouble(2);
                    ObjRobot.Parameter3 = reader.GetDouble(3);
                    ObjRobot.Parameter4 = reader.GetDouble(4);
                    ObjRobot.Parameter5 = reader.GetDouble(5);


                }
                reader.Close();

            }
            catch (MySqlException ex)
            {

                throw ex;
            }
            finally
            { con.Close();}
              
        return ObjRobot;
            //return ObjRobotsList.FirstOrDefault(e=>e.SerialNo==serialNo);
        }
        
    }
}
