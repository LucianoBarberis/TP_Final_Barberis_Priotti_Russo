using System;
using System.IO;

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
                PrintHeader();
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
                        GeneradorReportes.ReportesMenu();
                        break;
                    case "0":
                        salir = true;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("¡Adiós!");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                        Console.ResetColor();
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        private static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.WriteLine("          GESTOR DE ALUMNOS");
            Console.WriteLine("========================================");
            Console.ResetColor();
        }

        private static void MostrarMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n   [1] Crear Archivo");
            Console.WriteLine("   [2] Leer Archivo");
            Console.WriteLine("   [3] Editar Archivo");
            Console.WriteLine("   [4] Eliminar Archivo");
            Console.WriteLine("   [5] Conversor de Archivos");
            Console.WriteLine("   [6] Emitir Reporte");
            Console.WriteLine();
            Console.WriteLine("   [0] Salir");
            Console.ResetColor();
            Console.WriteLine("========================================");
            Console.Write("Selecciona una opción: ");
        }

        public static void ShowFiles()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--- Archivos Disponibles ---");
            Console.ResetColor();
            
            if (Directory.Exists(GestorArchivos.FolderPath))
            {
                string[] files = Directory.GetFiles(GestorArchivos.FolderPath);
                if (files.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("   (No hay archivos)");
                    Console.ResetColor();
                }
                else
                {
                    foreach (string file in files)
                    {
                        Console.WriteLine(" - " + Path.GetFileName(file));
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("   (Carpeta de archivos vacía o no existe)");
                Console.ResetColor();
            }
        }
    }
}