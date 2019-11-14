using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRUDMoto
{
    public class Moto
    {
        public string Marque { get; set; }
        public int Cylindree { get; set; }
        public int Id { get; set; }
        public int IdProprietaire { get; set; }

        public override string ToString()
        {
            return $"{Marque} - {Cylindree}";
        }
    }
}
