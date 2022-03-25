using UnityEngine;
using UnityEngine.Events;


namespace NucGames.Bombs
{
    public class DestoyAfterDelay : MonoBehaviour
    {
        public UnityEvent onStartDestroy;
        public UnityEvent onFinishDestroy;
        [SerializeField] private float timeLife = 1;
        [SerializeField] private float timePrepare = 1;
        [SerializeField] private float timeDestroy = 1;
        [SerializeField] private GameObject _startPref;
        [SerializeField] private GameObject _endPref;
        private bool _destroyed;


        private void OnEnable()
        {
            Invoke(nameof(StartDestroy), timeLife);
        }


        public void StartDestroy()
        {
            if (_destroyed)
                return;

            if (_startPref != null)
                Instantiate(_startPref, transform.position, transform.rotation);

            _destroyed = true;
            onStartDestroy?.Invoke();

            Invoke(nameof(EndDestroy), timePrepare);
        }
        private void EndDestroy()
        {
            if (_endPref != null)
                Instantiate(_endPref, transform.position, transform.rotation);

            onFinishDestroy?.Invoke();

            Destroy(gameObject, timeDestroy);
        }
    }
}
