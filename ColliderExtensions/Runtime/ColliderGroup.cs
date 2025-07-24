using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColliderGroup
{
    public IReadOnlyCollection<Collider2D> Colliders => colliders;

    [SerializeField] private Collider2D[] colliders;

    public ColliderGroup(Collider2D[] colliders)
    {
        this.colliders = colliders;
    }

    public ColliderGroup(Component comp)
    {
        this.colliders = comp.GetComponentsInChildren<Collider2D>();
    }

    public void IgnoreCollision(ColliderGroup excludedGroup)
    {
        foreach (Collider2D collider in excludedGroup.colliders)
        {
            IgnoreCollision(collider);
        }
    }

    public void IgnoreCollision(Collider2D excludedCollider)
    {
        foreach (Collider2D collider in colliders)
        {
            Physics2D.IgnoreCollision(collider, excludedCollider);
        }
    }
}