using UnityEngine;

namespace Sources.GameStates
{
    public abstract class Transition : MonoBehaviour
    {
        [SerializeField] private State _targetState;
        
        public bool IsNeeded { get; protected set; }
        public State TargetState => _targetState;
        
        protected Rapunzel Rapunzel { get; private set; }

        protected virtual void OnEnable()
        {
            IsNeeded = false;
        }

        public void Init(Rapunzel rapunzel)
        {
            Rapunzel = rapunzel;
        }
    }
}