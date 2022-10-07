using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeSearcher.Commands.Arguments.Parse;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSearcher.Commands.Implementation;
using CodeSearcher.Commands.Arguments.Implementation;

namespace CodeSearcher.Commands.Arguments.Parse.Tests
{
    [TestClass()]
    public class ArgumentParserTests
    {
        [TestMethod()]
        public void ParseTest()
        {
            var testData = new[]
            {
                "-st какой-то текст"
            };
            var sCommand = new SearchCommand();
            var parser = new ArgumentParser(sCommand, testData);
            parser.Parse();
            Assert.AreEqual(sCommand.Arguments[0].Value, "какой-то текст");
        }
        [TestMethod]
        public void ParseMultiLinesTest()
        {
            var testData = new[]
            {
                "-st какой-то текст /",
                "еще какой-то текст"
            };
            var sCommand = new SearchCommand();
            var parser = new ArgumentParser(sCommand, testData);
            parser.Parse();
            Assert.AreEqual(sCommand.Arguments[0].Value, "какой-то текст еще какой-то текст");
        }
        [TestMethod]
        public void ParseMultiArgumentsTest()
        {
            var testData = new[]
            {
                "-st какой-то текст -sa [еще, еще 1, еще 2]"
            };
            var sCommand = new SearchCommand();
            var parser = new ArgumentParser(sCommand, testData);
            parser.Parse();
            Assert.AreEqual(sCommand.Arguments[0].Value, "какой-то текст");
            Assert.IsTrue(((ArrayArgument)sCommand.Arguments[1]).Values.SequenceEqual(new[] { "еще", "еще 1", "еще 2" }));
        }
    }
}