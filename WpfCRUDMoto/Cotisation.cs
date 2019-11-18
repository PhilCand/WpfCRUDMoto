using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRUDMoto
{
    class Cotisation
    {

        public int Id { get; set; }
        public int Annee { get; set; }
        public double Montant { get; set; }
        public int MotardId { get; set; }

        public override string ToString()
        {
            return $"{Annee} - {Montant} €";
        }
    }
}
