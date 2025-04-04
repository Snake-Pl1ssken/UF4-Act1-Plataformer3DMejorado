using UnityEngine;

public abstract class DecisionTreeNode : MonoBehaviour
{
    protected Enemy enemy;

    public void SetEnemy(Enemy enemy) { this.enemy = enemy; }
    public abstract void Execute();
}
