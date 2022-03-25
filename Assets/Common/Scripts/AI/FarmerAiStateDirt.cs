using UnityEngine;


namespace NucGames.Bombs
{
    public class FarmerAiStateDirt : AIStateBase
    {
        [ShowOnly][SerializeField] private float _timeDirty;
        [SerializeField] private float _maxTimeDirty;
        [SerializeField] private float _minTimeDirty;
        [SerializeField] private MoveOnCells _moveOnCells;

        
        private void Awake()
        {
            _moveOnCells = GetComponent<MoveOnCells>();
        }
        
        
        public override void StartState()
        {
            _spendTime = 0;
            _timeDirty = Random.Range(_minTimeDirty, _maxTimeDirty);
            _moveOnCells.StartWander();
            
            _active = true;
            onStart?.Invoke();
        }
        public override void State()
        {
            UpdateTime();
            
            if(_spendTime >= _timeDirty || _spendTime >= _maxTime)
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
