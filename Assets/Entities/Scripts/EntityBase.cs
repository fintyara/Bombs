using UnityEngine;


namespace NucGames.Bombs
{
   public abstract class EntityBase : MonoBehaviour
   {
      public abstract void Damaged(int damage);
      public abstract void Clicked(int damage);
      public abstract EntityType GetEntityType();
   }
}
