using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using craf.Data;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace craf.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchesController : ControllerBase
    {
        private readonly crafContext _context;

        public SearchesController(crafContext context)
        {
            _context = context;
        }

        // GET: api/Searches Pegar nos ultimas 3 registos
        [HttpGet("last")]
        public async Task<ActionResult<IEnumerable<Search>>> GetSearch()
        {
            var a = this._context.Set<Search>().ToList();
            int contador = 3;
            if (a.Count() < 3)
            {
                contador = a.Count();
            }
            // ultimos tres registos
            var b = a.GetRange(a.Count() - contador, contador);
            b.Reverse();
            return b;
        }

        public Task GetSearches()
        {
            throw new NotImplementedException();
        }

        // GET: api/Searches/5
        [HttpGet("get/{PostCode_}")]
        public async Task<ActionResult<Search>> GetSearch(string PostCode_)
        {

            using (var client = new HttpClient())
            {
                try
                {
                
                    var response = await client.GetAsync("https://api.postcodes.io/postcodes/" + PostCode_);
                    response.EnsureSuccessStatusCode();

                    var contents = response.Content.ReadAsStringAsync();


                    var resultParsed = JObject.Parse(contents.Result);
                    var city = resultParsed["result"]["primary_care_trust"].ToString();
                    var region = resultParsed["result"]["region"].ToString();
                    var latitude = ((double)resultParsed["result"]["latitude"]);
                    var longitude = ((double)resultParsed["result"]["longitude"]);
                    var distance = CalculateDistance(latitude, longitude);
                    Search search = new Search() { PostCode = PostCode_, Longitude = longitude, Latitude = latitude, Region = region, City = city, DistanceToLondon = distance };
                    if (search == null)
                    {
                        throw new ArgumentNullException("entity must not be null");
                    }

                    try
                    {
                        await _context.AddAsync(search);
                        await _context.SaveChangesAsync();

                        return search;
                    }
                    catch (Exception ex)
                    {
                        return NotFound($"{nameof(search)} could not be saved: {ex.Message}");

                    }

                }
                catch (HttpRequestException eh)
                {

                    return NotFound($"Postal Code not found {eh.Message}");
                    //return NotFound("Postal Code not found");
                }
            }

        }


        // POST: api/Searches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        public double CalculateDistance(double lat1, double lon1)
        {
            var lat2 = 51.470022;
            var lon2 = -0.4542955;

            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;


            //case 'K': //Kilometers -> default
            //    return dist * 1.609344;
            //Miles

            return Math.Round(dist, 2);
        }

    }
}
