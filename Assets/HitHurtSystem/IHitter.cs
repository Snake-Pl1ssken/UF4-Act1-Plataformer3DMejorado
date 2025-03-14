using UnityEngine;

public interface IHitter 
{
    public float GetDamage();  //devolver el daño que hace el hitter

    public Transform GetTransform(); //tiene que devolver la transform del agresor
}
