using System;

namespace Match3.Items.Modifiers
{
    public class GravityModifier : IModifier
    {
        public event Action ModifierActivatedEvent;

        public GravityModifier(Action onModifierActivationAction)
        {
            ModifierActivatedEvent += onModifierActivationAction;
        }
        
        public void ActivateModifier()
        {
            ModifierActivatedEvent.Invoke();
            ModifierActivatedEvent = null;
        }
    }
}