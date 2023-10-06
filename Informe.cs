using EspacioCadete;
using EspacioCadeteria;

namespace EspacioInforme;

public class InformeCadete {
  private int cadeteId;
  private long totalRecaudado;
  private int porcentajeTotal;
  private int cantidadPedidos;

  public int CadeteId { get => cadeteId; set => cadeteId = value; }
  public long TotalRecaudado { get => totalRecaudado; set => totalRecaudado = value; }
  public int PorcentajeTotal { get => porcentajeTotal; set => porcentajeTotal = value; }
  public int CantidadPedidos { get => cantidadPedidos; set => cantidadPedidos = value; }
}

public class Informe {
  private long total;
  private List<InformeCadete> cadetes = new List<InformeCadete>();
  public long Total { get => total; }
  public List<InformeCadete> Cadetes = new List<InformeCadete>();

  public Informe(long total, List<InformeCadete> listaInformeCadete) {
    this.total = total;
    this.cadetes = listaInformeCadete;
  }

  public static Informe GenerarInformeCadeteria(Cadeteria cadeteria) {
    List<InformeCadete> listaInformeCadete = new List<InformeCadete>();
    foreach (Cadete cadeteItem in cadeteria.ListadoCadetes) {
      InformeCadete informeCadete = new InformeCadete();
      informeCadete.CadeteId = cadeteItem.Id;
      informeCadete.CantidadPedidos = cadeteria.GetPedidosDeCadete(cadeteItem.Id).Count();
      informeCadete.TotalRecaudado = cadeteria.JornalACobrar(cadeteItem.Id);
      informeCadete.PorcentajeTotal = cadeteria.GetPedidosDeCadete(cadeteItem.Id).Count() * 100 / cadeteria.GetCantidadDePedidos();
      listaInformeCadete.Add(informeCadete);
    }

    Informe informeCadeteria = new Informe(
      cadeteria.ListadoCadetes.Sum(cadete => cadeteria.JornalACobrar(cadete.Id)),
      listaInformeCadete
    );

    return informeCadeteria;
  }
}