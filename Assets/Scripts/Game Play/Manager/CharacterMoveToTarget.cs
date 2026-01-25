using UnityEngine;

public class CharacterMoveToTarget : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackDistance = 1.2f;

    private Transform target;
    private bool canMove;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        canMove = true;
    }

    private void Update()
    {
        if (!canMove || target == null)
            return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= attackDistance)
        {
            canMove = false;
            OnReachAttackRange();
            return;
        }

        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    private void OnReachAttackRange()
    {

        Debug.Log("OnReachAttackRange");
    }
}
