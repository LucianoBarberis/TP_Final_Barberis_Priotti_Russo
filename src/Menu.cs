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
                MostrarMenu();
                string? opcion = Console.ReadLine();
                
                switch (opcion)
                {
                    case "1":
                        //AgregarTarea();
                        Console.WriteLine("Funcionalidad 'Agregar Tarea' no implementada.");
                        break;
                    case "2":
                        //ListarTareas();
                        Console.WriteLine("Funcionalidad 'Listar Tareas' no implementada.");
                        break;
                    case "3":
                        //MarcarTareaComoCompletada();
                        Console.WriteLine("Funcionalidad 'Marcar Tarea como Completada' no implementada.");
                        break;
                    case "4":
                        //EliminarTarea();
                        Console.WriteLine("Funcionalidad 'Eliminar Tarea' no implementada.");
                        break;
                    case "5":
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
            Console.Clear();
            Console.WriteLine("\n--- Gestor de Alumnos ---");
            Console.WriteLine("1. Agregar Alumno");
            Console.WriteLine("2. Listar Alumnos");
            Console.WriteLine("3. Modificar Alumno");
            Console.WriteLine("4. Eliminar Alumno");
            Console.WriteLine("5. Salir");
            Console.Write("Selecciona una opción: ");
        }
    }
}