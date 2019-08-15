using Match3.Interfaces;
using Random = UnityEngine.Random;

namespace Match3.Items.Pool
{
    public class ItemPool : IPool<Item>
    {
        private readonly int _numberOfAvailableColors;
        
        public ItemPool(int numberOfAvailableColors)
        {
            _numberOfAvailableColors = numberOfAvailableColors;
        }

        public Item Get()
        {
            var item = new Item(Random.Range(0, _numberOfAvailableColors));
            item.OnGet();
            return item;
        }

        public void Return(Item item)
        {
            item.OnReturn();
        }
    }
}