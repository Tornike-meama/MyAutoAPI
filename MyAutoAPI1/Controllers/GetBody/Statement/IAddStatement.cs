﻿namespace MyAutoAPI1.Controllers.GetBody.Statement
{
    public class AddCurrencyModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CurrencyId { get; set; }
    }
}