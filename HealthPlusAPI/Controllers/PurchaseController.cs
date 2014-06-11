using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthPlusAPI.Models;

namespace HealthPlusAPI.Controllers
{
    public class PurchaseController : Controller
    {
        private healthplusEntities db = new healthplusEntities();

        // GET: /Purchase/
        public async Task<ActionResult> Index()
        {
            return View(await db.purchased_ad.ToListAsync());
        }

        // GET: /Purchase/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            purchased_ad purchased_ad = await db.purchased_ad.FindAsync(id);
            if (purchased_ad == null)
            {
                return HttpNotFound();
            }
            return View(purchased_ad);
        }

        // GET: /Purchase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Purchase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="id,name,price,discount,specialty,service,img_url,buyed_cupons,cid,client_id,state,start_time,end_time,purchase_time")] purchased_ad purchased_ad)
        {
            if (ModelState.IsValid)
            {
                db.purchased_ad.Add(purchased_ad);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(purchased_ad);
        }

        // GET: /Purchase/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            purchased_ad purchased_ad = await db.purchased_ad.FindAsync(id);
            if (purchased_ad == null)
            {
                return HttpNotFound();
            }
            return View(purchased_ad);
        }

        // POST: /Purchase/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="id,name,price,discount,specialty,service,img_url,buyed_cupons,cid,client_id,state,start_time,end_time,purchase_time")] purchased_ad purchased_ad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchased_ad).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(purchased_ad);
        }

        // GET: /Purchase/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            purchased_ad purchased_ad = await db.purchased_ad.FindAsync(id);
            if (purchased_ad == null)
            {
                return HttpNotFound();
            }
            return View(purchased_ad);
        }

        // POST: /Purchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            purchased_ad purchased_ad = await db.purchased_ad.FindAsync(id);
            db.purchased_ad.Remove(purchased_ad);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
