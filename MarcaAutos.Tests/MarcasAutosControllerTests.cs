using MarcaAutos.Api.Controllers;
using MarcaAutos.Api.Data;
using MarcaAutos.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarcaAutos.Tests
{
    public class MarcasAutosControllerTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.MarcasAutos.AddRange(
                new MarcaAuto { Id = 1, Nombre = "Toyota" },
                new MarcaAuto { Id = 2, Nombre = "Honda" },
                new MarcaAuto { Id = 3, Nombre = "Ford" }
            );
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task Get_ReturnsAllMarcas()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new MarcasAutosController(context);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var marcas = Assert.IsAssignableFrom<IEnumerable<MarcaAuto>>(okResult.Value);
            Assert.Equal(3, marcas.Count());
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsMarca()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new MarcasAutosController(context);

            // Act
            var result = await controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var marca = Assert.IsType<MarcaAuto>(okResult.Value);
            Assert.Equal(1, marca.Id);
            Assert.Equal("Toyota", marca.Nombre);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new MarcasAutosController(context);

            // Act
            var result = await controller.Get(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Post_WithValidMarca_CreatesMarca()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new MarcasAutosController(context);
            var nuevaMarca = new MarcaAuto { Nombre = "BMW" };

            // Act
            var result = await controller.Post(nuevaMarca);

            // Assert
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var marca = Assert.IsType<MarcaAuto>(createdAtResult.Value);
            Assert.Equal("BMW", marca.Nombre);
            Assert.True(marca.Id > 0);
            
            // Verify it was saved to database
            var savedMarca = await context.MarcasAutos.FindAsync(marca.Id);
            Assert.NotNull(savedMarca);
            Assert.Equal("BMW", savedMarca.Nombre);
        }

        [Fact]
        public async Task Post_WithEmptyNombre_ReturnsBadRequest()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new MarcasAutosController(context);
            var nuevaMarca = new MarcaAuto { Nombre = "" };

            // Act
            var result = await controller.Post(nuevaMarca);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Put_WithValidMarca_UpdatesMarca()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new MarcasAutosController(context);
            var marcaActualizada = new MarcaAuto { Id = 1, Nombre = "Toyota Actualizada" };

            // Act
            var result = await controller.Put(1, marcaActualizada);

            // Assert
            Assert.IsType<NoContentResult>(result);
            
            // Verify it was updated in database
            var marca = await context.MarcasAutos.FindAsync(1);
            Assert.NotNull(marca);
            Assert.Equal("Toyota Actualizada", marca.Nombre);
        }

        [Fact]
        public async Task Put_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new MarcasAutosController(context);
            var marcaActualizada = new MarcaAuto { Id = 999, Nombre = "No existe" };

            // Act
            var result = await controller.Put(999, marcaActualizada);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Put_WithMismatchedId_ReturnsBadRequest()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new MarcasAutosController(context);
            var marcaActualizada = new MarcaAuto { Id = 2, Nombre = "Test" };

            // Act
            var result = await controller.Put(1, marcaActualizada);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_WithValidId_DeletesMarca()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new MarcasAutosController(context);

            // Act
            var result = await controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
            
            // Verify it was deleted from database
            var marca = await context.MarcasAutos.FindAsync(1);
            Assert.Null(marca);
        }

        [Fact]
        public async Task Delete_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new MarcasAutosController(context);

            // Act
            var result = await controller.Delete(999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }

}