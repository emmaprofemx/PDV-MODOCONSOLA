using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDV_MODOCONSOLA
{

    public enum AdminOperation
    {
        AgregarItem = 1,
        EliminarItem = 2,
        ActualizarItem = 3,
        ActualizarPrecio = 4 ,
        MostrarLista= 5,
        Salir = 6 
    }
    class Inicio
    {//Empieza la clase Inicio


        public List<Item> Items;
        public Dictionary<int, ComprarItem> ComprarItems;
        public Dictionary<int, int> StockItem = new Dictionary<int, int>();
        public int Suma = 0;

        public string LoginOpcionPrompt = "Ingresa una opcion:";
        public string LoginOpcionErrorPrompt = "Opcion Incorrecta. Intenta de nuevo";
        public string AdminOpcionErrorPrompt = "Opcion Incorrecta. Intenta de nuevo";
        public string AdminOpcionPrompt = "Ingresa tu opcion: ";
        public string IngresarCantidad = "Ingresa Cantidad";
        public string CantidadErrorPrompt = "Opcion Incorrecta. Intenta de nuevo";
        public string ComprarPrompt = "¿Qué desea comprar?";
        public string ErrorCompraPrompt = "Opcion Incorrecta. Intenta de nuevo";
        public string VerCarritoPrompt = "Ingresa 0 para visualizar el carrito de compras";
        public string ItemnoEncontrado = "Item no encontrado. Intenta de nuevo";
        public string IngresarPrecio = "Ingresa Precio";

        public void Ejecutar()
        {
            Console.WriteLine("Ingresa 0 para iniciar como ADMIN o 1 para ingresar como Cliente");
            int opclogin = entradaUsuario(LoginOpcionPrompt, LoginOpcionErrorPrompt);
            switch (opclogin)
            {
                case 0:
                    Console.WriteLine("Ingresando como ADMIN...");
                    mostrarProductos();
                    operacionesAdmin();
                    break;
                case 1:
                    Console.WriteLine("Ingresando como Cliente...");
                    operacionesClientes();
                    break;
                default:
                    Ejecutar();
                    break;
            }
        }
        public void operacionesClientes()
        {
            mostrarProductos();
            Console.WriteLine(VerCarritoPrompt);
            int opc = entradaUsuario(ComprarPrompt, ErrorCompraPrompt);

            switch (opc)
            {
                case 0:
                    desplegarCarrito(ComprarItems);
                    break;
                default:
                    Item getItem = GetItem(opc);
                    if (getItem == null)
                    {
                        Console.WriteLine(ItemnoEncontrado);
                        operacionesClientes();
                    }
                    else
                    {
                        agregarAlCarrito(getItem);
                        mostrarProductos();
                    }
                    break;
            }
        }
        public Item GetItem(int opcion)
        {
            foreach (Item t in Items)
            {
                if (opcion == t.Id)
                    return t;
            }
            return null;
        }
        public void agregarAlCarrito(Item item)
        {
            //Se pide ingresar la cantidad del producto
            string itemNombre = item.ItemNombre;
            Console.Write("Producto {0} encontrado. ", itemNombre);
            int cantidad = entradaUsuario(IngresarCantidad, CantidadErrorPrompt);

            //Si hay suficientes unidades de x articulo pasamos a la condicion verdadera
            if (item.ItemStock >= cantidad)
            {
                Console.WriteLine("Producto agregado");
                checarInventario(item, cantidad);
                //Se resta la cantidad que ingreso el usuario al total de unidades disponibles
                item.ItemStock -= cantidad;
                operacionesClientes();
            }
            else
            {
                Console.WriteLine("{0} {1}", cantidad, itemNombre + " no esta en inventario");
                operacionesClientes();
            }
        }
        public void checarInventario(Item item, int cantidad)
        {

            if (!ComprarItems.ContainsKey(item.Id))
            {
                ComprarItems.Add(item.Id, new ComprarItem { Id = item.Id, Cantidad = cantidad, Item = item });
            }
            else
            {
                ComprarItems[item.Id].Cantidad += cantidad;
            }
        }
        public void desplegarCarrito(Dictionary<int, ComprarItem> productosCompradosList)
        {
            //Se muestra a continuacion el ticket de compra de los articulos que haya comprado

            int total = 0;
            double iva = 0.16;
            Console.WriteLine("\n-----------------------------------------TICKET---------------------------------------\n");
            Console.WriteLine("Id\t\tCantidad\t\tPrecio\t\tSuma");
            foreach (var pair in productosCompradosList)
            {
                Suma += pair.Value.Cantidad;
                int precio = pair.Value.Cantidad * pair.Value.Item.ItemPrecio;
                Console.WriteLine(pair.Value.Item.ItemNombre + "\t\t" + pair.Value.Cantidad + "\t\t\t" + pair.Value.Item.ItemPrecio + "\t\t\t" + precio);
                total += precio;
            }
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            double resultado = total * iva;
            Console.WriteLine("Iva: \t\t\t\t\t\t\t{0}", resultado);
            Console.WriteLine("Total a pagar\t\t\t\t\t\t\t${0}", (total + resultado));

            Console.WriteLine("\nComprar de nuevo 0 , Salir 1");
            int opc = entradaUsuario("Ingresa tu opcion", "Entrada Incorrecta");
            if (opc == 0)
            {
                mostrarProductos();
                operacionesClientes();
            }
            else
            {
                Ejecutar();
            }
        }
        public void operacionesAdmin()
        {
            Console.WriteLine("\n 1)Agregar nuevo producto \n 2)Eliminar Producto \n 3)Actualizar Stock \n 4)Actualizar Precio \n 5)Desplegar Lista \n 6)Salir");
            int opcAdmin = entradaUsuario(AdminOpcionPrompt, AdminOpcionErrorPrompt);
            switch (opcAdmin)
            {
                //Caso 1 para Insertar Producto
                case (int)PDV_MODOCONSOLA.AdminOperation.AgregarItem:
                    agregarProducto();
                    break;
                //Caso 2 para  ELIMINAR producto
                case (int)PDV_MODOCONSOLA.AdminOperation.EliminarItem:
                    eliminarProducto();
                    break;
                //Caso 3 para actualizar stock
                case (int)PDV_MODOCONSOLA.AdminOperation.ActualizarItem:
                    actualizarStock();
                    break;
                //Caso 4 para actualizar precio
                case (int)PDV_MODOCONSOLA.AdminOperation.ActualizarPrecio:
                    actualizarPrecio();
                    break;
                //caso 5 para mostrar lista de productos
                case (int)PDV_MODOCONSOLA.AdminOperation.MostrarLista:
                    mostrarProductos();
                   // eliminarProducto();
                    operacionesAdmin();
                    break;
                    //Caso 6 para salir de las operaciones en modo ADMIN
                case (int)PDV_MODOCONSOLA.AdminOperation.Salir:
                    Ejecutar();
                    mostrarProductos();
                    break;
                default:
                    Console.WriteLine(AdminOpcionErrorPrompt);
                    operacionesAdmin();
                    break;
            }
        }
        public void agregarProducto()
        {
            Console.WriteLine("Ingresa el nombre del producto: ");
            string nombre = Console.ReadLine();
            int precio = entradaUsuario("Ingresa el precio:", "Error , Ingresa el precio correcto...");
            int cantidad = entradaUsuario("Ingresa la cantidad:", "Error , Ingresa la cantidad correcta...");
            Items.Add(new Item { Id = Items.Count + 1, ItemNombre = nombre, ItemPrecio = precio, ItemStock = cantidad });
            Console.WriteLine("***Producto agregado exitosamente***");
            operacionesAdmin();
        }
        public void eliminarProducto()
        {
            //Estructura para eliminar un producto del inventario
            Console.WriteLine("Ingresa el item(ID) del producto: ");
            int item = Convert.ToInt32(Console.ReadLine());
            var itemToRemove = Items.Single(r => r.Id == item);
            Items.Remove(itemToRemove);
            Console.WriteLine("***Producto eliminado exitosamente***");
            operacionesAdmin();
        }

        //Funciona para cambiar el precio
        public void actualizarPrecio()
        {
            var input = entradaUsuario("Selecciona Id del producto:", AdminOpcionPrompt);
            if (input != 6)
                if (input <= Items.Count)
                {
                    int precio = entradaUsuario(IngresarPrecio , CantidadErrorPrompt);
                    if (precio > 0)
                        if (Items != null) Items[input - 1].ItemPrecio = precio; //Establecemos el cambio del precio
                    mostrarProductos();
                    operacionesAdmin();
                    return;
                }
                else
                {
                    Console.WriteLine(AdminOpcionErrorPrompt);
                    operacionesAdmin();
                }
            else
                mostrarProductos();
            operacionesAdmin();
        }
        //Caso 2
       
        public void actualizarStock()
        {
            var input = entradaUsuario("Selecciona Id del producto:", AdminOpcionPrompt);
            if (input != 6)
                if (input <= Items.Count)
                {
                    int cantidad = entradaUsuario(IngresarCantidad, CantidadErrorPrompt);
                    if (cantidad > 0)
                        if (Items != null) Items[input - 1].ItemStock += cantidad; //Aumentamos el stock con el valor ingresado y se suma con el valor actual del stock.
                    mostrarProductos();
                    operacionesAdmin();
                    return;
                }
                else
                {
                    Console.WriteLine(AdminOpcionErrorPrompt);
                    operacionesAdmin();
                }
            else
                mostrarProductos();
            operacionesAdmin();
        }


        public void mostrarProductos()
        {
            //Mostramos todos los productos mediante el uso de un foreach
            Console.WriteLine("Productos");
            Console.WriteLine("===========================");
            Console.WriteLine("Id\tProducto\tPrecio\t Inventario");
            Console.WriteLine("---------------------------------------------");
            foreach (var item in Items)
            {
                Console.WriteLine(item.Id + "\t" + item.ItemNombre + "\t\t" + item.ItemPrecio + "\t" + item.ItemStock);
            }
            
        }

        public void DefaultInit()
        {
            Items = new List<Item>
            {
                new Item{ Id = 1, ItemNombre = "Null", ItemPrecio = 0, ItemStock = 0 }
            };
            ComprarItems = new Dictionary<int, ComprarItem>();
        }
        public int entradaUsuario(string inputPrompt, string errorPrompt)
        {
            Console.WriteLine(inputPrompt);
            var entrada = Console.ReadLine();
            try
            {
                return Convert.ToInt32(entrada);
            }
            catch (Exception)
            {
                Console.WriteLine(errorPrompt);
                return entradaUsuario(inputPrompt, errorPrompt);
            }
        }

    }//******Fin de la clase Inicio



}
