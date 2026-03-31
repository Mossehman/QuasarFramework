using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected virtual bool Disposable => false; // Boolean to check if the singleton should be replaced by any new instances or not
    protected virtual bool SceneBound => false; // Boolean to check if the singleton should be DontDestroyOnLoad

    private static T instance; // The static instance of our Monobehaviour singleton
    private static bool toDestroy; // Flag to destroy the singleton, prevents any null references if we are quitting
    
    public static T Instance
    {
        get
        {
            if (toDestroy)
            {
                Debug.LogWarning($"MonoSingleton<{typeof(T)}>: Instance has already been destroyed, returning null reference by default...");
                return null;
            }

            if (!instance)
            {
                instance = FindAnyObjectByType<T>();
                if (!instance)
                {
                    instance = new GameObject(typeof(T).Name + " Singleton").AddComponent<T>();
                }
            }

            return instance;
        }
    }
    
    protected virtual void Awake()
    {
        if (!instance)
        {
            instance = this as T;
            if (!SceneBound) DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            if (!Disposable)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                DestroyImmediate(instance.gameObject);
                instance = this as T;
            }
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            toDestroy = true;
            instance = null;
        }
    }

    protected virtual void OnApplicationQuit()
    {
        toDestroy = true;
    }
}
