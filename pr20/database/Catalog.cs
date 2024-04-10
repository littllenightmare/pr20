using System;
using System.Collections.Generic;

namespace pr20.database;

public partial class Catalog
{
    public int CodeObj { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<Zakazy> Zakazies { get; set; } = new List<Zakazy>();
}
