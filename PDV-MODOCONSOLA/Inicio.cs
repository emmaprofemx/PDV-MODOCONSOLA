using System;
using System.Collections.Generic;
using System.Text;

namespace PDV_MODOCONSOLA
{
    public enum AdminOperation
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






        //Metodos 
        public void DesplegarItem()
        {
            Console.WriteLine("Productos");
            Console.WriteLine("===========================");
            Console.WriteLine("No.\tItem\t\tPrecio\t En Inventario");
            Console.WriteLine("---------------------------------------------");

            foreach (var item in Items)
            {
                Console.WriteLine(item.Id + "\t" + item.ItemName + "\t\t" + item.ItemPrice + "\t" + item.ItemStock);
            }

        }


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


    }//Fin de la clase Inicio



}
