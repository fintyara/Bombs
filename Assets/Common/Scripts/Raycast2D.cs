using UnityEngine;
using UnityEngine.Events;


namespace NucGames.Bombs
{
    public class Raycast2D : MonoBehaviour
    {
        public UnityEvent onHit;
        [SerializeField] private Vector2 _direction;
        [SerializeField] private float _distance;
        [SerializeField] private bool _autoCheck;
        [SerializeField] private EntitiesList _blockedEntities;
        [SerializeField] private LayerMask _layerMask;
        private Transform _curEntity;
        

        private void Update()
        {
            if (!_autoCheck)
                return;

            if (CheckRay())
                onHit?.Invoke();
        }

        public bool CheckRay()
        {
            _curEntity = null;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _direction, _distance, _layerMask);
            Debug.DrawRay(transform.position, _direction * _distance, Color.green);

            if (hit.collider != null)
            {
                EntityBase entityBase = hit.collider.GetComponent<EntityBase>();

                if (entityBase != null && _blockedEntities.EntityTypes.Contains(entityBase.GetEntityType()))
                {
                    Debug.DrawRay(transform.position, _direction * Vector3.Distance(transform.position, hit.point),
                        Color.red);
                    return true;
                }
            }

            return false;
        }
    }
}
