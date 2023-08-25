using EspacioCadete;

namespace EspacioCadeteria;

public class Cadeteria {
  private string? nombre;
  private long telefono;
  private List<Cadete> listadoCadetes;

  public string? Nombre { get => nombre; set => nombre = value; }
  public long Telefono { get => telefono; set => telefono = value; }
  public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
}