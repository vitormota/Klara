using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebInstitution.Models
{
    public class SessionModel
    {
        public int manager_id { get; set; }

        public InstitutionModel currentInstitution { get; set; }

        public List<InstitutionModel> institutions { get; set; }

        public List<SelectListItem> getInstitutionsList()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (InstitutionModel i in institutions)
            {
                list.Add(new SelectListItem 
                {
                    Text = i.name,
                    Value = i.id.ToString()
                });
            }

            return list;
        }
    }
}