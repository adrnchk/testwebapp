namespace testwebapp.Domain.Entities;

public class RatePlan
{
    public Guid Id { get; init; }
    public Guid RoomId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal PricePerNight { get; init; }
    public string Currency { get; init; } = "USD";
    public CancellationPolicyType CancellationPolicy { get; init; }

    public int? FreeCancellationHoursBeforeCheckIn { get; init; }

    public MealType? MealType { get; init; }

    public Room Room { get; init; } = null!;
}

public enum CancellationPolicyType
{
    NonRefundable,
    FreeCancellation,
}

public enum MealType
{
    BedAndBreakfast,
    HalfBoard,
    FullBoard,
    AllInclusive,
}
