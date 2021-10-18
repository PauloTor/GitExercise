using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Data;
using FrontEnd.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;

namespace FrontEnd.Controllers
{
    public class SearchDTOController : Controller
    {
        private readonly FrontEndContext _context;

        public SearchDTOController(FrontEndContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString, bool? checkResp)
        {
            //  guarda o valor de KM / MILES
            // ViewBag.dist = false;

            // erro de ligacao / postalcode nao encontrado
            ViewBag.codebool = true;

            ViewData["CurrentFilter"] = searchString;

            var d = searchString;
            if (d == null)
            {
                return View();
            }
            try
            {

                using (var client = new HttpClient())


                {
                    // Request para pesquisar postal code (grava pesquisa na base de dados) 
                    var response = await client.GetAsync("https://localhost:44343/api/Searches/get/" + searchString);
                    response.EnsureSuccessStatusCode();

                    // reservaAPIvar
                    var SearchAdress = await response.Content.ReadAsAsync<SearchDTO>();

                    // Request para pegar nos ultimos 3 registos
                    var response2 = await client.GetAsync("https://localhost:44343/api/Searches/last/");

                    if (response2.IsSuccessStatusCode)
                    {
                        var readTask = response2.Content.ReadAsAsync<List<SearchDTO>>();

                        List<SearchDTO> reservas = readTask.Result.ToList();

                        if (checkResp != null || checkResp == true)
                        {
                            ViewBag.dist = true;

                            var convertMilesKm = new List<SearchDTO>();
                            foreach (var item in reservas)
                            {
                                convertMilesKm.Add(new SearchDTO
                                {
                                    PostCode = item.PostCode,
                                    City = item.City,
                                    Latitude = item.Latitude,
                                    Longitude = item.Longitude,
                                    Region = item.Region,
                                    SearchID = item.SearchID,
                                    DistanceToLondon = Math.Round(item.DistanceToLondon * 1.609344, 2)

                                });
                            }
                            reservas = convertMilesKm;
                        }
                        else
                        {
                            ViewBag.dist = false;
                        }

                        return View(reservas);
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.codebool = false;

                return View();
            }
        }
    }
}