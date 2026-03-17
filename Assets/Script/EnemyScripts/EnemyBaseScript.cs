
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemyBaseScript : MonoBehaviour
{
    //public GameObject Target;
    public EnemyData Data;
    public float Health;
    public float Defense;
    public float MoveSpeed;
    public float DistanceFromPlayer;
    public float Damage;
    public float MaxExp;
    public float MinExp;
    
    private NavMeshAgent _navMeshAgent;
    private SpriteRenderer _spriteRenderer;
    private GameObject _player;
    private Vector3 _playerPos;
    private PlayerHealth _playerHealth;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindPlayer();
        _playerHealth = _player.GetComponent<PlayerHealth>();
        UpdatePlayerPosition();
        //Debug.Log(_player);
        //Из скриптабле обжект закидываем инфу о противнике
        _spriteRenderer = GetComponent<SpriteRenderer>(); 
        _spriteRenderer.sprite = Data.Sprite;
        
        Health = Data.Health;
        Defense = Data.Defense;
        MoveSpeed = Data.MoveSpeed;
        DistanceFromPlayer = Data.DistanceFromPlayer;
        Damage = Data.Damage;
        MaxExp = Data.MaxExp;
        MinExp = Data.MinExp;
        
        //Настраиваем NavMeshAgent
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _navMeshAgent.speed = MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        UpdatePlayerPosition();
        if ((_playerPos - gameObject.transform.position).magnitude > DistanceFromPlayer)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(_playerPos);
        }
        else
        {
            _navMeshAgent.isStopped = true;
            Attack();
        }
    }

    private GameObject FindPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }

    private void UpdatePlayerPosition()
    {
        _playerPos = _player.transform.position;
    } 

    protected virtual void Attack()
    {
        Debug.Log("Base Attack");
    }

    private void AttackAnimation()
    {
        //TODO: Запуск анимации атаки
    }

    public void GetDamage(float damage)
    {
        Health -= damage * Defense;
        if (Health <= 0)
        {
            Die();
        }
        
    }

    public void Die()
    {
        //TODO: Сделать спавн опыта
        Destroy(this.gameObject);
    }
    
    
    
}
