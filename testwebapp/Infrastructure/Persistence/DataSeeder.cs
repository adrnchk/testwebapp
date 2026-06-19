using Microsoft.EntityFrameworkCore;
using testwebapp.Domain.Entities;

namespace testwebapp.Infrastructure.Persistence;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext db, CancellationToken cancellationToken = default)
    {
        if (await db.Hotels.AnyAsync(cancellationToken))
            return;

        var grandHotelId = Guid.Parse("11111111-0000-0000-0000-000000000001");
        var boutiqueInnId = Guid.Parse("22222222-0000-0000-0000-000000000001");

        var standardRoomId    = Guid.Parse("11111111-0000-0000-0000-000000000010");
        var deluxeRoomId      = Guid.Parse("11111111-0000-0000-0000-000000000011");
        var suiteRoomId       = Guid.Parse("11111111-0000-0000-0000-000000000012");
        var cozySingleId      = Guid.Parse("22222222-0000-0000-0000-000000000010");
        var familyRoomId      = Guid.Parse("22222222-0000-0000-0000-000000000011");

        var hotels = new List<Hotel>
        {
            new()
            {
                Id = grandHotelId,
                Name = "Grand Palace Hotel",
                Description = "A luxurious 5-star hotel in the city centre with stunning views.",
                Rooms =
                [
                    new Room
                    {
                        Id = standardRoomId,
                        HotelId = grandHotelId,
                        Name = "Standard Room",
                        MaxAdults = 2,
                        AllowsChildren = true,
                        RatePlans =
                        [
                            new RatePlan
                            {
                                Id = Guid.NewGuid(),
                                RoomId = standardRoomId,
                                Name = "Non-refundable Rate",
                                PricePerNight = 120.00m,
                                Currency = "USD",
                                CancellationPolicy = CancellationPolicyType.NonRefundable,
                                MealType = null,
                            },
                            new RatePlan
                            {
                                Id = Guid.NewGuid(),
                                RoomId = standardRoomId,
                                Name = "Flexible Rate with Breakfast",
                                PricePerNight = 150.00m,
                                Currency = "USD",
                                CancellationPolicy = CancellationPolicyType.FreeCancellation,
                                FreeCancellationHoursBeforeCheckIn = 48,
                                MealType = MealType.BedAndBreakfast,
                            },
                        ],
                    },
                    new Room
                    {
                        Id = deluxeRoomId,
                        HotelId = grandHotelId,
                        Name = "Deluxe Room",
                        MaxAdults = 2,
                        AllowsChildren = true,
                        RatePlans =
                        [
                            new RatePlan
                            {
                                Id = Guid.NewGuid(),
                                RoomId = deluxeRoomId,
                                Name = "Non-refundable Rate",
                                PricePerNight = 200.00m,
                                Currency = "USD",
                                CancellationPolicy = CancellationPolicyType.NonRefundable,
                                MealType = null,
                            },
                            new RatePlan
                            {
                                Id = Guid.NewGuid(),
                                RoomId = deluxeRoomId,
                                Name = "Half Board Rate",
                                PricePerNight = 260.00m,
                                Currency = "USD",
                                CancellationPolicy = CancellationPolicyType.FreeCancellation,
                                FreeCancellationHoursBeforeCheckIn = 72,
                                MealType = MealType.HalfBoard,
                            },
                        ],
                    },
                    new Room
                    {
                        Id = suiteRoomId,
                        HotelId = grandHotelId,
                        Name = "Presidential Suite",
                        MaxAdults = 4,
                        AllowsChildren = true,
                        RatePlans =
                        [
                            new RatePlan
                            {
                                Id = Guid.NewGuid(),
                                RoomId = suiteRoomId,
                                Name = "All Inclusive Rate",
                                PricePerNight = 800.00m,
                                Currency = "USD",
                                CancellationPolicy = CancellationPolicyType.FreeCancellation,
                                FreeCancellationHoursBeforeCheckIn = 96,
                                MealType = MealType.AllInclusive,
                            },
                        ],
                    },
                ],
            },
            new()
            {
                Id = boutiqueInnId,
                Name = "Boutique Inn",
                Description = "A charming adults-only boutique hotel with a peaceful atmosphere.",
                Rooms =
                [
                    new Room
                    {
                        Id = cozySingleId,
                        HotelId = boutiqueInnId,
                        Name = "Cozy Single",
                        MaxAdults = 1,
                        AllowsChildren = false,
                        RatePlans =
                        [
                            new RatePlan
                            {
                                Id = Guid.NewGuid(),
                                RoomId = cozySingleId,
                                Name = "Bed & Breakfast",
                                PricePerNight = 85.00m,
                                Currency = "USD",
                                CancellationPolicy = CancellationPolicyType.FreeCancellation,
                                FreeCancellationHoursBeforeCheckIn = 24,
                                MealType = MealType.BedAndBreakfast,
                            },
                        ],
                    },
                    new Room
                    {
                        Id = familyRoomId,
                        HotelId = boutiqueInnId,
                        Name = "Family Suite",
                        MaxAdults = 2,
                        AllowsChildren = false,
                        RatePlans =
                        [
                            new RatePlan
                            {
                                Id = Guid.NewGuid(),
                                RoomId = familyRoomId,
                                Name = "Non-refundable Rate",
                                PricePerNight = 140.00m,
                                Currency = "USD",
                                CancellationPolicy = CancellationPolicyType.NonRefundable,
                                MealType = null,
                            },
                            new RatePlan
                            {
                                Id = Guid.NewGuid(),
                                RoomId = familyRoomId,
                                Name = "Flexible Rate",
                                PricePerNight = 165.00m,
                                Currency = "USD",
                                CancellationPolicy = CancellationPolicyType.FreeCancellation,
                                FreeCancellationHoursBeforeCheckIn = 48,
                                MealType = null,
                            },
                        ],
                    },
                ],
            },
        };

        db.Hotels.AddRange(hotels);
        await db.SaveChangesAsync(cancellationToken);
    }
}
