using UnityEngine;
public abstract class Zombie : MonoBehaviour
{
    private float baseHealth=100;
    protected ZombieState state;
    protected float health = 100;
    protected float givenDamage = 10;
    public float GetHealth()
    {
        return health;
    }
    public virtual void GetDemage(float demage)
    {
        health -= demage;
    }
    public virtual ZombieState GetCurrentState()
    {
        return state;
    }
    public virtual void SetHealth()
    {
        health = baseHealth;
    }
    public float GetBaseHealth()
    {
        return baseHealth;
    }
}
public enum ZombieState
{
    walking,
    attacking,
    dead
}
