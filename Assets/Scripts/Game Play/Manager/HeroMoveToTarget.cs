using UnityEngine;
using System.Collections;

public class HeroMoveToTarget : MonoBehaviour
{
    [SerializeField] HeroAttack heroAttack;
    [SerializeField] HeroUI heroUI;
    [SerializeField] HeroAnimationManager heroAnimationManager;
   // [SerializeField] private GameUIManager gameUIManager;

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackDistance = 1.2f;

    private Transform target;
    private Vector3 startPosition;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        startPosition = transform.position;
        StopAllCoroutines();
        StartCoroutine(MoveAndAttackRoutine());
    }

    private IEnumerator MoveAndAttackRoutine()
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

        // Attack
        
        heroAttack.Attack();
        // Stop
        yield return new WaitForSeconds(1f);

        // Return
        heroAnimationManager.PlayWalk();
        heroUI.RevertHeroDirection();//Change Hero Direction
        while (Vector2.Distance(transform.position, startPosition) > 0.05f)
        {
            MoveTowards(startPosition);
            yield return null;
        }


        transform.position = startPosition;
        heroAnimationManager.PlayIdle();
        heroUI.RevertHeroDirection();//Change Hero Direction


        if (!BattleManager.Instance.isPlayerTurn)
            yield return new WaitForSeconds(2f);

        BattleManager.Instance.OnAttackComplete();
    }

    private void MoveTowards(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

   
}
