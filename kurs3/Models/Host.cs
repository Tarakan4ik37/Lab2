using System;
using System.Collections.Generic;

namespace kurs3.Models;

public partial class Host
{
    public int HostId { get; set; }

    public string HostName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string? Telephone { get; set; }

    public int ServiceCost { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
