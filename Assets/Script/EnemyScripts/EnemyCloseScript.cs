using UnityEngine;

public class EnemyCloseScript : EnemyBaseScript
{
    protected override void Attack()
    {
        _playerHealth.TakeDamage(Damage);
        //Debug.Log("Close Attack");
    }
}
