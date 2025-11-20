using Microsoft.AspNetCore.Mvc;

public class PresupuestosController : Controller
{
    private readonly PresupuestosRepository repo;
    public PresupuestosController()
    {
        repo = new PresupuestosRepository();
    }   
    public IActionResult Index()
    {
        var lista = repo.GetAll();
        return View(lista);
    }


    public IActionResult Details(int id)
    {
        var presupuesto = repo.GetById(id);
        if (presupuesto == null)
        {
            return NotFound();
        }
        return View(presupuesto);
    }
}