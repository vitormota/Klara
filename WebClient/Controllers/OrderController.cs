using Newtonsoft.Json;
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
        public ActionResult FinalizePurchase()
        {
            ShoppingCart cart = (ShoppingCart)Session["shopping_cart"];
            if (cart == null) return RedirectToAction("Index", "Home");
            if (Session["user"] != null)
            {
                int internal_id = ((UserSession)Session["user"]).internal_id;
                cart.appendClientId(internal_id);
            }
            if (cart.isFastCheckout())
            {
                //A fast checkout
                Ad cupon = cart.getFCheckoutAd();
                if (Session["user"] != null)
                {
                    cupon.client_id = ((UserSession)Session["user"]).internal_id;
                }
                string response = mService.BuyCupon(JsonConvert.SerializeObject(cupon));
                cart.removeCupon(cupon.id);
                cart.unsetFastCheckout();
                
            }
            else
            {
                
                //Bulk insert on table Cupon
                string response = mService.BuyMultipleCupons(JsonConvert.SerializeObject(cart.getAds()));
                cart.clean();
            }

            //Send email to user confirming purchase
            return RedirectToAction("Index", "User");
        }

        public ActionResult ShowPaymentDetails(int? id)
        {
            //Workaround
            //TODO: API should return proper json
            string pay_details = mService.GetBankReference().TrimStart('[').TrimEnd(']');
            //List<Int32> payment_details = JsonConvert.DeserializeObject<List<Int32>>(mService.GetBankReference());
            int reference = int.Parse(pay_details.Split(',')[0]);
            int entity = int.Parse(pay_details.Split(',')[1]);
            ViewBag.reference = reference;
            ViewBag.entity = entity;
            //END
            ShoppingCart cart = ((ShoppingCart)Session["shopping_cart"]);
            if (cart == null)
            {
                cart = new ShoppingCart();
                Session["shopping_cart"] = cart;
            }
            if (id.HasValue)
            {
                //fast
                string json_ad = mService.GetAdById((int)id);

                KeyValuePair<Ad, InstitutionModel> adResult = JsonConvert.DeserializeObject<KeyValuePair<Ad, InstitutionModel>>(json_ad);
                Ad cupon = adResult.Key;
                cupon.institution_name = adResult.Value.name;
                cupon.local = adResult.Value.city;

                if (cupon.state != "active" || (cupon.start_time.CompareTo(DateTime.Now) > 0 || cupon.end_time.CompareTo(DateTime.Now) < 0)) {
                    //cannot buy cupon
                    ViewBag.error = "The cupon you are trying to buy is not valid and may be expired, we are sorry for the inconvenience.";
                    return View("PaymentDetails", cart);

                } 
                   cart.addCupon(cupon,true);

                   ViewBag.ad_name = cupon.name;
                   ViewBag.ad_price = cupon.price;

            } else cart.unsetFastCheckout();

            UserSession us = (UserSession)Session["user"];
            JObject userjson = JObject.Parse(mService.GetClientDetails(us.internal_id));
            UserInfo ui = UserInfo.jsonToModel(userjson);

            cart.user = ui;
            
            return View("PaymentDetails",cart);
        }

        public bool addToCart(Ad cupon)
        {
            if (cupon.state != "active" || (cupon.start_time.CompareTo(DateTime.Now) < 0 || cupon.end_time.CompareTo(DateTime.Now) > 0))
            {
                //cannot buy cupon
                return false;

            }
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