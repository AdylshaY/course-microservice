namespace CourseMicroservice.Catalog.API.Features.Categories.Update
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .Length(4, 25).WithMessage("{PropertyName} must be between 4 and 25 characters.");
        }
    }
}
