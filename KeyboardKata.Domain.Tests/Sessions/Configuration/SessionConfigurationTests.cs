using KeyboardKata.Domain.Actions.Pools;
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
        public void GivenMinimal_WhenRead_ReturnSessionConfiguration()
        {
            string configuration =
                """
                {
                    "quitPattern": 
                    {
                        "inputs": 
                        [
                            {
                                "key": 
                                {
                                    "keyCode": "Q",
                                },
                                "keyPress": "Down"
                            }
                        ]
                    },
                    "actions": 
                    {
                        "prompt": "Do something.",
                        "inputs": 
                        [
                            {
                                "key": 
                                {
                                    "keyCode": "A",
                                },
                                "keyPress": "Down",
                            }
                        ]
                    }
                }
                """;

            SessionConfiguration result = Read(configuration);

            TestKey q = new("Q");
            ExactMatchPattern quitPattern = Assert.IsType<ExactMatchPattern>(result.QuitPattern);
            Assert.Single(quitPattern.Inputs, i => i.Key == q && i.KeyPress == KeyPress.Down);
            SingleActionPool singleAction = Assert.IsType<SingleActionPool>(result.Actions);
            Assert.Equal("Do something.", singleAction.Action.Prompt);
        }

        [Fact]
        public void GivenInvalidStream_WhenRead_ThenThrows()
        {
            string invalid = "";

            Assert.Throws<ArgumentException>(() => Read(invalid));
        }

        [Fact]
        public void GivenIncompleteStream_WhenRead_ThenThrows()
        {
            string configuration =
                """
                {
                    "quitPattern": 
                    {
                        "inputs": 
                        [
                            {
                                "key": 
                                {
                                    "keyCode": "Q",
                                },
                                "keyPress": "Down"
                            }
                        ]
                    }
                }
                """;

            Assert.Throws<InvalidOperationException>(() => Read(configuration));
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
