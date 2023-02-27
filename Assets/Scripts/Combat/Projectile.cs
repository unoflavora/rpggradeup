using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Health _target;
    [SerializeField] private float speed;

    private float _damage;
    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            if (Vector3.Distance(transform.position, _target.transform.position) > .5f)
            {
                transform.LookAt(GetTargetPos());
                transform.Translate(speed * Time.deltaTime * Vector3.forward);
            }
        }
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void Shoot (Health target)
    {
        _target = target;
    }
    
    private Vector3 GetTargetPos ()
    {
        CapsuleCollider targetCollider = _target.GetComponent<CapsuleCollider>();
        return _target.transform.position + Vector3.up * targetCollider.height / 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == _target.transform.name)
        {
            _target.TakeDamage(_damage);
        }
        
        Destroy(gameObject);
    }
}
