using MediatR;

namespace testwebapp.Features.Hotels.Bookings;

public record CreateBookingCommand(
    Guid HotelId,
    Guid RoomId,
    Guid RatePlanId,
    DateOnly CheckIn,
    DateOnly CheckOut,
    int Adults,
    IReadOnlyList<int>? ChildrenAges,
    GuestInfo PrimaryGuest,
    IReadOnlyList<GuestInfo> AdditionalGuests
) : IRequest<Guid>;

public sealed class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Guid>
{
    public Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Booking creation is not yet implemented.");
    }
}
