using UnityEngine;
using UnityEngine.Events;


namespace NucGames.Bombs
{
    public class MoveOnCells : MonoBehaviour
    {
        public UnityEvent onStart;
        public UnityEvent onBreake;
        public Vector2Event onChangeDirection;
        [SerializeField] private bool _active;
        [SerializeField] private float _speedMove = 1;
        [SerializeField] private float _factorSpeed = 1;
        [SerializeField] private Transform _targetCell;
        [SerializeField] private Transform _myCell;
        [SerializeField] private Transform _target;
        [SerializeField] private float _chanceToRotate = 20;
        [SerializeField] private float _distanceInCell = 0.1f;
        [SerializeField] private bool _end;
        [SerializeField] private bool _inCell;
        
        [SerializeField] private float _distanceCheck = 1;
        [SerializeField] private EntitiesList _blockedMoveEntities;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private FindPath _findPath;
        [SerializeField] private Vector3 _direction;
        private Vector3[] _directionsCheck = new[] {Vector3.left, Vector3.right, Vector3.up, Vector3.down,};

        private void Awake()
        {
            FindMyCell();
            _inCell = true;
        }
        private void Update()
        {
            if (_end)
                return;

            if (_targetCell == null)
            {
                if (_target == null)
                {
                    if (!TryRotate() && !FindCellOnDirection() && !FindAroundCell())
                    {
                        return;
                    }
                }
                else
                {
                    if (!FindCellNearTarget())
                    {
                        BreakeMove();
                        return;
                    }
                }
            }
            if (Vector3.Distance(transform.position, _targetCell.position) < _distanceInCell)
            {
                _inCell = true;
                _myCell = _targetCell;
                _targetCell = null;
            }
            else
            {
                _inCell = false;
                Move();
            }
        }

        
        public void SetTarget(Transform t)
        {
            if (!_end)
                BreakeMove();
            
            _target = t;
            
            if (!FindCellNearTarget())
            {
                Debug.Log("not find cell around");
            }
            else
            {
                StartMove();
            }
        }
        public void StartWander()
        {
            if (!_end)
                BreakeMove();

            if (!FindAroundCell())
            {
                Debug.Log("not find cell around");
            }
            else
            {
                StartMove();
            }
        }
        public void SlowSpeedInPercent(float percent)
        {
            percent = Mathf.Clamp(percent, 0, 90);
            _factorSpeed = 1 - 1 * percent / 100;

        }
        public void EndSlowSpeed()
        {
            _factorSpeed = 1;
        }
        public void BreakeMove()
        {
            if (_end)
                return;

            _target = null;
            _targetCell = null;
            _direction = Vector3.zero;
            _end = true;
            onBreake?.Invoke();
        }
        
        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetCell.position, _speedMove *_factorSpeed * Time.deltaTime);
        }
        private bool TryRotate()
        {
            int chance = Random.Range(0, 100);
            if (chance > _chanceToRotate)
                return false;

            Vector3 newDirection;
            if (_direction.x != 0)
            {
                newDirection = _findPath.GetRandomVertical();
                if(newDirection == Vector3.zero)
                    return false;
            }
            else
            {
                newDirection = _findPath.GetRandomHorizontal();
                if(newDirection == Vector3.zero)
                    return false;
            }

            _direction = newDirection;
            
            Collider2D collider2D =
                Physics2D.OverlapPoint(transform.position + (_direction * _distanceCheck), _layerMask);

            if (collider2D != null)
            {
                EntityBase entityBase = collider2D.GetComponentInChildren<EntityBase>();

                if (entityBase == null || !_blockedMoveEntities.EntityTypes.Contains(entityBase.GetEntityType()))
                {
                    _targetCell = collider2D.transform;

                    onChangeDirection?.Invoke(_direction);
                    return true;
                }
            }
            
            return false;
        }
        private bool FindCellOnDirection()
        {
            Collider2D collider2D =
                Physics2D.OverlapPoint(transform.position + (_direction * _distanceCheck), _layerMask);

            if (collider2D != null)
            {
                EntityBase entityBase = collider2D.GetComponentInChildren<EntityBase>();

                if (entityBase == null || !_blockedMoveEntities.EntityTypes.Contains(entityBase.GetEntityType()))
                {
                    _targetCell = collider2D.transform;
                    return true;
                }
            }
            
            return false;
        }
        private bool FindAroundCell()
        {
            _direction = _findPath.GetRandomDirection();
            if (_direction == Vector3.zero)
            {
                return false;
            }
            
            Collider2D collider2D =
                Physics2D.OverlapPoint(transform.position + (_direction * _distanceCheck), _layerMask);

            if (collider2D != null)
            {
                EntityBase entityBase = collider2D.GetComponentInChildren<EntityBase>();

                if (entityBase == null || !_blockedMoveEntities.EntityTypes.Contains(entityBase.GetEntityType()))
                {
                    onChangeDirection?.Invoke(_direction);
                    _targetCell = collider2D.transform;
                    return true;
                }
            }
            
            return false;
        }
        private bool FindCellNearTarget()
        {
            if (_target == null)
            {
                BreakeMove();
                return false;
            }
            
            Collider2D collider2D;
            float minDist = float.MaxValue;
            int bestIndex = -1;
            Collider2D bestCollider = null;

            for (int i = 0; i < _directionsCheck.Length; i++)
            {
                collider2D = Physics2D.OverlapPoint(transform.position + _directionsCheck[i], _layerMask);
                if (collider2D != null)
                {
                    EntityBase entityBase = collider2D.GetComponentInChildren<EntityBase>();
                    if (entityBase == null || !_blockedMoveEntities.EntityTypes.Contains(entityBase.GetEntityType()))
                    {
                        float dist = Vector3.Distance(_target.position, collider2D.transform.position);
                        if (dist < minDist)
                        {
                            bestCollider = collider2D;
                            bestIndex = i;
                            minDist = dist;
                        }
                    }
                }
            }

            if (bestIndex != -1)
            {
                _direction = _directionsCheck[bestIndex];
                onChangeDirection?.Invoke(_direction);
                _targetCell = bestCollider.transform;
                return true;
            }
            return false;
        }
        private void StartMove()
        {
            _end = false;
            
            onStart?.Invoke();
        }
        private void FindMyCell()
        {
            Collider2D collider2D =
                Physics2D.OverlapPoint(transform.position, _layerMask);
            
            if (collider2D != null)
            {
                _myCell = collider2D.transform;
            }
        }
    }
}
