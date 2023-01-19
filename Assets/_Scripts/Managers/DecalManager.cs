using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ch.sycoforge.Decal;

public class DecalManager : Singleton<DecalManager>
{
    [SerializeField] float fadeOutSpeed;
    [Tooltip("Fadeout Will Start After the Delay")]
    [SerializeField] float fadeoutDelay;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnDecal(GameObject decalParent,Vector3 position,Vector3 normal)
    {
        GameObject decal = ObjectPool.Instance.GetDecalFromPool();
        decal.SetActive(true);
        EasyDecal.ProjectAt(decal, decalParent, position, normal: normal, false);
        
    }

    public void OnFadedOut(EasyDecal decal)
    {
        //Return Decal To Pool
        ObjectPool.Instance.ReturnDecalToPool(decal.gameObject);
    }
}
