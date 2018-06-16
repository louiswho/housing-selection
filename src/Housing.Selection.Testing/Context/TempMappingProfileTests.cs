using AutoMapper;
using Xunit;

namespace Housing.Selection.Testing.Context
{
    public class TempMappingProfileTests
    {
        [Fact]
        public void MappingProfile_MapsAreValid()
        {
            var config = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));

            config.AssertConfigurationIsValid();
        }
    }
}
