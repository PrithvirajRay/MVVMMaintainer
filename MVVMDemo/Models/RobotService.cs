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
        MySqlConnection ObjMySqlConnection;
        MySqlCommand ObjMySqlCommand;
        //private static List<Robot> ObjRobotsList;
        public RobotService()
        {
            ObjMySqlConnection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnect"].ConnectionString);
            ObjMySqlCommand = new MySqlCommand();
            ObjMySqlCommand.Connection = ObjMySqlConnection;
            ObjMySqlCommand.CommandType = CommandType.StoredProcedure;
            //ObjRobotsList = new List<Robot>()
            // {
            //     new Robot{SerialNo=10001,Parameter1=3.0,Parameter2=2.0,Parameter3=4.0,Parameter4=5.0,Parameter5=1.0}
            //  };
        }

        public List<Robot> GetAll()
        {
            List<Robot> ObjRobotsList = new List<Robot>();
            try
            {
                ObjMySqlCommand.Parameters.Clear();
                ObjMySqlCommand.CommandText = "udp_SelectAllRobots";
                ObjMySqlConnection.Open();
                var ObjMySqlDataReader = ObjMySqlCommand.ExecuteReader();
                if (ObjMySqlDataReader.HasRows)
                {
                    Robot ObjRobot = null;
                    while (ObjMySqlDataReader.Read())
                    {
                        ObjRobot = new Robot();
                        ObjRobot.SerialNo = ObjMySqlDataReader.GetInt32(0);
                        ObjRobot.Parameter1 = ObjMySqlDataReader.GetDouble(1);
                        ObjRobot.Parameter2 = ObjMySqlDataReader.GetDouble(2);
                        ObjRobot.Parameter3 = ObjMySqlDataReader.GetDouble(3);
                        ObjRobot.Parameter4 = ObjMySqlDataReader.GetDouble(4);
                        ObjRobot.Parameter5 = ObjMySqlDataReader.GetDouble(5);

                        ObjRobotsList.Add(ObjRobot);
                    }
                }
                ObjMySqlDataReader.Close();
            }
            catch (MySqlException ex)
            {

                throw ex;
            }
            return ObjRobotsList;
        }

        public bool Add(Robot objNewRobot)
        {
            bool IsAdded = false;

            try
            {
                ObjMySqlCommand.Parameters.Clear();
                ObjMySqlCommand.CommandText = "udp_InsertRobots";
                ObjMySqlCommand.Parameters.AddWithValue("SerialNo", objNewRobot.SerialNo);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter1", objNewRobot.Parameter1);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter2", objNewRobot.Parameter2);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter3", objNewRobot.Parameter3);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter4", objNewRobot.Parameter4);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter5", objNewRobot.Parameter5);

                ObjMySqlConnection.Open();
                int NoOfRowsAffected = ObjMySqlCommand.ExecuteNonQuery();
                IsAdded = NoOfRowsAffected > 0;

            }
            catch (MySqlException ex)
            {

                throw ex;
            }
            finally
            {
                ObjMySqlConnection.Close();
            }
            //Age must be between 21 and 58

            //ObjRobotsList.Add(objNewRobot);
            return IsAdded;
        }

        public bool Update(Robot objRobotToUpdate)
        {
            bool IsUpdated = false;
            try
            {
                ObjMySqlCommand.Parameters.Clear();
                ObjMySqlCommand.CommandText = "udp_UpdateRobots";
                ObjMySqlCommand.Parameters.AddWithValue("SerialNo", objRobotToUpdate.SerialNo);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter1", objRobotToUpdate.Parameter1);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter2", objRobotToUpdate.Parameter2);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter3", objRobotToUpdate.Parameter3);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter4", objRobotToUpdate.Parameter4);
                ObjMySqlCommand.Parameters.AddWithValue("Parameter5", objRobotToUpdate.Parameter5);

                ObjMySqlConnection.Open();
                int NoOfRowsAffected = ObjMySqlCommand.ExecuteNonQuery();
                IsUpdated = NoOfRowsAffected > 0;

            }
            catch (MySqlException ex)
            {

                throw ex;
            }
            finally
            {
                ObjMySqlConnection.Close();
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

        internal object Delete(int serialNo)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string serialNo)
        {
            bool IsDeleted = false;
            try
            {
                ObjMySqlCommand.Parameters.Clear();
                ObjMySqlCommand.CommandText = "udp_DeleteRobots";
                ObjMySqlCommand.Parameters.AddWithValue("SerialNo", serialNo);


                ObjMySqlConnection.Open();
                int NoOfRowsAffected = ObjMySqlCommand.ExecuteNonQuery();
                IsDeleted = NoOfRowsAffected > 0;

            }
            catch (MySqlException ex)
            {

                throw ex;
            }
            finally
            {
                ObjMySqlConnection.Close();
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

        public Robot Search(int serialNo)
        {
            Robot ObjRobot = null;
            try
            {
                ObjMySqlCommand.Parameters.Clear();
                ObjMySqlCommand.CommandText = "udp_SelectRobotsBySerialNo";

                ObjMySqlCommand.Parameters.AddWithValue("SerialNo", serialNo);


                ObjMySqlConnection.Open();

                var ObjMySqlDataReader = ObjMySqlCommand.ExecuteReader();
                if (ObjMySqlDataReader.HasRows)
                {
                    //Robot ObjRobot = null;
                    ObjMySqlDataReader.Read();
                    ObjRobot = new Robot();
                    ObjRobot.SerialNo = ObjMySqlDataReader.GetInt32(0);
                    ObjRobot.Parameter1 = ObjMySqlDataReader.GetDouble(1);
                    ObjRobot.Parameter2 = ObjMySqlDataReader.GetDouble(2);
                    ObjRobot.Parameter3 = ObjMySqlDataReader.GetDouble(3);
                    ObjRobot.Parameter4 = ObjMySqlDataReader.GetDouble(4);
                    ObjRobot.Parameter5 = ObjMySqlDataReader.GetDouble(5);


                }
                ObjMySqlDataReader.Close();
            
            }
            catch (MySqlException ex)
            {

                throw ex;
            }
    finally
    { ObjMySqlConnection.Close();}
            return ObjRobot;
            //return ObjRobotsList.FirstOrDefault(e=>e.SerialNo==serialNo);
        }
    }
}
