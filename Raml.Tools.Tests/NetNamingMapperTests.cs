using NUnit.Framework;
using Raml.Common;

namespace Raml.Tools.Tests
{
	[TestFixture]
	public class NetNamingMapperTests
	{
		[Test]
		public void Should_Convert_Object_Names()
		{
			Assert.AreEqual("GetSalesId", NetNamingMapper.GetObjectName("get-/sales/{id}"));
		}

		[Test]
		public void Should_Convert_Method_Names()
		{
			Assert.AreEqual("GetContactsById", NetNamingMapper.GetMethodName("get-/contacts/{id}"));
		}

		[Test]
		public void Should_Convert_Property_Names()
		{
			Assert.AreEqual("XRateMediaAbcDef", NetNamingMapper.GetPropertyName("X-Rate-Media:Abc/Def"));
		}

        [Test]
        public void Should_Remove_MediaTypeExtension_From_Object_Name()
        {
            Assert.AreEqual("Users", NetNamingMapper.GetObjectName("users{mediaTypeExtension}"));
        }

        [Test]
        public void Should_Remove_MediaTypeExtension_From_Method_Name()
        {
            Assert.AreEqual("Users", NetNamingMapper.GetObjectName("users{mediaTypeExtension}"));
        }

        [Test]
        public void Should_Avoid_Parentheses_In_Object_Name()
        {
            Assert.AreEqual("GetSalesId", NetNamingMapper.GetObjectName("get-/sales({id})"));
            Assert.AreEqual("GetSalesId", NetNamingMapper.GetObjectName("get-/sales(id)"));
        }

        [Test]
        public void Should_Avoid_Parentheses_In_Method_Name()
        {
            Assert.AreEqual("GetSalesById", NetNamingMapper.GetMethodName("get-/sales({id})"));
            Assert.AreEqual("GetSalesId", NetNamingMapper.GetMethodName("get-/sales(id)"));
        }

        [Test]
        public void Should_Avoid_Single_Quote_In_Object_Name()
        {
            Assert.AreEqual("GetSalesId", NetNamingMapper.GetObjectName("get-/sales('{id}')"));
            Assert.AreEqual("GetSalesId", NetNamingMapper.GetObjectName("get-/sales'id'"));
        }

        [Test]
        public void Should_Avoid_Single_Quote_In_Method_Name()
        {
            Assert.AreEqual("GetSalesById", NetNamingMapper.GetMethodName("get-/sales('{id}')"));
            Assert.AreEqual("GetSalesId", NetNamingMapper.GetMethodName("get-/sales'id'"));
        }

	}
}