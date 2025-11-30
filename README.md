# Gestor de Alumnos - TP Final Programación y Estructura de Datos

**Integrantes:** Luciano Barberis, Nazareno Russo, Lautaro Priotti.

## Descripción
Esta es una aplicacion de consola desarrollada en C# que permite la gestion de alumnos. El sistema esta diseñado para almacenar, leer, editar y convertir registros de estudiantes utilizando multiples formatos de archivo.

### Caracteristicas Principales
- **Gestion de Archivos:** Creacion, Lectura, Edicion y Eliminacion.
- **Formatos Soportados:** TXT, CSV, JSON y XML.
- **Conversion:** Herramienta integrada para convertir archivos entre los diferentes formatos soportados.
- **Reportes:** Generacion de reportes ordenados por apellido, con visualizacion en consola y exportacion a archivo de texto.
- **Validaciones:** Verificacion de datos obligatorios y formato de correo electronico mediante expresiones regulares.

## Requisitos del Sistema
- **SDK:** .NET 8 SDK o superior.
- **Sistema Operativo:** Windows, Linux o macOS (Compatible con cualquier entorno que soporte .NET 8).

## Instrucciones de Compilacion
Para compilar el proyecto, abra su terminal en la carpeta raiz del proyecto y ejecute:

```bash
dotnet build
```

Esto intalara las dependencias necesarias y compilara el codigo fuente, asegurando que no existan errores de sintaxis.

## Instrucciones de Uso BÃ¡sico
Para ejecutar la aplicacion, utilice el siguiente comando desde la rai­z del proyecto:

```bash
dotnet run
```

### Navegacion
El sistema presenta un menu principal numerico. Ingrese el numero de la opcion deseada y presione `Enter`.

1. **Archivo Nuevo:** 
   - Solicita nombre y formato (txt, csv, json, xml).
   - Permite la carga secuencial de alumnos validando los datos en tiempo real.
2. **Leer Archivo:** 
   - Muestra una tabla formateada en consola con los datos del archivo seleccionado.
3. **Editar Archivo:** 
   - Carga un archivo en memoria y permite:
     - Agregar nuevos alumnos.
     - Modificar alumnos existentes (busqueda por Legajo).
     - Eliminar alumnos.
   - Los cambios se guardan al seleccionar la opcion de "Guardar y Salir".
4. **Eliminar Archivo:** 
   - Elimina fisicamente el archivo del disco tras una confirmacion.
5. **Conversor de Archivos:** 
   - Permite transformar un archivo existente a otro formato (ej. de `.csv` a `.json`).
6. **Generar Reporte:** 
   - Genera un listado ordenado alfabeticamente por apellido.
   - Muestra subtotales por apellido y un resumen general.
   - Opcion para exportar el reporte a `.txt`.

> **Nota:** Todos los archivos de datos se almacenan y leen desde la carpeta `Archivos/` creada automaticamente en el directorio de ejecucion.

## Problemas Conocidos
- Es necesario respetar la estructura interna de los archivos si se editan manualmente fuera de la aplicacion para evitar errores de corrupcion ("Linea Corrupta").

## Extras
- **Estructura de Datos:**
  - **TXT:** Separado por ` | `
  - **CSV:** Separado por `,`
  - **JSON/XML:** Estandares web habituales.
- **Validaciones:**
  - DNI: Debe ser numerico.
  - Email: Se valida formato estandar (regex).
  - Campos vacios: No permitidos.
