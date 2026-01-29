using UnityEngine;
using System.Collections;

public class HeroMoveToTarget : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] Hero hero;
    [SerializeField] HeroUI heroUI;
    [SerializeField] HeroAnimationManager heroAnimationManager;

    [Header("Move and Attack")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackDistance = 1.2f;

    private Transform target;//Attack target
    private Vector3 startPosition;
    private Coroutine moveCoroutine;


    //Setting the target, which is the enemy being attacked, and then attacking towards it
    public void SetTargetAndAttack(Transform newTarget)
    {
        target = newTarget;
        startPosition = transform.position;

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveAndHitRoutine());
    }


    //The hero moves towards the target, hits it after reaching it, and returns to her position.
    private IEnumerator MoveAndHitRoutine()
    {

        // Move to target
        heroAnimationManager.PlayWalk();
        while (Vector2.Distance(transform.position, target.position) > attackDistance)
        {
            MoveTowards(target.position);
            yield return null;
        }

        // Stop
        heroAnimationManager.PlayAttack();
        yield return new WaitForSeconds(0.25f);

        // Hit to enemy
        hero.DealDamage();

        // Stop
        yield return new WaitForSeconds(1f);

        // Return to start position
        heroAnimationManager.PlayWalk();
        heroUI.RevertHeroDirection();//Change Hero Direction (Graphics)
        while (Vector2.Distance(transform.position, startPosition) > 0.05f)
        {
            MoveTowards(startPosition);
            yield return null;
        }
        transform.position = startPosition;

        heroAnimationManager.PlayIdle();
        heroUI.RevertHeroDirection();//Change Hero Direction (Graphics)


        if (!BattleManager.Instance.isPlayerTurn)//Delay to simulate enemy thinking about attacking
            yield return new WaitForSeconds(2f);

        BattleManager.Instance.OnAttackComplete();
    }

    private void MoveTowards(Vector3 destination)
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
    }


}
