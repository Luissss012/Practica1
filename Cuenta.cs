using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Practica_1_LuisdeLeón
{
    internal class Cuenta
    {
        public string NumeroCuenta;
        public string Pin;
        public decimal Saldo;

        public string getNumeroCuenta() {  return NumeroCuenta; }
        public string getPin() { return Pin; }
        public decimal getSaldo() { return Saldo; }
        public void setNumeroCuenta(string numeroCuenta) {this.NumeroCuenta = numeroCuenta;}
        public void setPin(string pin) {this.Pin = pin;}
        public void setSaldo(decimal saldo) {this.Saldo = saldo;}

        public Cuenta(string numeroCuenta,string Pin, decimal SaldoInicial)
        {
            this.NumeroCuenta = numeroCuenta;
            this.Pin = Pin;
            this.Saldo = SaldoInicial;
        }
        public bool ValidarPin(string pinIngresado)
        {
            return Pin == pinIngresado;
        }
        public bool Retirar(decimal monto)
        {
            if(Saldo >= monto)
            {
                Saldo -= monto;
                return true;
            }
            return false;
        }
        public void Depositar(decimal monto)
        {
            Saldo+= monto;
        }
    }
}
