using EspacioCadete;
using EspacioPedido;

namespace EspacioCadeteria;

public class Cadeteria {
  private string? nombre;
  private long telefono;
  private List<Cadete> listadoCadetes;

  public string? Nombre { get => nombre; set => nombre = value; }
  public long Telefono { get => telefono; set => telefono = value; }
  public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }

  public Boolean CrearPedido(Cadete cadete, Pedido pedido) {
    return cadete.AgregarPedido(pedido);
  }

  public Boolean CancelarPedido(Cadete cadete, Pedido pedido) {
    return cadete.CancelarPedido(pedido);
  }

  public Pedido BuscaPedido(int nroPedido) {
    return this.listadoCadetes.Find(
      cadeteItem => cadeteItem.ListadoPedidos.Find(
        pedidoItem => pedidoItem.Nro == nroPedido
      ) != null
    ).ListadoPedidos.Find(pedidoItem => pedidoItem.Nro == nroPedido);
  }

  public Boolean ExistePedido(int nroPedido) {
    return this.listadoCadetes.Find(
      cadeteItem => cadeteItem.ListadoPedidos.Find(
        pedidoItem => pedidoItem.Nro == nroPedido
      ) != null
    ) != null;
  }

  public Cadete GetCadeteByPedidoNro(int nroPedido) {
    return this.listadoCadetes.Find(
      cadeteItem => cadeteItem.ListadoPedidos.Find(
        pedidoItem => pedidoItem.Nro == nroPedido
      ) != null
    );
  }
}