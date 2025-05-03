using Business.Dtos;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/projects")]
//[Authorize]
public class ProjectsApiController : ControllerBase
{

    private readonly IProjectService _projectService;

    public ProjectsApiController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await _projectService.GetAllAsync();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(string id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if(project == null)
        {
            return NotFound($"Project with Id: {id} was not found");
        }
        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectDto dto)
    {
        Console.WriteLine("🔧 CreateAsync called with DTO:");
        Console.WriteLine($"Name: {dto.ProjectName}, Client: {dto.ClientName}");
        var createdProject = await _projectService.CreateAsync(dto);
        return CreatedAtAction(
            nameof(GetProjectById), 
            new { id = createdProject.Id }, 
            createdProject);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UpdateProjectDto dto)
    {
        var updatedProject = await _projectService.UpdateAsync(id, dto);
        if(updatedProject == null)
        {
            return NotFound($"Project with Id: {id} was found for update");
        }
        return Ok(updatedProject);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var successfulDelete = await _projectService.DeleteAsync(id);
        if (!successfulDelete)
        {
            return NotFound($"No project was found with id {id}");
        }
        return NoContent();
    }
}
