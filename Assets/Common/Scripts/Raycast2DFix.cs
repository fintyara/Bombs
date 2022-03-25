using UnityEngine;
using UnityEngine.Events;


namespace NucGames.Bombs
{
    [System.Serializable]
    public class EntityBaseEvent : UnityEvent<EntityBase> { };
    
    public class Raycast2DFix : MonoBehaviour
    {
        public EntityBaseEvent onHit;
        [SerializeField] private bool _auto;
        [SerializeField] private float _distance;
        [SerializeField] private float _reload;
        [SerializeField] private EntitiesList _blockedEntities;
        [SerializeField] private LayerMask _layerMask;
        private float _lastTime;
        private float _length;
        

        private void Update()
        {
          if(_auto && Time.time > _lastTime + _reload)
              Ray();
        }

        public EntityBase CheckRay()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, _distance, _layerMask);
            Debug.DrawRay(transform.position, transform.right * _distance, Color.green);

            if (hit.collider != null)
            {
                EntityBase entityBase = hit.collider.GetComponent<EntityBase>();

                if (entityBase != null && _blockedEntities.EntityTypes.Contains(entityBase.GetEntityType()))
                {
                    Debug.DrawRay(transform.position, transform.right * Vector3.Distance(transform.position, hit.point),
                        Color.red);

                    _length = Vector3.Distance(transform.position, hit.point);
                    return entityBase;
                }
            }

            _length = _distance;
            return null;
        }
        public float GetLengthRay()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, _distance, _layerMask);
            Debug.DrawRay(transform.position, transform.right * _distance, Color.green);

            if (hit.collider != null)
            {
                EntityBase entityBase = hit.collider.GetComponent<EntityBase>();

                if (entityBase != null && _blockedEntities.EntityTypes.Contains(entityBase.GetEntityType()))
                {
                    Debug.DrawRay(transform.position, transform.right * Vector3.Distance(transform.position, hit.point),
                        Color.red);

                    return Vector3.Distance(transform.position, hit.point);
                }
            }

            return _distance;
        }
        private void Ray()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, _distance, _layerMask);
            Debug.DrawRay(transform.position, transform.right * _distance, Color.green);

            if (hit.collider != null)
            {
                EntityBase entityBase = hit.collider.GetComponent<EntityBase>();

                if (entityBase != null && _blockedEntities.EntityTypes.Contains(entityBase.GetEntityType()))
                {
                    Debug.DrawRay(transform.position, transform.right * Vector3.Distance(transform.position, hit.point),
                        Color.red);
                    
                    _length = Vector3.Distance(transform.position, hit.point);
                    onHit?.Invoke(entityBase);
                }
            }
            
            _lastTime = Time.time;
        }
    }
}