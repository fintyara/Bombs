using System.Collections.Generic;
using UnityEngine;


namespace NucGames.Bombs
{
    public class FarmerAI : AIBase,ICanSwitch
    {
        [SerializeField] protected AIStateBase _idleState;
        [SerializeField] protected AIStateBase _moveState;
        [SerializeField] protected AIStateBase _pursuitState;
        [SerializeField] protected AIStateBase _dirtState;
        private bool _dirty;
      
        
        private void Awake()
        {
           FillStates();
           InitStates();
        }
        private void Start()
        {
            if (_startState != null)
                ChangeState(_startState);
            else
            {
                Debug.LogError("Need start state!");
            }
        }
        private void Update()
        {
            if (_active)
                StatesControl();
        }
        
        public override void SetTarget(Transform t)
        {
            if(!_active || _curState == _dirtState || _target != null)
                return;

            _target = t;
        }
        public void SetDirty()
        {
            if(!_active || _curState == _dirtState )
                return;

            _dirty = true;
            _target = null;
        }
        private void StatesControl()
        {
            _curState.State();
            
            if (_curState == _dirtState)
                return;
            
            if (_target == null && _curState == _pursuitState)
            {
                ChangeState(_idleState);
                return;
            }

            if (_dirty && _curState != _dirtState)
            {
                ChangeState(_dirtState);
                return;
            }
            if (_target && _curState != _pursuitState)
            {
                ChangeState(_pursuitState);
            }
        }
        public override void EndState()
        {
            if (_curState == _dirtState)
                _dirty = false;

            if (_curState == _pursuitState)
            {
                _target = null;
                ChangeState(_idleState);
            }

            ChangeState(_curState == _idleState ? _moveState : _idleState);
        }
        private void FillStates()
        {
            _states = new List<AIStateBase>();
            
            AIStateBase[] states = GetComponents<AIStateBase>();
            if (states.Length == 0)
            {
                Debug.LogError("Need game states!");
            }
            else
            {
                for (int i = 0; i < states.Length; i++)
                {
                    _states.Add(states[i]);
                }
            }
        }
        private void InitStates()
        {
            for (int i = 0; i < _states.Count; i++)
            {
                _states[i].InitState(this);
            }
        }
        public void Activate()
        {
            _active = true;
        }
        public void Deactivate()
        {
            _active = false;
        }
        public void Switch()
        {
            _active = !_active;
        }
    }
}