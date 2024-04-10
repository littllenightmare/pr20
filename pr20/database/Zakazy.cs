using System;
using System.Collections.Generic;

namespace pr20.database;

public partial class Zakazy
{
    public int NumZak { get; set; }

    public DateOnly DateZak { get; set; }

    public int CodeClient { get; set; }

    public int CodeObj { get; set; }

    public int Amount { get; set; }

    public virtual Client CodeClientNavigation { get; set; } = null!;

    public virtual Catalog CodeObjNavigation { get; set; } = null!;
}
