namespace Match3
{
    public class BoardClickHandler
    {
        private bool _haveSelectedTile;
        private Tile _selectedTile;
        public bool TryingToSwap { get; private set;  }
        public (Tile, Tile) TilesBeingSwapped { get; private set; }

        public void UpdateLastClickedTile(Tile clickedTile)
        {
            if (_haveSelectedTile)
            {
                if (clickedTile == _selectedTile)
                {
                    return;
                }
                
                TryingToSwap = true;
                TilesBeingSwapped = (_selectedTile, clickedTile);
                _haveSelectedTile = false;
            }
            else
            {
                TryingToSwap = false;
                _selectedTile = clickedTile;
                _haveSelectedTile = true;
            }
        }
    }
}