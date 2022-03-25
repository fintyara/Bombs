using UnityEngine;


namespace NucGames.Bombs
{
    public class FarmerAiStateMove : AIStateBase
    {
        [SerializeField] private MoveOnCells _moveOnCells;

        private void Awake()
        {
            _moveOnCells = GetComponent<MoveOnCells>();
        }

        public override void StartState()
        {
            _spendTime = 0;
            _moveOnCells.StartWander();
            
            _active = true;
            onStart?.Invoke();
        }
        public override void State()
        {
            UpdateTime();
            
            if(_spendTime >= _maxTime)
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