using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nexmo.Api.Test.Unit
{
    public class JWTTest
    {
        [Fact]
        public void TestJWT()
        {
            var mockAppId = "ffffffff-ffff-ffff-ffff-ffffffffffff";
            var mockRsaPrivateKey = @"-----BEGIN RSA PRIVATE KEY-----
MIICXAIBAAKBgQCRgWt83vGoI2vx+BIu1R39nLDvGLEvC8R4drrIvsiJkAvIlVZt
PlbeoifYJGDQwtlAR3a8i+B3/AP5tZEoWw+z+VWLX50aRjzHyTn22ih8OeGDoiBw
N3ysCTfQ/x8sDER6uSn8ElxfB9AEZTcRA+4rCRbmj+YLV/Nm+qSNoOIM4wIDAQAB
AoGAC8IZnY2mmZ/DKVqSnZY7RjNTWP710odw6QsvLOm96t/pE9x9j3ZqLrOL5LuL
11Lnm3oq7jGfghKrf5JcmJZDPnhWoGgZvtqFizt1l6y1GY/xlooWhOzEuK9kIrBS
PDhOjnvmLrQIB88Rjgq0LkxjNYsCa5d1zslkB2SfM7sOF4ECQQDkEzk/J0KnAa14
+T00BD5apQsDMU7Tz3aPA1IbmqsKY6wOuHcxrFBMmw3crce6jjq32w36samIPSwG
ucf/JngtAkEAo1IjyJLlVlFA74lGTJTbZ5drrotoYm/YeAnbe6Rhj6pXDfz+lVdQ
5ta/B0TKa1UEgx7pHs39Tmpl2IdfC5ozTwJBANVvL/lrsjI7na1CAQZ2miuVm9Kn
CA+rbFW1U9dFTJ7yW4eDFPhFOvgVeklzzx9EDqsTsedS70XxiQvaO9EInRkCQFKn
TELCzNvNTU6sq24wW4VmpXF1TgObVPMTEgfV3iYF7/69Td4ojWH1xkGYd9Sv9xOg
vhv/5bUctaRKhjhp9pMCQE8BLxzAMlS81dobP3GrCRLdlN/y9R7pu2hyURFFXUw5
j0hq3fgBZz1QLpLxY3TfkM3oFDVhpGvskzjINLk6hxc=
-----END RSA PRIVATE KEY-----";
            var tok = Jwt.CreateToken(mockAppId, mockRsaPrivateKey);
            Assert.False(string.IsNullOrEmpty(tok));
        }
    }
}
