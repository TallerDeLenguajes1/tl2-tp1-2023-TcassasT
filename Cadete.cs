using EspacioPedido;

namespace EspacioCadete;

public class Cadete {
  private const int PRECIO_PEDIDO = 500;

  private int id;
  private string? nombre;
  private string? direccion;
  private long telefono;
  private List<Pedido> listadoPedidos;

  public int Id { get => id; set => id = value; }
  public string? Nombre { get => nombre; set => nombre = value; }
  public string? Direccion { get => direccion; set => direccion = value; }
  public long Telefono { get => telefono; set => telefono = value; }

  public Boolean AgregarPedido(Pedido pedido) {
    int cantidadPreviaPedidos = this.listadoPedidos.Count();
    this.listadoPedidos.Add(pedido);

    return cantidadPreviaPedidos <= this.listadoPedidos.Count();
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

  public long JornalACobrar(int jornal) {
    List<Pedido> pedidosDelJornal = this.listadoPedidos.FindAll(pedidoItem => pedidoItem.Jornal == jornal);
    return pedidosDelJornal.Count() * PRECIO_PEDIDO;
  }
}