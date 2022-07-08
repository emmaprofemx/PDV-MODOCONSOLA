using System;
using System.Collections.Generic;
using System.Text;

namespace PDV_MODOCONSOLA
{
    public enum AdminOperation
    {
        AgregarItem = 1 ,  
        ActualizarItem = 2, 
        DesplegarItem = 3 , 
        Cerrar = 4
    }

    class Inicio
    {//Empieza la clase Inicio


        public int EntradaUsuario(string inputPrompt , string errorPrompt)
        {
            //Mostrando mensaje de entrada
            Console.WriteLine(inputPrompt);
            var input = Console.ReadLine();

            try{
                return Convert.ToInt32(input);
            }catch (Exception)
            {
                Console.WriteLine(errorPrompt);
                return EntradaUsuario(inputPrompt, errorPrompt);
            }

        }


    }//Fin de la clase Inicio



}
