using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace NucGames.Bombs
{
    [System.Serializable]
    public class TransformEvent : UnityEvent<Transform> { };
    
    public class Eye : MonoBehaviour
    {
        public TransformEvent onFindEnemy;
        [SerializeField] private List<EntityType> _enemyEntityTypes = new List<EntityType>();
        
        public void ControlVision(EntityBase entityBase)
        {
            if(_enemyEntityTypes.Contains(entityBase.GetEntityType()))
                onFindEnemy?.Invoke(entityBase.transform);
        }
    }
}
