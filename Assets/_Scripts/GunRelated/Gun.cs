[System.Serializable]
public struct Gun
{
    public enum GunNames
    {
        //Add your gun name here
        Pistol,
        AssualtRifle,
        BigGun
    }
    public GunNames gunName;
    public uint magazineSize;
    [UnityEngine.Tooltip("Bullet Speed")]
    public float shootPower;
    public float damage;
    [UnityEngine.Tooltip("Time to Wait Fire Again (Seconds)")]
    public float coolDown;
    [UnityEngine.Tooltip("Bullet Force that Will Apply When Hit the Enemy")]
    public float hitForce;
    public Gun(uint magazineSize,float shootPower,float damage,float coolDown,float hitForce,GunNames gunName) => 
        (this.magazineSize, this.shootPower,this.damage ,this.coolDown, this.hitForce,this.gunName) =
        (magazineSize, shootPower, damage, coolDown, hitForce ,gunName);
}

