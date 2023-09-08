using EspacioCadete;
using EspacioPedido;

namespace EspacioInforme;

public class Informe {
  public String GenerarInformeCadeteria(List<Cadete> listadoCadetes, List<Pedido> listadoPedidos) {
    string output = "";

    long totalRecaudado = listadoCadetes.Sum(cadete => cadete.JornalACobrar());
    output += 
    output += "- Informe de cierre\n";
    output += " x Monto total recaudado: " + totalRecaudado + "\n";
    output += " x Informe por cadete:\n";

    if (listadoPedidos.Count() != 0) {
      foreach (Cadete cadeteItem in listadoCadetes) {
        output += "   x Cadete " + cadeteItem.Nombre + "\n";
        output += "     x Pedidos: " + cadeteItem.ListadoPedidos.Count() + "\n";
        output += "     x Total recaudado: " + cadeteItem.JornalACobrar() + "\n";
        output += "     x Porcentaje respecto al total de pedidos: %" + (cadeteItem.ListadoPedidos.Count() * 100 / listadoPedidos.Count()) + "\n";
      }
    } else {
      Console.WriteLine("   x No hay pedidos registrados, se omite detalle por cadete.");
    }

    return output;
  }
}