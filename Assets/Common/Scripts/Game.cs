using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace NucGames.Bombs
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private List<GameState> _states;
        [SerializeField] private GameState _startState;
        private GameState _curState;

        private void Awake()
        {
            GameState[] states = GetComponentsInChildren<GameState>();
            if (states.Length == 0)
            {
                Debug.LogError("Need game states!");
            }
            else
            {
                for (int i = 0; i < states.Length; i++)
                {
                    _states.Add(states[i]);
                }
            }
        }
        private void Start()
        {
            if (_startState != null)
                ChangeState(_startState);
            else
            {
                Debug.LogError("Need start state!");
            }
        }


        public void ChangeState(GameState newState)
        {
            if (!_states.Contains(newState))
                return;
            
            if(_curState != null)
                _curState.EndState();

            _curState = newState;
            
            _curState.StartState();
        }
        public void ReloadLvl()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
