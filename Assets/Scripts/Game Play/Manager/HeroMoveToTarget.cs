using UnityEngine;
using System.Collections;

public class HeroMoveToTarget : MonoBehaviour
{
    [SerializeField] HeroAttack heroAttack;
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

        // 1️⃣ حرکت به سمت هدف
        while (Vector2.Distance(transform.position, target.position) > attackDistance)
        {
            MoveTowards(target.position);
            yield return null;
        }

        // 2️⃣ رسیدن → صبر 0.1 ثانیه
        yield return new WaitForSeconds(0.1f);

        // 3️⃣ ضربه
        
        heroAttack.Attack();
        // 4️⃣ صبر 1 ثانیه بعد از ضربه
        yield return new WaitForSeconds(1f);

        // 5️⃣ برگشت به مبدا
        spriteRenderer.flipX = !spriteRenderer.flipX;
        while (Vector2.Distance(transform.position, startPosition) > 0.05f)
        {
            MoveTowards(startPosition);
            yield return null;
        }

        transform.position = startPosition;
        spriteRenderer.flipX = !spriteRenderer.flipX; // بازگرداندن Sprite

        if(!BattleManager.Instance.isPlayerTurn)
            yield return new WaitForSeconds(2f);

        BattleManager.Instance.OnAttackComplete();
    }

    private void MoveTowards(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

   
}
