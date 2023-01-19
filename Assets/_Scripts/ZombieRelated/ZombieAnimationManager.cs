using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZombieAnimationManager : MonoBehaviour
{

    Animator myAnimator;
    ZombieAI ai;
    Player player;

    #region Animation Parameters
    private const string attackN = "attack";
    private const string walkN = "moving";
    private const string fallBackN = "fall";
    private const string deadN = "dead";

    int attackID;
    int walkID;
    int fallID;
    int deadID;
    #endregion
    private void OnEnable()
    {
        ZombieMode.ZombieDeath += OnDeath;
    }
    private void OnDisable()
    {

        ZombieMode.ZombieDeath -= OnDeath;
    }
    private void Awake()
    {
        attackID = Animator.StringToHash(attackN);
        walkID = Animator.StringToHash(walkN);
        fallID = Animator.StringToHash(fallBackN);
        deadID = Animator.StringToHash(deadN);
    }
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        ai = GetComponent<ZombieAI>();
        player = GameManager.Instance.player;//FindObjectOfType<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimParams();
    }
    void SetAnimParams()
    {
        var state = ai.GetCurrentState();
        //if (state == ZombieState.dead)
        //    SetBool(deadID, true);
        if(state != ZombieState.dead)
            SetBool(deadID, false);

        if (state == ZombieState.walking)
        {
            if (myAnimator.GetBool(walkID) == false)
               SetBool(walkID, true);
            if (myAnimator.GetBool(attackID) == true)
                SetBool(attackID, false);
        }
        else if(state == ZombieState.attacking)
        {
            if (myAnimator.GetBool(attackID) == false)
                SetBool(attackID, true);
            if (myAnimator.GetBool(walkID) == true)
                SetBool(walkID, false);
        }
        else
        {
            SetBool(walkID, false);
            SetBool(attackID, false);
        }

    }
    void SetBool(int id , bool value)
    {
        myAnimator.SetBool(id, value);
    }
    void OnDeath()
    {
        if (ai.GetCurrentState() != ZombieState.dead)
            return;
        SetBool(deadID, true);
        SetBool(walkID, false);
        SetBool(attackID, false);
        myAnimator.SetTrigger(fallID);
        
    }

    #region AnimationFunctions

    public void AnimAttack()
    {
        player.GetDamage(ai.zombieDamage);
    }
    public void AnimDeath() 
    {

    }

    

    #endregion
}
