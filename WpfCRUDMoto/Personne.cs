using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRUDMoto
{
    public class Personne
    {
        public Personne()
        {
        }

        public Personne(string nom, int id)
        {
            Nom = nom;
            Id = id;
        }

        public string Nom { get; set; }
        public int Id { get; set; }
        public List<Moto> Garage { get; set; } = new List<Moto>();

        public override string ToString()
        {
            return $"{Id} - {Nom}";
        }
    }
}
