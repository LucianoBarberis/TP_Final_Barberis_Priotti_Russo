using System;

namespace RussoPriottiBarberis_GestorAlumnos
{
    public static class MenuPrincipal
    {
        public static void Menu()
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                ShowFiles();

                MostrarMenu();
                string? opcion = Console.ReadLine();
                
                switch (opcion)
                {
                    case "1":
                        GestorArchivos.NewFile();
                        break;
                    case "2":
                        GestorArchivos.ReadFile();
                        break;
                    case "3":
                        GestorArchivos.EditFile();
                        break;
                    case "4":
                        GestorArchivos.DeleteFile();
                        break;
                    case "5":
                        Conversor.ConversorMenu();
                        break;
                    case "6":
                        //EmitReport();
                        Console.WriteLine("Funcionalidad 'EmitReport' no implementada.");
                        break;
                    case "0":
                        salir = true;
                        Console.WriteLine("¡Adiós!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        private static void MostrarMenu()
        {
            Console.WriteLine("\n--- Gestor de Archivos ---");
            Console.WriteLine("1. Crear Archivo");
            Console.WriteLine("2. Leer Archivo");
            Console.WriteLine("3. Editar Archivo");
            Console.WriteLine("4. Eliminar Archivo");
            Console.WriteLine("5. Conversor de Archivos");
            Console.WriteLine("6. Emitir Reporte");
            Console.WriteLine("0. Salir");
            Console.Write("Selecciona una opción: ");
        }

        public static void ShowFiles()
        {
            Console.WriteLine("--- Archivos Actuales ---");
            if (Directory.Exists(GestorArchivos.FolderPath))
            {
                string[] files = Directory.GetFiles(GestorArchivos.FolderPath);
                if (files.Length == 0)
                {
                    Console.WriteLine("(No hay archivos)");
                }
                else
                {
                    foreach (string file in files)
                    {
                        Console.WriteLine(Path.GetFileName(file));
                    }
                }
            }
            else
            {
                Console.WriteLine("(Carpeta de archivos vacía o no existe)");
            }
        }
    }
}