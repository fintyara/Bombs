using UnityEngine;


namespace NucGames.Bombs
{
    public class AiStateAttack : AIStateBase
    {
        public TransformEvent onAttack;
        [SerializeField] private float _distanceAttack = 1.5f;
        [SerializeField] private int _damage = 10000;
        [SerializeField] private DamageType _damageType;
        private Transform _target;
        
        
        public override void StartState()
        {
            _spendTime = 0;
            _target = _ai.GetTarget();
            _active = true;
            onStart?.Invoke();
        }
        public override void State()
        {
            UpdateTime();

            if (Vector3.Distance(transform.position, _target.position) < _distanceAttack)
            {
                Health health = _target.GetComponent<Health>();
                if (health != null)
                    health.Damage(_damageType, _damage);
                onAttack?.Invoke(_target);
                _ai.EndState();
            }
            
            if(_spendTime >= _maxTime)
                _ai.EndState();
            
            onState?.Invoke();
        }
        public override void EndState()
        {
            _active = false;
            onEnd?.Invoke();
        }
    }
}
