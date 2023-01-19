using System.Collections.Generic;
using UnityEngine;
public class GunManager : Singleton<GunManager>
{
    public List<Gun> guns = new List<Gun>();
    //[HideInInspector]
    public Gun activeGun { get; private set;}

    private void OnEnable()=>
        InputHandler.PlayerGunChanged += ChangeActiveGun;
    private void OnDisable()=>
        InputHandler.PlayerGunChanged -= ChangeActiveGun;

    protected override void Awake()
    {
        base.Awake();
        try
        {
            activeGun = guns[0];
        }
        catch
        {
            Debug.LogError("There is No Gun in The List");
        }
    }
    void ChangeActiveGun(Gun.GunNames gunName)
    {
        if (activeGun.gunName == gunName)
            return; //if same gun is active then do nothing

        guns.ForEach(x => activeGun = x.gunName == gunName ? x : activeGun);
        //print("Active Gun is " + "<color=magenta><b>" + activeGun.gunName +"</b></color>");
    }
    public void ReloadAmmo()
    {
        ShootManager.Instance.currentAmmo = activeGun.magazineSize;
        UIHandler.Instance.UpdateAmmoText(activeGun.magazineSize);
    }
}


