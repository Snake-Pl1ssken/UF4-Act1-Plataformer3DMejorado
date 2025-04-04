using UnityEngine;

public class Condition_TargetIsInWeaponRange : DecisionTreeNode_Condition
{

    protected override bool ConditionIsMeet()
    {
        return enemy.TargetIsInRange();
    }

}
