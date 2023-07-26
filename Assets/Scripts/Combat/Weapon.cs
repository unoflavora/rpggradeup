using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG Project/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController weaponAnimatorOverride;
        [SerializeField] private GameObject weaponPrefab;
        [SerializeField] private float weaponRange;
        [SerializeField] private float weaponDamage;
        [SerializeField] private bool isRightHanded;
        [SerializeField] private Projectile projectile;

        private Transform hand;
        
        public GameObject Spawn (Transform rightHand, Transform leftHand, Animator animator)
        {
            hand = isRightHanded ? rightHand : leftHand;
            if (weaponAnimatorOverride != null) animator.runtimeAnimatorController = weaponAnimatorOverride;
            if (weaponPrefab != null) return Instantiate (weaponPrefab, hand);
            
            return null;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public float GetWeaponRange()
        {
            return weaponRange;
        }
        
        public float GetWeaponDamage()
        {
            return weaponDamage;
        }

        public void Shoot(Health target)
        {
            if (hand == null) return;
            
            var projectileTarget = Instantiate(projectile, hand.position, Quaternion.identity);
            Debug.Log(projectileTarget);
            projectileTarget.Shoot(target);
            projectileTarget.transform.position = hand.position;
            projectileTarget.SetDamage(weaponDamage);
        }
    }
}