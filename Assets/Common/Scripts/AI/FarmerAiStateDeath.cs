
namespace NucGames.Bombs
{
    public class FarmerAiStateDeath : AIStateBase
    {
        private void Update()
        {
            if (_active)
                State();
        }


        public override void StartState()
        {
            _active = true;
            onStart?.Invoke();
        }
        public override void State()
        {
            onState?.Invoke();
        }
        public override void EndState()
        {
            _active = false;
            onEnd?.Invoke();
        }
    }
}