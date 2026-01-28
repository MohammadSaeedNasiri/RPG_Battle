using System;
using UnityEngine;

[Serializable]
public enum HeroAnimState
{
    Idle = 0,
    Walk = 1,
    Attack = 2,
    Die = 3
}
[RequireComponent(typeof(Animator))]
public class HeroAnimationManager : MonoBehaviour
{
   

    private Animator animator;
    private HeroAnimState currentState;


    private const string STATE_KEY = "State";

    void Awake()
    {
        animator = GetComponent<Animator>();
        PlayIdle();
    }


    public void PlayIdle()
    {
        SetState(HeroAnimState.Idle);
    }

    public void PlayWalk()
    {
        SetState(HeroAnimState.Walk);
    }

    public void PlayAttack()
    {
        SetState(HeroAnimState.Attack);
    }


    public void PlayDie()
    {
        SetState(HeroAnimState.Die);
    }



  

    private void SetState(HeroAnimState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        animator.SetInteger(STATE_KEY, (int)newState);
    }



}
