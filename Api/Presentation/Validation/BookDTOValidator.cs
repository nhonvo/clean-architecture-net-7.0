using FluentValidation;
using Models.Book;

namespace Api.Presentation.Validation
{
    public class BookDTOValidator : AbstractValidator<BookDTO>
    {
        public BookDTOValidator()
        {
            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(book => book.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(book => book.Price)
                .NotNull().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}