using UnityEngine;
using UnityEngine.Events;


namespace NucGames.Bombs
{
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { };
    
    public class MoveByCells : MonoBehaviour,ICanSwitch
    {
        public Vector2Event onDirectionMove;
        public BoolEvent onInCell;
        [SerializeField] private bool _active;
        [SerializeField] private bool _manualControl;
        [SerializeField] private float _speedMove = 1;
        [SerializeField] private float _distanceFinish = 0.1f;
        [SerializeField] private float _distanceCheck = 1;
        [SerializeField] private float distanceRotate = 0.4f;
        [SerializeField] private EntitiesList _blockedMoveEntities;
        [SerializeField] private LayerMask _layerMask;
        private Transform _targetT;
        private Transform _originalT;
        private Vector3 _curDirection;
        private Vector3 _needDirection;
        private bool _signed;
        private float _distanceToCur;
        private float _distanceToTarget;
        private bool canRotate;
        
        
        private void OnEnable()
        {
            if (!_signed && _manualControl)
                Subscribe();

            FindMyCell();
        }
        private void Update()
        {
            if (!_active)
                return;
            
            if (_originalT != null)
                _distanceToCur = Vector3.Distance(transform.position, _originalT.position);
            if (_targetT != null)
                _distanceToTarget = Vector3.Distance(transform.position, _targetT.position);

            if(_targetT != null)
                MoveControl();
            else if (_needDirection != Vector3.zero)
                FindTarget();
            
            ControlRotate();
            ControlReverse();
        }
        
        private void Subscribe()
        {
            Input4Direction input4Direction = GameObject.FindObjectOfType<Input4Direction>();
            if (input4Direction != null)
            {
                input4Direction.onDirectionUpdate.AddListener(SetMoveDirection);
                _signed = true;
            }
        }
        private void SetMoveDirection(Vector2 targetDirection)
        {
            _needDirection = targetDirection;
        }
        private void MoveControl()
        {
            if (_targetT == null)
                return;

            if (Vector3.Distance(transform.position, _targetT.position) > _distanceFinish)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetT.position, _speedMove * Time.deltaTime);
            }
            else
            {
                _originalT = _targetT;
                _targetT = null;
            }
        }
        private bool FindTarget()
        {
            Collider2D collider2D =
                Physics2D.OverlapPoint(transform.position + (_needDirection * _distanceCheck), _layerMask);
            
            if (collider2D != null)
            {
                EntityBase entityBase = collider2D.GetComponentInChildren<EntityBase>();

                if (entityBase == null || !_blockedMoveEntities.EntityTypes.Contains(entityBase.GetEntityType()))
                {
                    _curDirection = _needDirection;
                    if (_targetT != null)
                        _originalT = _targetT;
                    _targetT = collider2D.transform;
                    onDirectionMove?.Invoke(_curDirection);

                    return true;
                }
            }
            
            return false;
        }
        private void FindMyCell()
        {
            Collider2D collider2D =
                Physics2D.OverlapPoint(transform.position, _layerMask);
            
            if (collider2D != null)
            {
                _originalT = collider2D.transform;
            }
        }
        
        private void ControlRotate()
        {
            canRotate = _distanceToCur < distanceRotate || _distanceToTarget < distanceRotate;

            onInCell?.Invoke(canRotate);
            
            if (!canRotate)
                return;
            
            if ((_curDirection.x != 0 && _needDirection.y != 0 && _curDirection.x != _needDirection.y) ||
                (_curDirection.y != 0 && _needDirection.x != 0 && _curDirection.y != _needDirection.x))
            {
                FindTarget();
            }
        }
        private void ControlReverse()
        {
            if ((_curDirection.x != 0 && _needDirection.x != 0 && _curDirection.x != _needDirection.x) ||
                (_curDirection.y != 0 && _needDirection.y != 0 && _curDirection.y != _needDirection.y))
            {
                Transform temp = _targetT;
                _targetT = _originalT;
                _originalT = temp;

                _curDirection = _needDirection;
                onDirectionMove?.Invoke(_curDirection);
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

