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
    public class User_PerformanceTest
    {
        private Counter perfCounter;
        private IUserService service;

        private const string counterName = "PerfCounter";

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            var resource = new ProjectManagementEntitiesFake();
            this.service = new UserService(resource);
            perfCounter = context.GetCounter(counterName);
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 1000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [TimingMeasurement()]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Mesure_GetAllUsers_ThroughPutMode()
        {
            service.GetAllUsers();
            perfCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, RunTimeMilliseconds = 60000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [TimingMeasurement()]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Mesure_GetAllUsers_IterationsMode()
        {
            service.GetAllUsers();
            perfCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 1000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [TimingMeasurement()]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Mesure_GetUserById_ThroughPutMode()
        {
            service.GetUserById(1);
            perfCounter.Increment();
        }

        [PerfBenchmark(RunMode = RunMode.Throughput, RunTimeMilliseconds = 60000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [TimingMeasurement()]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Mesure_GetUserById_IterationsMode()
        {
            service.GetUserById(1);
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
