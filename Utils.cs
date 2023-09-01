namespace EspacioUtils;

public class Utils {
  public static int PedirInt(string key, Boolean obligatorio) {
    int input;
    Console.WriteLine(" - " + key + ":");
    if (!int.TryParse(Console.ReadLine(), out input) && obligatorio) {
        Console.WriteLine("x Input invalido, porfavor reintente:");
        return PedirInt(key, obligatorio);
    }
    return input;
  }

  public static string? PedirString(string key, Boolean obligatorio) {
    Console.WriteLine(" - " + key + ":");
    string? input = Console.ReadLine();
    if (input == null && obligatorio) {
      Console.WriteLine("x Input invalido, porfavor reintente.");
      return PedirString(key, obligatorio);
    }
    return input;
  }
}