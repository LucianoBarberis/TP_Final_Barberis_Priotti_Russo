using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace RussoPriottiBarberis_GestorAlumnos.src
{
    internal class SaveFile
    {
        public static void saveInTxt(List<Alumno> Alumnos, string FileName)
        {
            try
            {
                List<string> AlumnosAGuardar = new List<string>();
                foreach (var alumno in Alumnos)
                {
                    AlumnosAGuardar.Add(alumno.ToString());
                }
                File.WriteAllLines($"{FileName}.txt" , AlumnosAGuardar);
                // Pongo la extencion "hardcodeada" para asegurarme de que esta funcion unicamente cree txt
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear el archivo: " + ex.ToString());
            }
        } 
        public static void saveInCsv(List<Alumno> Alumnos, string FileName)
        {
            try
            {
                List<string> AlumnosAGuardar = new List<string>();

                AlumnosAGuardar.Add("Legajo,Apellido,Nombre,Nro. Doc.,Email,Teléfono");
                foreach (var alumno in Alumnos)
                {
                    AlumnosAGuardar.Add(alumno.ToCsvString());
                }

                File.WriteAllLines($"{FileName}.csv", AlumnosAGuardar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear el archivo: " + ex.ToString());
            }
        }
        public static void saveInJson(List<Alumno> Alumnos, string FileName)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText($"{FileName}.json", JsonSerializer.Serialize(Alumnos, options));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear el archivo: " + ex.ToString());
            }
        }

        // TODO: Arreglar Implementacion
        public static void saveInXml(List<Alumno> Alumnos, string FileName)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Alumno>));
                using (StreamWriter writer = new StreamWriter($"{FileName}.xml"))
                {
                    serializer.Serialize(writer, Alumnos);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear el archivo XML: " + ex.ToString());
            }
        }
    }
}
