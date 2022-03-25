using System.Collections.Generic;
using UnityEngine;


namespace NucGames.Bombs
{
   [CreateAssetMenu(menuName = "Entities/EntitiesList")]
   public class EntitiesList : ScriptableObject
   {
      public List<EntityType> EntityTypes => _entityTypes;
      [SerializeField] private List<EntityType> _entityTypes;
   }
}
