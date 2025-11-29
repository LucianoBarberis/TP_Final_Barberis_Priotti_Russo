using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RussoPriottiBarberis_GestorAlumnos
{
    public static class GeneradorReportes
    {
        public static void ReportesMenu() 
        {
            string? fileName;
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
                Console.ReadKey();
                return;
            }

            alumnos.Clear();
            alumnos = Conversor.FromFileToObj(fileName);

            if (alumnos == null || alumnos.Count == 0)
            {
                Console.WriteLine("Error: No se pudieron cargar alumnos o el archivo esta vacío.");
                Console.ReadKey();
                return;
            }

            if(fileName.EndsWith(".csv"))
            {
                alumnos.RemoveAt(0); //Eliminamos el encabezado en caso que sea un csv
            }

            // Ordenamos la lista de alumnos alfabeticamente por Apellido usando LINQ
            List<Alumno> alumnosOrdenados = alumnos.OrderBy(a => a.Apellido).ToList();

            GenerarReporteAlumnosPorApellido(alumnosOrdenados);
            
            Console.WriteLine("\nPresione una tecla para volver al menu principal...");
            Console.ReadKey();
        }

        public static void GenerarReporteAlumnosPorApellido(List<Alumno> alumnos)
        {
            StringBuilder sb = new StringBuilder();
            
            Console.Clear();
            sb.AppendLine("===============================");
            sb.AppendLine("REPORTE DE ALUMNOS POR APELLIDO");
            sb.AppendLine($"Fecha/Hora: {DateTime.Now}");
            sb.AppendLine("===============================");

            if (alumnos == null || alumnos.Count == 0)
            {
                 sb.AppendLine("No hay alumnos para mostrar.");
                 Console.WriteLine(sb.ToString());
                 return;
            }

            string apellidoActual = "";
            int cantidadPorApellido = 0;
            bool esPrimerGrupo = true;

            foreach (var alumno in alumnos)
            {
                // Corte de control: Si el apellido cambia
                if (alumno.Apellido != apellidoActual)
                {
                    if (!esPrimerGrupo)
                    {
                        sb.AppendLine("-------------------------------");
                        sb.AppendLine($"-> Subtotal {apellidoActual.ToUpper()}: {cantidadPorApellido} alumno(s)");
                    }

                    apellidoActual = alumno.Apellido;
                    cantidadPorApellido = 0;
                    esPrimerGrupo = false;

                    sb.AppendLine($"\nApellido: {apellidoActual.ToUpper()}");
                }

                // Detalle
                sb.AppendLine($"Legajo: {alumno.Legajo}");
                sb.AppendLine($"Nombre: {alumno.Nombre}");
                sb.AppendLine($"Documento: {alumno.Doc}" );
                sb.AppendLine($"Email: {alumno.Email}");
                sb.AppendLine($"Telefono: {alumno.Tel}");
                cantidadPorApellido++;
            }

            if (!esPrimerGrupo)
            {
                sb.AppendLine("-------------------------------");
                sb.AppendLine($"-> Subtotal {apellidoActual.ToUpper()}: {cantidadPorApellido} alumno(s)");
            }

            sb.AppendLine("\n===============================");
            sb.AppendLine("        RESUMEN GENERAL        ");
            sb.AppendLine("===============================");
            int totalAlumnos = alumnos.Count;
            int cantidadApellidosDiferentes = alumnos.Select(a => a.Apellido).Distinct().Count();
            sb.AppendLine($"Cantidad total de alumnos: {totalAlumnos}");
            sb.AppendLine($"Cantidad de apellidos diferentes: {cantidadApellidosDiferentes}");
            sb.AppendLine("===============================");

            // Imprimir reporte en consola
            Console.WriteLine(sb.ToString());
            Console.WriteLine("\n¿Desea guardar este reporte en un archivo de texto? (S/N): ");
            string? respuesta = Console.ReadLine();

            if (respuesta != null && respuesta.Trim().ToUpper() == "S")
            {
                GuardarReporteEnArchivo(sb.ToString());
            }
        }

        private static void GuardarReporteEnArchivo(string contenidoReporte)
        {
            string? nombreArchivo;
            do
            {
                Console.WriteLine("\nIngrese el nombre para el archivo de reporte (sin extensión): ");
                nombreArchivo = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(nombreArchivo) || !GestorArchivos.EsNombreDeArchivoValido(nombreArchivo));

            string fullPath = Path.Combine(GestorArchivos.FolderPath, nombreArchivo + ".txt");

            try
            {
                if (!Directory.Exists(GestorArchivos.FolderPath))
                {
                    Directory.CreateDirectory(GestorArchivos.FolderPath);
                }

                File.WriteAllText(fullPath, contenidoReporte);
                Console.WriteLine($"\nReporte guardado exitosamente en: {fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al guardar el reporte: {ex.Message}");
            }
        }
    }
}
