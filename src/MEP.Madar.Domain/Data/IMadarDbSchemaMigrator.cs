using System.Threading.Tasks;

namespace MEP.Madar.Data;

public interface IMadarDbSchemaMigrator
{
    Task MigrateAsync();
}
