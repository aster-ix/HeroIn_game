using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseScript : MonoBehaviour
{
    public GameObject Target;
    public EnemyData Data;
    public float Health;
    public float Defense;
    public float MoveSpeed;
    public float Damage;
    private NavMeshAgent _navMeshAgent;
    private SpriteRenderer _spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health = Data.Health;
        Defense = Data.Defense;
        MoveSpeed = Data.MoveSpeed;
        Damage = Data.Damage;
        
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        
        _navMeshAgent.SetDestination(Target.transform.position);
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = Data.Sprite;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(float damage)
    {
        Health -= damage * Defense;
    }
}
