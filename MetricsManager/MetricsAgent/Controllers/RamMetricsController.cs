﻿using System;
using System.Collections.Generic;
using MetricsAgent.DataAccessLayer;
using MetricsAgent.Metrics;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _repository;
        
        public RamMetricsController(IRamMetricsRepository repository, ILogger<RamMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogInformation(1, "NLog встроен в RamMetricsController");
        }
        
        //TODO: по замечанию create быть не должно
        //http://localhost:51684/api/metrics/ram/create
        //{ "Time": "2021-05-02", "Value": 100 }
        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _logger.LogInformation($"Создается запись с данными Time:{request.Time}; Value:{request.Value}");

            _repository.Create(new RamMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }
        
        //http://localhost:51684/api/metrics/ram/from/2021-04-10/to/2021-05-03
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetByTimePeriod(
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос записи From:{fromTime}; To:{toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new GetByPeriodRamMetricsResponse
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto
                {
                    Time = metric.Time, 
                    Value = metric.Value, 
                    Id = metric.Id
                });
            }

            return Ok(response);
        }
        /*
        [HttpGet("available")]
        public IActionResult GetFreeRamSizeMetrics()
        {
            return Ok();
        }
        */
    }
}