using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using RussoPriottiBarberis_GestorAlumnos.src;

namespace RussoPriottiBarberis_GestorAlumnos
{
    public static class GestorArchivos
    {
        public static void NewFile()
        {
            List<Alumno> AlumnosACargar = new List<Alumno>();
            string? fileName;
            string? fileFormat;
            int cantidadAlumnos;

            do
            {
                Console.Clear();
                Console.WriteLine("Ingrese el nombre del archivo a crear:");
                fileName = Console.ReadLine();
            }while (!EsNombreDeArchivoValido(fileName));
            // Si da True, el nombre es valido por lo tanto lo negamos para que el bucle   continue hasta que sea válido.

            do
            {
                Console.Clear();
                Console.WriteLine("Ingrese el formato del archivo (txt/csv/json/xml):");
                fileFormat = Console.ReadLine().ToLower();
            } while (fileFormat != "txt" && fileFormat != "csv" && fileFormat != "json" && fileFormat != "xml");

            string? input;
            do
            {
                Console.Clear();
                Console.WriteLine($"Cantidad de alumnos a cargar en el archivo {fileName}.{fileFormat}: ");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out cantidadAlumnos) || cantidadAlumnos <= 0);

            for (int i = 1; i <= cantidadAlumnos; i++)
            {
                Console.Clear();
                Console.WriteLine($"--- Ingresando Alumno {i} de {cantidadAlumnos} ---");

                Console.Write("Legajo: ");
                string? legajo = Console.ReadLine();

                Console.Write("Apellido: ");
                string? apellido = Console.ReadLine();

                Console.Write("Nombre: ");
                string? nombre = Console.ReadLine();

                Console.Write("DNI: ");
                string? doc = Console.ReadLine();

                Console.Write("Email: ");
                string? email = Console.ReadLine();

                Console.Write("Teléfono: ");
                string? tel = Console.ReadLine();
                
                Console.WriteLine(); // Espacio para los mensajes de error

                if (Alumno.ValidarYCrear(legajo, apellido, nombre, doc, email, tel, out var alumno))
                {
                    AlumnosACargar.Add(alumno!);
                    Console.WriteLine("\nAlumno agregado correctamente.");
                    Thread.Sleep(2000); // Pausa para que el usuario vea la confirmación
                }
                else
                {
                    // Los errores ya fueron impresos por el método de validación
                    Console.WriteLine("\nPresione una tecla para reintentar...");
                    Console.ReadKey();
                    i--; // Decrementamos para reintentar el ingreso del mismo alumno
                }

                switch (fileFormat)
                {
                    case "txt": SaveFile.saveInTxt(AlumnosACargar, fileName) ;break;
                    case "csv": SaveFile.saveInCsv(AlumnosACargar, fileName);break;
                    case "json": SaveFile.saveInJson(AlumnosACargar, fileName);break;
                    case "xml": Console.WriteLine("A Implementar");break;
                }
            }
        }

        public static bool EsNombreDeArchivoValido(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return false;
            }
            char[] caracteresInvalidos = Path.GetInvalidFileNameChars();
            // Comprobando si el nombre contiene alguno de los caracteres inválidos.
            if (fileName.Any(c => caracteresInvalidos.Contains(c)))
            {
                return false;
            }
            // Comprobando nombres reservados de windows
            string[] nombresReservados = { "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };
            string nombreSinExtension = Path.GetFileNameWithoutExtension(fileName);
            if (nombresReservados.Contains(nombreSinExtension.ToUpper()))
            {
                return false;
            }

            return true;
        }
        public static void ReadFile()
        {
            
        }
        public static void EditFile()
        {
            
        }
        public static void DeleteFile()
        {
            
        }

    }
}