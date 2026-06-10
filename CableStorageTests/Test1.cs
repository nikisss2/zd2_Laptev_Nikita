using Cables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection.Emit;
using Cables;

namespace Cables.Tests
{
    [TestClass]
    public class CableStorageTests
    {
        //тестирование метода ADD
        [TestMethod]
        public void Add_ObychnyiKabel_UvelichivaetCount()
        {
            CableStorage storage = new CableStorage();

            storage.Add("ВВГ", 3, 1.5, "Завод-1", 100);

            Assert.AreEqual(1, storage.Count);
        }

        [TestMethod]
        public void Add_NeskolkoKabeley_CountSovpadaet()
        {
            CableStorage storage = new CableStorage();

            storage.Add("ВВГ", 3, 1.5, "Завод-1", 100);
            storage.Add("ПВС", 2, 0.75, "Завод-2", 80);
            storage.Add("КГ", 4, 2.5, "Завод-3", 200, true, "медь", true);

            Assert.AreEqual(3, storage.Count);
            Assert.AreEqual(3, storage.GetMap().Count);
        }

        [TestMethod]
        public void Add_EkranirovannyiKabel_DobavlyaetsyaKakShieldedCable()
        {
            CableStorage storage = new CableStorage();

            storage.Add("КГ", 4, 2.5, "Завод-3", 200, true, "медь", true);
            List<Cable> all = storage.GetAll();

            Assert.IsInstanceOfType(all[0], typeof(ShieldedCable));
        }

        // тестирование Remove
        [TestMethod]
        public void Remove_PoIndeksu_UdalyaetNuzhnyi()
        {

            CableStorage storage = new CableStorage();
            storage.Add("ВВГ", 3, 1.5, "Завод-1", 100);
            storage.Add("ПВС", 2, 0.75, "Завод-2", 80);

            storage.Remove(0);

            Assert.AreEqual(1, storage.Count);
            Assert.AreEqual("ПВС", storage.GetAll()[0].Type);
        }

        [TestMethod]
        public void Remove_PoNevernomuIndeksu_NichegoNeUdalyaet()
        {
            CableStorage storage = new CableStorage();
            storage.Add("ВВГ", 3, 1.5, "Завод-1", 100);

            storage.Remove(10);
            storage.Remove(-1);

            Assert.AreEqual(1, storage.Count);
        }

        [TestMethod]
        public void Remove_PoTipu_UdalyaetNezavisimoOtRegistra()
        {
            CableStorage storage = new CableStorage();
            storage.Add("ВВГ", 3, 1.5, "Завод-1", 100);
            storage.Add("ПВС", 2, 0.75, "Завод-2", 80);

            storage.Remove("  ввг  ");

            Assert.AreEqual(1, storage.Count);
            Assert.AreEqual("ПВС", storage.GetAll()[0].Type);
        }

        //тестирование сортировки

        [TestMethod]
        public void SortByQuality_SortiruetPoUbyvaniyu()
        {
            CableStorage storage = new CableStorage();
            storage.Add("A", 4, 1.0, "Z", 10); 
            storage.Add("B", 2, 2.0, "Z", 10); 
            storage.Add("C", 5, 1.0, "Z", 10); 

            List<Cable> sorted = storage.SortByQuality();

            Assert.AreEqual("B", sorted[0].Type);
            Assert.AreEqual("A", sorted[1].Type);
            Assert.AreEqual("C", sorted[2].Type);
        }

        [TestMethod]
        public void SortByQuality_NeMenyaetIskhodnyiSpisok()
        {
            CableStorage storage = new CableStorage();
            storage.Add("A", 4, 1.0, "Z", 10);
            storage.Add("B", 2, 2.0, "Z", 10);

            storage.SortByQuality();

            Assert.AreEqual("A", storage.GetAll()[0].Type);
            Assert.AreEqual("B", storage.GetAll()[1].Type);
        }

        // тестирование Average

        [TestMethod]
        public void AveragePrice_SchitaetSrednee()
        {
            CableStorage storage = new CableStorage();
            storage.Add("A", 1, 1, "Z", 100);
            storage.Add("B", 1, 1, "Z", 200);
            storage.Add("C", 1, 1, "Z", 300);

            double avg = storage.AveragePrice();

            Assert.AreEqual(200, avg, 0.001);
        }

        [TestMethod]
        public void AveragePrice_PustoeHranilishche_VozvrashaetNol()
        {
            CableStorage storage = new CableStorage();

            double avg = storage.AveragePrice();

            Assert.AreEqual(0, avg, 0.001);
        }
    }
}