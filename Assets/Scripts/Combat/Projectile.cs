using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Health _target;
    [SerializeField] private float speed;
    [SerializeField] private bool _homing;
    [SerializeField] private GameObject hitEffect;
    private float _damage;

    void Update()
    {
        if (_target != null)
        {
            if (_homing && !_target.IsDead)
            {
                transform.LookAt(GetTargetPos());
            }
            
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
           
        }
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void Shoot (Health target)
    {
        _target = target;
        transform.LookAt(GetTargetPos());
    }
    
    private Vector3 GetTargetPos ()
    {
        CapsuleCollider targetCollider = _target.GetComponent<CapsuleCollider>();
        return _target.transform.position + Vector3.up * targetCollider.height / 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_target.IsDead) return;
        
        if (other != null && other.gameObject.name == _target.transform.name)
        {
            _target.TakeDamage(_damage);
            if(hitEffect != null) Instantiate(hitEffect, transform.position, Quaternion.identity) ;
            Destroy(gameObject);
        }
    }
}
