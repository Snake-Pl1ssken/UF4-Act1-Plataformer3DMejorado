using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Sight : MonoBehaviour
{
    [SerializeField] float range = 15f;
    [SerializeField] float width = 10f;
    [SerializeField] float height = 7f;

    [SerializeField] LayerMask visibleLayerMask = Physics.DefaultRaycastLayers;
    [SerializeField] LayerMask occludingLayerMask = Physics.DefaultRaycastLayers;
    public List<ITargeteable> targeteables = new();
    private void Update()
    {
        Collider[] colliders = Physics.OverlapBox(
            transform.position + (transform.forward * range / 2f), 
            (Vector3.forward * (range / 2f)) + 
            (Vector3.right * (width / 2f)) +
            (Vector3.up * (height / 2f)),
            transform.rotation,
            visibleLayerMask);

        targeteables.Clear();

        foreach (Collider c in colliders)
        {
            bool hasLineOfSight = false;
            ITargeteable targeteable = c.GetComponent<ITargeteable>();
            if (targeteable != null)
            {
                if (Physics.Raycast(transform.position, c.transform.position, out RaycastHit hit, range, occludingLayerMask))
                {
                    hasLineOfSight = hit.collider == c;
                }
                if (hasLineOfSight) { targeteables.Add(targeteable); }
            }
        }

        //todo:ordenar por distancia

        
    }

    public ITargeteable GetClosestTarget()
    { 
        return (targeteables.Count > 0) ? targeteables[0] : null;
    }
}
