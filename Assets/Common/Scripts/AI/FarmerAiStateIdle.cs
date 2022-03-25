using UnityEngine;


namespace NucGames.Bombs
{
    public class FarmerAiStateIdle : AIStateBase
    {
        [ShowOnly][SerializeField] private float _timeIdle;
        [SerializeField] private float _maxTimeIdle;
        [SerializeField] private float _minTimeIdle;
        [SerializeField] private float _speed = 0.02f;
        [SerializeField] private float _minDist = 0.1f;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private bool _needAligment;
        private Transform _myCell;
        
        
        public override void StartState()
        {
            _spendTime = 0;
            _timeIdle = Random.Range(_minTimeIdle, _maxTimeIdle);
            Collider2D collider2D =
                Physics2D.OverlapPoint(transform.position, _layerMask);
            if (collider2D != null)
            {
                _myCell = collider2D.transform;
            }
            
            _active = true;
            onStart?.Invoke();
        }
        public override void State()
        {
            UpdateTime();
            
            if (_needAligment && _myCell != null)
                Aligment();
            
            if(_spendTime >= _timeIdle || _spendTime >= _maxTime)
                _ai.EndState();
            
            onState?.Invoke();
        }
        public override void EndState()
        {
            _myCell = null;
            _active = false;
            onEnd?.Invoke();
        }

        private void Aligment()
        {
            if (Vector3.Distance(transform.position, _myCell.position) > _minDist)
                transform.position = Vector3.Lerp(transform.position, _myCell.position, _speed);
        }
    }
}