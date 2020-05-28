using System;
using System.Collections.Generic;
using Dolany.UtilityTool;
using NUnit.Framework;

namespace Dolany.ToolTest
{
    public class SafeConvertUT
    {
        [SetUp]
        public void Setup()
        {
        }

        public class ObjectToDictionaryTestClass
        {
            public int IntProperty { get; set; } = 1;

            public string StrProperty { get; } = "str";

            public Type ClassProperty { get; } = typeof(string);

            public double DField;

            public string MethodProperty => Math.Abs(-20).ToStringSafe();
        }

        [Test]
        public void ObjectToDictionaryUT()
        {
            var testObj = new ObjectToDictionaryTestClass();

            var dictionary = testObj.ObjectToDictionary();

            Assert.True(!dictionary.IsNullOrEmpty());

            Assert.True(dictionary.ContainsKey("IntProperty"));
            Assert.AreEqual(1, dictionary["IntProperty"]);

            Assert.True(dictionary.ContainsKey("StrProperty"));
            Assert.AreEqual("str", dictionary["StrProperty"]);

            Assert.True(dictionary.ContainsKey("ClassProperty"));
            Assert.AreEqual(typeof(string), dictionary["ClassProperty"]);

            Assert.True(!dictionary.ContainsKey("DField"));

            Assert.True(dictionary.ContainsKey("MethodProperty"));
            Assert.AreEqual("20", dictionary["MethodProperty"]);
        }

        [Test]
        public void DictionaryToObjectUT()
        {
            var dic = new Dictionary<string, object>()
            {
                {"IntProperty", 10},
                {"StrProperty", "DicStr"},
                {"DField", 2.63},
                {"MethodProperty", "10"},
                {"OtherField", TimeSpan.FromHours(1)}
            };

            var obj = dic.DictionaryToObject<ObjectToDictionaryTestClass>();

            Assert.True(obj != null);

            Assert.AreEqual(10, obj.IntProperty);
            Assert.AreEqual("str", obj.StrProperty);
            Assert.AreEqual(typeof(string), obj.ClassProperty);
            Assert.AreEqual(0, obj.DField);
            Assert.AreEqual("20", obj.MethodProperty);
        }

        [Test]
        public void ToDateTimeSafe_LocalUT01()
        {
            var time = DateTime.Now.ToUniversalTime();

            var convertedTime = time.ToDateTimeSafe_Local();

            Assert.AreEqual(time.ToLocalTime(), convertedTime);
        }

        [Test]
        public void ToDateTimeSafe_LocalUT02()
        {
            var time = DateTime.Now;

            var convertedTime = time.ToDateTimeSafe_Local();

            Assert.AreEqual(time, convertedTime);
        }

        [Test]
        public void ToDateTimeSafe_LocalUT03()
        {
            var now = DateTime.Now;
            var time = now.ToString("yyyy-MM-ddTHH:mm:ss");

            var convertedTime = time.ToDateTimeSafe_Local();

            Assert.AreEqual(time, convertedTime.ToString("yyyy-MM-ddTHH:mm:ss"));
        }

        [Test]
        public void ToDateTimeSafe_LocalUT04()
        {
            var now = DateTime.Now;
            var time = now.ToString("yyyy-MM-dd HH:mm:ss");

            var convertedTime = time.ToDateTimeSafe_Local();

            Assert.AreEqual(time, convertedTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}