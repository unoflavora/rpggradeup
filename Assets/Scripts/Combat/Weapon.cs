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

        private Transform weaponTransform;
        
        public void Spawn (Transform rightHand, Transform leftHand, Animator animator)
        {
            weaponTransform = isRightHanded ? rightHand : leftHand;
            if (weaponPrefab != null) Instantiate (weaponPrefab, weaponTransform);
            if (weaponAnimatorOverride != null) animator.runtimeAnimatorController = weaponAnimatorOverride;
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
            var projectileTarget = Instantiate(projectile);
            projectileTarget.transform.position = weaponTransform.position;
            projectileTarget.SetDamage(weaponDamage);
            projectileTarget.Shoot(target);
        }
    }
}