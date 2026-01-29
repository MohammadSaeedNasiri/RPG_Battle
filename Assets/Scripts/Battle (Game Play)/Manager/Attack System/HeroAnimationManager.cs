using System;
using UnityEngine;

//Hero states for animation
[Serializable]
public enum HeroAnimState
{
    Idle = 0,
    Walk = 1,
    Attack = 2,
    Die = 3
}

//This code manages the hero animations in the game environment.
[RequireComponent(typeof(Animator))]
public class HeroAnimationManager : MonoBehaviour
{
   

    private Animator animator;
    private HeroAnimState currentState;
    private bool isDead = false;

    private static readonly int StateHash = Animator.StringToHash("State");//Variable in Animator

    void Awake()
    {
        animator = GetComponent<Animator>();
        PlayIdle();//play idle anim on start
        currentState = HeroAnimState.Idle;
    }


    #region public functions for switch hero animation

    //Play Idle anim
    public void PlayIdle()
    {
        if (isDead) return;
        SetState(HeroAnimState.Idle);
    }

    //Play walking anim
    public void PlayWalk()
    {
        if (isDead) return;
        SetState(HeroAnimState.Walk);
    }

    //Play attack anim
    public void PlayAttack()
    {
        if (isDead) return;
        SetState(HeroAnimState.Attack);
    }


    //Play death of a hero anim
    public void PlayDie()
    {
        if (isDead) return;

        isDead = true;
        SetState(HeroAnimState.Die);
    }
    #endregion




    private void SetState(HeroAnimState newState)//Set hero new state anim in animator
    {
        if (currentState == newState) return;

        currentState = newState;
        animator.SetInteger(StateHash, (int)newState);
    }



}
