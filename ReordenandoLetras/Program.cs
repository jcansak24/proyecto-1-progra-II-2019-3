using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReordenandoLetras
{
    class Program
    {
        static void Main(string[] args)
        {
            int numCasos;
            string nombreArchivo = "reordenandoTexto.txt";
            string linea, auxLinea, frase = "", palabras = "";
            char caracter;
            bool band;

            List<string> listaPalabras = new List<string>();
            TextReader leerArch = new StreamReader(nombreArchivo);
            numCasos = int.Parse(leerArch.ReadLine());
            linea = leerArch.ReadLine();
            
            while (linea != null)
            {
                auxLinea = linea;
                string[] palOrden = linea.Split();
                linea = leerArch.ReadLine();
                string[] palDesorden = linea.Split();        
                        
                for (int i = 0;i<palDesorden.Length;i++) {
                    int tamPalDesorden = (char)palDesorden[i].Length;
                    string patronDesorden =  $"[{palDesorden[i].ElementAt<char>(0)}]|[{palDesorden[i].ElementAt<char>(tamPalDesorden-1)}]" ;
                    Regex miRegex = new Regex(patronDesorden);
                    band = false;
                    for (int j=0; j<palOrden.Length;j++) {
                        int tamPalOrden = (char)palOrden[j].Length;
                        string patronOrden = $"[{palOrden[j].ElementAt<char>(0)}]|[{palOrden[j].ElementAt<char>(tamPalOrden - 1)}]";
                        MatchCollection elMatch = miRegex.Matches(patronOrden);
                        if (elMatch.Count > 1)
                        {
                            for (int k = 0; k < tamPalOrden; k++)
                            {
                                if (palDesorden[i] != palOrden[j] && (tamPalOrden == tamPalDesorden))
                                {
                                    if (palDesorden[i].ElementAt<char>(k) != palOrden[j].ElementAt<char>(k))
                                    {
                                        caracter = palOrden[j].ElementAt<char>(k);
                                        palabras += caracter;
                                    }
                                    else
                                    {
                                        caracter = palDesorden[i].ElementAt<char>(k);
                                        palabras += caracter;
                                    }
                                    band = true;
                                }
                                else
                                {
                                    if (palOrden[j].Length < 5 && palabras!="" && band==false) palabras = palDesorden[i];
                                    k = palOrden[j].Length - 1;
                                }
                            }
                            Regex miRegexDes = new Regex(palOrden[j]); 
                            MatchCollection elMatchDup = miRegexDes.Matches(frase);
                            if (elMatchDup.Count == 0 && palabras!="") frase += palabras + " ";  
                            palabras = "";
                            caracter = ' ';   
                        }
                        else
                        {
                            Regex miRegexNoPal = new Regex(palDesorden[i]);
                            MatchCollection elMatchNoPal = miRegexNoPal.Matches(auxLinea);
                            if (j == palOrden.Length - 1 && (elMatchNoPal.Count == 0 || tamPalDesorden < 3) && band == false) frase += palDesorden[i] + " ";   
                        }   
                    }
                }
                listaPalabras.Add(frase);
                frase = "";
                linea = leerArch.ReadLine();
            }
            leerArch.Close();
            foreach (string pal in listaPalabras) Console.WriteLine(pal);
        }
    }
}
