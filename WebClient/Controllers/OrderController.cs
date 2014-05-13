﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient_.HealthPService;
using WebClient_.Models;


namespace WebClient_.Controllers
{
    public class OrderController : Controller
    {

        private HealthPService.IHPService mService = new HPServiceClient();

        //
        // GET: /Order/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShoppingCart()
        {
            ShoppingCart cart = (ShoppingCart)Session["shopping_cart"];
            if (cart == null)
            {
                return View("ShoppingCart");
            }
            return View("ShoppingCart",cart);
        }

        /// <summary>
        /// Finalizes a purchased using a cupon id (single purchase) or the shopping cart
        /// </summary>
        /// <param name="id">Cupon id\n Optional, if set means a fast checkout otherwise a normal checkout using the shopping cart</param>
        /// <returns></returns>
        public ActionResult FinalizePurchase(int? id)
        {
            if (id.HasValue)
            {
                //A fast checkout
                Ad cupon = ((ShoppingCart)Session["shopping_cart"]).getAdById((int)id);
                if (Session["user"] != null)
                {
                    cupon.client_id = ((UserSession)Session["user"]).internal_id;
                }
                mService.BuyCupon(JsonConvert.SerializeObject(cupon));

            }
            else
            {
                //Bulk insert on table Cupon
            }

            //Send email to user confirming purchase
            return RedirectToAction("Index", "User");
        }

        public bool addToCart(Ad cupon)
        {
            ShoppingCart cart = (ShoppingCart)Session["shopping_cart"];
            if (cart == null)
            {
                cart = new ShoppingCart();
                Session["shopping_cart"] = cart;
            }
            cart.addCupon(cupon);
            return true;
        }
	}
}