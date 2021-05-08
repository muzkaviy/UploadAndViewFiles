using Microsoft.EntityFrameworkCore;
using UploadFilesServer.Models;

namespace UploadFilesServer.Context
{
    public class FileContext: DbContext
    {
        public FileContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<File> Files { get; set; }
    }
}
