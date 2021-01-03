using Itgroupa.Common;
using Itgroupa.Dto;
using NUnit.Framework;

namespace Itgroupa.Tests.Common
{
    public class JsonSerializationTests
    {
        [Test]
        public void ToStringTest()
        {
            var data = JsonSerialization.ToString(DataGenerator.GetPrice());
            
            Assert.IsTrue(!string.IsNullOrEmpty(data));
        }
        
        [Test]
        public void GetObjectTest()
        {
            var obj = DataGenerator.GetPrice();
            var strData = JsonSerialization.ToString(obj);

            var newObj = JsonSerialization.GetObject<Price>(strData);
            
            Assert.IsTrue(obj.Ask.Equals(newObj.Ask));
        }
    }
}