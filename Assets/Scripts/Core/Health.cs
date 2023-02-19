using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;
        private bool isDead = false;
        public bool IsDead { get => isDead; }

        // Start is called before the first frame update
        public void TakeDamage(float damage)
        {
            if (IsDead) return;
            health = Mathf.Max(health - damage, 0);
            if(health <= Mathf.Epsilon)
            {
                Die();
            }
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelAction();
            isDead = true;
        }
    }
}

