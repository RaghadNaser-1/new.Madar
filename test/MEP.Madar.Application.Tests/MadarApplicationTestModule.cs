using Volo.Abp.Modularity;

namespace MEP.Madar;

[DependsOn(
    typeof(MadarApplicationModule),
    typeof(MadarDomainTestModule)
)]
public class MadarApplicationTestModule : AbpModule
{

}
