﻿namespace VaporStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VaporStore.Data.Models.Enums;
    
    public class Card
    {
        public Card()
        {
            this.Purchases = new HashSet<Purchase>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.CardNumberMaxLength)]
        public string Number { get; set; }

        [Required]
        [MaxLength(GlobalConstants.CVCNumberMaxLength)]
        public string Cvc { get; set; }

        public CardType Type { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
