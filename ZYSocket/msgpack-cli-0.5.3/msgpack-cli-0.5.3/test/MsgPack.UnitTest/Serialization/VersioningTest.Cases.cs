#region -- License Terms --
//
// MessagePack for CLI
//
// Copyright (C) 2010-2012 FUJIWARA, Yusuke
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
#endregion -- License Terms --

using System;
using System.Linq;
using System.Runtime.Serialization;

#if !MSTEST
using NUnit.Framework;
#else
using TestFixtureAttribute = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using TestAttribute = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
using TimeoutAttribute = NUnit.Framework.TimeoutAttribute;
using Assert = NUnit.Framework.Assert;
using Is = NUnit.Framework.Is;
#endif

namespace MsgPack.Serialization
{
	partial class VersioningTest
	{
#if !NETFX_CORE

		[Test]
		public void TestExtraField_NotExtensible_Array_FieldBased_Fail()
		{
			TestExtraFieldCore<VersioningTestTarget>( SerializationMethod.Array, EmitterFlavor.FieldBased );
		}

		[Test]
		public void TestMissingField_Array_FieldBased_MissingIsTreatedAsNil()
		{
			TestMissingFieldCore( SerializationMethod.Array, EmitterFlavor.FieldBased );
		}

		[Test]
		public void TestFieldInvalidType_Array_FieldBased_Fail()
		{
			Assert.Throws<SerializationException>( () => TestFieldInvalidTypeCore( SerializationMethod.Array, EmitterFlavor.FieldBased ) );
		}
#endif // !NETFX_CORE
#if !NETFX_CORE

		[Test]
		public void TestExtraField_NotExtensible_Array_ContextBased_Fail()
		{
			TestExtraFieldCore<VersioningTestTarget>( SerializationMethod.Array, EmitterFlavor.ContextBased );
		}

		[Test]
		public void TestMissingField_Array_ContextBased_MissingIsTreatedAsNil()
		{
			TestMissingFieldCore( SerializationMethod.Array, EmitterFlavor.ContextBased );
		}

		[Test]
		public void TestFieldInvalidType_Array_ContextBased_Fail()
		{
			Assert.Throws<SerializationException>( () => TestFieldInvalidTypeCore( SerializationMethod.Array, EmitterFlavor.ContextBased ) );
		}
#endif // !NETFX_CORE
#if !NETFX_35

		[Test]
		public void TestExtraField_NotExtensible_Array_ExpressionBased_Fail()
		{
			TestExtraFieldCore<VersioningTestTarget>( SerializationMethod.Array, EmitterFlavor.ExpressionBased );
		}

		[Test]
		public void TestMissingField_Array_ExpressionBased_MissingIsTreatedAsNil()
		{
			TestMissingFieldCore( SerializationMethod.Array, EmitterFlavor.ExpressionBased );
		}

		[Test]
		public void TestFieldInvalidType_Array_ExpressionBased_Fail()
		{
			Assert.Throws<SerializationException>( () => TestFieldInvalidTypeCore( SerializationMethod.Array, EmitterFlavor.ExpressionBased ) );
		}
#endif // !NETFX_35
#if !NETFX_CORE

		[Test]
		public void TestExtraField_NotExtensible_Map_FieldBased_Fail()
		{
			TestExtraFieldCore<VersioningTestTarget>( SerializationMethod.Map, EmitterFlavor.FieldBased );
		}

		[Test]
		public void TestMissingField_Map_FieldBased_MissingIsTreatedAsNil()
		{
			TestMissingFieldCore( SerializationMethod.Map, EmitterFlavor.FieldBased );
		}

		[Test]
		public void TestFieldInvalidType_Map_FieldBased_Fail()
		{
			Assert.Throws<SerializationException>( () => TestFieldInvalidTypeCore( SerializationMethod.Map, EmitterFlavor.FieldBased ) );
		}

		[Test]
		public void TestFieldModified_Map_FieldBased_ExtraIsStoredAsExtensionData_MissingIsTreatedAsNil()
		{
			TestFieldSwappedCore( EmitterFlavor.FieldBased );
		}
#endif // !NETFX_CORE
#if !NETFX_CORE

		[Test]
		public void TestExtraField_NotExtensible_Map_ContextBased_Fail()
		{
			TestExtraFieldCore<VersioningTestTarget>( SerializationMethod.Map, EmitterFlavor.ContextBased );
		}

		[Test]
		public void TestMissingField_Map_ContextBased_MissingIsTreatedAsNil()
		{
			TestMissingFieldCore( SerializationMethod.Map, EmitterFlavor.ContextBased );
		}

		[Test]
		public void TestFieldInvalidType_Map_ContextBased_Fail()
		{
			Assert.Throws<SerializationException>( () => TestFieldInvalidTypeCore( SerializationMethod.Map, EmitterFlavor.ContextBased ) );
		}

		[Test]
		public void TestFieldModified_Map_ContextBased_ExtraIsStoredAsExtensionData_MissingIsTreatedAsNil()
		{
			TestFieldSwappedCore( EmitterFlavor.ContextBased );
		}
#endif // !NETFX_CORE
#if !NETFX_35

		[Test]
		public void TestExtraField_NotExtensible_Map_ExpressionBased_Fail()
		{
			TestExtraFieldCore<VersioningTestTarget>( SerializationMethod.Map, EmitterFlavor.ExpressionBased );
		}

		[Test]
		public void TestMissingField_Map_ExpressionBased_MissingIsTreatedAsNil()
		{
			TestMissingFieldCore( SerializationMethod.Map, EmitterFlavor.ExpressionBased );
		}

		[Test]
		public void TestFieldInvalidType_Map_ExpressionBased_Fail()
		{
			Assert.Throws<SerializationException>( () => TestFieldInvalidTypeCore( SerializationMethod.Map, EmitterFlavor.ExpressionBased ) );
		}

		[Test]
		public void TestFieldModified_Map_ExpressionBased_ExtraIsStoredAsExtensionData_MissingIsTreatedAsNil()
		{
			TestFieldSwappedCore( EmitterFlavor.ExpressionBased );
		}
#endif // !NETFX_35
	}

	public class VersioningTestTarget
	{
		[MessagePackMember( 0 )]
		public Int32 Field1 { get; set; }
		[MessagePackMember( 1 )]
		public Int32 Field2 { get; set; }
		[MessagePackMember( 2 )]
		public String Field3 { get; set; }
	}
}