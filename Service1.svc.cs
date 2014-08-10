using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TimeToEatWebService.Model;

namespace TimeToEatWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        string cString = ConfigurationManager.AppSettings["SQLSERVER_CONNECTION_STRING"];
        private void fixCORS()
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "POST");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept");
        }
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", cString);
        }


        public List<Place> GetPlaces() { 
   
            fixCORS();

            List<Place> toReturn = new List<Place>();

            SqlConnection connection = new SqlConnection(cString);
            string sqlString = "SELECT * FROM Place";
            SqlCommand cmd = new SqlCommand(sqlString, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Place p = new Place();
                    p.ID = Int32.Parse(reader[0].ToString());
                    p.Name = reader[1].ToString();
                    p.MenuURL = reader[2].ToString();
                    p.TypeID = Int32.Parse(reader[3].ToString());

                    toReturn.Add(p);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Place p = new Place();
                p.ID = -1;
                p.Name = e.ToString();

                toReturn.Add(p);
                return toReturn;
            }
            finally
            {
                connection.Close();
            }
            return toReturn;        
        }
    }
}
