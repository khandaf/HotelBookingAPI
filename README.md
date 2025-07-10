# Hotel Room Booking API

This is a RESTful API built with ASP.NET Core 8 and EF Core, implementing a hotel room booking system following clean architecture principles.

## Features

- Hotels with room types: single, double, deluxe.
- Prevents double booking and enforces booking rules.
- API endpoints for:
  - Finding hotels by name.
  - Finding available rooms for given dates and guest count.
  - Booking rooms.
  - Retrieving booking details by booking reference.
- Admin endpoints to seed/reset data for testing.
- OpenAPI/Swagger documentation available.
- Automated unit and integration tests included.
- Uses SQLExpress database (can be changed in configuration).

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Optional: [Visual Studio 2022+](https://visualstudio.microsoft.com/) or VS Code with C# extension.

