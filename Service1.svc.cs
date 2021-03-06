﻿using System;
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
            WebOperationContext.Current.OutgoingResponse.Headers.Remove("Access-Control-Allow-Origin");
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
                    p.PlaceID = Int32.Parse(reader[0].ToString());
                    p.Name = reader[1].ToString();
                    p.MenuURL = reader[2].ToString();
                    p.TypeID = Int32.Parse(reader[3].ToString());
                    p.Phones = GetPhonesByPlaceID(p.PlaceID);
                    p.Type = GetPlaceType(p.TypeID);

                    toReturn.Add(p);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Place p = new Place();
                p.PlaceID = -1;
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

        public List<Phone> GetPhonesByPlaceID(int placeID)
        {

            fixCORS();

            List<Phone> toReturn = new List<Phone>();

            SqlConnection connection = new SqlConnection(cString);
            string sqlString = "SELECT * FROM Phone WHERE [PlaceID] = @placeID";
            SqlCommand cmd = new SqlCommand(sqlString, connection);

            cmd.Parameters.AddWithValue("placeID", placeID);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Phone p = new Phone();
                    p.PhoneID = Int32.Parse(reader[0].ToString());
                    p.PlaceID = Int32.Parse(reader[1].ToString());
                    p.PhoneNo = reader[2].ToString();
                    
                    toReturn.Add(p);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Phone p = new Phone();
                p.PhoneID = -1;
                p.PhoneNo = e.ToString();

                toReturn.Add(p);
                return toReturn;
            }
            finally
            {
                connection.Close();
            }
            return toReturn;
        }

        public PlaceType GetPlaceType(int typeID)
        {

            fixCORS();

            PlaceType toReturn = new PlaceType();

            SqlConnection connection = new SqlConnection(cString);
            string sqlString = "SELECT * FROM PlaceType WHERE [TypeID] = @typeID";
            SqlCommand cmd = new SqlCommand(sqlString, connection);

            cmd.Parameters.AddWithValue("typeID", typeID);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    toReturn.TypeID = Int32.Parse(reader[0].ToString());
                    toReturn.Name = reader[1].ToString();
                    break;
                }
                reader.Close();
            }
            catch (Exception e)
            {
                toReturn.TypeID = -1;
                toReturn.Name = e.ToString();

                return toReturn;
            }
            finally
            {
                connection.Close();
            }
            return toReturn;
        }

        public List<PlaceType> GetPlaceTypes()
        {

            fixCORS();

            List<PlaceType> toReturn = new List<PlaceType>();

            SqlConnection connection = new SqlConnection(cString);
            string sqlString = "SELECT * FROM PlaceType";
            SqlCommand cmd = new SqlCommand(sqlString, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PlaceType pt= new PlaceType();
                    pt.TypeID = Int32.Parse(reader[0].ToString());
                    pt.Name = reader[1].ToString();
                    toReturn.Add(pt);
                    
                }
                reader.Close();
            }
            catch (Exception e)
            {
                PlaceType pt = new PlaceType();
                pt.TypeID = -1;
                pt.Name = e.ToString();
                toReturn.Add(pt);
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
