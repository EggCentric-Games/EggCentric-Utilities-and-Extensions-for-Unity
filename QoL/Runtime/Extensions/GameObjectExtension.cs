using UnityEngine;

namespace EggCentric.QoL
{
    public static class GameObjectExtension
    {
        public static bool TryGetComponentInParent<TComponent>(this Component target, out TComponent component)
        {
            target.TryGetComponent(out component);

            if (component != null && !ReferenceEquals(component, target))
                return true;

            if (target.transform.parent == null)
                return false;

            component = target.transform.parent.GetComponentInParent<TComponent>();

            if (component != null && !ReferenceEquals(component, target))
                return true;

            return false;
        }

        public static TComponent GetOrCreate<TComponent>(this GameObject obj) where TComponent : Component
        {
            obj.TryGetComponent(out TComponent comp);
            if (!comp)
                obj.AddComponent<TComponent>();

            return comp;
        }

        public static void AdaptiveDestroy(this Object obj)
        {
            if (Application.isPlaying)
                Object.Destroy(obj);
            else
                Object.DestroyImmediate(obj);
        }

        public static TComponent CreateChildComponent<TComponent>(this Transform parent, string name) where TComponent : Component
        {
            TComponent component = new GameObject(name, typeof(TComponent)).GetComponent<TComponent>();
            component.transform.SetParent(parent, false);

            return component;
        }
    }
}