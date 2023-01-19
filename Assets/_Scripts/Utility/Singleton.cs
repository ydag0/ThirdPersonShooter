using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<T>();
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("Destroyed : " + this.gameObject.name);
            Destroy(this.gameObject);
        }
            
        else
            _instance = this as T;
    }
}
