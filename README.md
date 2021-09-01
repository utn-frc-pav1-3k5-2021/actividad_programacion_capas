# BugTracker - Programación en Capas


## 1. Clonar Repositorio (Clone/Checkout)

**1.1. Ejecutar comando clone para descargar repositorio:** 
```sh
$ git clone https://github.com/utn-frc-pav1-3k5-2020/actividad_programacion_capas.git
```
**1.2. Ubicarse en la carpeta generada con el nombre del repositorio:**

```sh
$ cd actividad_programacion_capas
```

**1.3. Crear un nuevo branch (rama)**

Para crear una nueva rama (branch) y saltar a ella, en un solo paso, puedes utilizar el comando  `git checkout`  con la opción  `-b`, indicando el nombre del nuevo branch (reemplazando el nro de legajo) de la siguiente forma branch_{legajo}, para el legajo 12345:

```sh
$ git checkout -b branch_12345 
Switched to a new branch "12345"
```
Y para que se vea reflejada en GitHub:
```sh
$ git push --set-upstream origin branch_12345
```

## 2. Ejecutar Script Base de datos
**2.1. Iniciar la aplicación `Sql Server Management Studio`**

Solicitará ingresar los datos de la base de datos para generar una conexión, completar los datos y hacer click en **Connect**. Los datos del servidor del labsis son:

 - **Tipo Servidor:** Database Engine
 - **Nombre Servidor:** .\SQLEXPRESS
 - **Autenticación:** Windows Authentication.
 
 
 **2.2. Abrir archivo `BugTracker_Crear_BaseDatos.sql`**
 Ir a la opción `Archivo -> Abrir -> Archivo` (o combinación de teclas `Ctrl + O`) y buscar el archivo BugTracker_Crear_BaseDatos.sql en el disco local.
  

**2.5. Ejecutar Script** 
Para ejecutar el script hacer click sobre el botón `Ejecutar` (o usar la tecla `F5`)

## 3. Actividad
1. Desarrollar la capa de Lógica (BusinessLayer) de las siguientes entidades de negocio para obtener el listado de cada usando programación en 3 capas (usar como base lo ya desarrollado para la entidad Bug):
* Producto
* Criticidad
* Prioridad
* Usuario
* Estado

2. En `frmConsultarBug` modificar el método `LlenarCombos` para usar los desarrollado en el punto anterior para obtener los datos del punto anterior.

## 4. Versionar en GitHub los cambios locales (add / commit / push)

> A continuación vamos a crear el **commit** y subir los cambios al servidor GitHub.

1. **Status**. Verificamos los cambios pendientes de versionar.

```sh
$ git status
```

2. **Add**. Agregamos todos los archivos nuevos no versionados.

```sh
$ git add -A
```

3. **Commit**. Generamos un commit con todos los cambios y agregamos un comentario.

```sh
$ git commit -a -m "Comentario"
```

4. **Push**. Enviamos todos los commits locales a GitHub

```sh
$ git push
```

5. **Status**. Verificar que no quedaron cambios pendientes de versionar

```sh
$ git status
```
