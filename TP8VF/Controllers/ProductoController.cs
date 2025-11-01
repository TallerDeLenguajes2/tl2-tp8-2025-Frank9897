using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// Asegúrate de que este 'using' coincida con la ubicación real de tu clase Productos y ProductoRepository
// Por ejemplo: using TuProyecto.Models; o usando el namespace correcto.

public class ProductoController : Controller
{
    private readonly ProductoRepository _productoRepository;

    // Constructor que recibe el repositorio (idealmente inyectado por DI)
    public ProductoController()
    {
        _productoRepository = new ProductoRepository(); // En un proyecto real, se usaría Inyección de Dependencias
    }

    // GET: /Producto/
    // Muestra la lista de todos los productos
    public IActionResult Index()
    {
        List<Productos> productos = _productoRepository.GetAll();
        return View(productos);
    }

    // GET: /Producto/Detalle/5
    // Muestra los detalles de un producto específico
    public IActionResult Detalle(int id)
    {
        Productos producto = _productoRepository.GetById(id);
        if (producto == null)
        {
            return NotFound(); // Retorna 404 si no encuentra el producto
        }
        return View(producto);
    }

    // GET: /Producto/Crear
    // Muestra el formulario para crear un nuevo producto
    public IActionResult Crear()
    {
        return View();
    }

    // POST: /Producto/Crear
    // Procesa el formulario para crear un nuevo producto
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(Productos producto)
    {
        // En una aplicación real, validarías el modelo (ModelState.IsValid)
        if (producto != null)
        {
            _productoRepository.Alta(producto);
            return RedirectToAction(nameof(Index)); // Redirige a la lista después de crear
        }
        return View(producto);
    }

    // GET: /Producto/Editar/5
    // Muestra el formulario para editar un producto existente
    public IActionResult Editar(int id)
    {
        Productos producto = _productoRepository.GetById(id);
        if (producto == null)
        {
            return NotFound();
        }
        return View(producto);
    }

    // POST: /Producto/Editar/5
    // Procesa el formulario para actualizar un producto
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(int id, Productos producto)
    {
        // El id de la URL debe coincidir con el id del objeto que se intenta editar
        // if (id != producto.IdProducto) return NotFound(); // Es una buena práctica

        // En una aplicación real, validarías el modelo (ModelState.IsValid)
        if (producto != null)
        {
            bool modificado = _productoRepository.Modificar(id, producto);
            if (modificado)
            {
                return RedirectToAction(nameof(Index)); // Redirige a la lista si la modificación fue exitosa
            }
            // Si la modificación falla (ej. id no existe), podrías manejarlo aquí
        }
        return View(producto);
    }

    // GET: /Producto/Eliminar/5
    // Muestra la vista de confirmación para eliminar
    public IActionResult Eliminar(int id)
    {
        Productos producto = _productoRepository.GetById(id);
        if (producto == null)
        {
            return NotFound();
        }
        return View(producto);
    }

    // POST: /Producto/Eliminar/5
    // Procesa la eliminación del producto
    [HttpPost, ActionName("Eliminar")]
    [ValidateAntiForgeryToken]
    public IActionResult ConfirmarEliminar(int id)
    {
        bool eliminado = _productoRepository.Eliminar(id);
        if (eliminado)
        {
            return RedirectToAction(nameof(Index));
        }
        
        // Si no se eliminó, podríamos volver a la vista de confirmación con un mensaje de error
        // O redirigir a una página de error, pero por ahora solo redirigimos a la lista.
        return RedirectToAction(nameof(Index)); 
    }
}