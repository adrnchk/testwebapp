using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace testwebapp.Features.Hotels.Availability;

[ApiController]
[Route("api/hotels/{hotelId:guid}")]
public sealed class AvailabilityController(ISender sender) : ControllerBase
{
    [HttpGet("availability")]
    [ProducesResponseType(typeof(AvailabilitySearchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SearchAvailability(
        Guid hotelId,
        [FromQuery] DateOnly checkIn,
        [FromQuery] DateOnly checkOut,
        [FromQuery] int rooms,
        [FromQuery] int adults,
        [FromQuery] int[]? childrenAges,
        CancellationToken cancellationToken)
    {
        var query = new SearchAvailabilityQuery(
            hotelId,
            checkIn,
            checkOut,
            rooms,
            adults,
            childrenAges);

        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }
}
