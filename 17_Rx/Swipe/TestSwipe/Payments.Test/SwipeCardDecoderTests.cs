using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Payments.Test
{
    [TestClass]
    public class SwipeCardDecoderTests
    {
        private SwipeCardDecoder decoder;

        [TestInitialize]
        public void Setup()
        {
            decoder = new SwipeCardDecoder();
        }

        [TestMethod]
        [ExpectedException(typeof(CardDecodingException))]
        public void Decode_WhenPassedANonBFormat_ShouldThrowException()
        {
            string raw = "%61234567890123456^SMITH/R E.MR^1611201165600106000000?;1234567890123456=161120116561060250?";

            decoder.Decode(raw);
        }

        [TestMethod]
        public void Decode_WhenPassedAValidCodedString_ShouldDecodeCorrectly()
        {
            string raw = "%B1234567890123456^SMITH/R E.MR^1611201165600106000000?;1234567890123456=161120116561060250?";

            var decoder = new SwipeCardDecoder();

            CardDetails details = decoder.Decode(raw);

            Assert.AreEqual("1234567890123456", details.Number);
            Assert.AreEqual("SMITH/R E.MR", details.Name);
            Assert.AreEqual("16", details.ExpiryYear);
            Assert.AreEqual("11", details.ExpiryMonth);

        }

        [TestMethod]
        public void Decode_WhenPassedAValidCodedStringWithAlternateSeparator_ShouldDecodeCorrectly()
        {
            string raw = "%B1234567890123456=SMITH/R E.MR=1611201165600106000000?;1234567890123456=161120116561060250?";

            var decoder = new SwipeCardDecoder();

            CardDetails details = decoder.Decode(raw);

            Assert.AreEqual("1234567890123456", details.Number);
            Assert.AreEqual("SMITH/R E.MR", details.Name);
            Assert.AreEqual("16", details.ExpiryYear);
            Assert.AreEqual("11", details.ExpiryMonth);

        }

        [TestMethod]
        public void Decode_WhenPassedAValidCodedStringWithWhitespaceOnName_ShouldTrimName()
        {
            string raw = "%B1234567890123456^SMITH/R E.MR      ^1611201165600106000000?;1234567890123456=161120116561060250?";

            var decoder = new SwipeCardDecoder();

            CardDetails details = decoder.Decode(raw);

            Assert.AreEqual("1234567890123456", details.Number);
            Assert.AreEqual("SMITH/R E.MR", details.Name);
            Assert.AreEqual("16", details.ExpiryYear);
            Assert.AreEqual("11", details.ExpiryMonth);

        }

        [TestMethod]
        public void Decode_WhenPassedAValidCodedStringWithTrack2DisagreeingWithTrack1_ShouldUseTrack2()
        {
            string raw = "%B1234567890123456^SMITH/R E.MR      ^1611201165600106000000?;2345678901234567=151220116561060250?";

            var decoder = new SwipeCardDecoder();

            CardDetails details = decoder.Decode(raw);

            Assert.AreEqual("2345678901234567", details.Number);
            Assert.AreEqual("SMITH/R E.MR", details.Name);
            Assert.AreEqual("15", details.ExpiryYear);
            Assert.AreEqual("12", details.ExpiryMonth);
        }

        [TestMethod]
        public void Decode_WhenPassedAValidCodedStringWithNoTrack2_ShouldUseTrack1()
        {
            string raw = "%B1234567890123456^SMITH/R E.MR      ^1611201165600106000000?";

            var decoder = new SwipeCardDecoder();

            CardDetails details = decoder.Decode(raw);

            Assert.AreEqual("1234567890123456", details.Number);
            Assert.AreEqual("SMITH/R E.MR", details.Name);
            Assert.AreEqual("16", details.ExpiryYear);
            Assert.AreEqual("11", details.ExpiryMonth);
        }

    }
}
