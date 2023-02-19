using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Core 
{
    public class ActionScheduler : MonoBehaviour 
    {
        IAction currentAction;
        
        public void StartAction(IAction action)
        {
            if(currentAction == action) return;

            if(currentAction != null)
            {
                print("Cancelling " + currentAction);
                currentAction.Cancel();
            }

            currentAction = action;
        }

        public void CancelAction()
        {
            StartAction(null);
        }
    }
}