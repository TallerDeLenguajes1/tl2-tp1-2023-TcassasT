using EspacioCliente;

namespace EspacioPedido;

public enum PEDIDO_ESTADOS {
  PENDIENTE,
  ENVIO,
  COMPLETADO,
  CANCELADO
}

public class Pedido {
  private int nro;
  private string? obs;
  private Cliente cliente;
  private PEDIDO_ESTADOS estado;

  public int Nro { get => nro; set => nro = value; }
  public string? Obs { get => obs; set => obs = value; }
  public Cliente Cliente { get => cliente; set => cliente = value; }
  public PEDIDO_ESTADOS Estado { get => estado; set => estado = value; }
}