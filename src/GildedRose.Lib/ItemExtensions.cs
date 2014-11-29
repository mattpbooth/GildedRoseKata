using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Lib
{
    public static class ItemExtensions
    {
        enum SellInModifyAction
        {
            Decrement,
            Nop
        };

        private static IDictionary<string, Func<Item, SellInModifyAction>> _itemQualityActions;
        private const int _sellInRateDecrease = 1;
        private const int _qualityRateDecrease = 1;
        private const int _qualityRateDecreaseOutOfDate = 2;
        

        static ItemExtensions()
        {
            _itemQualityActions = new Dictionary<string, Func<Item, SellInModifyAction>>();
            _itemQualityActions.Add("Aged Brie", updateAgedBrieQuality);
            _itemQualityActions.Add("Sulfuras", updateSulfurasQuality);
            _itemQualityActions.Add("Backstage pass", updateBackstagePassQuality);
            _itemQualityActions.Add("Conjured", updateConjuredQuality);
        }
        
        public static void UpdateQuality(this Item item)
        {
            var qualityActionPair =_itemQualityActions.Where(iqa => item.Name.Contains(iqa.Key)).FirstOrDefault();
            var action = (qualityActionPair.Key != null) ? qualityActionPair.Value : updateStandardQuality;

            if (action(item) == SellInModifyAction.Decrement)
            {
                --item.SellIn;
            }
        }

        private static SellInModifyAction updateStandardQuality(Item item)
        {
            updateItemQuality(item);
            return SellInModifyAction.Decrement;
        }

        private static SellInModifyAction updateAgedBrieQuality(Item item)
        {
            updateItemQuality(item, -1);
            return SellInModifyAction.Decrement;
        }

        private static SellInModifyAction updateSulfurasQuality(Item item)
        {
            return SellInModifyAction.Nop;
        }

        private static SellInModifyAction updateBackstagePassQuality(Item item)
        {
            if(item.SellIn <= 0)
            {
                updateItemQuality(item);
            }
            else if (item.SellIn <= 5)
            {
                updateItemQuality(item, -3);
            }
            else if(item.SellIn <= 10)
            {
                updateItemQuality(item, -2);
            }
            return SellInModifyAction.Decrement;
        }

        private static SellInModifyAction updateConjuredQuality(Item item)
        {
            updateItemQuality(item, 2);
            return SellInModifyAction.Decrement;
        }

        private static void updateItemQuality(Item item, int modifier = 1)
        {
            var qualityAlteration = ((item.SellIn > 0) ? _qualityRateDecrease : _qualityRateDecreaseOutOfDate) * modifier;
            item.Quality = Math.Min(Math.Max(item.Quality - qualityAlteration, 0), 50);
        }
    }
}

