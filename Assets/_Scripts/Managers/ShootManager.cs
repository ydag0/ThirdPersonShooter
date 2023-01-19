using UnityEngine;
using System.Diagnostics;
public class ShootManager : Singleton<ShootManager>
{
    [HideInInspector]
    public uint currentAmmo;
    [HideInInspector]
    public bool reloading;

    Transform firePoint;
    Stopwatch fireTimer = new();
    GameObject tempBullet;
    Camera mainCam;

    Vector3 targetPoint;
    Vector3 screenCenter;
    Vector3 direction;
    Ray ray;
    RaycastHit hit;

    float rayRange = 100f;

    private void OnEnable()
    {
        InputHandler.Shoot += Fire;
        InputHandler.Reload +=Reload;
    }
    private void OnDisable()
    {
        InputHandler.Shoot -= Fire;
        InputHandler.Reload -= Reload;
    }
    void Start()
    {
        screenCenter = new Vector3(Screen.width * .5f, Screen.height * .5f);
        mainCam = Camera.main;
        fireTimer.Start();
        currentAmmo = GunManager.Instance.activeGun.magazineSize;
        firePoint = GameManager.Instance.player.firePoint;
    }
    public void Fire()
    {
        if (!CanShoot())
            return;

        tempBullet = ObjectPool.Instance.GetBulletFromPool();
        tempBullet.transform.position = firePoint.position;

        targetPoint = CalculateTargetPoint();
        tempBullet.SetActive(true);

        direction = (targetPoint - firePoint.position ).normalized;
        tempBullet.transform.forward = direction;

        tempBullet.GetComponent<Rigidbody>().AddForce(direction * GunManager.Instance.activeGun.shootPower, ForceMode.Impulse);

        fireTimer.Restart();
        currentAmmo--;
        UIHandler.Instance.UpdateAmmoText(currentAmmo);
    }
    Vector3 CalculateTargetPoint()
    {
        ray = mainCam.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray,out hit, rayRange))
            return hit.point;
        else
            return ray.GetPoint(rayRange - 1);
    }
    public Vector3 CalculateTargetPoint(float range)
    {
        ray = mainCam.ScreenPointToRay(screenCenter);
        return ray.GetPoint(range);
    }
    bool CanShoot()
    {
        if (fireTimer.Elapsed.TotalSeconds < GunManager.Instance.activeGun.coolDown || reloading)
            return false; // wait for the cooldown or wait for reload
        if (currentAmmo == 0)
        {
            if (!reloading)
            {
                InputHandler.Instance.InvokeReload();
            }
            return false;
        }
        return true;
    }
    public bool CanReload()
    {
        //if not already reloading or ammo is not full
        bool canReload = reloading == false && GunManager.Instance.activeGun.magazineSize != currentAmmo;
        return canReload;
    }

    void Reload()=>
        reloading = true;
}
