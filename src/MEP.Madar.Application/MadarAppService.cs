using System;
using System.Collections.Generic;
using System.Text;
using MEP.Madar.Localization;
using Volo.Abp.Application.Services;

namespace MEP.Madar;

/* Inherit your application services from this class.
 */
public abstract class MadarAppService : ApplicationService
{
    protected MadarAppService()
    {
        LocalizationResource = typeof(MadarResource);
    }
}
