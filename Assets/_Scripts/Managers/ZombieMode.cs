using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ZombieMode : Singleton<ZombieMode>
{
    public static event Action Wave;
    public static event Action ZombieDeath;
    public static event Action<ZombieAI> ZombieCreated;

    [SerializeField] GameObject zombiePrefab;
    [SerializeField] Transform zombieParent;
    [SerializeField] float vaveWaitTime = 3.0f;
    [SerializeField] float zombieWaitTime = 1.5f;
    [SerializeField] float zombieHealth;
    [SerializeField] List<Vector3> zombieSpawnPos = new();

    public int zombieCount { get; private set; } = 1;

    private List<ZombieAI> zombies = new();
    private Player player;

    private Vector3 position = Vector3.zero;
    private int wave;
    private int zombieCountMultiplier = 1;
    private int aliveZombies;
    private void OnEnable()
    {
        Wave += NewWave;
        Player.PlayerDeath += PlayerDeath;
        ZombieDeath += () => aliveZombies--;
    }

    private void OnDisable()
    {
        Wave -= NewWave;
        Player.PlayerDeath -= PlayerDeath;
        ZombieDeath -= () => aliveZombies--;
    }
    void Start()
    {
        if (!zombieParent.CompareTag(Tags.enemy))
            zombieParent.tag = Tags.enemy;

        player = GameManager.Instance.player;
        vaveWaitTime = GameManager.Instance.gameSettings.vaveWaitTime;
        zombieWaitTime = GameManager.Instance.gameSettings.zombieWaitTime;
    }

    void HandleZombies()
    {
        while(zombies.Count < zombieCount)
        {
            var ai = Instantiate(zombiePrefab, position, Quaternion.identity, zombieParent).GetComponent<ZombieAI>();
            zombies.Add(ai);
            ZombieCreated?.Invoke(ai);
        }
        StartCoroutine(ActivateZombies());
    }
    IEnumerator ActivateZombies()
    {
        int activatedZombies = 0;
        while(activatedZombies < zombieCount)
        {
            zombies[activatedZombies].SetHealth();
            var z = zombies[activatedZombies].gameObject;
            z.transform.position = zombieSpawnPos.GetRandomItem();
            z.SetActive(true);
            activatedZombies++;
            yield return new WaitForSeconds(zombieWaitTime);
        }
    }
    private void Update()
    {
        if (aliveZombies == 0 && GameManager.Instance.started)
        {
            aliveZombies = -1;
            InvokeWave();
        }
    }
    void PlayerDeath()
    {
        UIHandler.Instance.PlayerDeath();
        GameManager.Instance.UnLockCursor();
    }
    public void RePlay()
    {
        player.ReBirthPlayer();
        zombies.ForEach(x => x.gameObject.SetActive(false));
        wave = 0;
        aliveZombies = 0;
        GunManager.Instance.ReloadAmmo();
    }
    void NewWave()
    {
        wave++;
        SetZombieCount();
        StartCoroutine(StartVave());
        UIHandler.Instance.StartNewWaveText(vaveWaitTime);
    }
    IEnumerator StartVave()
    {
        UIHandler.Instance.UpdateWaveText(wave);
        yield return new WaitForSeconds(vaveWaitTime);
        HandleZombies();
    }
    void SetZombieCount()
    {
        zombieCount = wave * zombieCountMultiplier;
        aliveZombies = zombieCount;
    }
    public void InvokeWave()
    {
        Wave?.Invoke();
    }
    public void InvokeDeath()
    {
        ZombieDeath?.Invoke();
    }
}