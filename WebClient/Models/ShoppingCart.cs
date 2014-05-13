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

        private List<Ad> ads;

        public ShoppingCart()
        {
            ads = new List<Ad>();
        }

        public void addCupon(Ad cupon){
            ads.Add(cupon);
        }

        public void removeCupon(int cupon_id)
        {
            foreach (Ad ad in ads)
            {
                if (ad.id == cupon_id)
                {
                    ads.Remove(ad);
                }
            }
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
    }
}