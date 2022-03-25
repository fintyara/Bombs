using UnityEngine;
using UnityEngine.Events;


namespace NucGames.Bombs
{
    public class Death : MonoBehaviour
    {
        public UnityEvent onStartDeath;
        public UnityEvent onEndDeath;
        [SerializeField] private float _timeDeath;
        [SerializeField] private GameObject _spawnOnDeathPref;
        private bool _isDead;

        public void StartDeath()
        {
            if (_isDead)
                return;

            _isDead = true;
            Invoke(nameof(EndDeath), _timeDeath);
            
            onStartDeath?.Invoke();
        }
        private void EndDeath()
        {
            if (_spawnOnDeathPref != null)
                Instantiate(_spawnOnDeathPref, transform.position, transform.rotation);
            
            onEndDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
