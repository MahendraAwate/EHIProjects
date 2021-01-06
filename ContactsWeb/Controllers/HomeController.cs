using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ContactsWeb.Models;
using DataLayer;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;

namespace ContactsWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HttpClient client = new HttpClient();
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            client.BaseAddress = new Uri("https://localhost:44355/api/");
        }

        public async Task<IActionResult> Index()
        {
            List<Contact> contacts = new List<Contact>();            
            HttpResponseMessage responce = await client.GetAsync("contacts");
            if(responce.IsSuccessStatusCode)
            {
                var result = responce.Content.ReadAsStringAsync().Result;
                contacts = JsonConvert.DeserializeObject<List<Contact>>(result);
            }
            return View(contacts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] Contact contact)
        {          
            var json = JsonConvert.SerializeObject(contact);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
            var response = await client.PostAsync("contacts", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }               
         
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(contact);            
        }
                
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Contact contact = new Contact();                   
            HttpResponseMessage responce = await client.GetAsync($"contacts/{id}"); 
            if (responce.IsSuccessStatusCode)
            {
                var result = responce.Content.ReadAsStringAsync().Result;
                contact = JsonConvert.DeserializeObject<Contact>(result);
            } 
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind] Contact contact)
        {
            if (id != contact.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(contact);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                var responce = await client.PostAsync($"contacts/{id}", stringContent);
                if (responce.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }         
            }
            return View(contact);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            Contact contact = new Contact();
            if (id == null)
            {
                return NotFound();
            }
            var responce = await client.GetAsync($"contacts/{id}");
            if (responce.IsSuccessStatusCode)
            {
                var result = responce.Content.ReadAsStringAsync().Result;
                contact = JsonConvert.DeserializeObject<Contact>(result);
            }           
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }            
            Contact contact = new Contact(); // objCustomer.GetCustomerData(id);          
            var responce = await client.GetAsync($"contacts/{id}");

            if (responce.IsSuccessStatusCode)
            {
                var result = responce.Content.ReadAsStringAsync().Result;
                contact = JsonConvert.DeserializeObject<Contact>(result);
            }
            return View(contact);
        }

        [HttpPost, ActionName("DeleteContact")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var responce = await client.DeleteAsync($"contacts/{id}");
            if (responce.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }           
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
