using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StringCalculator.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        /**
         * Tests that a empty string will return zero.
         * */
        [TestMethod()]
        public void TestThatEmptyStringsReturnZero()
        {
            Assert.AreEqual(0, StringCalculator.Add(""));
        }
        /*Tests that a comma delimited string will return proper value. ( string that does not specify a custom delimiter)
         */
        [TestMethod()]
        public void TestThatCommaDelmitedReturnsProperValue()
        {
            Assert.AreEqual(6, StringCalculator.Add("1,2,3"));
        }
        /*Tests that specifying a custom delimiter will return the proper value
         */
        [TestMethod()]
        public void TestThatCustomDelmiterReturnsPropervalue()
        {
            Assert.AreEqual(6, StringCalculator.Add("//$\n1$2$3"));
        }
        /*
         Tests that having multiple custom delmiters will still return the proper value.
         */
        [TestMethod()]
        public void TestThatMultipleCustomDelmitersReturnsPropervalue()
        {
            Assert.AreEqual(6, StringCalculator.Add("//$,#\n1$2#3"));
        }
        /*
         * tests that delmiters can be two characters and still return proper value.
         */
        [TestMethod()]
        public void TestThatMultipleCustomDelmitersLargerThan1CharacterReturnsPropervalue()
        {
            Assert.AreEqual(6, StringCalculator.Add("//$$,##\n1$$2##3"));
        }
        /**
         * Tests that multiple delmiters can be multiple characters in length and still return the proper value;
         * */
        [TestMethod()]
        public void TestThatMultipleCustomDelmitersLargerThan2CharacterReturnsPropervalue()
        {
            Assert.AreEqual(6, StringCalculator.Add("//$$$,####\n1$$$2####3"));
        }

        /**
         *Tests that values grater than 1000 are ignored. 
         */
        [TestMethod()]
        public void TestThatNumbersGreaterThan1000areIgnored()
        {
            Assert.AreEqual(6, StringCalculator.Add("//$,#\n1$2#3$1001"));
        }

        /**
         * Tests that negative numbers throw an exception. we check the exceptions message property.
         * */
        [TestMethod]
        public void TestThatNegativeNumbersThrowAnException()
        {
           try
           {
                StringCalculator.Add("//$,#\n-1$2#3$1001");
           }
            catch(System.Exception ex)
            {
                Assert.AreEqual("Negatives not allowed\nThe invalid numbers were -1, ", ex.Message);
            }
        }



    }
}