namespace testwebapp.Features.Hotels.Bookings;

public record CreateBookingRequest(
    Guid RoomId,
    Guid RatePlanId,
    DateOnly CheckIn,
    DateOnly CheckOut,
    int Adults,
    IReadOnlyList<int>? ChildrenAges,
    GuestInfo PrimaryGuest,
    IReadOnlyList<GuestInfo> AdditionalGuests
);

public record GuestInfo(
    string FirstName,
    string LastName,
    string? Email
);
