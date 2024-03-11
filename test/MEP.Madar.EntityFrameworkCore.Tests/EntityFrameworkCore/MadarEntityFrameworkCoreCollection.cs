using Xunit;

namespace MEP.Madar.EntityFrameworkCore;

[CollectionDefinition(MadarTestConsts.CollectionDefinitionName)]
public class MadarEntityFrameworkCoreCollection : ICollectionFixture<MadarEntityFrameworkCoreFixture>
{

}
