using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using webbooks.Models;

namespace webbooks.Controllers
{
    public class AuthorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string host
        {
            get
            {
                return "http://" + Request.Url.Host + ((Request.Url.Port != 0) ? ":" + Request.Url.Port.ToString() : "");
            }
        }

        // GET: Author
        public async Task<ActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(string.Format("{0}/api/Authors", host));
            var authors = await response.Content.ReadAsAsync<IEnumerable<Author>>();
            return View(authors);
        }

        // GET: Author/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(string.Format("{0}/api/Authors/{1}/", host, id));
            var author = await response.Content.ReadAsAsync<Author>();
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,nome")] Author author)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(host);
                        StringContent content = new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json");
                        var result = await client.PostAsync("/api/Authors", content);
                        await result.Content.ReadAsStringAsync();
                    }
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.InnerException.Message);
                }
            }

            return View(author);
        }

        // GET: Author/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(string.Format("{0}/api/Authors/{1}/", host, id));
            var author = await response.Content.ReadAsAsync<Author>();
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Author/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,nome")] Author author)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(host);
                        StringContent content = new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json");                        
                        var result = await client.PutAsync(string.Format("api/Authors/{0}/", author.ID), content);
                        await result.Content.ReadAsStringAsync();                        
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.InnerException.Message);
                }
            }
            return View(author);
        }

        // GET: Author/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(string.Format("{0}/api/Authors/{1}/", host, id));
            var author = await response.Content.ReadAsAsync<Author>();
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(host);
                var result = await client.DeleteAsync(string.Format("api/Authors/{0}/", id));
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }            
            return HttpNotFound();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
