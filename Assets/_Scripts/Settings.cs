using UnityEngine;
[CreateAssetMenu(fileName = "My Setting", menuName ="Create Settings/New Setting")]
public class Settings : ScriptableObject
{
    [Header("Character Settings")]
    [Tooltip("Movement Speed of Player")]
    public float speed;
    [Tooltip("Turning Speed of Player")]
    public float turningSpeed;
    [Tooltip("Jump Force, Higher Value For High Height")]
    public float jumpForce;
    [Space(10)]
    [Header("Zombie Mode Settings")]
    [Tooltip("Zombie Damage to Player")]
    public float zombieDamage;
    [Tooltip("Wait Time to Start Next Vave")]
    public float vaveWaitTime;
    [Tooltip("Wait Time to Instansiate Next Zombie")]
    public float zombieWaitTime;
}
