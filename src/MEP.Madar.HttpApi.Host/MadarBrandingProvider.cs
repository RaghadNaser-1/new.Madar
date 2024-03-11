using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace MEP.Madar;

[Dependency(ReplaceServices = true)]
public class MadarBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Madar";
}
