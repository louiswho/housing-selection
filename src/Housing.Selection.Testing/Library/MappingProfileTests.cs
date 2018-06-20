using Xunit;
using AutoMapper;
using Housing.Selection.Context.Selection;

namespace Housing.Selection.Testing.Library
{
    public class MappingProfileTests
    {
        [Fact]
        public void MappingProfile_MapsAreValid()
        {
            var config = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));

            config.AssertConfigurationIsValid();
        }
    }
}
