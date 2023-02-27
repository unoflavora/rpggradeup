using UnityEngine;

namespace RPG.Combat
{
    public class PickupWeapon : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<Fighter>().EquipWeapon(_weapon);
                Destroy(gameObject);
            }
        }
    }
}

