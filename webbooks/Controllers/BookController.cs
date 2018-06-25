using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using webbooks.Models;

namespace webbooks.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string host
        {
            get
            {
                return "http://" + Request.Url.Host + ((Request.Url.Port != 0) ? ":" + Request.Url.Port.ToString() : "");
            }
        }

        // GET: Book
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(string.Format("{0}/api/Books", host));
            var books = await response.Content.ReadAsAsync<IEnumerable<Book>>();
            return View(books);
        }

        // GET: Book/Details/5
        public async System.Threading.Tasks.Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(string.Format("{0}/api/Books/{1}/", host, id));
            var book = await response.Content.ReadAsAsync<Book>();
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            ViewBag.autor = new SelectList(db.author, "ID", "nome");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create([Bind(Include = "ID,isbn,titulo,autor,ano,sinopse")] Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(host);
                        StringContent content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                        var result = await client.PostAsync("/api/Books", content);
                        await result.Content.ReadAsStringAsync();
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.InnerException.Message);
                }
            }

            ViewBag.autor = new SelectList(db.author, "ID", "nome", book.autor);
            return View(book);
        }

        // GET: Book/Edit/5
        public async System.Threading.Tasks.Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(string.Format("{0}/api/Books/{1}/", host, id));
            var book = await response.Content.ReadAsAsync<Book>();
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.autor = new SelectList(db.author, "ID", "nome", book.autor);
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit([Bind(Include = "ID,isbn,titulo,autor,ano,sinopse")] Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(host);
                        StringContent content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                        var result = await client.PutAsync(string.Format("api/Books/{0}/", book.ID), content);
                        await result.Content.ReadAsStringAsync();
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.InnerException.Message);
                }
            }
            ViewBag.autor = new SelectList(db.author, "ID", "nome", book.autor);
            return View(book);
        }

        // GET: Book/Delete/5
        public async System.Threading.Tasks.Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(string.Format("{0}/api/Books/{1}/", host, id));
            var book = await response.Content.ReadAsAsync<Book>();
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(host);
                var result = await client.DeleteAsync(string.Format("api/Books/{0}/", id));
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
