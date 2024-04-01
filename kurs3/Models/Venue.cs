using System;
using System.Collections.Generic;

namespace kurs3.Models;

public partial class Venue
{
    public int VenueId { get; set; }

    public string VenueName { get; set; } = null!;

    public int AddressId { get; set; }

    public int? Capacity { get; set; }

    public int RentalCost { get; set; }

    public string? Photos { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
