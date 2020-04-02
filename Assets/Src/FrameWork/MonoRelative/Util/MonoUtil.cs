using UnityEngine;

namespace HG
{
    public static class MonoUtil
    {
        public static void Attach(this GameObject parent, GameObject child, bool keep = false)
        {
            child.transform.SetParent(parent.transform);

            if (!keep)
            {
                child.transform.localPosition = Vector3.zero;
                child.transform.localScale = Vector3.one;
                child.transform.localRotation = Quaternion.identity;    
            }
        }

        public static T GetSafeComponent<T>(this GameObject gameObject) where T : MonoBehaviour
        {
            var t = gameObject.GetComponent<T>();
            return t != null ? t : gameObject.AddComponent<T>();
        }
    }
}