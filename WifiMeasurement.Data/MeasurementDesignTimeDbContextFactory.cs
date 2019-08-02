using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiMeasurement.Data
{
    /// <summary>
    /// This factory creates the context used by "Add-Migration"
    /// </summary>
    internal class MeasurementDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MeasurementContext>
    {
        MeasurementContext IDesignTimeDbContextFactory<MeasurementContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MeasurementContext>()
                   .UseSqlServer("Data Source=.;"); // Used only for create migrations

            return new MeasurementContext(optionsBuilder.Options);
        }
    }
}
