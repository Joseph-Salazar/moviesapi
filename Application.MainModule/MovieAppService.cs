using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Core;
using Application.Dto.Postulant;
using Application.MainModule.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.MainModule.Entity;
using Domain.MainModule.IRepository;
using Domain.MainModule.Validations.PostulantValidations;
using Infrastructure.CrossCutting.Constants;
using Infrastructure.CrossCutting.CustomExections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.MainModule;

public class MovieAppService : BaseAppService, IMovieAppService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IConfiguration _configuration;

    public MovieAppService(
        IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _movieRepository = serviceProvider.GetService<IMovieRepository>() ?? throw new InvalidOperationException();
        _genreRepository = serviceProvider.GetService<IGenreRepository>() ?? throw new InvalidOperationException();
        _configuration = serviceProvider.GetService<IConfiguration>() ?? throw new InvalidOperationException();
    }

    public async Task<MovieDto> GetById(int movieId)
    {
        if (movieId == 0)
            throw new WarningException(MessageConst.InvalidSelection);

        var movieDto = await _movieRepository
            .Find(p => p.Id == movieId)
            .ProjectTo<MovieDto>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (movieDto is null)
            throw new WarningException(MessageConst.InvalidSelection);

        return movieDto;
    }

    public async Task<string> Update(UpdateMovieDto movieDto)
    {
        var movieDomain = await _movieRepository.GetAsync(movieDto.Id);

        if (movieDomain is null)
            throw new WarningException(MessageConst.InvalidSelection);

        Mapper.Map(movieDto, movieDomain);

        await _movieRepository.UpdateAsync(movieDomain, new AddMovieValidator(_movieRepository));
        await UnitOfWork.SaveChangesAsync();

        return MessageConst.ProcessSuccessfullyCompleted;
    }

    public async Task<string> Add(AddMovieDto movieDto)
    {
        var newMovie = Mapper.Map<Movie>(movieDto);
        await _movieRepository.AddAsync(newMovie, new AddMovieValidator(_movieRepository));
        await UnitOfWork.SaveChangesAsync();

        return MessageConst.ProcessSuccessfullyCompleted;
    }

    public async Task<string> AddGenreToMovie(int movieId, int genreId)
    {
        var movieDomain = await _movieRepository.GetAll().FirstOrDefaultAsync(x => x.Id == movieId);
        var genreDomain = await _genreRepository.GetAll().FirstOrDefaultAsync(x => x.Id == genreId);

        if (movieDomain is null || genreDomain is null)
        {
            throw new WarningException(MessageConst.InvalidSelection);
        }

        movieDomain.Genres.Add(genreDomain);
        await UnitOfWork.SaveChangesAsync();

        return MessageConst.ProcessSuccessfullyCompleted;
    }

    public async Task<string> Delete(int movieId)
    {
        var movieDomain = await _movieRepository.GetAsync(movieId);

        if (movieDomain is null) throw new WarningException(MessageConst.InvalidSelection);

        await _movieRepository.DeleteAsync(movieDomain);
        await UnitOfWork.SaveChangesAsync();

        return MessageConst.ProcessSuccessfullyCompleted;
    }

    public List<MovieDto> ListAll()
    {
        var result = _movieRepository.GetAll().ToList();
        return Mapper.Map<List<MovieDto>>(result);
    }

}