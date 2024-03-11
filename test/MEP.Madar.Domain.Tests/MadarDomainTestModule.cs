using Volo.Abp.Modularity;

namespace MEP.Madar;

[DependsOn(
    typeof(MadarDomainModule),
    typeof(MadarTestBaseModule)
)]
public class MadarDomainTestModule : AbpModule
{

}
