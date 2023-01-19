using UnityEngine;

public class AnimationManager : Singleton<AnimationManager>
{
    Animator animator;
    #region constructor
    AnimationManager()
    {
        runForwardID = Animator.StringToHash(runForward);
        runRightID = Animator.StringToHash(runRight);
        runLeftID = Animator.StringToHash(runLeft);
        runBackID = Animator.StringToHash(runBack);
        reloadID = Animator.StringToHash(reload);
        fireID = Animator.StringToHash(fire);
        jumpID = Animator.StringToHash(jump);
        
    }
    #endregion

    #region Animation Parameters
    const string runForward = "runForward";
    const string runRight = "runRight";
    const string runLeft = "runLeft";
    const string runBack = "runBack";
    const string reload = "reload";
    const string jump = "jump";
    const string fire = "fire";
    const string dead = "dead";

    int runForwardID;
    int runRightID;
    int runLeftID;
    int runBackID;

    int reloadID;
    int fireID;
    int jumpID;
    #endregion

    #region observer
    private void OnEnable()
    {
        InputHandler.Shoot += SetFire;
        InputHandler.Jump += SetJump;
        InputHandler.Reload += SetReload;
    }
    private void OnDisable()
    {
        InputHandler.Shoot -= SetFire;
        InputHandler.Jump -= SetJump;
        InputHandler.Reload -= SetReload;
    }
    #endregion
    private void Start()
    {
        if (animator == null)
            animator = GameManager.Instance.player.GetComponent<Animator>();
    }
    void Update()
    {
        SetRunning();
    }
    void SetRunning()
    {
        Vector3 input = InputHandler.Instance.input.normalized;

        if (input == Vector3.right)
            animator.SetBool(runRightID, true);
        else
            animator.SetBool(runRightID, false);
        if (input == Vector3.left)
            animator.SetBool(runLeftID, true);
        else
            animator.SetBool(runLeftID, false);
        if (input == Vector3.back)
            animator.SetBool(runBackID, true);
        else
            animator.SetBool(runBackID, false);

        if (input == Vector3.zero || animator.GetBool(runRightID) || animator.GetBool(runBackID) || animator.GetBool(runLeftID))
            animator.SetBool(runForwardID, false);
        else
            animator.SetBool(runForwardID, true);

    }
    void SetFire() => animator.SetTrigger(fireID);
    void SetJump() => animator.SetTrigger(jumpID);
    void SetReload() => animator.SetTrigger(reloadID);
    public void Death()
    {
        animator.SetLayerWeight(1, 0);
        animator.SetLayerWeight(2, 0);
        animator.SetBool("dead", true);
    }
    public void ReBirth()
    {
        animator.SetBool("dead", false);
        animator.SetLayerWeight(1, 1);
        animator.SetLayerWeight(2, 1);
    }

}
