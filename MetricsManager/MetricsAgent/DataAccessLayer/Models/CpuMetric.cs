﻿using System;

namespace MetricsAgent.DataAccessLayer.Models
{
    public class CpuMetric
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}