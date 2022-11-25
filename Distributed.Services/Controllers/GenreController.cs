using Application.Dto.Postulant;
using Application.MainModule;
using Application.MainModule.Interface;
using Domain.MainModule.Entity;
using Hangfire.MemoryStorage.Dto;
using Infrastructure.CrossCutting.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Distributed.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController
{
    private readonly IGenreAppService _genreAppService;

    public GenreController(IGenreAppService genreAppService)
    {
        _genreAppService = genreAppService;
    }

    /// <summary>
    /// Permite obtener un género por id
    /// </summary>
    [AllowAnonymous]
    [HttpGet("Get")]
    [ProducesResponseType(typeof(JsonResult<GenreDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(int genreId)
    {
        var result = await _genreAppService.GetById(genreId);
        return new OkObjectResult(new JsonResult<GenreDto>(result));
    }

    /// <summary>
    /// Permite actualizar un género
    /// </summary>
    [HttpPut(nameof(Update))]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateGenreDto movieDto)
    {
        var response = await _genreAppService.Update(movieDto);
        return new OkObjectResult(new JsonResult<string>(response));
    }

    /// <summary>
    /// Permite agregar un género
    /// </summary>
    [HttpPost(nameof(Add))]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddGenreDto movieDto)
    {
        var response = await _genreAppService.Add(movieDto);
        return new OkObjectResult(new JsonResult<string>(response));
    }

    /// <summary>
    /// Permite obtener todos los géneros
    /// </summary>
    [HttpGet("All")]
    public IActionResult All()
    {
        var result = _genreAppService.ListAll();
        return new OkObjectResult(result);
    }
}