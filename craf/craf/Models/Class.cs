using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace craf.Models
{
    public class Search
    {
        [Key]
        public long SearchID { get; set; }

        public string PostCode { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public double DistanceToLondon { get; set; }
        //public Search(string PostCode, double Longitude, double Latitude, string Region, string City, double DistanceToLondon)
        //{
        //    this.PostCode = PostCode;
        //    this.Longitude = Longitude;
        //    this.Latitude = Latitude;
        //    this.Region = Region;
        //    this.City = City;
        //    this.DistanceToLondon = DistanceToLondon;
        //}
        //public Search(int SearchID,string PostCode, double Longitude, double Latitude, string Region, string City, double DistanceToLondon)
        //{
        //    this.SearchID = SearchID;
        //    this.PostCode = PostCode;
        //    this.Longitude = Longitude;
        //    this.Latitude = Latitude;
        //    this.Region = Region;
        //    this.City = City;
        //    this.DistanceToLondon = DistanceToLondon;
        //}
    }
}
