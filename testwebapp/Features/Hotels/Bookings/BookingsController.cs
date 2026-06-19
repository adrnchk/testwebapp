using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace testwebapp.Features.Hotels.Bookings;

[ApiController]
[Route("api/hotels/{hotelId:guid}/bookings")]
public sealed class BookingsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    public async Task<IActionResult> CreateBooking(
        Guid hotelId,
        [FromBody] CreateBookingRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateBookingCommand(
            hotelId,
            request.RoomId,
            request.RatePlanId,
            request.CheckIn,
            request.CheckOut,
            request.Adults,
            request.ChildrenAges,
            request.PrimaryGuest,
            request.AdditionalGuests);

        var bookingId = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(CreateBooking), new { hotelId, bookingId }, null);
    }
}
