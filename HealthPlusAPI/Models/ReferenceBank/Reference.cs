using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthPlusAPI.Models.ReferenceBank
{
    public class Reference
    {
        public static List<Int32> NewReference()
        {
            List<Int32> reference = new List<Int32>();
            
            // Referencia usada nos pagamentos multibanco
            // A referencia e um valor novo, gerado a cada pedido de uma nova referencia multibanco 
            reference.Add(123456789);

            // Entidade usada nos pagamentos multibanco: 99666
            // A entidade e um valor atribuido a empresa e que nao pode ser modificado
            reference.Add(99666);

            return reference;
        }
    }
}