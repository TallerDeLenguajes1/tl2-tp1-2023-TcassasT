using EspacioCadete;
using EspacioCadeteria;
using EspacioPedido;

namespace EspacioInforme;

public class Informe {
  public String GenerarInformeCadeteria(Cadeteria cadeteria) {
    string output = "";

    long totalRecaudado = cadeteria.ListadoCadetes.Sum(cadete => cadeteria.JornalACobrar(cadete));
    output += 
    output += "- Informe de cierre\n";
    output += " x Monto total recaudado: " + totalRecaudado + "\n";
    output += " x Informe por cadete:\n";

    if (cadeteria.Pedidos.Count() != 0) {
      foreach (Cadete cadeteItem in cadeteria.ListadoCadetes) {
        output += "   x Cadete " + cadeteItem.Nombre + "\n";
        output += "     x Pedidos: " + cadeteria.GetCantidadDePedidos(cadeteItem) + "\n";
        output += "     x Total recaudado: " + cadeteria.JornalACobrar(cadeteItem) + "\n";
        output += "     x Porcentaje respecto al total de pedidos: %" + (cadeteria.GetCantidadDePedidos(cadeteItem) * 100 / cadeteria.GetCantidadDePedidos()) + "\n";
      }
    } else {
      Console.WriteLine("   x No hay pedidos registrados, se omite detalle por cadete.");
    }

    return output;
  }
}