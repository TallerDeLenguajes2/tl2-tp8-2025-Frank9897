using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class ProductoController : Controller
{
    private readonly ProductoRepository _productoRepository;

    // 🔥 Constructor con Inyección de Dependencias (ÚNICO)
    public ProductoController(ProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public IActionResult Index()
    {
        List<Productos> productos = _productoRepository.GetAll();
        return View(productos);
    }

    public IActionResult Detalle(int id)
    {
        Productos producto = _productoRepository.GetById(id);
        if (producto == null) return NotFound();
        return View(producto);
    }

    public IActionResult Crear() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(Productos producto)
    {
        if (producto != null)
        {
            _productoRepository.Alta(producto);
            return RedirectToAction(nameof(Index));
        }
        return View(producto);
    }

    public IActionResult Editar(int id)
    {
        Productos producto = _productoRepository.GetById(id);
        if (producto == null) return NotFound();
        return View(producto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(int id, Productos producto)
    {
        if (producto != null)
        {
            if (_productoRepository.Modificar(id, producto))
                return RedirectToAction(nameof(Index));
        }
        return View(producto);
    }

    public IActionResult Eliminar(int id)
    {
        Productos producto = _productoRepository.GetById(id);
        if (producto == null) return NotFound();
        return View(producto);
    }

    [HttpPost, ActionName("Eliminar")]
    [ValidateAntiForgeryToken]
    public IActionResult ConfirmarEliminar(int id)
    {
        _productoRepository.Eliminar(id);
        return RedirectToAction(nameof(Index));
    }
}
