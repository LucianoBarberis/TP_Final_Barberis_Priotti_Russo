using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Alumno
{
	public string Legajo { get; private set; }
	public string Apellido { get; private set; }
	public string Nombre { get; private set; }
	public string Doc { get; private set; }
	public string Email { get; private set; }
	public string Tel { get; private set; }

	// El constructor es privado para para asegurarnos crear a los alumnos en el metodo ValidaYCrear
	private Alumno(string legajo, string apellido, string nombre, string doc, string email, string tel)
	{
		Legajo = legajo;
		Apellido = apellido;
		Nombre = nombre;
		Doc = doc;
		Email = email;
		Tel = tel;
	}

    // Metodo para 
    public static bool ValidarYCrear(string? legajo, string? apellido, string? nombre, string? doc, string? email, string? tel, out Alumno? alumno)
    {
        bool esValido = true;
        alumno = null;

        if (string.IsNullOrWhiteSpace(legajo))
        {
            Console.WriteLine("- El legajo no puede estar vacío.");
            esValido = false;
        }

        if (string.IsNullOrWhiteSpace(apellido))
        {
            Console.WriteLine("- El apellido no puede estar vacío.");
            esValido = false;
        }

        if (string.IsNullOrWhiteSpace(nombre))
        {
            Console.WriteLine("- El nombre no puede estar vacío.");
            esValido = false;
        }

        if (string.IsNullOrWhiteSpace(doc))
        {
            Console.WriteLine("- El DNI no puede estar vacío.");
            esValido = false;
        }
        else if (!long.TryParse(doc, out _))
        {
            Console.WriteLine("- El DNI debe ser un número válido.");
            esValido = false;
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("- El email no puede estar vacío.");
            esValido = false;
        }
        else if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            Console.WriteLine("- El formato del email no es válido.");
            esValido = false;
        }

        if (string.IsNullOrWhiteSpace(tel))
        {
            Console.WriteLine("- El teléfono no puede estar vacío.");
            esValido = false;
        }

        if (esValido)
        {
            alumno = new Alumno(legajo!, apellido!, nombre!, doc!, email!, tel!);
            // ! Para decirle al compilador que estas variables no son nulas
        }

        return esValido;
    }

	public override string ToString()
	{
		return $"Legajo: {Legajo}, Apellido: {Apellido}, Nombre: {Nombre}, Documento: {Doc}, Email: {Email}, Teléfono: {Tel}";
	}

	public string ToCsvString()
	{
		return $"{Legajo};{Apellido};{Nombre};{Doc};{Email};{Tel}";
	}
}
