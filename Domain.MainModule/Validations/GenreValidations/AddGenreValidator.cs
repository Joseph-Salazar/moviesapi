using Domain.MainModule.Entity;
using Domain.MainModule.IRepository;
using FluentValidation;

namespace Domain.MainModule.Validations.PostulantValidations;

public class AddGenreValidator : AbstractValidator<Genre>
{
    private readonly IGenreRepository _genreRepository;

    public AddGenreValidator(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
}