using UnityEngine;


namespace NucGames.Bombs
{
    public class Damager : MonoBehaviour
    {
        #region VAR
        [SerializeField] private DamageType _myDamageType;
        [SerializeField] private int _damage;
        #endregion

       
        private void OnTriggerEnter2D(Collider2D other)
        {
            Health health = other.GetComponent<Health>();
            if(health != null)
                health.Damage(_myDamageType, _damage);
        }
    }
}
