using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Domain.Sessions.Configuration;
using KeyboardKata.Domain.Tests.Helpers;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace KeyboardKata.Domain.Tests.Sessions.Configuration
{
    public class SessionConfigurationTests
    {
        [Fact]
        public void GivenStream_WhenRead_ReturnSessionConfiguration()
        {
            string configuration =
                """
                {
                    "quitPattern": {
                        "_type": "ExactMatch",
                        "pattern": [
                            {
                                "key": {
                                    "keyCode": "Q",
                                },
                                "keyPress": "Down"
                            }
                        ]
                    }
                }
                """;

            SessionConfiguration result = Read(configuration);

            TestKey q = new("Q");
            ExactMatchPattern quitPattern = Assert.IsType<ExactMatchPattern>(result.QuitPattern);
            Assert.Single(quitPattern.Pattern, i => i.Key == q && i.KeyPress == KeyPress.Down);
        }

        [Fact]
        public void GivenInvalidStream_WhenRead_ThenThrows()
        {
            string invalid = "";

            Assert.Throws<ArgumentException>(() => Read(invalid));
        }

        private SessionConfiguration Read(string json)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            using Stream stream = new MemoryStream(bytes);

            SessionConfigurationReader reader = new(new TestKeyCodeMapper());
            return reader.Read(stream);
        }
    }
}
