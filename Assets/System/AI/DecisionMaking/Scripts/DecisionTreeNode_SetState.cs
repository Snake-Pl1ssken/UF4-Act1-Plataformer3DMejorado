using UnityEngine;

public class DecisionTreeNode_SetState : DecisionTreeNode
{
    [SerializeField] BaseState state;

    public override void Execute()
    {
        enemy.ChangeStateTo(state);
    }

}
