using FluentValidation;

namespace testwebapp.Features.Hotels.Availability;

public sealed class SearchAvailabilityValidator : AbstractValidator<SearchAvailabilityQuery>
{
    public SearchAvailabilityValidator(TimeProvider timeProvider)
    {
        var today = DateOnly.FromDateTime(timeProvider.GetLocalNow().DateTime);
        var maxCheckIn = today.AddYears(1);

        RuleFor(x => x.HotelId)
            .NotEmpty().WithMessage("Hotel ID is required.");

        RuleFor(x => x.CheckIn)
            .GreaterThanOrEqualTo(today)
                .WithMessage("Check-in date cannot be in the past.")
            .LessThan(maxCheckIn)
                .WithMessage("Check-in date cannot be more than one year in advance.");

        RuleFor(x => x.CheckOut)
            .GreaterThan(x => x.CheckIn)
                .WithMessage("Check-out date must be after check-in date.")
            .LessThanOrEqualTo(x => x.CheckIn.AddMonths(1))
                .WithMessage("Maximum stay duration is one month.");

        RuleFor(x => x.RoomCount)
            .GreaterThanOrEqualTo(1).WithMessage("At least one room is required.");

        RuleFor(x => x.Adults)
            .GreaterThanOrEqualTo(1).WithMessage("At least one adult is required.");

        RuleForEach(x => x.ChildrenAges)
            .InclusiveBetween(0, 17).WithMessage("Children ages must be between 0 and 17.");
    }
}
