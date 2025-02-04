﻿namespace OnlineShop.Models.Products.Components
{
    public class CentralProcessingUnit : Component
    {
        private const double CPUMultiplier = 1.25;
        public CentralProcessingUnit(int id, string manufacturer, string model, 
            decimal price, double overallPerformance, int generation) 
            : base(id, manufacturer, model, price, overallPerformance, generation) 
        {
        }

        public override double OverallPerformance => base.OverallPerformance * CPUMultiplier;
    }
}
