using EspacioCadete;
using EspacioCliente;
using EspacioPedido;

internal class Program {
  private static void Main(string[] args) {

  }

  public static Pedido InstanciarPedido(int pedidoNro) {
    Console.Clear();

    Console.WriteLine("Ingrese datos del cliente:");
    Cliente cliente = new Cliente();
    cliente.Nombre = PedirString("Nombre", true);
    cliente.Direccion = PedirString("Direccion", false);
    cliente.Telefono = PedirString("Telefono", true);
    cliente.DatosReferenciaDireccion = PedirString("Datos referencia de direccion", false);

    Console.WriteLine("\nIngrese datos del pedido:");
    Pedido pedido = new Pedido(pedidoNro, PedirString("Observaciones", false), cliente);

    return pedido;
  }

  public static Boolean AsignarPedidoACadete(Pedido pedido, Cadete cadete) {
    return cadete.AgregarPedido(pedido);
  }

  public static void ActualizarEstadoPedido(Pedido pedido) {
    PEDIDO_ESTADOS nuevoEstado = (PEDIDO_ESTADOS) MostrarEstadosYPedirOpcion();
    pedido.ActualizarEstado(nuevoEstado);
  }

  public static void ReasignarPedidoAOtroCadete(Cadete cadeteAnterior, Cadete cadeteNuevo, Pedido pedido) {
    cadeteAnterior.RemoverPedido(pedido);
    cadeteNuevo.AgregarPedido(pedido);
  }

  public static string? PedirString(string key, Boolean obligatorio) {
    string? input = Console.ReadLine();
    Console.WriteLine(" - " + key + ":");
    if (input == null && obligatorio) {
      Console.WriteLine("x Input invalido, porfavor reintente.");
      return PedirString(key, obligatorio);
    }
    return input;
  }

  public static int PedirInt(string key, Boolean obligatorio) {
    int input;
    Console.WriteLine(" - " + key + ":");
    if (!int.TryParse(Console.ReadLine(), out input) && obligatorio) {
        Console.WriteLine("x Input invalido, porfavor reintente:");
        return PedirInt(key, obligatorio);
    }
    return input;
  }

  public static int MostrarMenuYPedirOpcion() {
    Console.WriteLine("\n");
    Console.WriteLine("Ingrese alguna de las siguientes opciones:");
    
    Console.WriteLine(" 1- Dar de alta pedido");
    Console.WriteLine(" 2- Asignar pedido a cadete");
    Console.WriteLine(" 3- Cambiar estado de pedido");
    Console.WriteLine(" 4- Reasignar pedido a otro cadete");
    Console.WriteLine(" 5- Salir");

    int decision;
    if (int.TryParse(Console.ReadLine(), out decision)) {
      if (decision < 1 || decision > 5) {
        return MostrarMenuYPedirOpcion();
      }
      return decision;
    } else {
      Console.Clear();
      Console.WriteLine("x Opción invalida, porfavor reintente");
      return MostrarMenuYPedirOpcion();
    }
  }

  public static int MostrarEstadosYPedirOpcion() {
    Console.WriteLine("\n");
    Console.WriteLine("Ingrese que estado desea asignarle al pedido:");
    
    Console.WriteLine(" 0- CANCELADO");
    Console.WriteLine(" 1- PENDIENTE");
    Console.WriteLine(" 2- ASIGNADO");
    Console.WriteLine(" 3- EN CAMINO");
    Console.WriteLine(" 4- COMPLETADO");

    int decision;
    if (int.TryParse(Console.ReadLine(), out decision)) {
      if (decision < 1 || decision > 5) {
        return MostrarEstadosYPedirOpcion();
      }
      return decision;
    } else {
      Console.Clear();
      Console.WriteLine("x Opción invalida, porfavor reintente");
      return MostrarEstadosYPedirOpcion();
    }
  }
}