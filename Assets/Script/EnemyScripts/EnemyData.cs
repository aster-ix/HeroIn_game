using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy")]
public class EnemyData : ScriptableObject
{
    public string EnemyName;

    public float Health;
    public float Defense;
    public float MoveSpeed;
    public float Damage;

    public Sprite Sprite;
    public float ChaseDistance;
}