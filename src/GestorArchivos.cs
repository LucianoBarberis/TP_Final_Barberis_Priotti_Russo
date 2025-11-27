using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using RussoPriottiBarberis_GestorAlumnos.src;
using System.Text.Json;

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
            }while (fileName == null || !EsNombreDeArchivoValido(fileName));
            // Si da True, el nombre es valido por lo tanto lo negamos para que el bucle continue hasta que sea válido.

            do
            {
                Console.Clear();
                Console.WriteLine("Ingrese el formato del archivo (txt/csv/json/xml):");
                string? input = Console.ReadLine();
                fileFormat = input != null ? input.ToLower() : string.Empty;
            } while (fileFormat != "txt" && fileFormat != "csv" && fileFormat != "json" && fileFormat != "xml");

            do
            {
                Console.Clear();
                Console.WriteLine($"Cantidad de alumnos a cargar en el archivo {fileName}.{fileFormat}: ");
            } while (!int.TryParse(Console.ReadLine(), out cantidadAlumnos) || cantidadAlumnos <= 0);

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

                if (Alumno.ValidarAlumno(legajo, apellido, nombre, doc, email, tel))
                {
                    Alumno alumno = new Alumno(legajo!, apellido!, nombre!, doc!, email!, tel!);
                    // Usamos "!" delante de cada parametrio para decirle al compilador que estas variables no son nulas ni estan vacias
                    // ya que ya las validamos anteriormente con el metodo Validar alumno
                    AlumnosACargar.Add(alumno);
                    Console.WriteLine("\nPresione una tecla para seguir...");
                    Console.ReadKey();
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
                    case "xml": Console.WriteLine("Arreglar Implementacion");break;
                }
            }
        }
        public static bool EsNombreDeArchivoValido(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return false;
            }

            // Comprobando si el nombre contiene alguno de los caracteres inválidos.
            char[] caracteresInvalidos = Path.GetInvalidFileNameChars();
            if (fileName.Any(c => caracteresInvalidos.Contains(c)))
            {
                return false;
            }

            // Comprobando nombres reservados de windows
            string[] nombresReservados = { "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };
            if (nombresReservados.Contains(fileName.ToUpper()))
            {
                return false;
            }

            return true;
        }
        public static void ReadFile()
        {
            Alumno alumno;
            string? fileName;
            List<Alumno> alumnoList = new List<Alumno>();

            do
            {
                Console.Clear();
                Console.WriteLine("Ingrese el nombre del archivo a leer (Con extencion):");
                fileName = Console.ReadLine();
            } while (fileName == null || !EsNombreDeArchivoValido(fileName));

            if (!fileName.Contains(".txt") && !fileName.Contains(".csv") && !fileName.Contains(".json") && !fileName.Contains(".xls"))
            {
                Console.WriteLine("El archivo no contiene ninguna extencion valida...");
                return;
            }

            if (!File.Exists(fileName))
            {
                Console.WriteLine($"{fileName} no existe...");
                return;
            }
            
            if (fileName.Contains(".txt"))
            {
                string[] lineas = File.ReadAllLines(fileName);
                alumnoList.Clear();
                foreach (string linea in lineas)
                {
                    if (string.IsNullOrEmpty(linea)) continue;

                    alumno = Alumno.FromTxtToObj(linea);
                    if (alumno != null)
                    {
                        alumnoList.Add(alumno);
                    }
                }

                Console.WriteLine("==============================================================");
                Console.WriteLine("| Legajo | Apellido | Nombre | Nro. Doc. | Email | Teléfono |");
                foreach (Alumno alumnoToPrint in alumnoList)
                {
                    Console.WriteLine($"| {alumnoToPrint.Legajo} | {alumnoToPrint.Apellido} | {alumnoToPrint.Nombre} | {alumnoToPrint.Doc} | {alumnoToPrint.Email} | {alumnoToPrint.Tel}");
                }
                Console.WriteLine("==============================================================");
            }
            else if(fileName.Contains(".csv"))
            {
                string[] lineas = File.ReadAllLines(fileName);
                alumnoList.Clear();

                foreach (string linea in lineas)
                {
                    if (string.IsNullOrEmpty(linea)) continue;

                    alumno = Alumno.FromCsvToObj(linea);
                    if (alumno != null)
                    {
                        alumnoList.Add(alumno);
                    }
                }

                Console.WriteLine("==============================================================");
                // El encabezado ya esta en el archvio
                foreach (Alumno alumnoToPrint in alumnoList)
                {
                    Console.WriteLine($"| {alumnoToPrint.Legajo} | {alumnoToPrint.Apellido} | {alumnoToPrint.Nombre} | {alumnoToPrint.Doc} | {alumnoToPrint.Email} | {alumnoToPrint.Tel}");
                }
                Console.WriteLine("==============================================================");
            }
            else if(fileName.Contains(".json"))
            {
                string json = File.ReadAllText(fileName);
                List<Alumno> alumnosFromJson = JsonSerializer.Deserialize<List<Alumno>>(json);
                Console.WriteLine("==============================================================");
                Console.WriteLine("| Legajo | Apellido | Nombre | Nro. Doc. | Email | Teléfono |");
                foreach (Alumno alumnoToPrint in alumnosFromJson)
                {
                    Console.WriteLine($"| {alumnoToPrint.Legajo} | {alumnoToPrint.Apellido} | {alumnoToPrint.Nombre} | {alumnoToPrint.Doc} | {alumnoToPrint.Email} | {alumnoToPrint.Tel}");
                }
                Console.WriteLine("==============================================================");
            }
            else if(fileName.Contains(".xml"))
            {
                // To Do
            }
        }
        public static void EditFile()
        {
            
        }
        public static void DeleteFile()
        {
            string? fileName;

            do
            {
                Console.Clear();
                Console.WriteLine("Ingrese el nombre del archivo a eliminar (con extensión):");
                fileName = Console.ReadLine();
            } while (fileName == null || !EsNombreDeArchivoValido(fileName));

            // Verificar si existe
            if (!File.Exists(fileName))
            {
                Console.WriteLine($" El archivo {fileName} no existe en el sistema.");
                return;
            }

            // Verificamos que la extencion sea valida para evitar eliminar archivos no deseados
            if (!fileName.Contains(".txt") && !fileName.Contains(".csv") && !fileName.Contains(".json") && !fileName.Contains(".xls"))
            {
                Console.WriteLine("El archivo no contiene ninguna extencion valida...");
                return;
            }

            FileInfo infoArchivo = new FileInfo(fileName);
            Console.WriteLine("\n=== Información del archivo ===");
            Console.WriteLine($"Nombre completo: {infoArchivo.FullName}");
            Console.WriteLine($"Tamaño: {infoArchivo.Length / 1024.0:F2} KB");
            Console.WriteLine($"Fecha de creación: {infoArchivo.CreationTime}");
            Console.WriteLine($"Última modificación: {infoArchivo.LastWriteTime}");

            // Confirmación
            Console.Write("\nEscriba CONFIRMAR para eliminar el archivo o cualquier otra cosa para cancelar: ");
            string? confirmacion = Console.ReadLine();

            if (confirmacion == "CONFIRMAR")
            {
                try
                {
                    File.Delete(fileName);
                    Console.WriteLine(" Archivo eliminado con éxito.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Error al eliminar el archivo: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine(" Operación cancelada. Volviendo al menú principal...");
            }
        }

    }
}