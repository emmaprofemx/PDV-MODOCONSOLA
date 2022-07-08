using System;
using System.Collections.Generic;
using System.Text;

namespace PDV_MODOCONSOLA
{
    public enum AdminOperaciones
    {
        AgregarItem = 1,
        ActualizarItem = 2,
        DesplegarItem = 3,
        Cerrar = 4
    }

    class Inicio
    {//Empieza la clase Inicio
        //*******Variables
        public List<Item> Items;
        public Dictionary<int, ComprarItem> ComprarItems;
        public Dictionary<int, int> InventarioItem = new Dictionary<int, int>();
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
        public string ItemNotFound = "Item no encontrado. Intenta de nuevo";






        //~~~~~~Metodos~~~~~~~ 
        public void DesplegarItem()
        {
            Console.WriteLine("Productos");
            Console.WriteLine("===========================");
            Console.WriteLine("No.\tItem\t\tPrecio\t En Inventario");
            Console.WriteLine("---------------------------------------------");

            foreach (var item in Items)
            {
                Console.WriteLine(item.Id + "\t" + item.ItemNombre + "\t\t" + item.ItemPrecio + "\t" + item.ItemStock);
            }

        }

        //Metodo EntradaUsuario
        public int EntradaUsuario(string inputPrompt, string errorPrompt)
        {
            //Mostrando mensaje de entrada
            Console.WriteLine(inputPrompt);
            var input = Console.ReadLine();

            try
            {
                return Convert.ToInt32(input);
            }
            catch (Exception)
            {
                Console.WriteLine(errorPrompt);
                return EntradaUsuario(inputPrompt, errorPrompt);
            }

        }

        public void DefaultInit()
        {
            Items = new List<Item> { 
                new Item { Id = 1 , ItemNombre = "Pluma" , ItemPrecio = 5 , ItemStock = 10} , 
                new Item { Id = 2 , ItemNombre = "Libro" , ItemPrecio = 100 , ItemStock = 15} , 
                new Item { Id = 3 , ItemNombre = "Marcador" , ItemPrecio = 50 , ItemStock = 20} , 
                new Item { Id = 4 , ItemNombre = "Borrador" , ItemPrecio = 6 , ItemStock = 100}
            };
            ComprarItems = new Dictionary<int, ComprarItem>();
        }


        public void AdminOperacion()
        {
            Console.WriteLine("1)Para agregar nuevo Item \n 2)Para actualizar inventario \n 3)Desplegar lista \n 4)Cerrar Sesion");
        }


        public void AgregarItem()
        {
            Console.WriteLine("Ingresa el nombre del producto:");
            string nombre = Console.ReadLine();
            int precio = EntradaUsuario("Ingresa el precio" , "Equivocado , Ingresa el precio correcto");
            int cantidad = EntradaUsuario("Ingresa la cantidad" , "Equivocado , Ingresa la cantidad correcta");

            Items.Add(new Item { Id = Items.Count + 1 , ItemNombre = nombre , ItemPrecio = precio , ItemStock = cantidad});
            Console.WriteLine("Item agregado exitosamente");


        }

