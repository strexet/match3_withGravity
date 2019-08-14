using System;
using System.Collections.Generic;
using Match3.Board;
using Match3.Interfaces;
using Match3.Items.Modifiers;
using UnityEngine;

namespace Match3.Items
{
    public class Item : IPoolable
    {
        private  IModifier _destructionModifier;
        
        private PositionOnBoard _position;
        private int _itemIndex;
        private bool _isActive;
        
        public event Action ItemStateChangedEvent = delegate { };

        public Item(int itemIndex)
        {
            _itemIndex = itemIndex;
        }

        public int ItemIndex
        {
            get => _itemIndex;
            set
            {
                _itemIndex = value;
                ItemStateChangedEvent.Invoke();
            }
        }

        public PositionOnBoard Position
        {
            get => _position;
            set
            {
                _position = value;
                ItemStateChangedEvent.Invoke();
            }
        }

        public bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        public void OnGet()
        {
            _isActive = true;
            _destructionModifier = null;
        }

        public void OnReturn()
        {
            if (_destructionModifier != null)
            {
                _destructionModifier.ActivateModifier();
                _destructionModifier = null;
            }
            
            _isActive = false;
            ItemStateChangedEvent.Invoke();
            
            ItemStateChangedEvent = delegate { };
        }

        public void SetDestructionModifier(IModifier modifier)
        {
            _destructionModifier = modifier;
        }

        public IModifier GetDestructionModifier()
        {
            return _destructionModifier;
        }

        public bool HasDestructionModifier()
        {
            return _destructionModifier != null;
        }
    }
}