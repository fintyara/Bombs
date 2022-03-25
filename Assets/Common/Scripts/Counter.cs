using UnityEngine;
using UnityEngine.Events;


namespace NucGames.Bombs
{
    [System.Serializable]
    public class IntEvent : UnityEvent<int> { };
    
    public class Counter : MonoBehaviour
    {
        public UnityEvent onAdd;
        public IntEvent onUpdate;
        public UnityEvent onFull;
        [SerializeField] private string _descr;
        [Space(10)] 
        [SerializeField] private int _maxCount = 5;
        [ShowOnly][SerializeField] private int _curCount;
        private bool _full;


        private void OnEnable()
        {
            _curCount = 0;
            _full = false;
        }

        public void AddCount(int count)
        {
            if (_full)
                return;

            _curCount += count;
            onAdd?.Invoke();

            if (_curCount >= _maxCount)
            {
                _curCount = _maxCount;
                _full = true;
                
                onFull?.Invoke();
            }

            onUpdate?.Invoke(_curCount);
        }
    }
}
