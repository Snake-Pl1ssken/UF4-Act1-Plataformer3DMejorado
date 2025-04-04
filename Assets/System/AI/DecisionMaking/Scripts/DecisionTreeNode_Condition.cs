using UnityEngine;

public abstract class DecisionTreeNode_Condition : DecisionTreeNode
{
    public override void Execute()
    {
        int childrenToExecute = ConditionIsMeet() ? 0 : 1;

        transform.GetChild(childrenToExecute).
            GetComponent<DecisionTreeNode>().
            Execute();
    }

    protected abstract bool ConditionIsMeet();
}
