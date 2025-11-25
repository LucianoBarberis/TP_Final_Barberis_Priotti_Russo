using System;

public class Alumno
{
	public string? Legajo;
	public string? Apellido;
	public string? Nombre;
	public string? Doc;
	public string? Email;
	public string? Tel;

	public Alumno()
	{
		Legajo = string.Empty;
		Apellido = string.Empty;
		Nombre = string.Empty;
		Doc = string.Empty;
		Email = string.Empty;
		Tel = string.Empty;
	}

	public override string? ToString()
	{
		return $"Legajo: {Legajo}, Apellido: {Apellido}, Nombre: {Nombre}, Documento: {Doc}, Email: {Email}, Teléfono: {Tel}";
	}

	public string ToCsvString()
	{
		return $"{Legajo},{Apellido},{Nombre},{Doc},{Email},{Tel}";
	}
}