        public void ActualizarItem()
        {
            var input = EntradaUsuario("Agrega un Item" , AdminOpcionPrompt);
            /*if (input != 4)
                if (input <= Items.Count)
                {
                    int quantity = EntradaUsuario(IngresarCantidad, CantidadErrorPrompt);
                    if (quantity > 0)
                        if (Items != null) Items[input - 1].ItemStock += quantity;
                    DesplegarItem();
                    AdminOperacion();
                    return;
                }
                else
                {
                    Console.WriteLine(AdminOpcionErrorPrompt);
                    AdminOperacion();
                }
            else
                DesplegarItem();
            AdminOperacion();*/
            if (input != 4)
            {
                int quantity = EntradaUsuario(IngresarCantidad, CantidadErrorPrompt);
                if (input <= Items.Count)
                {

                    if (quantity > 0)
                    {
                        if (Items != null)
                        {
                            Items[input - 1].ItemStock += quantity;
                        }
                        DesplegarItem();
                        AdminOperacion();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine(AdminOpcionErrorPrompt);
                    AdminOperacion();
                }

            } else
            {
                DesplegarItem();
                AdminOperacion();

            }

        }


        public void ConsumidorOperacion()
        {
            DesplegarItem();
            Console.WriteLine(VerCarritoPrompt);
            int opc = EntradaUsuario(ComprarPrompt , ErrorCompraPrompt);
            switch (opc)
            {
                case 0:
                    //Desple
                    DesplegarItem();
                    break;
                default:
                    Item getItem = GetItem(opc);
                    if (getItem == null)
                    {
                        Console.WriteLine(ItemNotFound);
                        ConsumidorOperacion();
                    }
                    else
                    {
                        agregarAlCarrito(getItem);
                        //  AddToCart(getItem);
                        DesplegarItem();
                    }
                    break;
            }
        }

        public void agregarAlCarrito(Item item)
        {

        }

        public void Ejecutar()
        {
            Console.WriteLine("Ingresa 0 para iniciar como administrador o 1  para ingresar como consumidor");
            int opclogin = EntradaUsuario(LoginOpcionPrompt , LoginOpcionErrorPrompt);
            switch (opclogin)
            {
                case 0:
                    Console.WriteLine("Ingresando como Admin...");
                    DesplegarItem();
                    AdminOperacion();
                    break;
                case 1:
                    Console.WriteLine("Ingresando como Consumidor...");
                    //DesplegarItem();
                    //AdminOperacion();
                    break;
                default:
                    break;
            }

        }
       
        public void OpAdmin()
        {
            Console.WriteLine("1) Agregar nuevo producto \n 2)Actualizar Stock \n 3) Desplegar Lista \n 4)Salir");
            int opadmin = EntradaUsuario(AdminOpcionPrompt , AdminOpcionErrorPrompt);
            switch (opadmin)
            {
                case (int)PDV_MODOCONSOLA.AdminOperaciones.AgregarItem:
                    AgregarItem();
                    break;
                case (int)PDV_MODOCONSOLA.AdminOperaciones.ActualizarItem:
                    ActualizarItem();
                    break;
                case (int)PDV_MODOCONSOLA.AdminOperaciones.DesplegarItem:
                    DesplegarItem();
                    AdminOperacion();
                    break;
                case (int)PDV_MODOCONSOLA.AdminOperaciones.Cerrar:
                    DesplegarItem();
                    break;
                default:
                    Console.WriteLine(AdminOpcionErrorPrompt);
                    AdminOperacion();
                    break;
            }
        }

        public void DeplegarCarrito(Dictionary<int , ComprarItem> compraItemList)
        {
            int total = 0;
            Console.WriteLine("\n-----------------------------------------Productos en el carrito---------------------------------------\n");
            Console.WriteLine("Item\t\tCantidad\t\tPrecio\t\tSuma");
            foreach (var articulo in compraItemList)
            {
                Suma += articulo.Value.Cantidad;
                int price = articulo.Value.Cantidad * articulo.Value.Item.ItemPrecio;
                Console.WriteLine(articulo.Value.Item.ItemNombre + "\t\t" + articulo.Value.Cantidad + "\t\t\t" + articulo.Value.Item.ItemPrecio + "\t\t\t" + price);
                total += price;
            }
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine("Total a pagar\t\t\t\t\t\t\t{0}", total);

            //Console.WriteLine("\nFor shop again enter 0 or logout enter 1");
            Console.WriteLine("\nComprar de nuevo (0) - Cerrar Sesion (1)");
            int opc = EntradaUsuario("Ingresa tu opcion", "Entrada incorrecta");

            if (opc == 0)
            {
                DesplegarItem();
                ConsumidorOperacion();
              //  CustomerOperation();
            }
            else
            {
               // Begin();
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


    }//******Fin de la clase Inicio



}
