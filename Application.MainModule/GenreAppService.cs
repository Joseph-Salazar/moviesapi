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

public class GenreAppService : BaseAppService, IGenreAppService
{
    private readonly IGenreRepository _genreRepository;
    private readonly IConfiguration _configuration;

    public GenreAppService(
        IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _genreRepository = serviceProvider.GetService<IGenreRepository>() ?? throw new InvalidOperationException();
        _configuration = serviceProvider.GetService<IConfiguration>() ?? throw new InvalidOperationException();
    }

    public async Task<GenreDto> GetById(int genreId)
    {
        if (genreId == 0)
            throw new WarningException(MessageConst.InvalidSelection);

        var genreDto = await _genreRepository
            .Find(p => p.Id == genreId)
            .ProjectTo<GenreDto>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (genreDto is null)
            throw new WarningException(MessageConst.InvalidSelection);

        return genreDto;
    }

    public async Task<string> Update(UpdateGenreDto genreDto)
    {
        var genreDomain = await _genreRepository.GetAsync(genreDto.Id);

        if (genreDomain is null)
            throw new WarningException(MessageConst.InvalidSelection);

        Mapper.Map(genreDto, genreDomain);

        await _genreRepository.UpdateAsync(genreDomain, new AddGenreValidator(_genreRepository));
        await UnitOfWork.SaveChangesAsync();

        return MessageConst.ProcessSuccessfullyCompleted;
    }

    public async Task<string> Add(AddGenreDto genreDto)
    {
        var newGenre = Mapper.Map<Genre>(genreDto);
        await _genreRepository.AddAsync(newGenre, new AddGenreValidator(_genreRepository));
        await UnitOfWork.SaveChangesAsync();

        return MessageConst.ProcessSuccessfullyCompleted;
    }

    public async Task<string> Delete(int movieId)
    {
        var movieDomain = await _genreRepository.GetAsync(movieId);

        if (movieDomain is null) throw new WarningException(MessageConst.InvalidSelection);

        await _genreRepository.DeleteAsync(movieDomain);
        await UnitOfWork.SaveChangesAsync();

        return MessageConst.ProcessSuccessfullyCompleted;
    }

    public List<GenreDto> ListAll()
    {
        var result = _genreRepository.GetAll().ToList();
        return Mapper.Map<List<GenreDto>>(result);
    }
}