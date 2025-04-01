using DialogueSystem.Core;
using DialogueSystem.Infra;
using Xunit;

namespace DialogueSystem.Tests
{
    public class InMemoryPlayerStateTests
    {
        [Fact]
        public void Set_And_Get_Should_Return_Correct_Value()
        {
            var state = new InMemoryPlayerState();
            state.Set("score", 42);

            var value = state.Get<int>("score");

            Assert.Equal(42, value);
        }

        [Theory]
        [InlineData(10, "==", 10, true)]
        [InlineData(5, "!=", 3, true)]
        [InlineData(7, ">", 3, true)]
        [InlineData(2, "<", 10, true)]
        [InlineData(4, ">=", 4, true)]
        [InlineData(6, "<=", 9, true)]
        public void Check_Int_Conditions(int stored, string op, int value, bool expected)
        {
            var state = new InMemoryPlayerState();
            state.Set("x", stored);

            var result = state.Check("x", op, value);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Check_String_Equality_Should_Work()
        {
            var state = new InMemoryPlayerState();
            state.Set("name", "Alice");

            Assert.True(state.Check("name", "==", "Alice"));
        }

        [Fact]
        public void Check_Bool_Equality_Should_Work()
        {
            var state = new InMemoryPlayerState();
            state.Set("flag", true);

            Assert.True(state.Check("flag", "==", true));
        }
    }
}