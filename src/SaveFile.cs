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
                List<string> AlumnosAGuardar = new List<string>();
                foreach (var alumno in Alumnos)
                {
                    AlumnosAGuardar.Add(JsonSerializer.Serialize(alumno));
                }
                File.WriteAllLines($"{FileName}.json", AlumnosAGuardar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear el archivo: " + ex.ToString());
            }
        }
        public static void savaInXml(List<Alumno> Alumnos, string FileName)
        {
            try
            {
                List<string> AlumnosAGuardar = new List<string>();
                foreach (var alumno in Alumnos)
                {
                }
                File.WriteAllLines($"{FileName}.xml", AlumnosAGuardar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear el archivo: " + ex.ToString());
            }
        }
    }
}
