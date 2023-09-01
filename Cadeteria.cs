using EspacioCadete;
using EspacioPedido;
using EspacioDatos;
using EspacioUtils;
using EspacioCliente;

namespace EspacioCadeteria;

public class Cadeteria {
  private string? nombre;
  private long telefono;
  private List<Cadete> listadoCadetes;
  private List<Pedido> pedidos = new List<Pedido>();

  public string? Nombre { get => nombre; set => nombre = value; }
  public long Telefono { get => telefono; set => telefono = value; }
  public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }

  public Cadeteria(string? nombre, long telefono) {
    this.nombre = nombre;
    this.telefono = telefono;
    this.listadoCadetes = Datos.LeerCadetesCSV("cadetes.csv");
  }

  public Boolean CrearPedido(Cadete cadete, Pedido pedido) {
    return cadete.AgregarPedido(pedido);
  }

  public Boolean CancelarPedido(Cadete cadete, Pedido pedido) {
    return cadete.CancelarPedido(pedido);
  }

  private Pedido BuscaPedido(int nroPedido) {
    return this.listadoCadetes.Find(
      cadeteItem => cadeteItem.ListadoPedidos.Find(
        pedidoItem => pedidoItem.Nro == nroPedido
      ) != null
    ).ListadoPedidos.Find(pedidoItem => pedidoItem.Nro == nroPedido);
  }

  private Boolean ExistePedido(int nroPedido) {
    return this.listadoCadetes.Find(
      cadeteItem => cadeteItem.ListadoPedidos.Find(
        pedidoItem => pedidoItem.Nro == nroPedido
      ) != null
    ) != null;
  }

  private Cadete GetCadeteByPedidoNro(int nroPedido) {
    return this.listadoCadetes.Find(
      cadeteItem => cadeteItem.ListadoPedidos.Find(
        pedidoItem => pedidoItem.Nro == nroPedido
      ) != null
    );
  }

  private Cadete EligeCadete(Boolean mostrarListaDeCadetes) {
    if (mostrarListaDeCadetes) {
      Console.WriteLine("- Listado de cadetes:");
      foreach (Cadete cadeteItem in this.ListadoCadetes) {
        Console.WriteLine(" x " + cadeteItem.Nombre + ", N°: " + cadeteItem.Id + ". Pedidos asignados: " + cadeteItem.GetCantidadDePedidos());
      }
    }

    int idCadeteACobrar = Utils.PedirInt("Id de cadete", true);
    Cadete? cadeteACobrar = this.ListadoCadetes.Find(cadeteItem => cadeteItem.Id == idCadeteACobrar);
    while (cadeteACobrar == null && idCadeteACobrar != 0) {
      Console.WriteLine(" x Id de cadete invalido, porfavor reintente.");
      idCadeteACobrar = Utils.PedirInt("Nro de pedido", true);
      cadeteACobrar = this.ListadoCadetes.Find(cadeteItem => cadeteItem.Id == idCadeteACobrar);
    }

    return cadeteACobrar;
  }

  public void CobrarJornalCadete() {
    Cadete cadeteACobrar = EligeCadete(true);
    Console.WriteLine(" - Cadete " + cadeteACobrar.Nombre + " debe recibir: $" + cadeteACobrar.JornalACobrar());
  }

  public void ReasignarPedidoAOtroCadete() {
    Console.Clear();

    if (this.pedidos.Count() == 0) {
      Console.WriteLine(" x No hay pedidos disponibles para reasignar");
      return;
    }

    Console.WriteLine("- Listado de pedidos por cadete");

    foreach (Cadete cadeteItem in this.ListadoCadetes) {
      Console.WriteLine(" x " + cadeteItem.Id + "\t" + cadeteItem.Nombre);
      foreach (Pedido pedidoItem in cadeteItem.ListadoPedidos) {
        Console.WriteLine("   - " + pedidoItem.ToString());
      }
    }

    Console.WriteLine("\nPor favor elija el pedido a reasignar:");
    int nroPedidoAReasignar = Utils.PedirInt("Nro pedido", true);
    Pedido pedidoAReasignar = this.BuscaPedido(nroPedidoAReasignar);
    while (pedidoAReasignar == null && nroPedidoAReasignar != 0) {
      Console.WriteLine(" x Nro de pedido invalido, porfavor reintente.");
      nroPedidoAReasignar = Utils.PedirInt("Nro de pedido", true);
      pedidoAReasignar = this.BuscaPedido(nroPedidoAReasignar);
    }

    Console.WriteLine("\nAhora el cadete al que quiere asignar el pedido N° " + pedidoAReasignar.Nro + ":");
    Cadete cadeteAAsignar = EligeCadete(false);

    this.GetCadeteByPedidoNro(nroPedidoAReasignar).RemoverPedido(pedidoAReasignar);
    cadeteAAsignar.AgregarPedido(pedidoAReasignar);
  }

  public void ActualizarEstadoPedido() {
    Console.Clear();
    Console.WriteLine("- Listado de pedidos por cadete");

    foreach (Cadete cadeteItem in this.ListadoCadetes) {
      Console.WriteLine(" x " + cadeteItem.Id + "\t" + cadeteItem.Nombre);
      foreach (Pedido pedidoItem in cadeteItem.ListadoPedidos) {
        Console.WriteLine("   - " + pedidoItem.ToString());
      }
    }

    int nroPedidoAActualizar = Utils.PedirInt("Nro pedido", true);
    Pedido pedidoAActualizar = this.BuscaPedido(nroPedidoAActualizar);
    while (pedidoAActualizar == null && nroPedidoAActualizar != 0) {
      Console.WriteLine(" x Nro de pedido invalido, porfavor reintente.");
      nroPedidoAActualizar = Utils.PedirInt("Nro de pedido", true);
      pedidoAActualizar = this.BuscaPedido(nroPedidoAActualizar);
    }

    if (pedidoAActualizar == null) {
      return;
    }

    Console.WriteLine("\n");
    PEDIDO_ESTADOS nuevoEstado = (PEDIDO_ESTADOS) Pedido.MostrarEstadosYPedirOpcion();
    pedidoAActualizar.ActualizarEstado(nuevoEstado);
  }

  public Pedido InstanciarPedido() {
    Console.Clear();

    Console.WriteLine("Ingrese datos del cliente:");
    Cliente cliente = new Cliente();
    cliente.Nombre = Utils.PedirString("Nombre", true);
    cliente.Direccion = Utils.PedirString("Direccion", false);
    cliente.Telefono = Utils.PedirString("Telefono", true);
    cliente.DatosReferenciaDireccion = Utils.PedirString("Datos referencia de direccion", false);

    Console.WriteLine("\nIngrese datos del pedido:");
    Pedido pedido = new Pedido(this.pedidos.Count(), Utils.PedirString("Observaciones", false), cliente);

    int coutAntesDeAgregar = this.pedidos.Count();
    this.pedidos.Add(pedido);

    if (coutAntesDeAgregar < this.pedidos.Count()) {
      Console.WriteLine("== ¡Pedido instanciado exitosamente! ==");
    }

    return pedido;
  }

  public Boolean AsignarPedidoACadete() {
    Console.Clear();

    if (this.pedidos.Count() == 0) {
      Console.WriteLine(" x No hay pedidos disponibles para asignar");
      return false;
    }

    Console.WriteLine("- Listado de pedidos pendientes de asignación:");
    foreach (Pedido pedido in this.pedidos) {
      Console.WriteLine(" x " + pedido.ToString());
    }

    int nroPedidoAAsignar = Utils.PedirInt("Nro de pedido", true);
    Pedido? pedidoAAsignar = this.pedidos.Find(pedidoItem => pedidoItem.Nro == nroPedidoAAsignar);
    while (pedidoAAsignar == null && nroPedidoAAsignar != 0) {
      Console.WriteLine(" x Nro de pedido invalido, porfavor reintente.");
      nroPedidoAAsignar = Utils.PedirInt("Nro de pedido a asignar", true);
      pedidoAAsignar = this.pedidos.Find(pedidoItem => pedidoItem.Nro == nroPedidoAAsignar);
    }

    if (pedidoAAsignar == null) {
      return false;
    }

    Cadete cadeteAAsignar = EligeCadete(true);

    if (cadeteAAsignar == null) {
      return false;
    }

    return cadeteAAsignar.AgregarPedido(pedidoAAsignar);
  }

  public void ImprimirInforme() {
    long totalRecaudado = this.listadoCadetes.Sum(cadete => cadete.JornalACobrar());

    Console.Clear();
    Console.WriteLine("- Informe de cierre");
    Console.WriteLine(" x Monto total recaudado: " + totalRecaudado);
    Console.WriteLine(" x Informe por cadete:");

    foreach (Cadete cadeteItem in this.listadoCadetes) {
      Console.WriteLine("   x Cadete " + cadeteItem.Nombre);
      Console.WriteLine("     x Pedidos: " + cadeteItem.ListadoPedidos.Count());
      Console.WriteLine("     x Total recaudado: " + cadeteItem.JornalACobrar());
      Console.WriteLine("     x Porcentaje respecto al total de pedidos: " + cadeteItem.ListadoPedidos.Count() / this.pedidos.Count());
    }
  }
}