using EspacioPedido;

namespace EspacioCadete;

using System.Text.Json.Serialization;

public class Cadete {
  private int id;
  private string? nombre;
  private string? direccion;
  private long telefono;

  [JsonPropertyName("id")]
  public int Id { get => id; }
  [JsonPropertyName("nombre")]
  public string? Nombre { get => nombre; }
  [JsonPropertyName("direccion")]
  public string? Direccion { get => direccion; }
  [JsonPropertyName("telefono")]
  public long Telefono { get => telefono; }

  public Cadete(int id, string nombre, string direccion, long telefono) {
    this.id = id;
    this.nombre = nombre;
    this.direccion = direccion;
    this.telefono = telefono;
  }
}