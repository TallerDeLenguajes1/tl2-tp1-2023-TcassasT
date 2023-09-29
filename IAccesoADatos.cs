namespace EspacioAccesoADatos;

using EspacioCadete;
using EspacioCadeteria;

public interface IAccesoADAtos {
  static public abstract List<Cadete> LeerCadetes(string nombreDeArchivo);
  static public abstract Cadeteria LeerCadeteria(string nombreDeArchivo);
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