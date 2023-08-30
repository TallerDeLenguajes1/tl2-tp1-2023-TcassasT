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
          Cadete cadete = new Cadete();
          cadete.Id = int.Parse(cadeteLineaPropiedades[0]);
          cadete.Nombre = cadeteLineaPropiedades[1];
          cadete.Direccion = cadeteLineaPropiedades[2];
          cadete.Telefono = long.Parse(cadeteLineaPropiedades[3]);
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