using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using testwebapp.Domain.Entities;
using testwebapp.Infrastructure.Persistence;

namespace testwebapp.Features.Hotels.Availability;

public record SearchAvailabilityQuery(
    Guid HotelId,
    DateOnly CheckIn,
    DateOnly CheckOut,
    int RoomCount,
    int Adults,
    IReadOnlyList<int>? ChildrenAges
) : IRequest<AvailabilitySearchResponse>;

public record AvailabilitySearchResponse(IReadOnlyList<RoomAvailabilityDto> Rooms);

public record RoomAvailabilityDto(
    Guid RoomId,
    string RoomName,
    IReadOnlyList<RatePlanDto> RatePlans
);

public record RatePlanDto(
    Guid RatePlanId,
    string Name,
    PriceDto TotalPrice,
    CancellationPolicyDto CancellationPolicy,
    MealInfoDto? Meal
);

public record PriceDto(decimal Amount, string Currency);

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(NonRefundablePolicyDto), "nonRefundable")]
[JsonDerivedType(typeof(FreeCancellationPolicyDto), "freeCancellation")]
public abstract record CancellationPolicyDto;
public record NonRefundablePolicyDto() : CancellationPolicyDto;
public record FreeCancellationPolicyDto(DateTimeOffset Deadline) : CancellationPolicyDto;

public record MealInfoDto(string Name, string Type);

public sealed class SearchAvailabilityQueryHandler(AppDbContext db, TimeProvider timeProvider)
    : IRequestHandler<SearchAvailabilityQuery, AvailabilitySearchResponse>
{
    public async Task<AvailabilitySearchResponse> Handle(
        SearchAvailabilityQuery request,
        CancellationToken cancellationToken)
    {
        var hasChildren = request.ChildrenAges is { Count: > 0 };
        var nights = request.CheckOut.DayNumber - request.CheckIn.DayNumber;

        var rooms = await db.Rooms
            .AsNoTracking()
            .Where(r =>
                r.HotelId == request.HotelId &&
                r.MaxAdults >= request.Adults &&
                (!hasChildren || r.AllowsChildren))
            .Include(r => r.RatePlans)
            .OrderBy(r => r.Name)
            .ToListAsync(cancellationToken);

        var checkInDateTime = request.CheckIn.ToDateTime(TimeOnly.MinValue);

        var roomDtos = rooms.Select(room => new RoomAvailabilityDto(
            room.Id,
            room.Name,
            room.RatePlans
                .OrderBy(rp => rp.PricePerNight)
                .Select(rp => MapRatePlan(rp, nights, checkInDateTime))
                .ToList()
        )).ToList();

        return new AvailabilitySearchResponse(roomDtos);
    }

    private CancellationPolicyDto MapCancellationPolicy(RatePlan ratePlan, DateTime checkIn)
    {
        if (ratePlan.CancellationPolicy == CancellationPolicyType.FreeCancellation
            && ratePlan.FreeCancellationHoursBeforeCheckIn.HasValue)
        {
            var deadline = checkIn.AddHours(-ratePlan.FreeCancellationHoursBeforeCheckIn.Value);
            return new FreeCancellationPolicyDto(new DateTimeOffset(deadline, TimeSpan.Zero));
        }

        return new NonRefundablePolicyDto();
    }

    private static MealInfoDto? MapMeal(MealType? mealType) => mealType switch
    {
        MealType.BedAndBreakfast => new MealInfoDto("Bed & Breakfast", nameof(MealType.BedAndBreakfast)),
        MealType.HalfBoard       => new MealInfoDto("Half Board", nameof(MealType.HalfBoard)),
        MealType.FullBoard        => new MealInfoDto("Full Board", nameof(MealType.FullBoard)),
        MealType.AllInclusive    => new MealInfoDto("All Inclusive", nameof(MealType.AllInclusive)),
        null                     => null,
        _                        => null,
    };

    private RatePlanDto MapRatePlan(RatePlan rp, int nights, DateTime checkIn) => new(
        rp.Id,
        rp.Name,
        new PriceDto(rp.PricePerNight * nights, rp.Currency),
        MapCancellationPolicy(rp, checkIn),
        MapMeal(rp.MealType)
    );
}
