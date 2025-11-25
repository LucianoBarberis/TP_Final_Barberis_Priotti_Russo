using System.IO;
using System.Linq;

namespace RussoPriottiBarberis_GestorAlumnos
{
    public static class GestorArchivos
    {
        public static void NewFile()
        {
            string? fileName;
            string? fileFormat;
            int? cantidadAlumnos;

            do
            {
                Console.Clear();
                Console.WriteLine("Ingrese el nombre del archivo a crear:");
                fileName = Console.ReadLine();
            }while (!EsNombreDeArchivoValido(fileName));
            // Si da True, el nombre es válido por lo tanto lo negamos para que el bucle   continue hasta que sea válido.

            do
            {
                Console.Clear();
                Console.WriteLine("Ingrese el formato del archivo (txt/csv/json/xml):");
                fileFormat = Console.ReadLine();
                fileFormat = fileFormat.ToLower();
            } while (fileFormat != "txt" && fileFormat != "csv" && fileFormat != "json" && fileFormat != "xml");

            do
            {
                Console.Clear();
                Console.WriteLine($"Cantidad de alumnos a cargar en el archivo {fileName}.{fileFormat}: ");
            } while (!int.TryParse(Console.ReadLine(), out int cantidadAlumnos) || cantidadAlumnos <= 0);

            for (int i = 0; i < cantidadAlumnos; i++)
            {
                // Lógica para agregar alumnos al archivo
            }
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
    }
}