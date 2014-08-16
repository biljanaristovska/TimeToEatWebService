using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TimeToEatWebService.Model;

namespace TimeToEatWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped,
         UriTemplate = "GetData?value={value}")]
        string GetData(int value);

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped,
         UriTemplate = "GetPlaces")]
        List<Place> GetPlaces();

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped,
         UriTemplate = "GetPlaceTypes")]
        List<PlaceType> GetPlaceTypes();

     }
}
