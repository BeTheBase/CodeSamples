using DialogueSystem.Core;

namespace DialogueSystem.Infra
{
    public class IntConditionEvaluator : IConditionEvaluator
    {
        public bool Evaluate(object left, string op, object right)
        {
            if (left is int a && right is int b)
            {
                return op switch
                {
                    "==" => a == b,
                    "!=" => a != b,
                    ">"  => a > b,
                    "<"  => a < b,
                    ">=" => a >= b,
                    "<=" => a <= b,
                    _ => false
                };
            }

            return false;
        }
    }
}