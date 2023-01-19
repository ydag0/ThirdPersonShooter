using UnityEngine;
public class AnimationEvents : MonoBehaviour
{
    public void ReloadEvent()
    {
        GunManager.Instance.ReloadAmmo();
        ShootManager.Instance.reloading = false;
    }
}
