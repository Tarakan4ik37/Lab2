using System;
using System.Collections.Generic;

namespace kurs3.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public int NomerHouse { get; set; }

    public virtual ICollection<Venue> Venues { get; set; } = new List<Venue>();
}
