namespace EspacioAccesoJSON;

using EspacioAccesoADatos;
using EspacioCadete;
using EspacioCadeteria;
using System.Text.Json;

public class AccesoJSON: IAccesoADAtos {
  static public List<Cadete> LeerCadetes(string nombreDeArchivo) {
    List<Cadete> listaDeCadetes = new List<Cadete>();

    if (IAccesoADAtos.Existe(nombreDeArchivo)) {
      string? contenidoDeArchivoDeCadetes = File.ReadAllText(nombreDeArchivo);
      listaDeCadetes = JsonSerializer.Deserialize<List<Cadete>>(contenidoDeArchivoDeCadetes);
    }

    return listaDeCadetes;
  }

  static public Cadeteria LeerCadeteria(string nombreDeArchivo) {
    Cadeteria cadeteria = null;

    if (IAccesoADAtos.Existe(nombreDeArchivo)) {
      string? contenidoDeArchivoCadeteria = File.ReadAllText(nombreDeArchivo);
      cadeteria = JsonSerializer.Deserialize<Cadeteria>(contenidoDeArchivoCadeteria);
    }

    return cadeteria;
  }
}