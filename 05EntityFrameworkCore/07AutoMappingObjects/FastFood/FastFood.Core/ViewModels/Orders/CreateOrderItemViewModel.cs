﻿namespace FastFood.Core.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class CreateOrderItemViewModel
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; }
    }
}
