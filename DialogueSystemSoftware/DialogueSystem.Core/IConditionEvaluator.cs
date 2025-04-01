namespace DialogueSystem.Core
{
    public interface IConditionEvaluator
    {
        bool Evaluate(object left, string op, object right);
    }
}