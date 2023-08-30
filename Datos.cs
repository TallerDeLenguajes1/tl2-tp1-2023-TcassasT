using EspacioCadete;
using EspacioPedido;

namespace EspacioDatos;

public class Datos {
  static public List<Cadete> LeerCadetesCSV(string nombreDeArchivo) {
    List<Cadete> listaDeCadetes = new List<Cadete>();

    if (Existe(nombreDeArchivo)) {
      string? contenidoDeArchivoDeProductos = File.ReadAllText(nombreDeArchivo);

      Cadete cadete = new Cadete();
      foreach (var cadeteLinea in contenidoDeArchivoDeProductos.Split("\n")) {
        List<string> cadeteLineaPropiedades = new List<string>();

        for(int i = 0; i < cadeteLineaPropiedades.Count(); i++) {
          switch(i) {
            case 0:
              cadete.Id = int.Parse(cadeteLineaPropiedades[i]);
              break;
            case 1:
              cadete.Nombre = cadeteLineaPropiedades[i];
              break;
            case 2:
              cadete.Direccion = cadeteLineaPropiedades[i];
              break;
            case 3:
              cadete.Telefono = long.Parse(cadeteLineaPropiedades[i]);
              break;
          }
        }
      }

      listaDeCadetes.Add(cadete);
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