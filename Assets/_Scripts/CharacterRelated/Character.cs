using UnityEngine;
public class Character : MonoBehaviour
{
    public Transform firePoint;
    public bool dead { get; private set; }

    protected float baseHealth = 100;
    protected float health;
    protected float jumpForce=5;
    protected Rigidbody rb;
    protected float speed = 5;
    protected float turninngSpeed = 2;
    protected Vector3 movedir = Vector3.zero;

    private float groundRayD = 0.1f;
    #region observer
    protected void OnEnable() => InputHandler.Jump += Jump;
    protected void OnDisable() => InputHandler.Jump -= Jump;
    #endregion
    #region protected methods

    protected virtual void Jump()
    {
        if (!CheckForGrounded())
            return;
        if (dead)
            return;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    protected void Rotate()
    {
        if (InputHandler.Instance.mouseX != 0 && !dead )
        {
            rb.angularVelocity = turninngSpeed * Time.deltaTime * new Vector3(0, InputHandler.Instance.mouseX, 0);
        }
        else
            rb.angularVelocity = Vector3.zero;
    }
    protected void Move()
    {
        if (InputHandler.Instance.input != Vector3.zero && !dead )
        {
            CalculateMoveDir();
            movedir = speed * Time.deltaTime * movedir.normalized;
            rb.velocity = new Vector3(movedir.x, rb.velocity.y, movedir.z);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
    protected bool CheckForGrounded()
    {
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z),
            -transform.up);
        if (Physics.Raycast(ray, out RaycastHit hit, groundRayD))
        {
            return hit.transform.CompareTag(Tags.ground);
        }
        return false;
    }
    protected void SetHealth()
    {
        health = baseHealth;
    }
    #endregion
    #region private methods
    private void CalculateMoveDir()
    {
        if (InputHandler.Instance.input == Vector3.zero)
            return;
        float x = 0, z = 0;

        if (InputHandler.Instance.input.x != 0)
            x = InputHandler.Instance.input.x > 0 ? 1 : -1;

        if (InputHandler.Instance.input.z != 0)
            z = InputHandler.Instance.input.z > 0 ? 1 : -1;
        movedir = transform.right * x + transform.forward * z;
    }
    #endregion
    #region public methods
    public float GetHealth()
    {
        return health;
    }
    public float GetBaseHealth()
    {
        return baseHealth;
    }
    public virtual void GetDamage(float damage)
    {
        health -= damage;
    }
    public void OnPlayerDeath()
    {
        dead = true;
        //disabled aim IK script
        GetComponent<RootMotion.Demos.SecondHandOnGun>().enabled = false;
        //death anim
        AnimationManager.Instance.Death();
        rb.useGravity = false;
    }
    public void ReBirthPlayer()
    {
        transform.position = Vector3.zero;
        rb.useGravity = true;
        dead = false;
        SetHealth();
        GetComponent<RootMotion.Demos.SecondHandOnGun>().enabled = true;
        AnimationManager.Instance.ReBirth();
        GetComponent<AnimationEvents>().ReloadEvent();
    }
    #endregion
}
