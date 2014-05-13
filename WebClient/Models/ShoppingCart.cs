using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient_.Models
{
    /// <summary>
    /// Simple Shopping cart to be stored on session
    /// contains a list of Cupons (Ad) to be bought
    /// </summary>
    public class ShoppingCart
    {
        private int fcheckout_ad = 0;
        private List<Ad> ads;

        public ShoppingCart()
        {
            ads = new List<Ad>();
        }

        public bool isFastCheckout()
        {
            return fcheckout_ad != 0 ? true : false;
        }

        public void unsetFastCheckout()
        {
            fcheckout_ad = 0;
        }

        public void addCupon(Ad cupon, bool fast_checkout = false){
            if (fast_checkout) fcheckout_ad = cupon.id;
            ads.Add(cupon);
        }

        public void removeCupon(int cupon_id)
        {
            foreach (Ad ad in ads)
            {
                if (ad.id == cupon_id)
                {
                    ads.Remove(ad);
                    return;
                }
            }
        }

        public decimal getSubTotal()
        {
            if (isFastCheckout())
            {
                return getAdById(fcheckout_ad).price;
            }
            decimal result = 0;
            foreach (Ad a in ads)
            {
                result += a.price;
            }
            return result;
        }

        public int getCount()
        {
            return ads.Count();
        }

        public IReadOnlyCollection<Ad> getAds()
        {
            return ads.AsReadOnly();
        }

        public Ad getAdById(int id)
        {
            foreach (Ad ad in ads)
            {
                if (ad.id == id)
                {
                    return ad;
                }
            }
            return null;
        }

        public Ad getFCheckoutAd()
        {
            if (!isFastCheckout()) return null;
            return getAdById(fcheckout_ad);
        }

        public void clean()
        {
            unsetFastCheckout();
            ads.Clear();
        }
    }
}