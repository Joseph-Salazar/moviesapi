using Domain.MainModule.Entity;
using Domain.MainModule.IRepository;
using FluentValidation;

namespace Domain.MainModule.Validations.PostulantValidations;

public class AddMovieValidator : AbstractValidator<Movie>
{
    private readonly IMovieRepository _movieRepository;

    public AddMovieValidator(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
}