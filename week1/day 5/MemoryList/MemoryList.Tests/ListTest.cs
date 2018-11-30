
using MemoryList.Library;
using System;
using Xunit;


namespace MemoryList.Tests
{
    public class ListTest
    {
        [Fact]
        public void ShouldAddItemToList()
        {
            //Create a new Memory List
            NewList list = new NewList();

            //Add item
            list.Add("test");
            list.Add("test2");

            //Check item is in the first position
            Assert.Equal("test", list[0]);
            Assert.Equal("test2", list[1]);
        }

        [Theory]
        [InlineData(new String[] { }, 0)]
        [InlineData(new String[] { "a" }, 1)]
        [InlineData(new String[] { "a", "b" }, 2)]
        public void ShouldReturnListSize(string[] items, int expected)
        {
            //Create a new Memory List
            NewList list = new NewList();
            //Add items in list
            /*
                for(int i=0; i<items.Count; i++)
                {
                    string item = items[i];

                    list.Add(item);
                }
            */
            foreach (string item in items)
            {
                list.Add(item);
            }

            //Get the size
            int size = list.Count;

            //Compare the size to expected
            Assert.Equal(expected, size);
        }

    }
}
