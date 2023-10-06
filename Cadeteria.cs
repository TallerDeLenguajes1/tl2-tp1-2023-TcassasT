using EspacioCadete;
using EspacioPedido;
using EspacioCliente;
using EspacioInforme;
using System.Text.Json.Serialization;

namespace EspacioCadeteria;

public class Cadeteria {
  private string? nombre;
  private long telefono;
  private List<Cadete> listadoCadetes;
  private List<Pedido> pedidos = new List<Pedido>();

  [JsonPropertyName("nombre")]
  public string? Nombre { get => nombre; }
  [JsonPropertyName("telefono")]
  public long Telefono { get => telefono; }
  public List<Cadete> ListadoCadetes { get => listadoCadetes; }
  public List<Pedido> Pedidos { get => pedidos; }

  public Cadeteria(string? nombre, long telefono) {
    this.nombre = nombre;
    this.telefono = telefono;
  }

  public void ReasignarPedidoAOtroCadete(int nroPedido, int cadeteId) {
    Pedido pedidoAReasignar = this.GetPedidoByNro(nroPedido);
    Cadete cadeteAAsignar = this.GetCadeteById(cadeteId);
    pedidoAReasignar.AsignarPedido(cadeteAAsignar);
  }

  public void ActualizarEstadoPedido(int nroPedido, PEDIDO_ESTADOS nuevoEstado) {
    Pedido pedidoAActualizar = this.GetPedidoByNro(nroPedido);
    pedidoAActualizar.ActualizarEstado(nuevoEstado);
  }

  public Pedido InstanciarPedido(String nombre, String direccion, String telefono, String? datosReferencia, String? obs) {
    Cliente cliente = new Cliente();
    cliente.Nombre =  nombre;
    cliente.Direccion = direccion;
    cliente.Telefono = telefono;
    cliente.DatosReferenciaDireccion = datosReferencia;
    Pedido pedido = new Pedido(this.GetCantidadDePedidos(), obs, cliente);
    this.pedidos.Add(pedido);
    return pedido;
  }

  public Boolean AsignarPedidoACadete(int nroPedido, int cadeteId) {
    Pedido pedidoAAsignar = this.GetPedidoByNro(nroPedido);
    Cadete cadeteAAsignar = this.GetCadeteById(cadeteId);
    return pedidoAAsignar.AsignarPedido(cadeteAAsignar);
  }

  public Informe GenerarInforme() {
    return Informe.GenerarInformeCadeteria(this);
  }

  public Boolean RemoverPedido(int pedidoNro) {
    int cantidadPreviaPedidos = this.GetCantidadDePedidos();
    Pedido pedidoARemover = this.GetPedidoByNro(pedidoNro);
    this.pedidos.Remove(pedidoARemover);
    return cantidadPreviaPedidos >= this.GetCantidadDePedidos();
  }

  public Boolean CancelarPedido(int pedidoNro) {
    Pedido pedidoACancelar = this.GetPedidoByNro(pedidoNro);

    if (pedidoACancelar != null) {
      pedidoACancelar.Cancelar();

      return true;
    } else {
      return false;
    }
  }

  public long JornalACobrar(int cadeteId) {
    List<Pedido> pedidosCompletados = this.pedidos.FindAll(pedidoItem =>
      pedidoItem.Estado == PEDIDO_ESTADOS.COMPLETADO.ToString() &&
      pedidoItem.Cadete.Id == cadeteId
    );
    return pedidosCompletados.Count() * Pedido.PRECIO_PEDIDO;
  }

  public int GetCantidadDePedidos() {
    return this.pedidos.Count();
  }

  private List<Pedido> GetPedidosSinAsignar() {
    return this.pedidos.FindAll(pedidoItem =>
      pedidoItem.Cadete == null &&
      pedidoItem.Estado != PEDIDO_ESTADOS.COMPLETADO.ToString()
    );
  }

  public List<Pedido> GetPedidosDeCadete(int cadeteId) {
    return this.pedidos.FindAll(pedidoItem =>
      pedidoItem.Cadete != null &&
      pedidoItem.Cadete.Id == cadeteId
    );
  }

  private Pedido GetPedidoByNro(int nroPedido) {
    return this.pedidos.Find(pedidoItem => pedidoItem.Nro == nroPedido);
  }

  private Cadete GetCadeteById(int cadeteId) {
    return this.listadoCadetes.Find(cadeteItem => cadeteItem.Id == cadeteId);
  }
}