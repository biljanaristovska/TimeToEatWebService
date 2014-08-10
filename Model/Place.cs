using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeToEatWebService.Model
{
    public class Place
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string MenuURL { get; set; }
        public int TypeID { get; set; }
        public List<Phone> Phones { get; set; }
        public PlaceType Type { get; set; }
    }
}