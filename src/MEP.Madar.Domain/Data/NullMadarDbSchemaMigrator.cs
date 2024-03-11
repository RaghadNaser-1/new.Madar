using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MEP.Madar.Data;

/* This is used if database provider does't define
 * IMadarDbSchemaMigrator implementation.
 */
public class NullMadarDbSchemaMigrator : IMadarDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
