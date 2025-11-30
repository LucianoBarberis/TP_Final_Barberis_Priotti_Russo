# Manual de Usuario - Gestor de Alumnos

Bienvenido al manual de usuario del sistema de Gestión de Alumnos. Este documento le guiará a través de las funcionalidades del sistema, proporcionando ejemplos paso a paso y soluciones a problemas comunes.

## Índice
1. [Introducción](#introducción)
2. [Inicio del Sistema](#inicio-del-sistema)
3. [Funcionalidades y Ejemplos](#funcionalidades-y-ejemplos)
    - [1. Crear Nuevo Archivo](#1-crear-nuevo-archivo)
    - [2. Leer Archivo](#2-leer-archivo)
    - [3. Editar Archivo](#3-editar-archivo)
    - [4. Eliminar Archivo](#4-eliminar-archivo)
    - [5. Conversor de Formatos](#5-conversor-de-formatos)
    - [6. Generar Reportes](#6-generar-reportes)
4. [Casos de Error Comunes y Soluciones](#casos-de-error-comunes-y-soluciones)

---

## Introducción
Esta aplicación de consola permite administrar una base de datos local de alumnos. Usted podrá crear registros, visualizarlos en tablas, editarlos, convertir entre formatos (TXT, CSV, JSON, XML) y generar reportes ordenados.

---

## Inicio del Sistema
Al ejecutar el programa (`dotnet run`), se encontrará con el menú principal:

```text
=========================================
       GESTOR DE ALUMNOS - MENU
=========================================
1. Archivo Nuevo
2. Leer Archivo
3. Editar Archivo
4. Eliminar Archivo
5. Conversor de Archivos
6. Generar Reporte
7. Salir
=========================================
Seleccione una opción:
```

---

## Funcionalidades y Ejemplos

### 1. Crear Nuevo Archivo
Permite crear un archivo desde cero cargando alumnos manualmente.

**Paso a paso:**
1. Seleccione la **Opción 1**.
2. Ingrese el nombre del archivo (ej: `curso_2025`).
3. Seleccione el formato escribiendo: `txt`, `csv`, `json` o `xml`.
4. Indique la cantidad de alumnos a cargar.
5. Complete los datos solicitados por cada alumno.

**Vista de Pantalla (Simulación):**
```text
Ingrese el nombre del archivo a crear:
> curso_A

Ingrese el formato del archivo (txt/csv/json/xml):
> csv

Cantidad de alumnos a cargar en el archivo curso_A.csv:
> 1

--- Ingresando Alumno 1 de 1 ---
Legajo: 12345
Apellido: Perez
Nombre: Juan
DNI: 33123456
Email: juan.perez@email.com
Teléfono: 1122334455

Archivo Creado Correctamente!!!
```

### 2. Leer Archivo
Muestra el contenido de un archivo existente en formato tabular.

**Paso a paso:**
1. Seleccione la **Opción 2**.
2. Ingrese el nombre del archivo **con su extensión** (ej: `curso_A.csv`).

**Vista de Pantalla (Simulación):**
```text
Ingrese el nombre del archivo a leer (Con extension):
> curso_A.csv

==============================================================
| Legajo | Apellido | Nombre | Nro. Doc. | Email | Teléfono |
| 12345 | Perez | Juan | 33123456 | juan.perez@email.com | 1122334455
==============================================================
Total de alumnos: 1
```

### 3. Editar Archivo
Permite modificar el contenido de un archivo ya creado.

**Paso a paso:**
1. Seleccione la **Opción 3**.
2. Ingrese el nombre del archivo a editar.
3. Se abrirá un sub-menú de edición:
   - **1. Agregar:** Añade un alumno al final.
   - **2. Modificar:** Busca por Legajo y permite cambiar datos.
   - **3. Eliminar:** Busca por Legajo y borra el registro.
   - **4. Guardar:** **Importante** para confirmar los cambios.

**Vista de Pantalla (Sub-menú):**
```text
--- Editando Archivo: curso_A.csv (1 alumnos) ---
=== OPCIONES DE MODIFICACIÓN ===
1. Agregar nuevo alumno
2. Modificar alumno existente (por legajo)
3. Eliminar alumno (por legajo)
4. Guardar y salir
5. Cancelar sin guardar
Selecciona una opción:
```

### 4. Eliminar Archivo
Borra permanentemente un archivo del disco.

**Paso a paso:**
1. Seleccione la **Opción 4**.
2. Ingrese el nombre del archivo (ej: `error.txt`).
3. Confirme escribiendo `CONFIRMAR`.

### 5. Conversor de Formatos
Transforma un archivo de un formato a otro (ejemplo: de CSV a JSON).

**Paso a paso:**
1. Seleccione la **Opción 5**.
2. Ingrese el nombre del archivo origen (ej: `datos.csv`).
3. El sistema detectará el formato actual.
4. Seleccione el formato destino.
5. Se creará un nuevo archivo con el mismo nombre pero nueva extensión.

**Vista de Pantalla (Simulación):**
```text
Formato Actual: .csv
Archivo cargado: datos.csv (5 registros)

Seleccione el formato de destino (.txt/.csv/.json/.xml):
> .json

Convirtiendo .csv -> .json...
Conversion finalizada exitosamente.
```

### 6. Generar Reportes
Crea un reporte ordenado por Apellido, agrupando alumnos.

**Paso a paso:**
1. Seleccione la **Opción 6**.
2. Ingrese el archivo a analizar.
3. El reporte se mostrará en pantalla.
4. Al final, se le preguntará si desea guardar el reporte en un archivo `.txt` separado.

---

## Casos de Error Comunes y Soluciones

| Error / Mensaje | Causa Probable | Solución |
|-----------------|----------------|----------|
| **"El archivo no existe..."** | Escribió mal el nombre o faltó la extensión. | Verifique que el archivo esté en la carpeta `Archivos` y escriba el nombre completo (ej: `lista.txt`). |
| **"El formato del email no es válido"** | El correo no tiene @ o dominio. | Ingrese un correo real (ej: `usuario@dominio.com`). |
| **"DNI debe ser un número válido"** | Ingresó letras en el DNI. | Solo ingrese números, sin puntos ni espacios. |
| **"Línea Corrupta" (al leer)** | El archivo fue editado manualmente y se rompió el formato. | Intente arreglar el archivo manualmente respetando los separadores (comas o barras) o elimínelo y créelo de nuevo. |
| **Cierres inesperados al editar XML** | Archivos XML mal formados. | Evite editar los XML manualmente con un editor de texto externo si no conoce la estructura. |

---