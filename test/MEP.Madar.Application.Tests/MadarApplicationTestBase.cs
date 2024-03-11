using Volo.Abp.Modularity;

namespace MEP.Madar;

public abstract class MadarApplicationTestBase<TStartupModule> : MadarTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
