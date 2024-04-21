using Microsoft.AspNetCore.Mvc;
using PROYECTO.Models;

namespace PROYECTO.Controllers;

public class PersonasController : Controller
{
    private readonly MyDbContext _context;
    private static List<Persona> personas = new List<Persona>();
    private static int idunico=0;
    public PersonasController(MyDbContext context) 
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var gente = _context.Personas.ToList();   
        return View(gente);

    }

    public IActionResult Crear()
    {
        return View();
    }

    public IActionResult Editar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(Persona persona)
    {
        persona.Id = idunico;
        idunico++;
        personas.Add(persona);
        return RedirectToAction("Index");
    }

    

    [HttpPost]
    public IActionResult Editar (int id, Persona personaEditada)
    {
        var persona = personas.FirstOrDefault (p => p.Id == id);
        if (persona == null)
        {
            return NotFound();
        }
        persona.Nombre = personaEditada.Nombre;
        persona.Edad = personaEditada.Edad;
        return RedirectToAction("Index");
    }

    public IActionResult Eliminar (int id)
    {
        var persona = personas.FirstOrDefault(p => p.Id == id);
        if (persona == null)
        {
            return NotFound();
        }
        return View(persona);
    }

    [HttpPost]
    public IActionResult EliminarConfirmado(int id)
    {
        var persona = personas.FirstOrDefault(p =>p.Id == id);
        if (persona == null)
        {
            return NotFound();
        }
        personas.Remove(persona);
        return RedirectToAction("Index");
    }
}
