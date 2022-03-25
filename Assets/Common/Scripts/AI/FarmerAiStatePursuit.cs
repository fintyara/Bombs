using UnityEngine;


namespace NucGames.Bombs
{
    public class FarmerAiStatePursuit : AIStateBase
    {
        public TransformEvent onReached;
        [SerializeField] private Transform _target;
        [SerializeField] private float _distanceReached = 1.5f;
        [SerializeField] private int _damage = 10000;
        [SerializeField] private DamageType _damageType;
        [SerializeField] private MoveOnCells _moveOnCells;

        
        private void Awake()
        {
            _moveOnCells = GetComponent<MoveOnCells>();
        }
        
        public override void StartState()
        {
            _spendTime = 0;
            _target = _ai.GetTarget();
            _active = true;
           _moveOnCells.SetTarget(_target);
            onStart?.Invoke();
        }
        public override void State()
        {
            if (_target == null || Vector3.Distance(transform.position, _target.position) < _distanceReached)
            {
                if (_target == null)
                    return;
                Health health = _target.GetComponent<Health>();
                if (health != null)
                    health.Damage(_damageType, _damage);
                
                onReached?.Invoke(_target);
                _ai.EndState();
                return;
            }
            UpdateTime();
            
            
            if (_spendTime >= _maxTime)
                _ai.EndState();

            onState?.Invoke();
        }
        public override void EndState()
        {
            _moveOnCells.BreakeMove();
            _active = false;
            onEnd?.Invoke();
        }
    }
}