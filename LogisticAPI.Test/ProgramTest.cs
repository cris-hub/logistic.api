
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace LogisticAPI.Test
{
    public class ProgramTest
    {

        [Fact]
        public void InitProgramTest()
        {
            Program progra = new();
            Assert.NotNull(progra);
        }
    }

}
