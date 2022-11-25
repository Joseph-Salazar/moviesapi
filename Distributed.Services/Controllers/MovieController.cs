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
public class MovieController
{
    private readonly IMovieAppService _movieAppService;

    public MovieController(IMovieAppService movieAppService)
    {
        _movieAppService = movieAppService;
    }

    /// <summary>
    /// Permite obtener una película por id
    /// </summary>
    [AllowAnonymous]
    [HttpGet("Get")]
    [ProducesResponseType(typeof(JsonResult<MovieDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(int movieId)
    {
        var result = await _movieAppService.GetById(movieId);
        return new OkObjectResult(new JsonResult<MovieDto>(result));
    }

    /// <summary>
    /// Permite actualizar una película
    /// </summary>
    [HttpPut(nameof(Update))]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateMovieDto movieDto)
    {
        var response = await _movieAppService.Update(movieDto);
        return new OkObjectResult(new JsonResult<string>(response));
    }

    /// <summary>
    /// Permite agregar una película
    /// </summary>
    [HttpPost(nameof(Add))]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddMovieDto movieDto)
    {
        var response = await _movieAppService.Add(movieDto);
        return new OkObjectResult(new JsonResult<string>(response));
    }

    /// <summary>
    /// Permite agregar un género a una película
    /// </summary>
    [HttpPost(nameof(AddGenre))]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddGenre(int movieId, int genreId)
    {
        var response = await _movieAppService.AddGenreToMovie(movieId, genreId);
        return new OkObjectResult(new JsonResult<string>(response));
    }

    /// <summary>
    /// Permite obtener todas las películas
    /// </summary>
    [AllowAnonymous]
    [HttpGet("All")]
    [ProducesResponseType(typeof(JsonResult<MovieDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult<string>), StatusCodes.Status400BadRequest)]
    public IActionResult All()
    {
        var result =  _movieAppService.ListAll();
        return new OkObjectResult(result);
    }
}