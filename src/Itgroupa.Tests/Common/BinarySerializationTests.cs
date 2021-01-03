using Itgroupa.Common;
using Itgroupa.Dto;
using NUnit.Framework;

namespace Itgroupa.Tests.Common
{
    public class BinarySerializationTests
    {
        [Test]
        public void ToByteArrayTest()
        {
            var obj = DataGenerator.GetPrice();
            var byteData = GetBytes(obj);
            
            Assert.IsTrue(byteData.Length != 0);
        }
        
        [Test]
        public void GetStringTest()
        {
            var obj = DataGenerator.GetPrice();
            
            var byteData = GetBytes(obj);

            var strDataNew = BinarySerialization.GetString(byteData);
            var objNew = JsonSerialization.GetObject<Price>(strDataNew);
            
            Assert.IsTrue(objNew.Ask.Equals(obj.Ask));
        }

        private static byte[] GetBytes(Price obj)
        {
            var strData = JsonSerialization.ToString(obj);
            var byteData = BinarySerialization.ToByteArray(strData);

            return byteData;
        }
    }
}