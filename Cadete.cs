using EspacioPedido;

namespace EspacioCadete;

public class Cadete {
  private const int PRECIO_PEDIDO = 500;

  private int id;
  private string? nombre;
  private string? direccion;
  private long telefono;
  private List<Pedido> listadoPedidos = new List<Pedido>();

  public int Id { get => id; set => id = value; }
  public string? Nombre { get => nombre; set => nombre = value; }
  public string? Direccion { get => direccion; set => direccion = value; }
  public long Telefono { get => telefono; set => telefono = value; }
  public List<Pedido> ListadoPedidos { get => listadoPedidos; }

  public Cadete(int id, string nombre, string direccion, long telefono, List<Pedido> pedidos) {
    this.id = id;
    this.nombre = nombre;
    this.direccion = direccion;
    this.telefono = telefono;
    this.listadoPedidos = pedidos;
  }

  public Boolean AgregarPedido(Pedido pedido) {
    int cantidadPreviaPedidos = this.listadoPedidos.Count();
    this.listadoPedidos.Add(pedido);

    return cantidadPreviaPedidos <= this.listadoPedidos.Count();
  }

  public Boolean RemoverPedido(Pedido pedido) {
    int cantidadPreviaPedidos = this.listadoPedidos.Count();
    this.listadoPedidos.Remove(pedido);

    return cantidadPreviaPedidos >= this.listadoPedidos.Count();
  }

  public Boolean CancelarPedido(Pedido pedido) {
    Pedido pedidoACancelar = this.listadoPedidos.Find(pedidoItem => pedidoItem.Nro == pedido.Nro);

    if (pedidoACancelar != null) {
      pedidoACancelar.Cancelar();

      return true;
    } else {
      return false;
    }
  }

  public Boolean ActualizarEstadoPedido(Pedido pedido, PEDIDO_ESTADOS nuevoEstado) {
    Pedido pedidoAUpdetear = this.listadoPedidos.Find(pedidoItem => pedidoItem.Nro == pedido.Nro);

    if (pedidoAUpdetear != null) {
      pedidoAUpdetear.ActualizarEstado(nuevoEstado);

      return true;
    } else {
      return false;
    }
  }

  public long JornalACobrar() {
    List<Pedido> pedidosCompletados = this.listadoPedidos.FindAll(pedidoItem => pedidoItem.Estado == PEDIDO_ESTADOS.COMPLETADO.ToString());
    return pedidosCompletados.Count() * PRECIO_PEDIDO;
  }

  public int GetCantidadDePedidos() {
    return this.listadoPedidos.Count();
  }
}