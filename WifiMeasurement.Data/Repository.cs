using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WifiMeasurement.Shared;

namespace WifiMeasurement.Data
{
    public interface IRepository
    {
        Task MigrateAsync();

        void Migrate();

        Task SaveChangesAsync();
    }

    internal abstract class Repository : IRepository
    {
        private readonly DbContext _context;
        private readonly ILogger<Repository> _logger;

        protected Repository(DbContext context, ILogger<Repository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public async Task MigrateAsync()
        {
            var pendingMigrations = _context.Database.GetPendingMigrations().ToArray();
            if (pendingMigrations.Length == 0)
            {
                _logger.LogInformation("No database migration required");
                return;
            }

            _logger.LogInformation("Start database migrations: " + string.Join(";", pendingMigrations));

            await _context.Database.MigrateAsync();

            _logger.LogInformation("Finished database migration.");
        }

        public void Migrate()
        {
            AsyncHelpers.RunSync(MigrateAsync);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
