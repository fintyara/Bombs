using UnityEngine;
using UnityEngine.UI;


namespace NucGames.Bombs
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private string _buttonTag;
        [SerializeField] private GameObject _pref;
        [SerializeField] private Transform _pos;
        [SerializeField] private float _reload;
        [SerializeField] private bool _active;
        private float _lastTime;

        private void Awake()
        {
            if (_buttonTag == "")
                return;
            
            Button button = GameObject.FindGameObjectWithTag(_buttonTag).GetComponent<Button>();
            if (button != null)
                button.onClick.AddListener(Spawn);
        }

        public void Spawn()
        {
            if (!_active || Time.time < _lastTime + _reload)
                return;

            Instantiate(_pref, _pos.position, _pos.rotation);
            _lastTime = Time.time;
        }
        public void FilterSpawn(bool canSpawn)
        {
            _active = canSpawn;
        }
    }
}
