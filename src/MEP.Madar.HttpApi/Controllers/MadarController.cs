using MEP.Madar.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MEP.Madar.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MadarController : AbpControllerBase
{
    protected MadarController()
    {
        LocalizationResource = typeof(MadarResource);
    }
}
