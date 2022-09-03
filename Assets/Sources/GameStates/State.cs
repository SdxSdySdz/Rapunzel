using System.Collections.Generic;
using UnityEngine;

namespace Sources.GameStates
{
    public abstract class State : MonoBehaviour
    {
        private List<Transition> _transitions;
        
        protected Rapunzel Rapunzel { get; private set; }
        
        public void Enter(Rapunzel rapunzel)
        {
            if (enabled) 
                return;

            Rapunzel = rapunzel;
            enabled = true;
                
            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(rapunzel);
            }
            
            OnEnter();
        }
        
        public void Exit()
        {
            if (enabled == false) 
                return;
            
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }

            enabled = false;
        }

        public bool TryGetReadyState(out State nextState)
        {
            foreach (var transition in _transitions)
            {
                if (transition.IsNeeded)
                {
                    nextState = transition.TargetState;
                    return true;
                }
            }
            
            nextState = null;
            return false;
        }

        protected virtual void OnEnter()
        {
        }
    }
}