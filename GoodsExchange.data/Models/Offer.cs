﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace GoodsExchange.data.Models;

public partial class Offer
{
    public int OfferId { get; set; }

    public int CustomerId { get; set; }

    public bool? IsApproved { get; set; }

    public DateTime OfferDate { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual ICollection<OfferDetail> OfferDetails { get; set; } = new List<OfferDetail>();
}