using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Biblia_Reina_valera_Vs._1960.Properties;


namespace Biblia_Reina_valera_Vs._1960.Clases_Extraer_Datos
{
    public class BuscarCitas
    {

        public static String[] BuscarVersiculo(string libro, int Capitulo, string[] Versiculo, string siguienteLibro)
        {

            string miarray = "";
            string[] array; 
            
            bool Cbol = false, cbol2 = false;


            using (StreamReader ArchivoTxt = new StreamReader("BIBLIA COMPLETA.txt"))
            {


                while (ArchivoTxt.Peek() > -1)
                {

                    var linea = ArchivoTxt.ReadLine().Trim();

                    if (!string.IsNullOrEmpty(linea))
                    {

                        if (linea == siguienteLibro) break;
                        if (linea == libro) Cbol = true;

                        if (Cbol)
                        {
                            if (linea == "Capítulo " + (Capitulo + 1)) break;
                            if ("Capítulo " + Capitulo == linea) cbol2 = true;

                            if (cbol2 && !linea.Contains("Epístola "))
                            {
                                if (linea.StartsWith(Capitulo + ":")) linea = linea.Substring(Capitulo.ToString().Length + 1);

                                if (Regex.IsMatch(linea.Substring(0, 1), @"^[0-9]+$"))
                                {
                                    miarray += "_" + linea + " ";
                                }
                                else
                                {
                                    if(miarray.Trim().EndsWith("."))
                                        miarray += "\n" + linea + " ";
                                    else 
                                        miarray += linea + " ";
                                }
                            }

                        }
                    }
                }

            
                ArchivoTxt.Close();
            }

            array = filtrarPorCapituloVersiculo(miarray, Versiculo);


            return array;

        }

        private static string[] filtrarPorCapituloVersiculo(string arrayV,string[] versicul)
        {
            string arrayF = null;

            if (versicul.Length == 1)
            {
                return arrayV.Trim().Split('_').ToArray().Where(I => I.StartsWith(versicul.First() + " ")).ToArray();
            }
            else
            {
                
                foreach (var I in versicul)
                {
                    foreach (var J in arrayV.Split('_').ToArray())
                    {
                        if (J.StartsWith(I + " ")) arrayF += J + "\n";
                    }

                }

               if (arrayF != null)  return arrayF.Split('\n').ToArray();
            }

            return arrayV.Trim().Split('_').ToArray();
        }
    }
}
