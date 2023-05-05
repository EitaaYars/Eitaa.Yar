using Eitaa.Yar;
using Eitaa.Yar.Requests;
using Eitaa.Yar.Types;

namespace EarlyTests
{
    [TestClass]
    public class EarlyTests
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            EitaaYarClient yar = new("TOKEN_HERE");

            User me = await yar.MakeRequestAsync(new GetMeRequest());

            Assert.IsNotNull(me);
        }
    }
}