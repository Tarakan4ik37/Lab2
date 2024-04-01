using Microsoft.VisualStudio.TextTemplating;
using System;
using System.Collections.Generic;

namespace kurs3.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string  Patronymic { get; set; } = null!;

    public string? Telephone { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
