using EspacioCadete;
using EspacioCadeteria;
using EspacioCliente;
using EspacioPedido;

internal class Program {
  private static Cadeteria cadeteria = new Cadeteria();
  private static List<Pedido> pedidosSinAsignar = new List<Pedido>();

  private static void Main(string[] args) {
    int decision = MostrarMenuYPedirOpcion();
    int idPedidoAutoIncremental = 1;

    while (decision != 6) {
      switch(decision) {
        case 1:
          pedidosSinAsignar.Add(InstanciarPedido(idPedidoAutoIncremental));
          idPedidoAutoIncremental += 1;
          break;
        case 2:
          AsignarPedidoACadete(cadeteria);
          break;
        case 3:
          ActualizarEstadoPedido(cadeteria);
          break;
        case 4:
          ReasignarPedidoAOtroCadete(cadeteria);
          break;
      }
    }
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

  public static Boolean AsignarPedidoACadete(Cadeteria cadeteria) {
    Console.Clear();
    Console.WriteLine("- Listado de pedidos pendientes de asignación:");
    foreach (Pedido pedidoSinAsignar in pedidosSinAsignar) {
      Console.Write(" x " + pedidoSinAsignar.ToString());
    }

    int nroPedidoAAsignar = PedirInt("Nro de pedido", true);
    Pedido? pedidoAAsignar = pedidosSinAsignar.Find(pedidoItem => pedidoItem.Nro == nroPedidoAAsignar);
    while (pedidoAAsignar == null && nroPedidoAAsignar != 0) {
      Console.WriteLine(" x Nro de pedido invalido, porfavor reintente.");
      nroPedidoAAsignar = PedirInt("Nro de pedido", true);
      pedidoAAsignar = pedidosSinAsignar.Find(pedidoItem => pedidoItem.Nro == nroPedidoAAsignar);
    }

    if (pedidoAAsignar == null) {
      return false;
    }

    // --

    Console.WriteLine("- Listado de cadetes disponibles:");
    foreach (Cadete cadeteItem in cadeteria.ListadoCadetes) {
      Console.Write(" x " + cadeteItem.Nombre + ", N°: " + cadeteItem.Id + ". Pedidos asignados: " + cadeteItem.GetCantidadDePedidos());
    }

    int idCadeteAAsignar = PedirInt("Id de cadete", true);
    Cadete? cadeteAAsignar = cadeteria.ListadoCadetes.Find(cadeteItem => cadeteItem.Id == idCadeteAAsignar);
    while (cadeteAAsignar == null && idCadeteAAsignar != 0) {
      Console.WriteLine(" x Id de cadete invalido, porfavor reintente.");
      idCadeteAAsignar = PedirInt("Nro de pedido", true);
      cadeteAAsignar = cadeteAAsignar = cadeteria.ListadoCadetes.Find(cadeteItem => cadeteItem.Id == idCadeteAAsignar);
    }

    if (cadeteAAsignar == null) {
      return false;
    }

    Boolean asignacionResponse = cadeteAAsignar.AgregarPedido(pedidoAAsignar);

    if (asignacionResponse) {
      pedidosSinAsignar.Remove(pedidoAAsignar);
    }

    return asignacionResponse;
  }

  public static void ActualizarEstadoPedido(Cadeteria cadeteria) {
    Console.Clear();
    Console.WriteLine("- Listado de pedidos por cadete");

    foreach (Cadete cadeteItem in cadeteria.ListadoCadetes) {
      Console.Write(" x " + cadeteItem.Id + "\t" + cadeteItem.Nombre);
      foreach (Pedido pedidoItem in cadeteItem.ListadoPedidos) {
        Console.WriteLine("   - " + pedidoItem.ToString());
      }
    }

    int nroPedidoAActualizar = PedirInt("Nro pedido", true);
    Pedido pedidoAActualizar = cadeteria.BuscaPedido(nroPedidoAActualizar);
    while (pedidoAActualizar == null && nroPedidoAActualizar != 0) {
      Console.WriteLine(" x Nro de pedido invalido, porfavor reintente.");
      nroPedidoAActualizar = PedirInt("Nro de pedido", true);
      pedidoAActualizar = cadeteria.BuscaPedido(nroPedidoAActualizar);
    }

    if (pedidoAActualizar == null) {
      return;
    }

    Console.WriteLine("\n");
    PEDIDO_ESTADOS nuevoEstado = (PEDIDO_ESTADOS) MostrarEstadosYPedirOpcion();
    pedidoAActualizar.ActualizarEstado(nuevoEstado);
  }

  public static void ReasignarPedidoAOtroCadete(Cadeteria cadeteria) {
    Console.Clear();
    Console.WriteLine("- Listado de pedidos por cadete");

    foreach (Cadete cadeteItem in cadeteria.ListadoCadetes) {
      Console.WriteLine(" x " + cadeteItem.Id + "\t" + cadeteItem.Nombre);
      foreach (Pedido pedidoItem in cadeteItem.ListadoPedidos) {
        Console.WriteLine("   - " + pedidoItem.ToString());
      }
    }

    Console.WriteLine("\nPor favor elija el pedido a reasignar:");
    int nroPedidoAReasignar = PedirInt("Nro pedido", true);
    Pedido pedidoAReasignar = cadeteria.BuscaPedido(nroPedidoAReasignar);
    while (pedidoAReasignar == null && nroPedidoAReasignar != 0) {
      Console.WriteLine(" x Nro de pedido invalido, porfavor reintente.");
      nroPedidoAReasignar = PedirInt("Nro de pedido", true);
      pedidoAReasignar = cadeteria.BuscaPedido(nroPedidoAReasignar);
    }

    // --

    Console.WriteLine("\nAhora el cadete al que quiere asignar el pedido N° " + pedidoAReasignar.Nro + ":");
    int idCadeteAReasignar = PedirInt("Id de cadete", true);
    Cadete? cadeteAAsignar = cadeteria.ListadoCadetes.Find(cadeteItem => cadeteItem.Id == idCadeteAReasignar);
    while (cadeteAAsignar == null && idCadeteAReasignar != 0) {
      Console.WriteLine(" x Id de cadete invalido, porfavor reintente.");
      idCadeteAReasignar = PedirInt("Nro de pedido", true);
      cadeteAAsignar = cadeteAAsignar = cadeteria.ListadoCadetes.Find(cadeteItem => cadeteItem.Id == idCadeteAReasignar);
    }

    cadeteria.GetCadeteByPedidoNro(nroPedidoAReasignar).RemoverPedido(pedidoAReasignar);
    cadeteAAsignar.AgregarPedido(pedidoAReasignar);
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