﻿using System;

namespace MetricsManager.DataAccessLayer.Models
{
    public class ApiRamMetric
    {
        public int Id { get; set; } 
        public int Value { get; set; } 
        public DateTimeOffset Time { get; set; }
        public int AgentId { get; set; }
    }
}