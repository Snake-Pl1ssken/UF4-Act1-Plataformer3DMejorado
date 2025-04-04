using UnityEngine;

public class Condition_HasAlreadyVisitedTheLastTargetPosition : DecisionTreeNode_Condition
{
    protected override bool ConditionIsMeet()
    {
        return enemy.HasAlreadyVisitedTheLastTargetPosition();
    }
}
