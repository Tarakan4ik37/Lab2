using System;
using System.Collections.Generic;

namespace kurs3.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public int Cost { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
