using System;
using System.Collections.Generic;

namespace adres_api.Entities;

public partial class Mahalleler
{
    public int MahalleId { get; set; }

    public string? MahalleAdi { get; set; }

    public int? IlceId { get; set; }

    public string? IlceAdi { get; set; }

    public int? IlId { get; set; }

    public string? IlAdi { get; set; }
}
