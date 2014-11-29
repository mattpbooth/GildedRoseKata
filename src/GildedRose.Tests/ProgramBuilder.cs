using GildedRose.Console;
using GildedRose.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GildedRose.Tests
{
    public class ItemSimulationBuilder
    {
        private IList<Item> _items;
        private bool _shouldUpdate;

        public ItemSimulationBuilder()
        {
            _items = new List<Item>();
        }

        public ItemSimulationBuilder WithItems(Item item)
        {
            _items.Add(item);
            return this;
        }

        public ItemSimulationBuilder ExecutingOneUpdate()
        {
            _shouldUpdate = true;
            return this;
        }

        public IList<Item> Build()
        {
            if(_shouldUpdate)
            {
                foreach(var i in _items)
                {
                    i.UpdateQuality();
                }
            }
            return _items;
        }
    }
}
