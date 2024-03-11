using MEP.Madar.Samples;
using Xunit;

namespace MEP.Madar.EntityFrameworkCore.Domains;

[Collection(MadarTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<MadarEntityFrameworkCoreTestModule>
{

}
