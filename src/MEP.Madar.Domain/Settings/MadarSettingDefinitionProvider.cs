using Volo.Abp.Settings;

namespace MEP.Madar.Settings;

public class MadarSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MadarSettings.MySetting1));
    }
}
