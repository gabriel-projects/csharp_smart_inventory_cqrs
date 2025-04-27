using Api.GRRInnovations.SmartInventory.Infrastructure.Persistence;
using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using Api.GRRInnovations.SmartInventory.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.SmartInventory.Benchmarks.Services
{
    public class BenchmarkService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;

        public BenchmarkService(ApplicationDbContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        //todo: corrigir o erro de iniciar e testar o bench mark com e sem AsNoTracking
        public async Task BenchmarkGetAllWithAndWithoutTracking()
        {
            var options = new ProductOptionsPagination
            {
                Page = 1,
                PageSize = 1000,
                OrderBy = EOrderByType.Name,
                OrderDirection = EOrderByDirection.Ascending,
                AsNoTracking = false
            };

            // Sem AsNoTracking
            var sw = Stopwatch.StartNew();
            var tracked = await _productRepository.GetAllAsync(options);
            sw.Stop();
            var trackedTime = sw.ElapsedMilliseconds;
            Console.WriteLine($"⏱️ With Tracking: {tracked.Count} items in {trackedTime} ms");

            // Com AsNoTracking
            options.AsNoTracking = true;
            sw.Restart();
            var notracked = await _productRepository.GetAllAsync(options);
            sw.Stop();
            var notrackedTime = sw.ElapsedMilliseconds;
            Console.WriteLine($"🚀 Without Tracking: {notracked.Count} items in {notrackedTime} ms");

            Console.WriteLine($"📊 Tracking vs NoTracking: {trackedTime}ms vs {notrackedTime}ms");
        }
    }
}
