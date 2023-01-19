using UnityEngine;
using ch.sycoforge.Decal;
public class Decal : MonoBehaviour
{
    EasyDecal myScript=null;
    private void Start()
    {
        if (myScript == null)
            myScript = GetComponent<EasyDecal>();
    }
    private void LateUpdate()
    {
        if (myScript.Alpha < 0.05f)
        {
            DecalManager.Instance.OnFadedOut(myScript);
        }
    }

}
