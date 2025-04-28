using System.Web;
using Microsoft.AspNetCore.Mvc;
using WorkoutTrackerWebsite.Data;
using WorkoutTrackerWebsite.Models;
using WorkoutTrackerWebsite.Services;

namespace WorkoutTrackerWebsite.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkoutController : ControllerBase
{
    WorkoutService _service;
    public WorkoutController(WorkoutContext context)
    {
        _service = new WorkoutService(context);
    }
    public WorkoutController(IWorkoutDB context)
    {
        _service = new WorkoutService(context);
    }

    //GET
    [HttpGet]
    public async Task<ActionResult<List<Workout>>> GetAll() => await _service.GetSortedWorkouts();

    //GET by ID
    [HttpGet("{id:int}")]
    public ActionResult<Workout> Get(int id)
    {
        var workout = _service.Get(id);
        if (workout is null)
            return NotFound();

        return workout;
    }

    //GET List by name
    [HttpGet("{name}")]
    public ActionResult<List<Workout>> GetWorkoutsByName(string name)
    {
        string sanitizedString = HttpUtility.HtmlEncode(name);

        var results = _service.GetByWorkoutName(sanitizedString);

        if (results.Count == 0)
            return NotFound();

        return results;
    }

    //POST
    [HttpPost]
    public IActionResult Create(Workout workout)
    {
        _service.Add(workout);
        return CreatedAtAction(nameof(Get), new { id = workout.Id }, workout);
    }

    //PUT
    [HttpPut]
    public IActionResult Update(int id, Workout workout)
    {
        if (id != workout.Id)
            return BadRequest();

        var existingWorkout = _service.Get(workout.Id);
        if (existingWorkout is null)
            return NotFound();

        _service.Update(workout);
        return NoContent();
    }

    //DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var workout = _service.Get(id);
        if (workout is null)
            return NotFound();

        _service.Detete(id);
        return NoContent();
    }
}