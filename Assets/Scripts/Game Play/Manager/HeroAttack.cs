using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    [SerializeField] Hero hero;

    public void Attack()
    {
        hero.DealDamage();
        Debug.Log("Attack executed!");
    }
}
