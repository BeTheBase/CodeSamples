using DialogueSystem.Infra;
using Xunit;

namespace DialogueSystem.Tests
{
    public class IntConditionEvaluatorTests
    {
        private readonly IntConditionEvaluator _evaluator = new();

        [Theory]
        [InlineData(10, "==", 10, true)]
        [InlineData(5, "!=", 3, true)]
        [InlineData(7, ">", 3, true)]
        [InlineData(2, "<", 10, true)]
        [InlineData(4, ">=", 4, true)]
        [InlineData(6, "<=", 9, true)]
        [InlineData(1, "??", 2, false)]
        public void Evaluate_Should_Return_Correct_Result(int a, string op, int b, bool expected)
        {
            var result = _evaluator.Evaluate(a, op, b);
            Assert.Equal(expected, result);
        }
    }
}