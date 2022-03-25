using UnityEngine;
using UnityEngine.Events;


namespace NucGames.Bombs
{
    public class Result : MonoBehaviour
    {
        public UnityEvent onWin;
        public UnityEvent onLose;

        public void LoseGame()
        {
            onLose?.Invoke();
        }
        public void WinGame()
        {
            onWin?.Invoke();
        }
    }
}
