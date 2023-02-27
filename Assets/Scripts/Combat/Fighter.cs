using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private Transform rightHand;
        [SerializeField] private Transform leftHand;
        
        [SerializeField] private Weapon defaultWeapon;
        [SerializeField] private Weapon currentWeapon;
        
        private Health target;
        private float timeSinceLastAttack = Mathf.Infinity;
        private Animator animator;
        private void Start()
        {
            animator = GetComponent<Animator>();
        }


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            
            if (currentWeapon == null) EquipWeapon(defaultWeapon);

            if (target == null) return;

            if (target.IsDead) return;
            
            if (!GetIsInRange())
            {
               GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
            
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);

            if(timeSinceLastAttack >= timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetWeaponRange();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }

        /* Animation Event */
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(currentWeapon.GetWeaponDamage());
        }

        void Shoot()
        {
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.Shoot(target);
            }
        }

        public bool CanAttack(GameObject combatTarget)
        {
            return combatTarget != null && !combatTarget.GetComponent<Health>().IsDead;
        }
        
        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            currentWeapon.Spawn(rightHand, leftHand, animator);
        }


    }
}
