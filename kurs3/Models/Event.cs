using System;
using System.Collections.Generic;

namespace kurs3.Models;

public partial class Event
{
    public int EventId { get; set; }

    public int CustomerId { get; set; }

    public string EventType { get; set; } = null!;

    public DateTime Date { get; set; }

    public int TotalCost { get; set; }

    public int VenueId { get; set; }

    public int HostId { get; set; }

    public int ServiceId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Host Host { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;

    public virtual Venue Venue { get; set; } = null!;
}
