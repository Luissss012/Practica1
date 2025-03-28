using System;
using System.Collections.Generic;

namespace Practica_1_LuisdeLeón
{
    internal class Estacion
    {
        public string Nombre { get; set; }
        public List<Boveda> Bovedas { get; set; }

        public Estacion(string nombre)
        {
            this.Nombre = nombre;

            Bovedas = new List<Boveda>
            {
                new Boveda("Quetzales", 0, 50), // Bóveda 1: Billetes de 50 Quetzales
                new Boveda("Quetzales", 0, 10), // Bóveda 2: Billetes de 10 Quetzales
                new Boveda("Quetzales", 0, 1),  // Bóveda 3: Billetes de 1 Quetzal
                new Boveda("Dolares", 0, 1)    // Bóveda 4: Billetes de 20 Dólares
            };
        }
    }
}