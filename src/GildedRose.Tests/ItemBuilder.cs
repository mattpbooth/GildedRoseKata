using GildedRose.Console;
using GildedRose.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GildedRose.Tests
{
    public class ItemBuilder
    {
        private int _quality;
        private string _name = string.Empty;
        private int _sellIn;
        private readonly int _maxQuality;
        public ItemBuilder(int maxQuality)
        {
            _maxQuality = maxQuality;
        }

        public ItemBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ItemBuilder WithSellIn(int sellIn)
        {
            _sellIn = sellIn;
            return this;
        }

        public ItemBuilder WhenInDate()
        {
            _sellIn = Int32.MaxValue;
            return this;
        }

        public ItemBuilder WhenOutOfDate()
        {
            _sellIn = 0;
            return this;
        }

        public ItemBuilder WithAgedBrie()
        {
            _name = "Aged Brie";
            return this;
        }
        public ItemBuilder WithSulfuras()
        {
            _name = "Sulfuras";
            return this;
        }

        public ItemBuilder WithBackstagePass()
        {
            _name = "Backstage pass";
            return this;
        }

        public ItemBuilder WithConjured()
        {
            _name = "Conjured";
            return this;
        }

        public ItemBuilder WithQuality(int quality)
        {
            _quality = quality;
            return this;
        }

        public ItemBuilder WithHighQuality()
        {
            _quality = Int32.MaxValue;
            return this;
        }

        public Item Build()
        {
            return new Item { SellIn = _sellIn, Quality = _quality, Name = _name };
        }
    }
}
