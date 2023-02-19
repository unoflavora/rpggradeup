using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement 
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] private Transform _target;
        private NavMeshAgent _navmeshAgent;
        private Animator _animator;
        private Health _health;
        private float _maxSpeed = 5.6f;
        void Start()
        {
            _navmeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
        }

        void Update()
        {
            _navmeshAgent.enabled = !_health.IsDead;
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            _navmeshAgent.isStopped = false;
            _navmeshAgent.speed = _maxSpeed * Mathf.Clamp01(speedFraction);
            _navmeshAgent.destination = destination;
        }

        public void Cancel()
        {
            _navmeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(_navmeshAgent.velocity);
            _animator.SetFloat("forwardSpeed", localVelocity.z);        
        }
    }
}