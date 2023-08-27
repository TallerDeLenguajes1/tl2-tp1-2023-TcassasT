using EspacioCliente;

namespace EspacioPedido;

public enum PEDIDO_ESTADOS {
  CANCELADO,
  PENDIENTE,
  ASIGNADO,
  EN_CAMINO,
  COMPLETADO,
}

public class Pedido {
  private int nro;
  private string? obs;
  private Cliente cliente;
  private PEDIDO_ESTADOS estado;

  public int Nro { get => nro; set => nro = value; }
  public string? Obs { get => obs; set => obs = value; }

  public Pedido(int nro, string? obs, Cliente cliente) {
    this.nro = nro;
    this.obs = obs;
    this.cliente = cliente;
    this.estado = PEDIDO_ESTADOS.PENDIENTE;
  }

  public void ActualizarEstadoSiguiente() {
    if (this.estado != PEDIDO_ESTADOS.CANCELADO) {
      this.estado = this.estado + 1;
    }
  }
  public void Cancelar() {
    this.estado = PEDIDO_ESTADOS.CANCELADO;
  }
}