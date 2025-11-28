using System.Text.Json;
using RussoPriottiBarberis_GestorAlumnos.src;

namespace RussoPriottiBarberis_GestorAlumnos
{
    public static class Conversor
    {

        public static void ConversorMenu()
        {
            string? fileName;
            string? fileFormatToParse;
            List<Alumno> alumnos = new List<Alumno>();

            do
            {
                Console.Clear();
                Console.WriteLine("Ingrese el nombre del archivo a leer (Con extencion):");
                fileName = Console.ReadLine();
            } while (fileName == null || !GestorArchivos.EsNombreDeArchivoValido(fileName));

            if (!GestorArchivos.EsFormatoValido(fileName))
            {
                Console.WriteLine("El archivo no contiene ninguna extencion valida...");
                return;
            }
            
            alumnos.Clear();
            alumnos = FromFileToObj(fileName);

            if(alumnos == null || alumnos.Count == 0)
            {
                Console.WriteLine("Error: No se pudieron cargar alumnos o el archivo está vacío.");
                return;
            }

            string extensionActual = Path.GetExtension(fileName).ToLower();
            fileFormatToParse = null; // Initialize here for wider scope
            bool esValidoElFormatoDeDestino = false;

            Console.Clear();
            Console.WriteLine($"Formato Actual: {extensionActual}");
            Console.WriteLine($"Archivo cargado: {fileName} ({alumnos.Count} registros)");

            do
            {
                Console.WriteLine("\nSeleccione el formato de destino (.txt/.csv/.json/.xml):");
                fileFormatToParse = Console.ReadLine()?.ToLower().Trim();

                if (string.IsNullOrEmpty(fileFormatToParse))
                {
                    Console.WriteLine("Error: El formato no puede estar vacío.");
                    continue;
                }

                if (!GestorArchivos.EsFormatoValido(fileFormatToParse))
                {
                    Console.WriteLine("Error: Formato no reconocido.");
                    continue;
                }

                if (fileFormatToParse == extensionActual)
                {
                    Console.WriteLine("Error: El formato de destino es igual al de origen.");
                    continue;
                }

                esValidoElFormatoDeDestino = true;

            } while (esValidoElFormatoDeDestino == false);

            string carpeta = GestorArchivos.FolderPath;
            string nombreSinExt = Path.GetFileNameWithoutExtension(fileName);
            string nuevaRuta = Path.Combine(carpeta, nombreSinExt);

            Console.WriteLine($"\nConvirtiendo {extensionActual} -> {fileFormatToParse}...");

            switch (fileFormatToParse)
            {
                case ".txt":
                    SaveFile.saveInTxt(alumnos, nuevaRuta);break;
                case ".csv":
                    SaveFile.saveInCsv(alumnos, nuevaRuta);break;
                case ".json":
                    SaveFile.saveInJson(alumnos, nuevaRuta);break;
                case ".xml":
                    SaveFile.saveInXml(alumnos, nuevaRuta);break;
            }
            
            Console.WriteLine("Conversión finalizada exitosamente.");
        }

        public static List<Alumno> FromFileToObj(string fileName)
        {
            List<Alumno> alumnos = new List<Alumno>();
            Alumno alumno;
            string fullPath = Path.Combine(GestorArchivos.FolderPath, fileName);

            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"{fileName} no existe...");
                return null;
            }

            if (fileName.EndsWith(".txt"))
            {
                string[] lineas = File.ReadAllLines(fullPath);
                foreach (string linea in lineas)
                {
                    if (string.IsNullOrEmpty(linea)) continue;

                    alumno = Alumno.FromTxtToObj(linea);
                    if (alumno != null)
                    {
                        alumnos.Add(alumno);
                    }
                }
                return alumnos;
            }
            if (fileName.EndsWith(".csv"))
            {
                string[] lineas = File.ReadAllLines(fullPath);
                alumnos.Clear();

                foreach (string linea in lineas)
                {
                    if (string.IsNullOrEmpty(linea)) continue;

                    alumno = Alumno.FromCsvToObj(linea);
                    if (alumno != null)
                    {
                        alumnos.Add(alumno);
                    }
                }
                return alumnos;
            }
            if (fileName.EndsWith(".json"))
            {
                string json = File.ReadAllText(fullPath);
                List<Alumno> alumnosFromJson = JsonSerializer.Deserialize<List<Alumno>>(json);
                if(alumnosFromJson != null)
                {
                    return alumnosFromJson;
                }
                return null;
            }
            if (fileName.EndsWith(".xml"))
            {
                // To Do
                return null;
            }
            return null;
        }
    }
}