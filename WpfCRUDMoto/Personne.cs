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
            IsRed = true;
        }

        public Personne(string nom, int id)
        {
            Nom = nom;
            Id = id;
            IsRed = true;
            
        }

        public string Nom { get; set; }
        public int Id { get; set; }
        public List<Moto> Garage { get; set; } = new List<Moto>();
        public bool IsRed { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Nom}";
        }
    }
}
