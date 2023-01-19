using UnityEngine;
using System;
public class InputHandler : Singleton<InputHandler>
{
    public static event Action Shoot;
    public static event Action Jump;
    public static event Action Reload;
    public static event Action<Gun.GunNames> PlayerGunChanged;

    [SerializeField] float mouseSensivity = .15f;

    public Vector3 input { get; private set; }
    public float mouseX { get; private set; }

    void Update()
    {
        input = new Vector3(Input.GetAxisRaw(InputNames.horizontal),0,  Input.GetAxisRaw(InputNames.vertical));
        mouseX = Input.GetAxisRaw(InputNames.mouseX);
        setX();
        //mouseY= Input.GetAxisRaw(InputNames.mouseY);

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            InvokeShoot();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeJump();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            InvokeReload();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InvokeGunChanged(Gun.GunNames.Pistol);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InvokeGunChanged(Gun.GunNames.AssualtRifle);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InvokeGunChanged(Gun.GunNames.BigGun);
        }
    }
    void setX()
    {
        if (mouseX > mouseSensivity)
            mouseX = 1;
        else if (mouseX < -mouseSensivity)
            mouseX = -1;
        else
            mouseX = 0;
    }
    public void InvokeShoot()
    {
        Shoot?.Invoke();
    }
    public void InvokeJump()
    {
        if(GameManager.Instance.player.CanJump())
            Jump?.Invoke();
    }
    public void InvokeReload()
    {
        //if ammo is not full
        if(ShootManager.Instance.CanReload())
            Reload?.Invoke();
    }
    public void InvokeGunChanged(Gun.GunNames gunName)
    {
        PlayerGunChanged?.Invoke(gunName);
    }
}
public static class InputNames
{
    public static string mouseX = "Mouse X";
    public static string mouseY = "Mouse Y";
    public static string vertical = "Vertical";
    public static string horizontal = "Horizontal";
}
public static class Tags
{
    public static string player = "Player";
    public static string enemy = "Enemy";
    public static string ground = "Ground";
    
}
