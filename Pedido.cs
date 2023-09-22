using EspacioCliente;
using EspacioCadete;

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
  private Cadete cadete;

  public int Nro { get => nro; }
  public string? Obs { get => obs; }
  public string Estado { get => estado.ToString(); }
  public Cadete Cadete { get => cadete; }
  public const int PRECIO_PEDIDO = 500;

  public Pedido(int nro, string? obs, Cliente cliente) {
    this.nro = nro;
    this.obs = obs;
    this.cliente = cliente;
    this.estado = PEDIDO_ESTADOS.PENDIENTE;
  }

  public void ActualizarEstado(PEDIDO_ESTADOS nuevoEstado) {
    this.estado = nuevoEstado;
  }

  public Boolean AsignarPedido(Cadete cadete) {
    Cadete cadeteAnterior = this.cadete;
    this.cadete = cadete;
    return this.cadete == cadeteAnterior || this.cadete == cadeteAnterior;
  }

  public void Cancelar() {
    this.estado = PEDIDO_ESTADOS.CANCELADO;
  }

  public override string ToString() {
    string pedidoString = "Pedido N° " + this.Nro;
    if (!String.IsNullOrEmpty(this.Obs)) {
      pedidoString += " (" + this.Obs + ").";
    }
    return pedidoString;
  }

  public static int MostrarEstadosYPedirOpcion() {
    Console.WriteLine("\n");
    Console.WriteLine("Ingrese que estado desea asignarle al pedido:");
    
    Console.WriteLine(" 0- CANCELADO");
    Console.WriteLine(" 1- PENDIENTE");
    Console.WriteLine(" 2- ASIGNADO");
    Console.WriteLine(" 3- EN CAMINO");
    Console.WriteLine(" 4- COMPLETADO");

    int decision;
    if (int.TryParse(Console.ReadLine(), out decision)) {
      if (decision < 1 || decision > 5) {
        return MostrarEstadosYPedirOpcion();
      }
      return decision;
    } else {
      Console.Clear();
      Console.WriteLine("x Opción invalida, porfavor reintente");
      return MostrarEstadosYPedirOpcion();
    }
  }
}