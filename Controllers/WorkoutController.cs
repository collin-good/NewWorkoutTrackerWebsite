using System.Web;
using Microsoft.AspNetCore.Mvc;
using WorkoutTrackerWebsite.Data;
using WorkoutTrackerWebsite.Models;
using WorkoutTrackerWebsite.Services;

namespace WorkoutTrackerWebsite.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutController : ControllerBase
{
    WorkoutService _service;
    ILogger<WorkoutController> _logger;

    /// <summary>
    /// I'm not sure if the interface will get the WorkoutContext injected so this is left just in case
    /// </summary>
    /// <param name="context"></param>
    public WorkoutController(WorkoutContext context, LoggerFactory logger)
    {
        _service = new WorkoutService(context);
        _logger = logger.CreateLogger<WorkoutController>();
    }
    public WorkoutController(IWorkoutDB context, LoggerFactory logger)
    {
        _service = new WorkoutService(context);
        _logger = logger.CreateLogger<WorkoutController>();
    }

    /// <summary>
    /// HTTP GET
    /// Gets the list of workouts from WorkoutService and returns them with an OK HTTP response
    /// 
    /// !Currently returns an error page when using a http request
    /// </summary>
    /// <returns>The list of workouts or an empty list</returns>
    [HttpGet]
    public ActionResult<List<Workout>> GetAll()
    {
        _logger.LogTrace("Called GetAll");
        return _service.GetSortedWorkouts();
    }
    /// <summary>
    /// HTTP GET 
    /// Gets the workout (if it exists) with the provided ID
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The workout with the specified ID or null</returns>
    [HttpGet("{id:int}")]
    public ActionResult<Workout> GetWorkoutsByID(int id)
    {
        var workout = _service.GetByID(id);
        if (workout is null)
            return NotFound();

        return Ok(workout);
    }

    /// <summary>
    /// HTTP GET
    /// Gets a  list of workouts with the given name
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns>A list of workouts with the specified name or an empty list</returns>
    [HttpGet("{name}")]
    public ActionResult<List<Workout>> GetWorkoutsByName(string name)
    {
        string sanitizedString = HttpUtility.HtmlEncode(name);

        var results = _service.GetByWorkoutName(sanitizedString);

        if (results.Count == 0)
            return NotFound();

        return Ok(results);
    }

    /// <summary>
    /// HTTP POST
    /// Adds a workout to the database
    /// 
    /// </summary>
    /// <param name="workout"></param>
    /// <returns>A CreatetedAtAction if successful or a BadRequest if not</returns>
    [HttpPost]
    public IActionResult CreateNewWorkout(Workout workout)
    {
        if (workout is null || workout == new Workout())
            return BadRequest();

        workout.Name = HttpUtility.HtmlEncode(workout.Name);

        _service.Add(workout);
        return CreatedAtAction(nameof(GetWorkoutsByID), new { id = workout.Id }, workout);
    }

    /// <summary>
    /// HTTP PUT
    /// Updates the workout at the given ID with the provided workout
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="workout"></param>
    /// <returns>
    /// NoContent if the update is successful, 
    /// BadRequest if the given ID does not match the ID of the new workout, or 
    /// Notfound if the given ID does not exist in the database
    /// </returns>
    [HttpPut]
    public IActionResult UpdateExistingWorkout(int id, Workout workout)
    {
        if (id != workout.Id)
            return BadRequest();

        var existingWorkout = _service.GetByID(workout.Id);
        if (existingWorkout is null)
            return NotFound();

        workout.Name = HttpUtility.HtmlEncode(workout.Name);

        _service.Update(workout);
        return NoContent();
    }

    /// <summary>
    /// HTTP DELETE
    /// Deletes the workout from the database with the given ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>NotFound if there is no workout in the database with the given ID, NoContent otherwise</returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteWorkout(int id)
    {
        var workout = _service.GetByID(id);
        if (workout is null)
            return NotFound();

        _service.Detete(id);
        return NoContent();
    }
}