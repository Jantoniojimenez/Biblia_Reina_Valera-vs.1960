using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblia_Reina_valera_Vs._1960.Clases_Extraer_Datos
{
    class Renombrar_archivos
    {

        string nombrearchivos = string.Empty;

        //string rutaactual = @"C:\Users\anton\OneDrive\Himnario_Adventista\MUSICA\instrumental";
        string rutaactual = @"This PC\Moto Z3 Play\Almacenamiento interno compartido\ADM\";


        public void arcivosmotp()
        {

            foreach (var file in Directory.GetFiles(rutaactual))

            {

                FileInfo archi = new FileInfo(file);


                //if (int.Parse(archi.Name.Trim().Substring(0, 3)) <= 613 && archi.Name.EndsWith(".mp3")) nombrearchivos += archi.Name.Trim().Substring(0, archi.Name.Trim().Length-4) + "\n" ;
            }

            //var j = nombrearchivos.Trim().Split('\n').ToArray();

            //MessageBox.Show(j.Length.ToString());

            //string nombrearchivos2 = string.Empty; int k = 0;

            //string rutaactual2 = @"C:\Users\anton\OneDrive\Proyecto_shalom\Videos Himnario Adventista";

            //foreach (var file in Directory.GetFiles(rutaactual2))

            //{

            //    if (File.Exists(file)) File.Copy(file, @"C:\Users\anton\OneDrive\Proyecto_shalom\Videos Renombrados\" + j[k]+".mp4");
            //    //FileInfo archi = new FileInfo(file);
            //    //if (int.Parse(archi.Name.Trim().Substring(0, 3)) <= 613) nombrearchivos2 += archi.Name + "\n";

            //    k++;
            //}

        }
}
}
