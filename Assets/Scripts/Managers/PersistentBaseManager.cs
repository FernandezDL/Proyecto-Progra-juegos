using UnityEngine;

namespace Managers
{
    public abstract class PersistentBaseManager<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Try to find existing instance in the scene
                    var objs = FindObjectsByType<T>(FindObjectsSortMode.None);
                    if (objs.Length > 0)
                    {
                        _instance = objs[0];
                    }

                    if (objs.Length > 1)
                    {
                        Debug.LogError($"[Singleton] More than one instance of {typeof(T)} found!");
                    }

                    if (_instance == null)
                    {
                        GameObject obj = new GameObject(typeof(T).Name);
                        _instance = obj.AddComponent<T>();
                        // Set to not destroy on load
                        DontDestroyOnLoad(obj);
                    }
                    else
                    {
                        // If instance was found in scene, make sure it's not destroyed
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            // Make sure we only have one instance
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject); // destroy duplicate
            }
        }
    }
}
