namespace ProjectAPI.Models;

public class DataStorage
{
    public static List<Room> Rooms { get; set; } = new List<Room>
    {
        new Room { Id = 1, Name = "Sala A101", BuildingCode = "A", Floor = 1, Capacity = 30, HasProjector = true, IsActive = true },
        new Room { Id = 2, Name = "Sala B204", BuildingCode = "B", Floor = 2, Capacity = 24, HasProjector = true, IsActive = true },
        new Room { Id = 3, Name = "Laboratorium C01", BuildingCode = "C", Floor = 0, Capacity = 15, HasProjector = false, IsActive = true },
        new Room { Id = 4, Name = "Aula Główna", BuildingCode = "A", Floor = 0, Capacity = 120, HasProjector = true, IsActive = true },
        new Room { Id = 5, Name = "Sala B110", BuildingCode = "B", Floor = 1, Capacity = 20, HasProjector = false, IsActive = false },
    };

    public static List<Reservation> Reservations { get; set; } = new List<Reservation>
    {
        new Reservation { Id = 1, RoomId = 1, OrganizerName = "Anna Kowalska", Topic = "Warsztaty z HTTP i REST", Date = new DateOnly(2026, 5, 10), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(12, 30), Status = "confirmed" },
        new Reservation { Id = 2, RoomId = 2, OrganizerName = "Piotr Nowak", Topic = "Wprowadzenie do SQL", Date = new DateOnly(2026, 5, 10), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(11, 0), Status = "planned" },
        new Reservation { Id = 3, RoomId = 1, OrganizerName = "Maria Wiśniewska", Topic = "ASP.NET Core Controllers", Date = new DateOnly(2026, 5, 11), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(15, 0), Status = "planned" },
        new Reservation { Id = 4, RoomId = 3, OrganizerName = "Tomasz Wójcik", Topic = "Testy jednostkowe w C#", Date = new DateOnly(2026, 5, 12), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(10, 0), Status = "confirmed" },
        new Reservation { Id = 5, RoomId = 4, OrganizerName = "Katarzyna Lis", Topic = "Prezentacja projektów", Date = new DateOnly(2026, 5, 15), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(17, 0), Status = "cancelled" },
    };
}