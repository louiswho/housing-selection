using System;
using System.Linq;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Library.HousingModels;
using Xunit;

namespace Housing.Selection.Testing.Integration
{
    public class BatchRepositoryTests
    {
        private readonly BatchRepository _batchRepository;
        private readonly HousingSelectionDbContext _context;

        public BatchRepositoryTests()
        {
            _context = new HousingSelectionDbContext(IntegrationHelpers.ResolveOptions());
            _batchRepository = new BatchRepository(_context);

            _context.Database.EnsureCreated();
            _context.Batches.RemoveRange(_context.Batches);
            _context.SaveChanges();
        }

        [Fact]
        public void AddBatch_Batch_AddsBatchToDatabase()
        {
            var batch = new Batch
            {
                Id = Guid.NewGuid()
            };

            _batchRepository.AddBatch(batch);

            var batches = _context.Batches.ToList();

            Assert.Contains(batch, batches);
        }

        [Fact]
        public void AllBatches_Empty_ReturnsAllBatches()
        {
            var batch = new Batch
            {
                Id = Guid.NewGuid()
            };
            _context.Batches.Add(batch);
            _context.SaveChanges();

            var batches = _batchRepository.GetBatches();

            Assert.Contains(batch, batches);
        }
    }
}
