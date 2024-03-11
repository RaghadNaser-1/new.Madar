using Volo.Abp.Modularity;

namespace MEP.Madar;

/* Inherit from this class for your domain layer tests. */
public abstract class MadarDomainTestBase<TStartupModule> : MadarTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
