using System;
using System.Collections.Generic;

namespace pr20.database;

public partial class Kakoe
{
    public int? Month { get; set; }

    public string Name { get; set; } = null!;

    public int Amount { get; set; }
}
