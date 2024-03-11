using MEP.Madar.Samples;
using Xunit;

namespace MEP.Madar.EntityFrameworkCore.Applications;

[Collection(MadarTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<MadarEntityFrameworkCoreTestModule>
{

}
