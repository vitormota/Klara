using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HpREST_Bridge.Auth
{
    public enum REQUEST
    {
        EMAIL = 5,
        USERNAME = 10,
        PROF_PIC = 15,
        HOMETWON = 20
    }

    interface ILoginProvider
    {
       

        void addFieldRequest(params REQUEST[] reqests);

        Object makeRequest();


    }
}
