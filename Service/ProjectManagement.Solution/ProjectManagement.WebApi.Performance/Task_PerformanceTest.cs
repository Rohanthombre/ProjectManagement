using NBench;
using ProjectManagement.Services;
using ProjectManagement.Services.Contracts;
using ProjectManagement.Test.QualityTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ProjectManagement.WebApi.Performance
{
    public class Task_PerformanceTest
    {
        private Counter perfCounter;
        private ITaskService service;
     
        private const string counterName = "PerfCounter";

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            var resource = new ProjectManagementEntitiesFake();
            this.service = new TaskService(resource);
            perfCounter = context.GetCounter(counterName);
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 1000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [TimingMeasurement()]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Mesure_GetAllTasks_ThroughPutMode()
        {
            service.GetAllTasks();
            perfCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, RunTimeMilliseconds = 60000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [TimingMeasurement()]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Mesure_GetAllTasks_IterationsMode()
        {
            service.GetAllTasks();
            perfCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 1000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [TimingMeasurement()]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Mesure_GetTaskById_ThroughPutMode()
        {
            service.GetTaskById(1);
            perfCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, RunTimeMilliseconds = 60000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [TimingMeasurement()]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Mesure_GetTaskById_IterationsMode()
        {
            service.GetTaskById(1);
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
