using System;

namespace Match3.Items.Modifiers
{
    public interface IModifier
    {
        event Action ModifierActivatedEvent;
        void ActivateModifier();
    }
}