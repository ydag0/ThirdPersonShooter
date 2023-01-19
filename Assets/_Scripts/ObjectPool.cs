using System.Collections.Generic;
using UnityEngine;
using ch.sycoforge.Decal;
public class ObjectPool : Singleton<ObjectPool>
{
    [Tooltip("Bullet Object That On the Pool")]
    public GameObject bullet;
    [Tooltip("Decal Object That On the Pool")]
    public GameObject decal;

    [Tooltip("Pool Count")]
    [SerializeField] int poolCount;
    [SerializeField] Transform ammoPoolParentTransform;
    [SerializeField] Transform decalPoolParentTransform;
    Queue<GameObject> ammoPool = new Queue<GameObject>();
    Queue<GameObject> decalPool = new Queue<GameObject>();
    Vector3 decalScale;
    void Start()
    {
        decalScale = decal.transform.localScale;
        CreateAmmoPool();
        CreateDecalPool();
    }

    void CreateAmmoPool()
    {
        for(int i = 0; i < poolCount; i++)
        {
            GameObject temp = Instantiate(bullet);
            ammoPool.Enqueue(temp);
            if (ammoPoolParentTransform != null)
                temp.transform.SetParent(ammoPoolParentTransform);
            temp.SetActive(false);
        }
    }
    void CreateDecalPool()
    {
        for (int i = 0; i < poolCount; i++)
        {
            GameObject temp = Instantiate(decal);
            decalPool.Enqueue(temp);
            if (decalPoolParentTransform != null)
                temp.transform.SetParent(decalPoolParentTransform);
            temp.SetActive(false);
        }
    }
    public GameObject GetBulletFromPool()
    {
        if (ammoPool.Count > 0)
            return ammoPool.Dequeue();
        else
            return Instantiate(bullet);
    }
    public GameObject GetDecalFromPool()
    {
        if (decalPool.Count > 0)
            return decalPool.Dequeue();
        else
            return Instantiate(decal);            
    }
    public void ReturnBulletToPool(GameObject obj)
    {
        obj.SetActive(false);
        ammoPool.Enqueue(obj);
    }
    public void ReturnDecalToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.localScale = decalScale;
        obj.GetComponent<EasyDecal>().Alpha = 1;
        if (decalPoolParentTransform != null)
            obj.transform.SetParent(decalPoolParentTransform);
        decalPool.Enqueue(obj);
    }
}
