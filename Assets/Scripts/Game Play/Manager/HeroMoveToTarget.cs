using UnityEngine;
using System.Collections;

public class HeroMoveToTarget : MonoBehaviour
{
    [SerializeField] HeroAttack heroAttack;
    [SerializeField] HeroUI heroUI;
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
        while (Vector2.Distance(transform.position, target.position) > attackDistance)
        {
            MoveTowards(target.position);
            yield return null;
        }

        // Stop
        yield return new WaitForSeconds(0.1f);

        // Attack
        
        heroAttack.Attack();
        // Stop
        yield return new WaitForSeconds(1f);

        // Return

        heroUI.RevertHeroDirection();//Change Hero Direction
        while (Vector2.Distance(transform.position, startPosition) > 0.05f)
        {
            MoveTowards(startPosition);
            yield return null;
        }

        transform.position = startPosition;

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
