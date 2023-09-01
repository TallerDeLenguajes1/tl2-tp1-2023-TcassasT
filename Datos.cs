using EspacioCadete;
using EspacioPedido;

namespace EspacioDatos;

public class Datos {
  static public List<Cadete> LeerCadetesCSV(string nombreDeArchivo) {
    List<Cadete> listaDeCadetes = new List<Cadete>();

    if (Existe(nombreDeArchivo)) {
      string? contenidoDeArchivoDeProductos = File.ReadAllText(nombreDeArchivo);

      foreach (var cadeteLinea in contenidoDeArchivoDeProductos.Split("\n")) {
        string[] cadeteLineaPropiedades = cadeteLinea.Split(",");

        if (cadeteLineaPropiedades.Length == 4) {
          Cadete cadete = new Cadete(
            int.Parse(cadeteLineaPropiedades[0]),
            cadeteLineaPropiedades[1],
            cadeteLineaPropiedades[2],
            long.Parse(cadeteLineaPropiedades[3]),
            new List<Pedido>()
          );

          listaDeCadetes.Add(cadete);
        }
      }
    }

    return listaDeCadetes;
  }

  static public Boolean Existe(string nombreDeArchivo) {
    Boolean existeYTieneCotenido = false;

    if (File.Exists(nombreDeArchivo)) {
      string? contenidoDeArchivo = File.ReadAllText(nombreDeArchivo);

      if (!string.IsNullOrEmpty(contenidoDeArchivo)) {
        existeYTieneCotenido = true;
      }
    }

    return existeYTieneCotenido;
  }
}