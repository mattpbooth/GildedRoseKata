using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
    [TestFixture]
    public class TestAssemblyTests
    {
        private ItemBuilder _itemBuilder;
        private ItemSimulationBuilder _itemSimulationBuilder;
        private const int _maxQuality = 50;
        
        [SetUp]
        public void Setup()
        {
            _itemBuilder = new ItemBuilder(_maxQuality);
            _itemSimulationBuilder = new ItemSimulationBuilder();
        }

        [Test]
        public void TestTheTruth()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void CanCreateItem_Success()
        {   
            Assert.IsNotNull(_itemBuilder.Build());
        }

        [Test]
        public void CanAddItemToList_Success()
        {
            var items = _itemSimulationBuilder.WithItems(_itemBuilder.Build()).Build();
            Assert.AreEqual(1, items.Count);
        }

        [Test]
        public void ItemStandard_PropertySellin_DecreasesByOneAfterUpdate_Success()
        {
            var items = _itemSimulationBuilder.ExecutingOneUpdate().WithItems(_itemBuilder.WithSellIn(1).Build()).Build();
            Assert.AreEqual(0, items[0].SellIn);
        }

        [Test]
        public void ItemStandard_PropertyQuality_WhenInDate_DecreasesByOneAfterUpdate_Success()
        {
            var items = _itemSimulationBuilder.ExecutingOneUpdate().WithItems(_itemBuilder.WhenInDate().WithQuality(1).Build()).Build();
            Assert.AreEqual(0, items[0].Quality);
        }

        [Test]
        public void ItemStandard_PropertyQuality_WhenOutOfDate_DecreasesByTwoAfterUpdate_Success()
        {
            var items = _itemSimulationBuilder.ExecutingOneUpdate().WithItems(_itemBuilder.WhenOutOfDate().WithQuality(2).Build()).Build();
            Assert.AreEqual(0, items[0].Quality);
        }

        [Test]
        public void ItemStandard_PropertyQuality_WhenQualityIsZero_EquivalentAfterUpdate_Success()
        {
            var items = _itemSimulationBuilder.ExecutingOneUpdate().WithItems(_itemBuilder.WithQuality(0).Build()).Build();
            Assert.AreEqual(0, items[0].Quality);
        }

        [Test]
        public void ItemAgedBrie_PropertyQuality_WhenInDate_IncreasesByOneAfterUpdate_Success()
        {
            var items = _itemSimulationBuilder.ExecutingOneUpdate().WithItems(_itemBuilder.WhenInDate().WithQuality(0).WithAgedBrie().Build()).Build();
            Assert.AreEqual(1, items[0].Quality);
        }

        [Test]
        public void ItemAgedBrie_PropertyQuality_WhenOnDate_IncreaseByTwoAfterUpdate_Success()
        {
            var items = _itemSimulationBuilder.ExecutingOneUpdate().WithItems(_itemBuilder.WhenOutOfDate().WithQuality(0).WithAgedBrie().Build()).Build();
            Assert.AreEqual(2, items[0].Quality);
        }
        
        [Test]
        public void ItemAgedBrie_PropertyQuality_GreaterThanMaxQualityOnConstruction_Success()
        {
            var items = _itemSimulationBuilder.WithItems(_itemBuilder.WithQuality(_maxQuality + 1).Build()).Build();
            Assert.AreEqual(_maxQuality + 1, items[0].Quality);
        }

        [Test]
        public void ItemAgedBrie_PropertyQuality_WhenInDate_NeverGreaterThanFiftyAfterUpdate_Success()
        {
            var items = _itemSimulationBuilder.ExecutingOneUpdate().WithItems(_itemBuilder.WhenOutOfDate().WithQuality(_maxQuality).WithAgedBrie().Build()).Build();
            Assert.AreEqual(_maxQuality, items[0].Quality);
        }

        [Test]
        public void ItemSulfuras_PropertySellIn_EquivalentAfterUpdate_Success()
        {
            var items = _itemSimulationBuilder.ExecutingOneUpdate().WithItems(_itemBuilder.WithSellIn(1).WithSulfuras().Build()).Build();
            Assert.AreEqual(1, items[0].SellIn);
        }

        [Test]
        public void ItemSulfuras_PropertyQuality_EquivalentAfterUpdate_Success()
        {
            var items = _itemSimulationBuilder.ExecutingOneUpdate().WithItems(_itemBuilder.WithQuality(1).WithSulfuras().Build()).Build();
            Assert.AreEqual(1, items[0].Quality);
        }

        [Test]
        public void ItemBackstagePass_PropertyQuality_WhenSellInTen_IncreaseByTwoAfterUpdate_Success()
        {
            var items = _itemSimulationBuilder.ExecutingOneUpdate().WithItems(_itemBuilder.WithSellIn(10).WithQuality(0).WithBackstagePass().Build()).Build();
            Assert.AreEqual(2, items[0].Quality);
        }

        [Test]
        public void ItemBackstagePass_PropertyQuality_WhenSellInSix_IncreaseByTwoAfterUpdate_Success()
        {
            var items = _itemSimulationBuilder.ExecutingOneUpdate().WithItems(_itemBuilder.WithSellIn(6).WithQuality(0).WithBackstagePass().Build()).Build();
            Assert.AreEqual(2, items[0].Quality);
        }

        [Test]
        public void ItemBackstagePass_PropertyQuality_WhenSellInFive_IncreaseByThreeAfterUpdate_Success()
        {
            var items = _itemSimulationBuilder.ExecutingOneUpdate().WithItems(_itemBuilder.WithSellIn(5).WithQuality(0).WithBackstagePass().Build()).Build();
            Assert.AreEqual(3, items[0].Quality);
        }

        [Test]
        public void ItemBackstagePass_PropertyQuality_WhenSellInOne_IncreaseByThreeAfterUpdate_Success()
        {
            var items = _itemSimulationBuilder.ExecutingOneUpdate().WithItems(_itemBuilder.WithSellIn(1).WithQuality(0).WithBackstagePass().Build()).Build();
            Assert.AreEqual(3, items[0].Quality);
        }

        [Test]
        public void ItemBackstagePass_PropertyQuality_WhenOutOfDate_QualityDecreaseByOneAfterUpdate_Success()
        {
            var items = _itemSimulationBuilder.ExecutingOneUpdate().WithItems(_itemBuilder.WhenOutOfDate().WithQuality(1).WithBackstagePass().Build()).Build();
            Assert.AreEqual(0, items[0].Quality);
        }

        [Test]
        public void ItemConjured_PropertyQuality_WhenInDate_QualityDecreaseByTwoAfterUpdate_Success()
        {
            var items = _itemSimulationBuilder.ExecutingOneUpdate().WithItems(_itemBuilder.WhenInDate().WithQuality(2).WithConjured().Build()).Build();
            Assert.AreEqual(0, items[0].Quality);
        }

        [Test]
        public void ItemConjured_PropertyQuality_WhenOutOfDate_QualityDecreaseByFourAfterUpdate_Success()
        {
            var items = _itemSimulationBuilder.ExecutingOneUpdate().WithItems(_itemBuilder.WhenOutOfDate().WithQuality(4).WithConjured().Build()).Build();
            Assert.AreEqual(0, items[0].Quality);
        }
    }
}