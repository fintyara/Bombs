using UnityEngine;
using UnityEngine.Events;


namespace NucGames.Bombs
{
   public class GameState : MonoBehaviour
   {
      public UnityEvent onStart;
      public UnityEvent onState;
      public UnityEvent onEnd;
      [SerializeField] private GameObject _stateUI;
      private bool _active;


      private void Update()
      {
         if (_active)
            State();
      }


      public void StartState()
      {
         if (_stateUI != null)
            _stateUI.SetActive(true);
         
         _active = true;
         onStart?.Invoke();
      }
      public void State()
      {
         onState?.Invoke();
      }
      public void EndState()
      {
         if (_stateUI != null)
            _stateUI.SetActive(false);
         
         _active = false;
         onEnd?.Invoke();
      }
   }
}

