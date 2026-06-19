namespace testwebapp.Domain.Entities;

public class Hotel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;

    public ICollection<Room> Rooms { get; init; } = [];
}
