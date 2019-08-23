using NBench;
using ProjectManagement.Services;
using ProjectManagement.Services.Contracts;
using ProjectManagement.Test.QualityTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.WebApi.Performance
{
    public class Project_PerformanceTest
    {
        private Counter perfCounter;
        private IProjectService service;

        private const string counterName = "PerfCounter";

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            var resource = new ProjectManagementEntitiesFake();
            this.service = new ProjectService(resource);
            perfCounter = context.GetCounter(counterName);
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 1000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [TimingMeasurement()]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Mesure_GetAllProjects_ThroughPutMode()
        {
            service.GetAllProjects();
            perfCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, RunTimeMilliseconds = 60000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [TimingMeasurement()]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Mesure_GetAllProjects_IterationsMode()
        {
            service.GetAllProjects();
            perfCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 1000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [TimingMeasurement()]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Mesure_GetProjectById_ThroughPutMode()
        {
            service.GetProjectById(1);
            perfCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, RunTimeMilliseconds = 60000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [TimingMeasurement()]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Mesure_GetProjectById_IterationsMode()
        {
            service.GetProjectById(1);
            perfCounter.Increment();
        }

        [PerfCleanup]
        public void Cleanup()
        {
            service.Dispose();
            service = null;
        }
    }
}
