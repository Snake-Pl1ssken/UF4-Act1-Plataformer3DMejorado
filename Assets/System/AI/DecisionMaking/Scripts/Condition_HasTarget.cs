using UnityEngine;

public class Condition_HasTarget : DecisionTreeNode_Condition
{
    protected override bool ConditionIsMeet()
    {
        return enemy.HasTarget();
    }
}
