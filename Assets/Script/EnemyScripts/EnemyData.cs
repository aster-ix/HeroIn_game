using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy")]
public class EnemyData : ScriptableObject
{
    public string EnemyName;

    public float Health;
    public float Defense;
    public float MoveSpeed;
    public float DistanceFromPlayer;
    public float Damage;
    public float MaxExp;
    public float MinExp;

    public Sprite Sprite;
    public float ChaseDistance;
}