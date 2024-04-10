using System;
using System.Collections.Generic;

namespace pr20.database;

public partial class Client
{
    public int CodeClient { get; set; }

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Address { get; set; } = null!;

    public long Phone { get; set; }

    public virtual ICollection<Zakazy> Zakazies { get; set; } = new List<Zakazy>();
}
