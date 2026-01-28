using UnityEngine;

public class ColliderGroupComponent : MonoBehaviour
{
    [SerializeField] private ColliderGroup colliderGroup;

    private void OnValidate()
    {
        if(colliderGroup == null)
            colliderGroup = new ColliderGroup(transform);
    }
}
