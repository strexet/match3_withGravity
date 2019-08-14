using System;
using System.Collections.Generic;
using Match3.Board;
using Match3.Interfaces;
using Match3.Items.Modifiers;
using Match3.Scriptable;
using UnityEngine;

namespace Match3.Items
{
    public class ItemView : MonoBehaviour, IPoolable
    {        

        [SerializeField] private ItemViewSettings ItemViewSettings;
        private Item _associatedItem;
        private IBoardPositionConverter _boardPositionConverter;
        private GameObject _view;
        private GameObject _gravityModifierView;

        public event Action<ItemView> DeactivationEvent = delegate { };
            
        public PositionOnBoard PositionOnBoard { get; private set; }

        public Item AssociatedItem
        {
            set
            {
                _associatedItem = value;
                _associatedItem.ItemStateChangedEvent += OnAssociatedItemStateChanged;
                OnAssociatedItemStateChanged();
            }
        }

        public void OnAssociatedItemStateChanged()
        {
            if (!_associatedItem.IsActive)
            {
                DeactivationEvent.Invoke(this);
                return;
            }

            UpdateModifier();
            UpdatePosition();
        }

        private void UpdateModifier()
        {
            if (_associatedItem.HasDestructionModifier())
            {
                AddModifierView();
            }
            else
            {
                DestroyModifier();
            }
        }

        private void AddModifierView()
        {
            switch (_associatedItem.GetDestructionModifier())
            {
                case GravityModifier gravityModifier:
                {
                    if (_gravityModifierView == null)
                    {
                        var modifierViewPrefab = ItemViewSettings.ModifierViewPrefabs.GravityModifier;
                        _gravityModifierView =
                            Instantiate(modifierViewPrefab, transform.position, transform.rotation, transform);
                    }
                    break;
                }
            } 
        }

        private void UpdatePosition()
        {
            PositionOnBoard = _associatedItem.Position;
            transform.position = _boardPositionConverter.GetWorldPosition(PositionOnBoard);
        }

        public void OnGet()
        {
            gameObject.SetActive(true);
        }

        public void OnReturn()
        {
            gameObject.SetActive(false);
            _associatedItem.ItemStateChangedEvent -= OnAssociatedItemStateChanged;
            _associatedItem = null;

            DestroyModifier();
        }

        private void DestroyModifier()
        {
            if (_gravityModifierView != null)
            {
                Destroy(_gravityModifierView);
            }
        }

        public void Initialize(IBoardPositionConverter boardPositionConverter)
        {
            _boardPositionConverter = boardPositionConverter;
        }
    }
}