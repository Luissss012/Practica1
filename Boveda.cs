using System;

namespace Practica_1_LuisdeLeón
{
    internal class Boveda
    {
        public string Moneda { get; set; }
        public decimal CantidadBilletes { get; set; }
        public int Denominacion { get; set; }

        // Constructor
        public Boveda(string moneda, decimal cantidadBilletes, int denominacion)
        {
            Moneda = moneda;
            CantidadBilletes = cantidadBilletes;
            Denominacion = denominacion;
        }

        // Método para calcular el saldo
        public decimal ObtenerSaldo()
        {
            return CantidadBilletes * Denominacion;
        }

        // Método para retirar dinero
        public bool Retirar(decimal monto)
        {
            decimal saldo = ObtenerSaldo();
            if (monto > saldo) return false;

            decimal cantidadRetirar = monto / Denominacion;
            if (cantidadRetirar > CantidadBilletes) cantidadRetirar = CantidadBilletes;
            monto -= cantidadRetirar * Denominacion;

            if (monto == 0)
            {
                CantidadBilletes -= cantidadRetirar;
                return true;
            }
            return false;
        }

        // Método para depositar dinero
        public void Depositar(decimal cantidadBilletes)
        {
            CantidadBilletes += cantidadBilletes;
        }
    }
}