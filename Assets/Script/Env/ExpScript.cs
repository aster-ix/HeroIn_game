using System;
using UnityEngine;

public class ExpScript : MonoBehaviour
{
    public float Exp = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        if (other.TryGetComponent(out LevelManager _player))
        {
            _player.AddXP(Exp);
            Destroy(this.gameObject);
        }
        
    }
}
