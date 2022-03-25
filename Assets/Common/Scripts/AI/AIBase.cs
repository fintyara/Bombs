using System.Collections.Generic;
using UnityEngine;


namespace NucGames.Bombs
{
    public abstract class AIBase : MonoBehaviour
    {
        [SerializeField] protected bool _active;
        [Space(20)]
        [SerializeField] protected AIStateBase _startState;
        protected List<AIStateBase> _states;
        protected AIStateBase _curState;
        protected Transform _target;

        public abstract void EndState();
        public virtual void SetTarget(Transform t)
        {
            _target = t;
        }
        public virtual Transform GetTarget()
        {
            return _target;
        }
        public virtual void ChangeState(AIStateBase newState)
        {
            if (!_states.Contains(newState))
                return;
            
            if(_curState != null)
                _curState.EndState();

            _curState = newState;
            
            _curState.StartState();
        }
    }
}

