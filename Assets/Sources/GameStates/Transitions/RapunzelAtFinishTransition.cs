namespace Sources.GameStates.Transitions
{
    public class RapunzelAtFinishTransition : Transition
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            Rapunzel.Finished += OnRapunzelFinished;
        }

        private void OnDisable()
        {
            Rapunzel.Finished -= OnRapunzelFinished;
        }

        private void OnRapunzelFinished()
        {
            IsNeeded = true;
        }
    }
}