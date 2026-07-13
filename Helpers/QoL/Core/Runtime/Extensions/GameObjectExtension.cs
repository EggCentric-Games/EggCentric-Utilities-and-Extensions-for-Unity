using UnityEngine;

namespace EggCentric.QoL
{
    public static class GameObjectExtension
    {
        public static bool TryGetComponentInParent<TComponent>(this Component target, out TComponent component)
        {
            Transform parent = target.transform.parent;
            component = parent ? parent.GetComponentInParent<TComponent>() : default;
            return component != null;
        }

        public static bool TryGetComponentInChildren<TComponent>(this Component target, out TComponent component)
        {
            foreach (Transform child in target.transform)
            {
                component = child.GetComponentInChildren<TComponent>();

                if (component != null)
                    return true;
            }

            component = default;
            return component != null;
        }

        public static bool TryGetComponentInHierarchy<TComponent>(this Component target, out TComponent component)
        {
            if(target.TryGetComponent(out component))
                return true;

            if (TryGetComponentInParent(target, out component))
                return true;

            if(TryGetComponentInChildren(target, out component))
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