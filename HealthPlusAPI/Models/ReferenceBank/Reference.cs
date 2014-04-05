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
            List<Int32> payment = new List<Int32>();

            // Referencia usada nos pagamentos multibanco
            // A referencia e um valor novo, gerado a cada pedido de uma nova referencia multibanco 
            Random rnd = new Random();
            Int32 first_three = rnd.Next(0, 1000); // primeiros tres algarismos da referencia
            Int32 second_three = rnd.Next(0, 1000); // segundos tres algarismos da referencia
            Int32 third_three = rnd.Next(0, 1000); // ultimos tres algarismos da referencia

            Int32 reference = (first_three * 1000000) + (second_three * 1000) + third_three; // formacao do valor da referencia
            payment.Add(reference); // adicionar a referencia a lista


            // Entidade usada nos pagamentos multibanco: 99666
            // A entidade e um valor atribuido a empresa e que nao pode ser modificado
            Int32 entity = 99666;
            payment.Add(entity); // adicionar a entidade a lista

            return payment;
        }
    }
}