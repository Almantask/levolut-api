﻿using System.ComponentModel.DataAnnotations;

namespace Levolut.Api.V2.Database.Entities
{
    public class Balance
    {
        [Key]
        public int Id { get; set; }
        public long UserId { get; internal set; }
        public long BankId { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public decimal Amount { get; internal set; }

        public Currency Currency { get; set; }
    }
}