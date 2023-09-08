#### ¿Cuál de estas relaciones que considera que se realiza por composición y cuál por agregación?
- Cadetería - Cadete: Composición
- Pedido - Cliente: Composición
- Pedido - Cadete: Agregación

----------

#### ¿Qué métodos considera que debería tener la clase Cadetería y la clase Cadete?
**Cadetería** debería ser la encargada de poder la creación de pedidos, cambio de sus estados, y manipular los pedidos de los cadetes, **teniendo en cuenta que cadeteria contiene la lista la lista de cadetes, y que cada cadete tiene su lista de pedidos** así que debería tener:
- CrearPedido.
- CancelarPedido.
- ReasignarPedidoAOtroCadete.
- InstanciarPedido.
- AsignarPedidoACadete.
- ActualizarEstadoPedido.

Entre otros metodos de utilidad, como:
- GetCadeteByPedidoNro.
- ExistePedido.
- BuscaPedido.

**Cadete** debería poder administrar individualmente los pedidos de cada uno:
- AgregarPedido.
- RemoverPedido.
- CancelarPedido.
- ActualizarEstadoPedido.
- JornalACobrar.


----------

#### Teniendo en cuenta los principios de abstracción y ocultamiento, que atributos, e propiedades y métodos deberían ser públicos y cuáles privados.

- Cadeteria:
  - Propiedades: Todas public solo lectura.
  - Metodos: 
    - CrearPedido, CancelarPedido, CobrarJornalCadete, ReasignarPedidoAOtroCadete, ActualizarEstadoPedido, InstanciarPedido y AsignarPedidoACadete son metodos publicos ya que deben ser accesibles para ser utilizados en otra clase.
    - BuscaPedido, ExistePedido y GetCadeteByPedidoNro son metodos privados.
- Cadete:
  - Propiedades: Todas public solo lectura.
  - Metodos:
    - AgregarPedido, RemoverPedido, CancelarPedido, ActualizarEstadoPedido, JoranlACobrar y GetCantidadDePedidos son metodos publicos ya que son utilizados por Cadeteria en cada instancia de Cadete.
- Pedido:
  - Propiedades: Todas public solo lectura.
  - Metodos:
    - ActualizarEstado, Cancelar y ToString Override son metodos publicos ya que son utilizados por Cadeteria y Cadete para administrar sus pedidos.
- Pedido:
  - Propiedades: Todas publicas de lectura y escritura.
  - Metodos: Sin metodos.
----------

#### ¿Cómo diseñaría los constructores de cada una de las clases?
- Cadeteria: Recibir nombre y telefono por parametros e inicializar ListadoCadetes en base a la lectura de un archivo de datos.
- Cadete: Recibir todos sus atributos y utilizarlos para instanciar un nuevo cadete.
- Pedido: Recibir Nro, Obs y Cliente por parametros e inicializar Estado en el valor del enum PEDIDOS_ESTADOS.PENDIENTE.
- Cliente: Yo no hice cree un constructor para esta clase, sino que le voy agregando los datos necesarios mediante setters.
----------

#### ¿Se le ocurre otra forma que podría haberse realizado el diseño de clases?
Habría decidido que los pedidos no estén ligados directamente a los cadetes, sino darles un poco mas de "libertad", haciendo que los pedidos tengan una referencia al cadete al que pertenece. Esto lo pensé teniendo en cuenta el posible caso en el que no hayan cadetes disponibles pero que se puedan aceptar pedidos de todas formas.
----------