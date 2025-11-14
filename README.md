# API de Marcas de Autos

API REST desarrollada en .NET 8 para la gestión de marcas de automóviles. Proporciona operaciones CRUD completas con persistencia en PostgreSQL.

## 📋 Tabla de Contenidos

- [Características](#características)
- [Tecnologías](#tecnologías)
- [Requisitos Previos](#requisitos-previos)
- [Instalación](#instalación)
- [Ejecución](#ejecución)
  - [Ejecución Local](#ejecución-local)
  - [Ejecución con Docker](#ejecución-con-docker)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Endpoints de la API](#endpoints-de-la-api)
- [Ejecución de Tests](#ejecución-de-tests)
- [Base de Datos](#base-de-datos)
- [Swagger/OpenAPI](#swaggeropenapi)

## ✨ Características

- ✅ Operaciones CRUD completas (Create, Read, Update, Delete)
- ✅ Validación de datos
- ✅ Base de datos PostgreSQL
- ✅ Entity Framework Core con migraciones
- ✅ Tests unitarios con xUnit
- ✅ Documentación Swagger/OpenAPI
- ✅ Docker y Docker Compose para despliegue
- ✅ In-memory database para tests

## 🛠 Tecnologías

- **.NET 8.0** - Framework de desarrollo
- **ASP.NET Core Web API** - Framework para APIs REST
- **Entity Framework Core 8.0** - ORM para acceso a datos
- **PostgreSQL 16** - Base de datos relacional
- **Npgsql.EntityFrameworkCore.PostgreSQL** - Proveedor de EF Core para PostgreSQL
- **xUnit** - Framework de testing
- **Swashbuckle (Swagger)** - Documentación de API
- **Docker & Docker Compose** - Contenedorización

## 📦 Requisitos Previos

Para ejecutar el proyecto necesitas:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (opcional, para ejecución con Docker)
- [PostgreSQL](https://www.postgresql.org/download/) (opcional, si ejecutas localmente sin Docker)

## 🚀 Instalación

1. **Clonar el repositorio** (si aplica):
   ```bash
   git clone <url-del-repositorio>
   cd CarBrandApi
   ```

2. **Restaurar dependencias NuGet**:
   ```bash
   dotnet restore
   ```

## ▶️ Ejecución

### Ejecución Local

1. **Asegúrate de tener PostgreSQL ejecutándose** en el puerto 5432 con:
   - Usuario: `postgres`
   - Contraseña: `postgres`
   - Base de datos: `MarcasDb`

2. **Actualiza la cadena de conexión** en `MarcaAutos.Api/appsettings.json` si es necesario:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=MarcasDb;Username=postgres;Password=postgres"
     }
   }
   ```

3. **Ejecutar la aplicación**:
   ```bash
   cd MarcaAutos.Api
   dotnet run
   ```

4. **Acceder a la API**:
   - API: `http://localhost:5104` (o el puerto configurado)
   - Swagger UI: `http://localhost:5104/swagger`

### Ejecución con Docker

La forma más sencilla de ejecutar el proyecto es usando Docker Compose:

1. **Construir y ejecutar los contenedores**:
   ```bash
   docker-compose up -d
   ```

2. **Ver los logs**:
   ```bash
   docker-compose logs -f api
   ```

3. **Acceder a la API**:
   - API: `http://localhost:5000`
   - Swagger UI: `http://localhost:5000/swagger`

4. **Detener los contenedores**:
   ```bash
   docker-compose down
   ```

5. **Detener y eliminar volúmenes** (elimina la base de datos):
   ```bash
   docker-compose down -v
   ```

## 📁 Estructura del Proyecto

```
CarBrandApi/
├── MarcaAutos.Api/                 # Proyecto principal de la API
│   ├── Controllers/                 # Controladores de la API
│   │   └── MarcasAutosController.cs
│   ├── Data/                        # Contexto de base de datos
│   │   └── AppDbContext.cs
│   ├── Entities/                    # Entidades del dominio
│   │   └── MarcaAuto.cs
│   ├── Migrations/                  # Migraciones de Entity Framework
│   │   ├── 20241113200000_InitialCreate.cs
│   │   ├── AppDbContextModelSnapshot.cs
│   │   └── DesignTimeDbContextFactory.cs
│   ├── Program.cs                   # Punto de entrada de la aplicación
│   ├── appsettings.json             # Configuración de la aplicación
│   └── Dockerfile                   # Configuración de Docker
├── MarcaAutos.Tests/                # Proyecto de tests unitarios
│   └── MarcasAutosControllerTests.cs
├── docker-compose.yml               # Configuración de Docker Compose
└── README.md                        # Este archivo
```

## 🔌 Endpoints de la API

Base URL: `http://localhost:5000/api/MarcasAutos` (Docker) o `http://localhost:5104/api/MarcasAutos` (local)

### GET - Obtener todas las marcas
```http
GET /api/MarcasAutos
```

**Respuesta exitosa (200 OK):**
```json
[
  {
    "id": 1,
    "nombre": "Toyota"
  },
  {
    "id": 2,
    "nombre": "Honda"
  },
  {
    "id": 3,
    "nombre": "Ford"
  }
]
```

### GET - Obtener una marca por ID
```http
GET /api/MarcasAutos/{id}
```

**Ejemplo:**
```http
GET /api/MarcasAutos/1
```

**Respuesta exitosa (200 OK):**
```json
{
  "id": 1,
  "nombre": "Toyota"
}
```

**Respuesta de error (404 Not Found):**
```json
"No se encontró la marca con ID {id}"
```

### POST - Crear una nueva marca
```http
POST /api/MarcasAutos
Content-Type: application/json
```

**Cuerpo de la solicitud:**
```json
{
  "nombre": "BMW"
}
```

**Respuesta exitosa (201 Created):**
```json
{
  "id": 4,
  "nombre": "BMW"
}
```

**Respuesta de error (400 Bad Request):**
```json
"El nombre de la marca es requerido"
```

### PUT - Actualizar una marca existente
```http
PUT /api/MarcasAutos/{id}
Content-Type: application/json
```

**Cuerpo de la solicitud:**
```json
{
  "id": 1,
  "nombre": "Toyota Actualizada"
}
```

**Respuesta exitosa (204 No Content)**

**Respuestas de error:**
- `400 Bad Request`: "El ID de la URL no coincide con el ID del cuerpo de la solicitud" o "El nombre de la marca es requerido"
- `404 Not Found`: "No se encontró la marca con ID {id}"

### DELETE - Eliminar una marca
```http
DELETE /api/MarcasAutos/{id}
```

**Respuesta exitosa (204 No Content)**

**Respuesta de error (404 Not Found):**
```json
"No se encontró la marca con ID {id}"
```

## 🧪 Ejecución de Tests

Para ejecutar los tests unitarios:

```bash
dotnet test
```

Para ejecutar tests con más detalles:

```bash
dotnet test --verbosity normal
```

Para ejecutar tests de un proyecto específico:

```bash
dotnet test MarcaAutos.Tests/MarcaAutos.Tests.csproj
```

**Tests incluidos:**
- ✅ `Get_ReturnsAllMarcas` - Verifica que GET devuelve todas las marcas
- ✅ `Get_WithValidId_ReturnsMarca` - Verifica GET por ID válido
- ✅ `Get_WithInvalidId_ReturnsNotFound` - Verifica GET por ID inválido
- ✅ `Post_WithValidMarca_CreatesMarca` - Verifica creación de marca
- ✅ `Post_WithEmptyNombre_ReturnsBadRequest` - Verifica validación en POST
- ✅ `Put_WithValidMarca_UpdatesMarca` - Verifica actualización de marca
- ✅ `Put_WithInvalidId_ReturnsNotFound` - Verifica PUT con ID inválido
- ✅ `Put_WithMismatchedId_ReturnsBadRequest` - Verifica validación de ID en PUT
- ✅ `Delete_WithValidId_DeletesMarca` - Verifica eliminación de marca
- ✅ `Delete_WithInvalidId_ReturnsNotFound` - Verifica DELETE con ID inválido

## 🗄️ Base de Datos

### Esquema de la Base de Datos

**Tabla: MarcasAutos**

| Columna | Tipo | Descripción |
|---------|------|-------------|
| Id | integer (PK, Identity) | Identificador único de la marca |
| Nombre | varchar(100) | Nombre de la marca (requerido) |

### Datos Iniciales (Seed Data)

La base de datos se inicializa con las siguientes marcas:
- Toyota (Id: 1)
- Honda (Id: 2)
- Ford (Id: 3)

### Migraciones

El proyecto utiliza Entity Framework Core para gestionar el esquema de la base de datos. Las migraciones se aplican automáticamente al iniciar la aplicación usando `EnsureCreated()`.

**Nota:** En producción, se recomienda usar `Migrate()` en lugar de `EnsureCreated()` para un mejor control de versiones del esquema.

## 📚 Swagger/OpenAPI

La API incluye documentación interactiva con Swagger. Una vez que la aplicación esté ejecutándose, puedes acceder a:

- **Swagger UI**: `http://localhost:5000/swagger` (Docker) o `http://localhost:5104/swagger` (local)
- **Swagger JSON**: `http://localhost:5000/swagger/v1/swagger.json`

Desde Swagger UI puedes:
- Ver todos los endpoints disponibles
- Probar los endpoints directamente desde el navegador
- Ver los modelos de datos y esquemas de respuesta

## 🔧 Configuración

### Variables de Entorno (Docker)

Las siguientes variables de entorno se configuran en `docker-compose.yml`:

- `ASPNETCORE_URLS`: URL donde escucha la API (`http://+:8080`)
- `ConnectionStrings__DefaultConnection`: Cadena de conexión a PostgreSQL

### Configuración Local

Edita `MarcaAutos.Api/appsettings.json` para cambiar la configuración:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=MarcasDb;Username=postgres;Password=postgres"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## 🐛 Solución de Problemas

### Error: "relation 'MarcasAutos' does not exist"

Si encuentras este error, significa que la base de datos no se ha inicializado correctamente. Soluciones:

1. **Con Docker**: Elimina los volúmenes y reinicia:
   ```bash
   docker-compose down -v
   docker-compose up -d
   ```

2. **Localmente**: Verifica que la base de datos existe y que la aplicación puede conectarse a ella.

### Error de conexión a la base de datos

- Verifica que PostgreSQL esté ejecutándose
- Confirma las credenciales en `appsettings.json`
- Asegúrate de que el puerto 5432 esté disponible

### Los tests fallan

- Asegúrate de que todas las dependencias estén restauradas: `dotnet restore`
- Verifica que el proyecto de tests tenga referencia al proyecto de API

## 📝 Notas Adicionales

- La API utiliza `AsNoTracking()` en las consultas GET para mejorar el rendimiento
- Los tests utilizan una base de datos en memoria (InMemory) para ejecutarse de forma aislada
- El proyecto está configurado para usar HTTPS redirection en producción
- La aplicación crea automáticamente la base de datos y las tablas al iniciar

## 📄 Licencia

Este proyecto es de código abierto y está disponible para uso educativo y comercial.

## 👤 Autor

Desarrollado como parte de una prueba técnica de backend.

---

**¿Necesitas ayuda?** Revisa la documentación de Swagger en `/swagger` o consulta los logs de la aplicación para más detalles.


