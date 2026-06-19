namespace testwebapp.Domain.Entities;

public class RatePlan
{
    public Guid Id { get; init; }
    public Guid RoomId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal PricePerNight { get; init; }
    public string Currency { get; init; } = "USD";
    public CancellationPolicyType CancellationPolicy { get; init; }

    /// <summary>
    /// Hours before check-in within which free cancellation is available.
    /// Null when CancellationPolicy is NonRefundable.
    /// </summary>
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
