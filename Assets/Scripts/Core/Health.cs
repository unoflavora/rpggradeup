using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RPG.Saving;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float health = 100f;
        [SerializeField] private bool isDead = false;
        public bool IsDead { get => isDead; }

        // Start is called before the first frame update
        public void TakeDamage(float damage)
        {
            if (IsDead) return;
            health = Mathf.Max(health - damage, 0);
            CheckDie();
        }

        private void CheckDie()
        {
            if (health <= Mathf.Epsilon)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelAction();
            isDead = true;
        }

        public object CaptureState()
        {
            return health;
        }
        public void RestoreState (object data)
        {
            // data is serialized as double so need to convert to float
            health = Convert.ToSingle(data);
            CheckDie();
        }
    }
}

