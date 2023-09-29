using EspacioCadete;
using EspacioPedido;
using EspacioUtils;
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

  private Pedido BuscaPedido(int nroPedido) {
    return this.pedidos.Find(pedidoItem => pedidoItem.Nro == nroPedido);
  }

  private Boolean ExistePedido(int nroPedido) {
    return BuscaPedido(nroPedido) != null;
  }

  private Cadete GetCadeteByPedidoNro(int nroPedido) {
    return this.pedidos.Find(pedidoItem => pedidoItem.Nro == nroPedido).Cadete;
  }

  private Cadete EligeCadete(Boolean mostrarListaDeCadetes) {
    if (mostrarListaDeCadetes) {
      Console.WriteLine("- Listado de cadetes:");
      foreach (Cadete cadeteItem in this.ListadoCadetes) {
        Console.WriteLine(" x " + cadeteItem.Nombre + ", N°: " + cadeteItem.Id + ". Pedidos asignados: " + this.GetCantidadDePedidos(cadeteItem));
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

  public long CobrarJornalCadete() {
    Cadete cadeteACobrar = EligeCadete(true);
    long cantidadACobrar = JornalACobrar(cadeteACobrar);
    Console.WriteLine(" - Cadete " + cadeteACobrar.Nombre + " debe recibir: $" + JornalACobrar(cadeteACobrar));
    return cantidadACobrar;
  }

  public void ReasignarPedidoAOtroCadete() {
    Console.Clear();

    if (this.GetCantidadDePedidos() == 0) {
      Console.WriteLine(" x No hay pedidos disponibles para reasignar");
      return;
    }

    Console.WriteLine("- Listado de pedidos por cadete");

    foreach (Cadete cadeteItem in this.ListadoCadetes) {
      Console.WriteLine(" x " + cadeteItem.Id + "\t" + cadeteItem.Nombre);
      
      foreach (Pedido pedidoItem in this.GetPedidosDeCadete(cadeteItem)) {
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

    pedidoAReasignar.AsignarPedido(cadeteAAsignar);
  }

  public void ActualizarEstadoPedido() {
    Console.Clear();
    Console.WriteLine("- Listado de pedidos por cadete");

    foreach (Cadete cadeteItem in this.ListadoCadetes) {
      Console.WriteLine(" x " + cadeteItem.Id + "\t" + cadeteItem.Nombre);
      foreach (Pedido pedidoItem in this.GetPedidosDeCadete(cadeteItem)) {
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
    Pedido pedido = new Pedido(this.GetCantidadDePedidos(), Utils.PedirString("Observaciones", false), cliente);

    int coutAntesDeAgregar = this.GetCantidadDePedidos();
    this.pedidos.Add(pedido);

    if (coutAntesDeAgregar < this.GetCantidadDePedidos()) {
      Console.WriteLine("== ¡Pedido instanciado exitosamente! ==");
    }

    return pedido;
  }

  public Boolean AsignarPedidoACadete() {
    Console.Clear();

    if (this.GetCantidadDePedidos() == 0) {
      Console.WriteLine(" x No hay pedidos disponibles para asignar");
      return false;
    }

    Console.WriteLine("- Listado de pedidos pendientes de asignación:");
    this.GetPedidosSinAsignar().ForEach(pedidoItem => 
      Console.WriteLine(" x " + pedidoItem.ToString())
    );

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

    return pedidoAAsignar.AsignarPedido(cadeteAAsignar);
  }

  public String GenerarInforme() {
    Informe informe = new Informe();
    return informe.GenerarInformeCadeteria(this);
  }

  // Funciones que antes estaban en clase cadete
  public Boolean AgregarPedido(Pedido pedido) {
    int cantidadPreviaPedidos = this.GetCantidadDePedidos();
    this.pedidos.Add(pedido);

    return cantidadPreviaPedidos <= this.GetCantidadDePedidos();
  }

  public Boolean RemoverPedido(Pedido pedido) {
    int cantidadPreviaPedidos = this.GetCantidadDePedidos();
    this.pedidos.Remove(pedido);

    return cantidadPreviaPedidos >= this.GetCantidadDePedidos();
  }

  public Boolean CancelarPedido(Pedido pedido) {
    Pedido pedidoACancelar = BuscaPedido(pedido.Nro);

    if (pedidoACancelar != null) {
      pedidoACancelar.Cancelar();

      return true;
    } else {
      return false;
    }
  }

  public Boolean ActualizarEstadoPedido(Pedido pedido, PEDIDO_ESTADOS nuevoEstado) {
    Pedido pedidoAUpdetear = BuscaPedido(pedido.Nro);

    if (pedidoAUpdetear != null) {
      pedidoAUpdetear.ActualizarEstado(nuevoEstado);

      return true;
    } else {
      return false;
    }
  }

  public long JornalACobrar(Cadete cadete) {
    List<Pedido> pedidosCompletados = this.pedidos.FindAll(pedidoItem =>
      pedidoItem.Estado == PEDIDO_ESTADOS.COMPLETADO.ToString() &&
      pedidoItem.Cadete.Id == cadete.Id
    );
    return pedidosCompletados.Count() * Pedido.PRECIO_PEDIDO;
  }

  public int GetCantidadDePedidos() {
    return this.pedidos.Count();
  }

  public int GetCantidadDePedidos(Cadete cadete) {
    return this.pedidos.FindAll(pedidoItem =>
      pedidoItem.Cadete != null &&
      pedidoItem.Cadete.Id == cadete.Id
    ).Count;
  }

  private List<Pedido> GetPedidosSinAsignar() {
    return this.pedidos.FindAll(pedidoItem =>
      pedidoItem.Cadete == null &&
      pedidoItem.Estado != PEDIDO_ESTADOS.COMPLETADO.ToString()
    );
  }

  private List<Pedido> GetPedidosDeCadete(Cadete cadete) {
    return this.pedidos.FindAll(pedidoItem =>
      pedidoItem.Cadete != null &&
      pedidoItem.Cadete.Id == cadete.Id
    );
  }

  public void setListaCadetes(List<Cadete> cadetes) {
    if (cadetes != null) {
      this.listadoCadetes = cadetes;
    }
  }
}