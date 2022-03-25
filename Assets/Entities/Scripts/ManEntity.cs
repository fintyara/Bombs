using UnityEngine;


namespace NucGames.Bombs
{
    public class ManEntity : EntityBase
    {
        [SerializeField] private EntityType _entityType;

        public override void Damaged(int damage)
        {
            throw new System.NotImplementedException();
        }
        public override void Clicked(int damage)
        {
            throw new System.NotImplementedException();
        }
        public override EntityType GetEntityType()
        {
            if (_entityType == null)
            {
                Debug.LogError("Need EntityType!");
            }
            
            return _entityType;
        }
    }
}