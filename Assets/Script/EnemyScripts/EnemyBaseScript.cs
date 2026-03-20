
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemyBaseScript : MonoBehaviour
{
    //public GameObject Target;
    public EnemyData Data;
    public GameObject ExpPrefab;
    public float Health;
    public float Defense;
    public float MoveSpeed;
    public float DistanceFromPlayer;
    public float Damage;
    public float MaxExp;
    public float MinExp;
    public float AttackCooldown = 1;

    protected bool _isAttacking = false;
    protected bool _playerInRange = false;
    
    protected NavMeshAgent _navMeshAgent;
    protected SpriteRenderer _spriteRenderer;
    protected GameObject _player;
    protected Vector3 _playerPos;
    protected PlayerHealth _playerHealth;
    protected Coroutine _atackingPlayer;
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>(); 
        _navMeshAgent = GetComponent<NavMeshAgent>();
        
        
        _player = FindPlayer();
        _playerHealth = _player.GetComponent<PlayerHealth>();
        UpdatePlayerPosition();

        
        
        //Из скриптабле обжект закидываем инфу о противнике
        
        
        _spriteRenderer.sprite = Data.Sprite;
        Health = Data.Health;
        Defense = Data.Defense;
        MoveSpeed = Data.MoveSpeed;
        DistanceFromPlayer = Data.DistanceFromPlayer;
        Damage = Data.Damage;
        MaxExp = Data.MaxExp;
        MinExp = Data.MinExp;
        
        //Настраиваем NavMeshAgent
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _navMeshAgent.speed = MoveSpeed;
        _navMeshAgent.stoppingDistance = DistanceFromPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        UpdatePlayerPosition();
        _navMeshAgent.SetDestination(_playerPos);
        if (_navMeshAgent.remainingDistance > DistanceFromPlayer)
        {
            _playerInRange = false;
        }
        else  if(!_isAttacking)
        {
            _playerInRange = true;
            StartCoroutine(DamagePlayer());
            
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
        GameObject Exp = Instantiate(ExpPrefab, transform.position, transform.rotation);
        Exp.GetComponent<ExpScript>().Exp = Random.Range(MinExp, MaxExp);
        Destroy(this.gameObject);
    }

    private IEnumerator DamagePlayer()
    {
        _isAttacking = true;
        while (_playerInRange && _playerHealth != null)
        {
            Attack();
           // Debug.Log("Base Damage");
            yield return new WaitForSeconds(AttackCooldown);
        }
        _isAttacking = false;
    }
}
