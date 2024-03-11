using MEP.Madar.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace MEP.Madar.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MadarEntityFrameworkCoreModule),
    typeof(MadarApplicationContractsModule)
    )]
public class MadarDbMigratorModule : AbpModule
{
}
