namespace EspacioAccesoCSV;

using EspacioAccesoADatos;
using EspacioCadete;
using EspacioCadeteria;
using EspacioPedido;

public class AccesoCSV: IAccesoADAtos {
  static public List<Cadete> LeerCadetes(string nombreDeArchivo) {
    List<Cadete> listaDeCadetes = new List<Cadete>();

    if (IAccesoADAtos.Existe(nombreDeArchivo)) {
      string? contenidoDeArchivoDeCadetes = File.ReadAllText(nombreDeArchivo);

      foreach (var cadeteLinea in contenidoDeArchivoDeCadetes.Split("\n")) {
        string[] cadeteLineaPropiedades = cadeteLinea.Split(",");

        if (cadeteLineaPropiedades.Length == 4) {
          Cadete cadete = new Cadete(
            int.Parse(cadeteLineaPropiedades[0]),
            cadeteLineaPropiedades[1],
            cadeteLineaPropiedades[2],
            long.Parse(cadeteLineaPropiedades[3])
          );

          listaDeCadetes.Add(cadete);
        }
      }
    }

    return listaDeCadetes;
  }

  static public Cadeteria LeerCadeteria(string nombreDeArchivo) {
    Cadeteria cadeteria = null;

    if (IAccesoADAtos.Existe(nombreDeArchivo)) {
      string? contenidoDeArchivoDeCadeteria = File.ReadAllText(nombreDeArchivo);
      string[] cadeteriaLineaPropiedades = contenidoDeArchivoDeCadeteria.Split(",");

      if (cadeteriaLineaPropiedades.Length == 2) {
        cadeteria = new Cadeteria(
          cadeteriaLineaPropiedades[0],
          long.Parse(cadeteriaLineaPropiedades[1])
        );
      }
    }

    return cadeteria;
  }
}