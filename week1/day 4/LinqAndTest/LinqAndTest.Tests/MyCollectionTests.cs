using LinqAndTestingLibrary;
using System;
using Xunit;

namespace LinqAndTest.Tests
{
    //typically one test class to test each real class
    public class MyCollectionTests
    {
        //In XUnit each test, to test one thing, should be a method with the Fact attribute

        [Fact] //This kind of thing is called an "attribute" - 
               //   Special kind of class that adds extra behavior toa  class, method, property, etc
        public void EmptyCollectionShouldHaveZeroLenght()
        {
            //Arrange (set up the situations to be tested)
            //Sometimes people use acronym "SUT" for "subject under test"
            var sut = new MyCollection(); //It's already empty


            //Act (run the method/behavior that we're specifically testing
            var result = sut.Length;

            //Asert (define what the correct result is and check the we got it
            Assert.Equal(0, result);
        }

        [Fact]
        public void CollectionShouldHaveOneLenght()
        {
            var sut = new MyCollection();

            sut.Add("Teste");
            var result = sut.Length;

            Assert.Equal(1, result);
        }

        /*
         * Fact is for tests that don't take any parameters
         * Theory is a convenient way to run a parameterized test with more than one set of data
         *      DON'T REPEAT yourself
         */

        [Theory]
        [InlineData(new String[] { "a", "ab" }, "ab")]
        [InlineData(new String[] { "ab", "a" }, "ab")]
        [InlineData(new String[] { "a" }, "a")]
        [InlineData(new String[] { "ab", "b2" }, "ab")]
        [InlineData(new String[] {  }, null)]
        [InlineData(new String[] { "ab", null, "a" }, "ab")]
        [InlineData(new String[] { "" }, "")]
        public void LongestShouldREturnLongest(string[] items, string expected)
        {
            //arrange
            var coll = new MyCollection();

            foreach(var item in items)
            {
                coll.Add(item);
            }

            //act
            var actual = coll.Longest();

            //asert
            Assert.Equal(expected, actual);

        }

        //test-drive development
        //Step 1: write tests(s) that fails
        //Step 2: write the code to make the test(s) pass.


        [Fact]
        public void EmptyShouldBeEmpty()
        {
            var coll = new MyCollection();

            var isEmpty = coll.Empty();

            Assert.True(isEmpty);
        }
    }
}
