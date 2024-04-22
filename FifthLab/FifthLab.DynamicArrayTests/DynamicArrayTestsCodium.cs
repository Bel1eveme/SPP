


using FifthLab.DynamicArray;

namespace FifthLab.DynamicArrayTests;

public class DynamicArrayTests
{

// Can get an item at a valid index
    [Fact]
    public void CanGetItemAtValidIndex()
    {
        // Given
        DynamicArray<int> dynamicArray = new DynamicArray<int>();
        dynamicArray.Add(1);
        dynamicArray.Add(2);
        dynamicArray.Add(3);

        // When
        int result = dynamicArray[1];

        // Then
        Assert.Equal(2, result);
    }

// Throws IndexOutOfRangeException when getting an item at an invalid index
    [Fact]
    public void ThrowsIndexOutOfRangeExceptionWhenGettingItemAtInvalidIndex()
    {
        // Given
        DynamicArray<int> dynamicArray = new DynamicArray<int>();
        dynamicArray.Add(1);
        dynamicArray.Add(2);
        dynamicArray.Add(3);

        // When, Then
        Assert.Throws<IndexOutOfRangeException>(() => dynamicArray[3]);
    }

// Throws IndexOutOfRangeException when setting an item at an invalid index
    [Fact]
    public void ThrowsIndexOutOfRangeExceptionWhenSettingItemAtInvalidIndex()
    {
        // Given
        DynamicArray<int> dynamicArray = new DynamicArray<int>();
        dynamicArray.Add(1);
        dynamicArray.Add(2);
        dynamicArray.Add(3);

        // When, Then
        Assert.Throws<IndexOutOfRangeException>(() => dynamicArray[3] = 4);
    }

// Remove an item at index Count / 2
    [Fact]
    public void remove_item_at_middle_index()
    {
        // Given
        DynamicArray<int> dynamicArray = new DynamicArray<int>();
        dynamicArray.Add(1);
        dynamicArray.Add(2);
        dynamicArray.Add(3);
        dynamicArray.Add(4);
        dynamicArray.Add(5);

        // When
        dynamicArray.RemoveAt(dynamicArray.Count / 2);

        // Then
        Assert.Equal(4, dynamicArray.Count);
        Assert.Equal(1, dynamicArray[0]);
        Assert.Equal(2, dynamicArray[1]);
        Assert.Equal(4, dynamicArray[2]);
        Assert.Equal(5, dynamicArray[3]);
    }

// Can iterate over the array using foreach
    [Fact]
    public void test_can_iterate_over_array_using_foreach()
    {
        // Given
        DynamicArray<int> dynamicArray = new DynamicArray<int>();
        dynamicArray.Add(1);
        dynamicArray.Add(2);
        dynamicArray.Add(3);

        // When
        List<int> result = new List<int>();
        foreach (int item in dynamicArray)
        {
            result.Add(item);
        }

        // Then
        Assert.Equal(new List<int> { 1, 2, 3 }, result);
    }

// Throws ArgumentNullException when adding a null item
    [Fact]
    public void Test_ThrowsArgumentNullException_WhenAddingNullItem()
    {
        // Given
        DynamicArray<string> dynamicArray = new DynamicArray<string>();

        // When
        Action action = () => dynamicArray.Add(null);

        // Then
        Assert.Throws<ArgumentNullException>(action);
    }

// Does not throw when clearing an empty array
    [Fact]
    public void Test_ClearEmptyArray()
    {
        // Given
        DynamicArray<int> dynamicArray = new DynamicArray<int>();

        // When
        dynamicArray.Clear();

        // Then
        // No exception should be thrown
    }

// Does not throw when removing an item that is not in the array
    [Fact]
    public void Test_Does_Not_Throw_When_Removing_Item_Not_In_Array()
    {
        // Given
        DynamicArray<int> dynamicArray = new DynamicArray<int>();
        dynamicArray.Add(1);
        dynamicArray.Add(2);
        dynamicArray.Add(3);

        // When
        bool result = dynamicArray.Remove(4);

        // Then
        Assert.False(result);
    }

// Can handle adding more items than the initial capacity
    [Fact]
    public void Can_Add_More_Items_Than_Initial_Capacity()
    {
        // Given
        DynamicArray<int> dynamicArray = new DynamicArray<int>(4);

        // When
        for (int i = 0; i < 10; i++)
        {
            dynamicArray.Add(i);
        }

        // Then
        Assert.Equal(10, dynamicArray.Count);
        for (int i = 0; i < 10; i++)
        {
            Assert.Equal(i, dynamicArray[i]);
        }
    }

// Resizes the array when adding an item beyond the current capacity
    [Fact]
    public void Test_Resizes_Array_When_Adding_Item_Beyond_Capacity()
    {
        // Given
        DynamicArray<int> dynamicArray = new DynamicArray<int>(4);

        // When
        dynamicArray.Add(1);
        dynamicArray.Add(2);
        dynamicArray.Add(3);
        dynamicArray.Add(4);
        dynamicArray.Add(5);

        // Then
        Assert.Equal(5, dynamicArray.Count);
        Assert.Equal(5, dynamicArray[4]);
    }

// Implements ICollection<T> interface
    [Fact]
    public void Should_ReturnCorrectCount_AfterAddingItems()
    {
        // Given
        var dynamicArray = new DynamicArray<int>();

        // When
        dynamicArray.Add(1);
        dynamicArray.Add(2);
        dynamicArray.Add(3);

        // Then
        Assert.Equal(3, dynamicArray.Count);
    }
}