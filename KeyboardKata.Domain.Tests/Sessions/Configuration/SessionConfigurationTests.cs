using KeyboardKata.Domain.Actions.Pools;
using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Domain.Sessions.Configuration;
using KeyboardKata.Domain.Tests.Helpers;
using System;
using System.IO;
using System.Linq;
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
                        "pattern":
                        {
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
        public void Test()
        {
            string json =
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
                        "linear":
                        [
                            {
                                "prompt": "Press A!",
                                "pattern":
                                {
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
                            },
                            {
                                "prompt": "Press B!",
                                "pattern":
                                {
                                    "inputs": 
                                    [
                                        {
                                            "key": 
                                            {
                                                "keyCode": "B",
                                            },
                                            "keyPress": "Down",
                                        }
                                    ]
                                }
                            }
                        ]
                    }
                }
                """;

            SessionConfiguration result = Read(json);

            SingleActionPool single = Assert.IsType<SingleActionPool>(result.Actions.Actions.First());
            Assert.NotNull(single.Pattern);
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

            Assert.ThrowsAny<Exception>(() => Read(configuration));
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
