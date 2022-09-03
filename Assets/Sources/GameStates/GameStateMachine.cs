using System;
using UnityEngine;

namespace Sources.GameStates
{
    public class GameStateMachine : MonoBehaviour
    {
        [SerializeField] private Rapunzel _rapunzel;
        [SerializeField] private State _firstState;
        
        private State _currentState;
        
        private void Start()
        {
            Transit(_firstState);
        }

        private void Update()
        {
            if (_currentState.TryGetReadyState(out State nextState))
                Transit(nextState);
        }

        private void Transit(State nextState)
        {
            _currentState.Exit();

            _currentState = nextState;
            
            _currentState.Enter(_rapunzel);
        }
    }
}