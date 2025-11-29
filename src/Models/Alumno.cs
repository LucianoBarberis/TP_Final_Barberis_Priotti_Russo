using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Alumno
{
	public string Legajo { get; set; }
	public string Apellido { get; set; }
	public string Nombre { get; set; }
	public string Doc { get; set; }
	public string Email { get; set; }
	public string Tel { get; set; }

	public Alumno(string legajo, string apellido, string nombre, string doc, string email, string tel)
	{
		Legajo = legajo;
		Apellido = apellido;
		Nombre = nombre;
		Doc = doc;
		Email = email;
		Tel = tel;
	}

    // Metodo para validar que los datos del almuno no esten vacios y el mail tenga un formato correcto
    public static bool ValidarAlumno(string? legajo, string? apellido, string? nombre, string? doc, string? email, string? tel)
    {
        bool esValido = true;

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

        return esValido;
    }
	public override string ToString()
	{
		return $"{Legajo} | {Apellido} | {Nombre} | {Doc} | {Email} | {Tel}";
	}
	public string ToCsvString()
	{
		return $"{Legajo},{Apellido},{Nombre},{Doc},{Email},{Tel}";
	}
    public static Alumno FromTxtToObj(string linea)
    {
        string[] partes = linea.Split(" | ");
        if (partes.Length == 0 || partes.Length != 6) 
        {
            Console.WriteLine("Error: Linea Corrupta");
            return null;
        }

        try
        {
            string legajo = partes[0];
            string apellido = partes[1];
            string nombre = partes[2];
            string doc = partes[3];
            string email = partes[4];
            string tel = partes[5];

            return new Alumno(legajo, apellido, nombre, doc, email, tel);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }
    public static Alumno FromCsvToObj(string linea)
    {
        string[] partes = linea.Split(",");
        if (partes.Length != 6)
        {
            Console.WriteLine("Error: Linea Corrupta");
            return null;
        }

        try
        {
            string legajo = partes[0];
            string apellido = partes[1];
            string nombre = partes[2];
            string doc = partes[3];
            string email = partes[4];
            string tel = partes[5];

            return new Alumno(legajo, apellido, nombre, doc, email, tel);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }
}
