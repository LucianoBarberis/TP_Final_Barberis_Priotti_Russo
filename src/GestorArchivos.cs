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
        public const string FolderPath = "Archivos";

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

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            if(File.Exists(Path.Combine(FolderPath, $"{fileName}.{fileFormat}")))
            {
                Console.WriteLine($"El Archivo {fileName}.{fileFormat} ya existe, si continuas se sobreescribira la informacio. (S/n)");
                if(Console.ReadLine().ToLower() == "n")
                {
                    return;
                }
            }

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

                string pathForSave = Path.Combine(FolderPath, fileName);

                switch (fileFormat)
                {
                    case "txt": SaveFile.saveInTxt(AlumnosACargar, pathForSave) ;break;
                    case "csv": SaveFile.saveInCsv(AlumnosACargar, pathForSave);break;
                    case "json": SaveFile.saveInJson(AlumnosACargar, pathForSave);break;
                    case "xml": Console.WriteLine("Arreglar Implementacion");break;
                }
                Console.WriteLine("Archivo Creado Correctamente!!!");
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
        public static bool EsFormatoValido(string fileName)
        {
            if(fileName.EndsWith(".txt"))
            {
                return true;
            }
            
            if(fileName.EndsWith(".csv"))
            {
                return true;
            }
            
            if(fileName.EndsWith(".json"))
            {
                return true;
            }
            
            if(fileName.EndsWith(".xls"))
            {
                return true;
            }
            return false;
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

            if (!EsFormatoValido(fileName))
            {
                Console.WriteLine("El archivo no contiene ninguna extencion valida...");
                return;
            }

            string fullPath = Path.Combine(FolderPath, fileName);

            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"{fileName} no existe...");
                return;
            }
            
            if (fileName.EndsWith(".txt"))
            {
                string[] lineas = File.ReadAllLines(fullPath);
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
                for (int i = 0; i < alumnoList.Count; i++)
                {
                    Console.WriteLine($"| {alumnoList[i].Legajo} | {alumnoList[i].Apellido} | {alumnoList[i].Nombre} | {alumnoList[i].Doc} | {alumnoList[i].Email} | {alumnoList[i].Tel}");
                    if ((i + 1) % 20 == 0)
                    {
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                    }
                }
                Console.WriteLine("==============================================================");
            }
            else if(fileName.EndsWith(".csv"))
            {
                string[] lineas = File.ReadAllLines(fullPath);
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
                alumnoList.RemoveAt(0); // Eliminamos el encabezado
                Console.WriteLine("==============================================================");
                Console.WriteLine("| Legajo | Apellido | Nombre | Nro. Doc. | Email | Telefono |");
                for (int i = 0; i < alumnoList.Count; i++)
                {
                    Console.WriteLine($"| {alumnoList[i].Legajo} | {alumnoList[i].Apellido} | {alumnoList[i].Nombre} | {alumnoList[i].Doc} | {alumnoList[i].Email} | {alumnoList[i].Tel}");
                    if ((i + 1) % 20 == 0)
                    {
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                    }
                }
                Console.WriteLine("==============================================================");
            }
            else if(fileName.EndsWith(".json"))
            {
                string json = File.ReadAllText(fullPath);
                alumnoList.Clear();
                alumnoList = JsonSerializer.Deserialize<List<Alumno>>(json);
                Console.WriteLine("==============================================================");
                Console.WriteLine("| Legajo | Apellido | Nombre | Nro. Doc. | Email | Teléfono |");
                for(int i = 0; i < alumnoList.Count; i++)
                {
                    Console.WriteLine($"| {alumnoList[i].Legajo} | {alumnoList[i].Apellido} | {alumnoList[i].Nombre} | {alumnoList[i].Doc} | {alumnoList[i].Email} | {alumnoList[i].Tel}");
                    if ((i + 1) % 20 == 0)
                    {
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                    }
                }
                Console.WriteLine("==============================================================");
            }
            else if(fileName.EndsWith(".xml"))
            {
                // To Do
            }

            Console.WriteLine("Total de alumnos: " + alumnoList.Count());
        }
        public static void EditFile()
        {
            Console.Clear();
            string? fileName;
            List<Alumno> alumnosEnMemoria = new List<Alumno>();
            string formatoOriginal = string.Empty;
            bool cambiosRealizados = false;

            // Solicitar el nombre del archivo a modificar
            do
            {
                Console.WriteLine("Ingrese el nombre del archivo a modificar (con extensión):");
                fileName = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(fileName));

            string fullPath = Path.Combine(FolderPath, fileName);

            // Cargar todos los registros en memoria
            alumnosEnMemoria = Conversor.FromFileToObj(fileName);

            if (!EsFormatoValido(fileName))
            {
                Console.WriteLine("\nPresione una tecla para volver al menú principal...");
                Console.ReadKey();
                return;
            }

            bool salirDeEdicion = false;
            while (!salirDeEdicion)
            {
                Console.Clear();
                Console.WriteLine($"--- Editando Archivo: {fileName} ({alumnosEnMemoria.Count} alumnos) ---");
                Console.WriteLine("=== OPCIONES DE MODIFICACIÓN ===");
                Console.WriteLine("1. Agregar nuevo alumno");
                Console.WriteLine("2. Modificar alumno existente (por legajo)");
                Console.WriteLine("3. Eliminar alumno (por legajo)");
                Console.WriteLine("4. Guardar y salir");
                Console.WriteLine("5. Cancelar sin guardar");
                Console.Write("Selecciona una opción: ");
                string? opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarAlumno(alumnosEnMemoria);
                        cambiosRealizados = true;
                        break;
                    case "2":
                        ModificarAlumno(alumnosEnMemoria);
                        cambiosRealizados = true;
                        break;
                    case "3":
                        EliminarAlumno(alumnosEnMemoria);
                        cambiosRealizados = true;
                        break;
                    case "4":
                        SaveFileInOriginalFormat(alumnosEnMemoria, fullPath);
                        salirDeEdicion = true;
                        break;
                    case "5":
                        if (cambiosRealizados)
                        {
                            Console.WriteLine("\nADVERTENCIA: Se detectaron cambios no guardados. ¿Desea descartarlos? (S/N)");
                            if (Console.ReadLine()?.ToUpper() != "S")
                            {
                                break;
                            }
                        }
                        Console.WriteLine("\nCambios descartados. Volviendo al menú principal.");
                        salirDeEdicion = true;
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Inténtalo de nuevo.");
                        break;
                }

                if (!salirDeEdicion)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
            private static void AgregarAlumno(List<Alumno> alumnos)
            {
                string? legajo, apellido, nombre, doc, email, tel;
                bool datosValidos = false;

                do
                {
                    Console.Clear();
                    Console.WriteLine("--- Agregar Nuevo Alumno ---");

                    Console.Write("Legajo: ");
                    legajo = Console.ReadLine();

                    if (alumnos.Any(a => a.Legajo == legajo))
                    {
                        Console.WriteLine("- Error: El legajo ya existe.");
                        Console.WriteLine("\nPresione una tecla para reintentar...");
                        Console.ReadKey();
                        continue;
                    }

                    Console.Write("Apellido: ");
                    apellido = Console.ReadLine();

                    Console.Write("Nombre: ");
                    nombre = Console.ReadLine();

                    Console.Write("DNI: ");
                    doc = Console.ReadLine();

                    Console.Write("Email: ");
                    email = Console.ReadLine();

                    Console.Write("Teléfono: ");
                    tel = Console.ReadLine();

                    datosValidos = Alumno.ValidarAlumno(legajo, apellido, nombre, doc, email, tel);

                    if (datosValidos)
                    {
                        Alumno nuevoAlumno = new Alumno(legajo!, apellido!, nombre!, doc!, email!, tel!);
                        alumnos.Add(nuevoAlumno);
                        Console.WriteLine($"\n Alumno {legajo} - {apellido} {nombre} agregado exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("\nPresione una tecla para reintentar...");
                        Console.ReadKey();
                    }

                } while (!datosValidos);
            }
            private static void ModificarAlumno(List<Alumno> alumnos)
            {
                Console.Clear();
                Console.WriteLine("--- Modificar Alumno Existente ---");
                Console.Write("Ingrese el legajo del alumno a modificar: ");
                string? legajoBusqueda = Console.ReadLine();

                Alumno? alumnoAModificar = alumnos.FirstOrDefault(a => a.Legajo == legajoBusqueda);

                if (alumnoAModificar == null)
                {
                    Console.WriteLine($"Error: No se encontró un alumno con el legajo '{legajoBusqueda}'.");
                    return;
                }

                Console.WriteLine("\n--- Datos Actuales ---");
                Console.WriteLine($"Legajo: {alumnoAModificar.Legajo}");
                Console.WriteLine($"Apellido: {alumnoAModificar.Apellido}");
                Console.WriteLine($"Nombre: {alumnoAModificar.Nombre}");
                Console.WriteLine($"DNI: {alumnoAModificar.Doc}");
                Console.WriteLine($"Email: {alumnoAModificar.Email}");
                Console.WriteLine($"Teléfono: {alumnoAModificar.Tel}");
                Console.WriteLine("\nPresione ENTER para mantener el valor actual o ingrese el nuevo valor.");

                string? nuevoValor;
                bool datosValidos = false;

                do
                {
                    datosValidos = true;

                    // Apellido
                    Console.Write($"Nuevo Apellido ({alumnoAModificar.Apellido}): ");
                    nuevoValor = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nuevoValor)) alumnoAModificar.Apellido = nuevoValor.Trim();
                    if (string.IsNullOrWhiteSpace(alumnoAModificar.Apellido)) { Console.WriteLine("- El apellido no puede estar vacío."); datosValidos = false; }

                    // Nombre
                    Console.Write($"Nuevo Nombre ({alumnoAModificar.Nombre}): ");
                    nuevoValor = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nuevoValor)) alumnoAModificar.Nombre = nuevoValor.Trim();
                    if (string.IsNullOrWhiteSpace(alumnoAModificar.Nombre)) { Console.WriteLine("- El nombre no puede estar vacío."); datosValidos = false; }

                    // DNI
                    Console.Write($"Nuevo DNI ({alumnoAModificar.Doc}): ");
                    nuevoValor = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nuevoValor)) alumnoAModificar.Doc = nuevoValor.Trim();
                    if (!long.TryParse(alumnoAModificar.Doc, out _)) { Console.WriteLine("- El DNI debe ser un número válido."); datosValidos = false; }


                    // Email
                    Console.Write($"Nuevo Email ({alumnoAModificar.Email}): ");
                    nuevoValor = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nuevoValor)) alumnoAModificar.Email = nuevoValor.Trim();
                    if (string.IsNullOrWhiteSpace(alumnoAModificar.Email)) { Console.WriteLine("- El email no puede estar vacío."); datosValidos = false; }
                    // Se asume el uso de System.Text.RegularExpressions
                    else if (!System.Text.RegularExpressions.Regex.IsMatch(alumnoAModificar.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) { Console.WriteLine("- El formato del email no es válido."); datosValidos = false; }


                    // Teléfono
                    Console.Write($"Nuevo Teléfono ({alumnoAModificar.Tel}): ");
                    nuevoValor = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nuevoValor)) alumnoAModificar.Tel = nuevoValor.Trim();
                    if (string.IsNullOrWhiteSpace(alumnoAModificar.Tel)) { Console.WriteLine("- El teléfono no puede estar vacío."); datosValidos = false; }

                    if (!datosValidos)
                    {
                        Console.WriteLine("\nError de validación. Presione una tecla para reintentar los campos inválidos...");
                        Console.ReadKey();
                    }

                } while (!datosValidos);

                Console.WriteLine($"\n✓ Alumno con legajo {alumnoAModificar.Legajo} modificado exitosamente.");
            }
            private static void EliminarAlumno(List<Alumno> alumnos)
            {
                Console.Clear();
                Console.WriteLine("--- Eliminar Alumno ---");
                Console.Write("Ingrese el legajo del alumno a eliminar: ");
                string? legajoBusqueda = Console.ReadLine();

                Alumno? alumnoAEliminar = alumnos.FirstOrDefault(a => a.Legajo == legajoBusqueda);

                if (alumnoAEliminar == null)
                {
                    Console.WriteLine($"Error: No se encontró un alumno con el legajo '{legajoBusqueda}'.");
                    return;
                }

                Console.WriteLine("\n--- Datos del Alumno a Eliminar ---");
                Console.WriteLine($"Legajo: {alumnoAEliminar.Legajo}");
                Console.WriteLine($"Apellido: {alumnoAEliminar.Apellido}");
                Console.WriteLine($"Nombre: {alumnoAEliminar.Nombre}");
                Console.WriteLine($"DNI: {alumnoAEliminar.Doc}");
                Console.WriteLine($"Email: {alumnoAEliminar.Email}");
                Console.WriteLine($"Teléfono: {alumnoAEliminar.Tel}");

                // Solicitar confirmación
                Console.Write("\n¿Está seguro que desea eliminar este alumno? (S/N): ");
                string? confirmacion = Console.ReadLine();

                if (confirmacion != null && confirmacion.Trim().ToUpper() == "S")
                {
                    alumnos.Remove(alumnoAEliminar);
                    Console.WriteLine($"\n✓ Alumno con legajo {alumnoAEliminar.Legajo} eliminado exitosamente.");
                }
                else
                {
                    Console.WriteLine("\nOperación de eliminación cancelada.");
                }
            }
            private static void SaveFileInOriginalFormat(List<Alumno> alumnos, string fileName)
            {
                try
                {
                    string backupFileName = fileName + ".bak";
                    if (File.Exists(fileName))
                    {
                        File.Copy(fileName, backupFileName, true);
                        Console.WriteLine($"\nCopia de seguridad creada: {backupFileName}");
                    }

                    // Guardar los cambios en el archivo original
                    string directory = Path.GetDirectoryName(fileName) ?? FolderPath;
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                    string pathForSave = Path.Combine(directory, fileNameWithoutExt);


                    if(fileName.EndsWith(".txt"))
                    {
                        SaveFile.saveInTxt(alumnos, pathForSave);
                    }
                    else if(fileName.EndsWith(".csv"))
                    {
                        alumnos.RemoveAt(0); // Eliminamos el encabezado ya que en saveInCsv se va a agregar nuevamente
                        SaveFile.saveInCsv(alumnos, pathForSave);
                    }
                    else if (fileName.EndsWith(".json"))
                    {
                        SaveFile.saveInJson(alumnos, pathForSave);
                    }
                    else if (fileName.EndsWith(".xml"))
                    {
                        SaveFile.saveInXml(alumnos, pathForSave);
                    } else
                    {
                        Console.WriteLine("Formato de archivo no soportado para guardar.");
                    }

                    Console.WriteLine($"\nArchivo '{fileName}' guardado y actualizado exitosamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar el archivo: {ex.Message}");
                }
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

            string fullPath = Path.Combine(FolderPath, fileName);

            // Verificar si existe
            if (!File.Exists(fullPath))
            {
                Console.WriteLine($" El archivo {fileName} no existe en el sistema.");
                return;
            }

            // Verificamos que la extencion sea valida para evitar eliminar archivos no deseados
            if (!fileName.EndsWith(".txt") && !fileName.EndsWith(".csv") && !fileName.EndsWith(".json") && !fileName.EndsWith(".xls"))
            {
                Console.WriteLine("El archivo no contiene ninguna extencion valida...");
                return;
            }

            FileInfo infoArchivo = new FileInfo(fullPath);
            Console.WriteLine("\nInformación del archivo");
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
                    File.Delete(fullPath);
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