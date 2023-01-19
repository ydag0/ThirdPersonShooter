using UnityEngine;
public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject characterPrefab;
    public Settings gameSettings;
    public bool started { get; private set; } = false;
    public Player player { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        AnimationManager.Instance.enabled = false;
        GetComponentInChildren<HealthBarManager>().enabled = false;
        InputHandler.Instance.enabled = false;
        ShootManager.Instance.enabled = false;
        ZombieMode.Instance.enabled = false;
    }
    public void StartGame(bool instantiate=true)
    {
        started = true;
        if (instantiate)
            InstantiatePlayer();
    }
    void InstantiatePlayer()
    {
        player = Instantiate(characterPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        EnableScripts();
    }
    public void LockCursor()=>Cursor.lockState = CursorLockMode.Locked;
    public void UnLockCursor() => Cursor.lockState = CursorLockMode.None;
    void EnableScripts()
    {
        AnimationManager.Instance.enabled = true;
        GetComponentInChildren<HealthBarManager>().enabled = true;
        ShootManager.Instance.enabled = true;
        InputHandler.Instance.enabled = true;
        ZombieMode.Instance.enabled = true;
        LockCursor();
    }
}
