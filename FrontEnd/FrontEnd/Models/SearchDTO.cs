using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class SearchDTO
    {
        
        
            [Key]
            public long SearchID { get; set; }

            public string PostCode { get; set; }

            public double Longitude { get; set; }
            public double Latitude { get; set; }

            public string Region { get; set; }

            public string City { get; set; }

            public double DistanceToLondon { get; set; }

    }
    }

