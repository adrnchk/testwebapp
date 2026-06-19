namespace testwebapp.Domain.Entities;

public class Room
{
    public Guid Id { get; init; }
    public Guid HotelId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int MaxAdults { get; init; }
    public bool AllowsChildren { get; init; }

    public Hotel Hotel { get; init; } = null!;
    public ICollection<RatePlan> RatePlans { get; init; } = [];
}
