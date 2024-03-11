using MEP.Madar.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MEP.Madar.Permissions;

public class MadarPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MadarPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(MadarPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MadarResource>(name);
    }
}
