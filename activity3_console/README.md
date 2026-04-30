# Actividad #3 – C#

Es una organización llamada AstroNova Mission Control.
Necesita un sistema para gestionar sus operaciones espaciales

Actividad realizada según GitHub Gist, puedes visitarlo haciendo click [Aquí](https://gist.github.com/JARV005/d6458f1d516408d92d0d54cfd0a75ea3)

## Datos existentes

- Astronautas
- Ingenieros
- Naves
- Misiones (entidad relacionada)
    - → Un astronauta puede participar en muchas misiones
    - → Una nave puede ser usada en muchas misiones
    - → Cada misión tiene un astronauta principal y una nave asignada
- Registros De Exploración
    - → Una misión puede tener múltiples registros de exploración
    - → Cada registro pertenece a una sola misión

## Requisitos Técnicos

- CRUD completo para las 5 entidades (crear, leer, actualizar, eliminar)

- Validaciones
    - → Horas de experiencia (Astronauta) debe ser mayor a cero
    - → Capacidad de nave (Nave) debe ser mayor a cero
    - → Nivel de riesgo (Registro de Exploración) debe ser válido (bajo, medio alto)
    - → Estado de misión (Mision) debe ser válido
    - → Años de experiencia (Ingenieros) no pueden ser negativos

- Cada acción realizada debe devolver un mensaje claro al usuario.

- Se deben realizar migraciones (evitando escribir sql)

## Estructura del proyecto

- Repositories
    - → Astronauts.repository.cs
    - → Engineers.repository.cs
    - → Spaceships.repository.cs
    - → Misions.repository.cs
    - → ExplorationRegisters.repository.cs
    - **Nota**: Deben incluir las acciones mínimas de CRUD
- Entities
    - → Astronaut.cs
    - → Engineer.cs
    - → Spaceship.cs
    - → Mision.cs
    - → ExplorationRegister.cs
- Migrations **Nota:** Acá se genera el código SQL en C#
- AstroNovaSystem.cs → Main methods
- Program.cs → Program execution

## Entidades

### Astronauta
- ID (INT Primary Key, Auto_Increment)
- Nombre (Varchar(30) NOT NULL)
- Apellido (Varchar(30) NOT NULL)
- Rango (Enum("novato", "piloto", "comandante") DEFAULT 'Novato')
- Horas de Experiencia (INT NOT NULL CHECK (horas de experiencia > 0))

### Ingeniero
- ID (INT Primary Key, Auto_Increment)
- Nombre (Varchar(30) NOT NULL)
- Apellido (Varchar(30) NOT NULL)
- Especialidad (Enum("propulsión", "sistemas", "IA") NOT NULL)
- Años de Experiencia (INT NOT NULL CHECK (años experiencia > 0))

### Nave
- ID (INT Primary Key, Auto_Increment)
- Nombre (Varchar(60) NOT NULL)
- Modelo (Varchar(100) NOT NULL)
- Capacidad Tripulacion (INT NOT NULL CHECK (capacidad > 0))
- Estado (Enum("operativa", "mantenimiento", "retirada") DEFAULT 'operativa')

### Mision
- ID (INT Primary Key, Auto_Increment)
- Nombre Mision (Varchar(60) NOT NULL)
- Fecha Lanzamiento (Datetime NOT NULL DEFAULT now())
- Estado (Enum("planificada", "en curso", "completada", "fallida") DEFAULT 'planificada")
- ID Astronauta (FOREIGN KEY)
- ID Nave (FOREIGN KEY)

### Registro Exploracion
- ID (INT Primary Key, Auto_Increment)
- Planeta Destino (Varchar(50) NOT NULL)
- Descripcion (Varchar(200) NOT NULL)
- Nivel Riesgo (Enum("bajo", "medio", "alto") DEFAULT "bajo")
- ID Mision (FOREIGN KEY)

## Procesos

...

- Buscar naves operativas
- Filtrar astronautas por rango
- Obtener misiones con su astronauta y nave
- Obtener registros de exploración por misión
- Agrupar misiones por estado
- Contar misiones por astronauta
- Astronautas con más de 3 misiones
- Naves no utilizadas
- Misiones con nivel de riesgo alto
- Misiones en curso con registros asociados

## Important Migration Commands

### Add migration

Usalo cuando: Quieres registrar cambios realizados en el codigo sobre la base de datos

```
dotnet ef migrations add NombreCambio
```

### Delete migration

Usalo cuando: Quieres eliminar una migración creada que NO se ha subido a la base de datos

```
dotnet ef migrations remove
```

### Update changes

Usalo cuando: Quieres subir los cambios registrados a la base de datos

```
dotnet ef database update
```

## Paquetes usados
- → Microsoft.EntityFrameworkCore – Base del ORM
- → Microsoft.EntityFrameworkCore.Design – Genera las migraciones
- → Microsoft.EntityFrameworkCore.Tools – Habilita comandos para las migraciones
- → Pomelo.EntityFrameworkCore.Mysql – Conexión con MySQL


