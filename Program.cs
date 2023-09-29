using EspacioAccesoCSV;
using EspacioAccesoJSON;
using EspacioCadeteria;
using EspacioUtils;

internal class Program {
  private static void Main(string[] args) {
    Cadeteria cadeteria = InstanciarYPoblarCadeteria();
    int decision = MostrarMenuYPedirOpcion();

    while (true) {
      switch(decision) {
        case 1:
          cadeteria.InstanciarPedido();
          break;
        case 2:
          cadeteria.AsignarPedidoACadete();
          break;
        case 3:
          cadeteria.ActualizarEstadoPedido();
          break;
        case 4:
          cadeteria.ReasignarPedidoAOtroCadete();
          break;
        case 5:
          cadeteria.CobrarJornalCadete();
          break;
        case 6:
          Console.WriteLine(cadeteria.GenerarInforme());
          return;
      }

      Thread.Sleep(4000);
      Console.Clear();
      decision = MostrarMenuYPedirOpcion();
    }
  }

  public static int MostrarMenuYPedirOpcion() {
    Console.WriteLine("Ingrese alguna de las siguientes opciones:");
    
    Console.WriteLine(" 1- Dar de alta pedido");
    Console.WriteLine(" 2- Asignar pedido a cadete");
    Console.WriteLine(" 3- Cambiar estado de pedido");
    Console.WriteLine(" 4- Reasignar pedido a otro cadete");
    Console.WriteLine(" 5- Cobrar jornal de cadete");
    Console.WriteLine(" 6- Salir");

    int decision;
    if (int.TryParse(Console.ReadLine(), out decision)) {
      if (decision < 1 || decision > 6) {
        return MostrarMenuYPedirOpcion();
      }
      return decision;
    } else {
      Console.Clear();
      Console.WriteLine("x Opción invalida, porfavor reintente");
      return MostrarMenuYPedirOpcion();
    }
  }

  public static Cadeteria InstanciarYPoblarCadeteria() {
    Console.WriteLine("Desea cargar datos de: ");
    Console.WriteLine(" 1- Archivo CSV");
    Console.WriteLine(" 2- Archivo JSON");

    int opcion = Utils.PedirInt("Opcion", true);
    while (!new List<int>(){1, 2}.Contains(opcion)) {
      Console.WriteLine(" x Opcion invalida, por favor reintente:");
      opcion = Utils.PedirInt("Nro de pedido", true);
    }

    Cadeteria cadeteria = null;
    switch(opcion) {
      case 1:
        cadeteria = AccesoCSV.LeerCadeteria("cadeteria.csv");
        cadeteria.setListaCadetes(AccesoCSV.LeerCadetes("cadetes.csv"));
        break;
      case 2:
        cadeteria = AccesoJSON.LeerCadeteria("cadeteria.json");
        cadeteria.setListaCadetes(AccesoJSON.LeerCadetes("cadetes.json"));
        break;
    }

    Console.Clear();

    return cadeteria;
  }
}