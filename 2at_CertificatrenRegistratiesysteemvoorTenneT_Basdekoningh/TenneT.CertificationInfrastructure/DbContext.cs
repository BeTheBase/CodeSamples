using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TenneT.CertificationDomain.Models;

namespace TenneT.CertificationInfrastructure;

public class CertificationDbContext : DbContext
{
    public CertificationDbContext(DbContextOptions<CertificationDbContext> options) : base(options) { }

    public DbSet<Technician> Technicians => Set<Technician>();
    public DbSet<CertificateUpload> CertificateUploads => Set<CertificateUpload>();
    public DbSet<CertificateType> CertificateTypes => Set<CertificateType>();
}
