using UnityEngine;
using UnityEngine.Events;


namespace NucGames.Bombs
{
   public abstract class AIStateBase : MonoBehaviour
   {
      public UnityEvent onStart;
      public UnityEvent onState;
      public UnityEvent onEnd;
      [SerializeField] protected bool _active;
      [SerializeField] protected float _maxTime;
      [ShowOnly] [SerializeField] protected float _spendTime;
      protected AIBase _ai;
     

      public abstract void StartState();
      public abstract void State();
      public abstract void EndState();
      public virtual void InitState(AIBase ai)
      {
         _ai = ai;
      }
      protected virtual void UpdateTime()
      {
         _spendTime += Time.deltaTime;
      }
   }
}