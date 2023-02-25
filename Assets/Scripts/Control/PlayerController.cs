using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control {
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;

        private Health health;
        // Start is called before the first frame update
        void Start()
        {
            _mover = GetComponent<Mover>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (health.IsDead) return;
            if (InteractWithCombat()) return;
            if (MoveToCursor()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                
                if(target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }

            return false;
        }

        private bool MoveToCursor()
        {
            Ray ray = GetMouseRay();
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit);
            
            if (hasHit) 
            {
                if(Input.GetMouseButton(0)) 
                {
                    _mover.StartMoveAction(hit.point, 1f);
                }
            }

            return hasHit;
        }

        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}

