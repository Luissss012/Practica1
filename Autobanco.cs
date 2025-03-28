using System;
using System.Collections.Generic;
using Practica_1_LuisdeLeón;
using System.IO;


class Autobanco
{
    private List<Cuenta> cuentas = new List<Cuenta>();
    private Boveda bovedaQ = new Boveda("Quetzales", 20, 50);

    public void Iniciar()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== AUTOBANCO =====");
            Console.WriteLine("1. Configurar Cuentas");
            Console.WriteLine("2. Realizar Retiro");
            Console.WriteLine("3. Realizar Depósito");
            Console.WriteLine("4. Transferir");
            Console.WriteLine("5. Comprar dólares");
            Console.WriteLine("6. Vender dólares");
            Console.WriteLine("7. Ver Saldo en Bóvedas");
            Console.WriteLine("8. Ver cuentas existentes");
            Console.WriteLine("9. Actualizar bovedas");
            Console.WriteLine("10. Eliminar cuenta");
            Console.WriteLine("11. Configurar cuenta");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    ConfigurarCuenta();
                    break;
                case "2":
                    RealizarRetiro();
                    break;
                case "3":
                    RealizarDeposito();
                    break;
                case "4":
                    Transferir();
                    break;
                case "5":
                    ComprarDolares();
                    break;
                case "6":
                    VenderDolares();
                    break;
                case "7":
                    VerSaldoEnBovedas();
                    break;
                case "8":
                    VerCuentasExistentes();
                    break;
                case "9":
                    ActualizarBovedas();
                    break;
                case "10":
                    EliminarCuenta();
                    break;
                default:
                    Console.WriteLine("Opción inválida. Presione ENTER para continuar.");
                    Console.ReadLine();
                    break;
            }
        }
    }


    private void ModificarCuenta()
    {
        Console.Clear();
        Console.WriteLine("+++ CONFIGURAR CUENTA +++");

        Console.Write("Ingrese número de cuenta a configurar (deje en blanco para crear una nueva cuenta): ");
        string numeroCuenta = Console.ReadLine();

        Cuenta cuenta = cuentas.Find(c => c.NumeroCuenta == numeroCuenta);

        if (cuenta == null)
        {
            if (cuentas.Count >= 3)
            {
                Console.WriteLine("Ya hay tres cuentas registradas.");
                Console.ReadLine();
                return;
            }

            Console.Write("Ingrese nuevo número de cuenta: ");
            numeroCuenta = Console.ReadLine();

            Console.Write("Ingrese nuevo PIN: ");
            string pin = Console.ReadLine();

            Console.Write("Ingrese saldo inicial: ");
            decimal saldo = decimal.Parse(Console.ReadLine());

            cuentas.Add(new Cuenta(numeroCuenta, pin, saldo));
            Console.WriteLine("Cuenta registrada exitosamente.");
        }
        else
        {
            Console.Write("Ingrese nuevo PIN (deje en blanco para no cambiar): ");
            string nuevoPin = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevoPin))
            {
                cuenta.Pin = nuevoPin;
            }

            Console.Write("Ingrese nuevo saldo (deje en blanco para no cambiar): ");
            string nuevoSaldoStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevoSaldoStr))
            {
                decimal nuevoSaldo = decimal.Parse(nuevoSaldoStr);
                cuenta.Saldo = nuevoSaldo;
            }

            Console.WriteLine("Cuenta modificada exitosamente.");
        }

        Console.ReadLine();
    }

    private void EliminarCuenta()
    {
        Console.Clear();
        Console.WriteLine("+++ ELIMINAR CUENTA +++");

        Console.Write("Ingrese número de cuenta a eliminar: ");
        string numeroCuenta = Console.ReadLine();

        Cuenta cuenta = cuentas.Find(c => c.NumeroCuenta == numeroCuenta);
        if (cuenta == null)
        {
            Console.WriteLine("Cuenta no encontrada.");
            Console.ReadLine();
            return;
        }

        cuentas.Remove(cuenta);
        Console.WriteLine("Cuenta eliminada exitosamente.");
        Console.ReadLine();
    }


    private void VerCuentasExistentes()
    {
        Console.Clear();
        Console.WriteLine("+++ CUENTAS EXISTENTES +++");

        if (cuentas.Count == 0)
        {
            Console.WriteLine("No hay cuentas registradas.");
        }
        else
        {
            foreach (var cuenta in cuentas)
            {
                Console.WriteLine($"Número de Cuenta: {cuenta.NumeroCuenta}, Saldo Actual: {cuenta.Saldo}");
            }
        }

        Console.WriteLine("Presione ENTER para regresar al menú principal.");
        Console.ReadLine();
    }


    private void VerSaldoEnBovedas()
    {
        Console.Clear();
        Console.WriteLine("=== SALDO POR BÓVEDA Y DENOMINACIÓN ===");

        for (int i = 0; i < estaciones[0].Bovedas.Count; i++)
        {
            var boveda = estaciones[0].Bovedas[i];
            Console.WriteLine($"Bóveda {i + 1}: {boveda.Moneda}, Billetes de {boveda.Denominacion} cantidad: {boveda.CantidadBilletes}");
            Console.WriteLine($"Total de dinero por esta bóveda: {boveda.ObtenerSaldo()}");
            Console.WriteLine();
        }

        Console.WriteLine("Presione ENTER para regresar al menú principal.");
        Console.ReadLine();
    }

    private void ConfigurarCuenta()
    {
        if (cuentas.Count >= 3)
        {
            Console.WriteLine("Ya hay tres cuentas registradas.");
            Console.ReadLine();
            return;
        }

        Console.Write("Ingrese número de cuenta: ");
        string numeroCuenta = Console.ReadLine();

        Console.Write("Ingrese PIN: ");
        string pin = Console.ReadLine();

        Console.Write("Ingrese saldo inicial: ");
        decimal saldo = decimal.Parse(Console.ReadLine());

        cuentas.Add(new Cuenta(numeroCuenta, pin, saldo));
        Console.WriteLine("Cuenta registrada exitosamente.");
        Console.ReadLine();
    }

    private void RealizarRetiro()
    {
        Console.Write("Ingrese número de cuenta: ");
        string numeroCuenta = Console.ReadLine();

        Cuenta cuenta = cuentas.Find(c => c.NumeroCuenta == numeroCuenta);
        if (cuenta == null)
        {
            Console.WriteLine("Cuenta no encontrada.");
            Console.ReadLine();
            return;
        }

        Console.Write("Ingrese PIN: ");
        string pin = Console.ReadLine();
        if (!cuenta.ValidarPin(pin))
        {
            Console.WriteLine("PIN incorrecto.");
            Console.ReadLine();
            return;
        }

        Console.Write("Ingrese monto a retirar: ");
        decimal monto = decimal.Parse(Console.ReadLine());

        if (cuenta.Retirar(monto) && bovedaQ.Retirar(monto))
        {
            Console.WriteLine($"Retiro exitoso. Nuevo saldo: {cuenta.Saldo}");
        }
        else
        {
            Console.WriteLine("Fondos insuficientes.");
        }
        Console.ReadLine();
    }
    //


    private void ActualizarBovedas()
    {
        Console.Clear();
        Console.WriteLine("+++ ACTUALIZAR BÓVEDAS +++");

        for (int i = 0; i < estaciones[0].Bovedas.Count; i++)
        {
            var boveda = estaciones[0].Bovedas[i];
            Console.WriteLine($"Bóveda {i + 1}: {boveda.Moneda}, Billetes de {boveda.Denominacion} cantidad: {boveda.CantidadBilletes}");
            Console.WriteLine($"Total de dinero por esta bóveda: {boveda.ObtenerSaldo()}");
            Console.WriteLine();

            Console.Write($"Ingrese la cantidad de billetes de {boveda.Denominacion} para agregar a la bóveda {i + 1}: ");
            decimal cantidadAgregar = decimal.Parse(Console.ReadLine());

            boveda.Depositar(cantidadAgregar);
            Console.WriteLine($"Bóveda actualizada. Nueva cantidad de billetes: {boveda.CantidadBilletes}");
            Console.WriteLine($"Nuevo total de dinero por esta bóveda: {boveda.ObtenerSaldo()}");
            Console.WriteLine();
        }

        Console.WriteLine("Presione ENTER para regresar al menú principal.");
        Console.ReadLine();
    }

    private void Transferir()
    {
        Console.Write("Ingrese su número de cuenta: ");
        string origen = Console.ReadLine();

        Cuenta cuentaOrigen = cuentas.Find(c => c.NumeroCuenta == origen);
        if (cuentaOrigen == null)
        {
            Console.WriteLine("Cuenta de origen no encontrada.");
            Console.ReadLine();
            return;
        }

        Console.Write("Ingrese número de cuenta destino: ");
        string destino = Console.ReadLine();

        Cuenta cuentaDestino = cuentas.Find(c => c.NumeroCuenta == destino);
        if (cuentaDestino == null)
        {
            Console.WriteLine("Cuenta destino no encontrada.");
            Console.ReadLine();
            return;
        }

        Console.Write("Ingrese monto a transferir: ");
        decimal monto = decimal.Parse(Console.ReadLine());

        if (cuentaOrigen.Retirar(monto))
        {
            cuentaDestino.Depositar(monto);
            Console.WriteLine("Transferencia realizada.");
        }
        else
        {
            Console.WriteLine("Fondos insuficientes.");
        }
        Console.ReadLine();
    }

    //
    private void RealizarDeposito()
    {
        Console.Write("Ingrese número de cuenta: ");
        string numeroCuenta = Console.ReadLine();

        Cuenta cuenta = cuentas.Find(c => c.NumeroCuenta == numeroCuenta);
        if (cuenta == null)
        {
            Console.WriteLine("Cuenta no encontrada.");
            Console.ReadLine();
            return;
        }

        Console.Write("Ingrese monto a depositar: ");
        decimal monto = decimal.Parse(Console.ReadLine());

        cuenta.Depositar(monto);
        Console.WriteLine($"Depósito exitoso. Nuevo saldo: {cuenta.Saldo}");
        Console.ReadLine();
    }

    private List<Estacion> estaciones = new List<Estacion>
    {
        new Estacion("Estación 1"),
        new Estacion("Estación 2"),
        new Estacion("Estación 3"),
        new Estacion("Estación 4")
    };

    private decimal tasaCambio = 7.75m; // 1 USD = 7.75 Q

    private void ComprarDolares()
    {
        Console.Write("Ingrese número de cuenta: ");
        string numeroCuenta = Console.ReadLine();

        Cuenta cuenta = cuentas.Find(c => c.NumeroCuenta == numeroCuenta);
        if (cuenta == null)
        {
            Console.WriteLine("Cuenta no encontrada.");
            Console.ReadLine();
            return;
        }

        Console.Write("Ingrese monto en quetzales a cambiar: ");
        decimal montoQ = decimal.Parse(Console.ReadLine());

        decimal montoUSD = montoQ / tasaCambio;
        Console.WriteLine($"Recibirá: {montoUSD:F2} USD");

        cuenta.Retirar(montoQ);
        Console.WriteLine("Compra de dólares exitosa.");
        Console.ReadLine();
    }

    private void VenderDolares()
    {
        Console.Write("Ingrese número de cuenta: ");
        string numeroCuenta = Console.ReadLine();

        Cuenta cuenta = cuentas.Find(c => c.NumeroCuenta == numeroCuenta);
        if (cuenta == null)
        {
            Console.WriteLine("Cuenta no encontrada.");
            Console.ReadLine();
            return;
        }

        Console.Write("Ingrese monto en dólares a vender: ");
        decimal montoUSD = decimal.Parse(Console.ReadLine());

        decimal montoQ = montoUSD * tasaCambio;
        Console.WriteLine($"Recibirá: {montoQ:F2} Q");

        cuenta.Depositar(montoQ);
        Console.WriteLine("Venta de dólares exitosa.");
        Console.ReadLine();
    }
    private void RegistrarTransaccion(string tipo, string cuenta, decimal monto)
    {
        string ruta = "transacciones.csv";
        using (StreamWriter sw = new StreamWriter(ruta, true))
        {
            sw.WriteLine($"{DateTime.Now},{tipo},{cuenta},{monto}");
        }
    }
    
}




