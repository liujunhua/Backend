﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.34014
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MsgPack.Serialization.GeneratedSerializers.MapBased {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("MsgPack.Serialization.CodeDomSerializers.CodeDomSerializerBuilder", "0.5.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public class MsgPack_Serialization_ComplexTypeGeneratedEnclosureSerializer : MsgPack.Serialization.MessagePackSerializer<MsgPack.Serialization.ComplexTypeGeneratedEnclosure> {
        
        private MsgPack.Serialization.MessagePackSerializer<string> _serializer0;
        
        private MsgPack.Serialization.MessagePackSerializer<MsgPack.Serialization.ComplexTypeGenerated> _serializer1;
        
        private System.Reflection.MethodBase _methodBaseComplexType_get_History0;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_AddOnlyCollection_DateTimeField1;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_AddOnlyCollection_MessagePackObjectField2;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_AddOnlyCollection_ObjectField3;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ArrayListField4;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_Collection_MessagePackObjectField5;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_CollectionDateTimeField6;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_CollectionObjectField7;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_Dictionary_MessagePackObject_MessagePackObjectField8;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_DictionaryObjectObjectField9;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_DictionaryStringDateTimeField10;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_HashSet_MessagePackObjectField11;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_HashSetDateTimeField12;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_HashSetObjectField13;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_HashtableField14;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ICollection_MessagePackObjectField15;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ICollectionDateTimeField16;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ICollectionObjectField17;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_IDictionary_MessagePackObject_MessagePackObjectField18;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_IDictionaryObjectObjectField19;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_IDictionaryStringDateTimeField20;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_IList_MessagePackObjectField21;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_IListDateTimeField22;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_IListObjectField23;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ISet_MessagePackObjectField24;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ISetDateTimeField25;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ISetObjectField26;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_List_MessagePackObjectField27;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ListDateTimeField28;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ListObjectField29;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ObservableCollection_MessagePackObjectField30;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ObservableCollectionDateTimeField31;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_ObservableCollectionObjectField32;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_StringKeyedCollection_DateTimeField33;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_StringKeyedCollection_MessagePackObjectField34;
        
        private System.Reflection.MethodBase _methodBaseComplexTypeGenerated_get_StringKeyedCollection_ObjectField35;
        
        public MsgPack_Serialization_ComplexTypeGeneratedEnclosureSerializer(MsgPack.Serialization.SerializationContext context) : 
                base(context) {
            this._serializer0 = context.GetSerializer<string>();
            this._serializer1 = context.GetSerializer<MsgPack.Serialization.ComplexTypeGenerated>();
            this._methodBaseComplexType_get_History0 = typeof(MsgPack.Serialization.ComplexType).GetMethod("get_History", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_AddOnlyCollection_DateTimeField1 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_AddOnlyCollection_DateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_AddOnlyCollection_MessagePackObjectField2 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_AddOnlyCollection_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_AddOnlyCollection_ObjectField3 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_AddOnlyCollection_ObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ArrayListField4 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ArrayListField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_Collection_MessagePackObjectField5 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_Collection_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_CollectionDateTimeField6 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_CollectionDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_CollectionObjectField7 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_CollectionObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_Dictionary_MessagePackObject_MessagePackObjectField8 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_Dictionary_MessagePackObject_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_DictionaryObjectObjectField9 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_DictionaryObjectObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_DictionaryStringDateTimeField10 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_DictionaryStringDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_HashSet_MessagePackObjectField11 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_HashSet_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_HashSetDateTimeField12 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_HashSetDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_HashSetObjectField13 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_HashSetObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_HashtableField14 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_HashtableField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ICollection_MessagePackObjectField15 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ICollection_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ICollectionDateTimeField16 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ICollectionDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ICollectionObjectField17 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ICollectionObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_IDictionary_MessagePackObject_MessagePackObjectField18 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_IDictionary_MessagePackObject_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_IDictionaryObjectObjectField19 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_IDictionaryObjectObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_IDictionaryStringDateTimeField20 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_IDictionaryStringDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_IList_MessagePackObjectField21 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_IList_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_IListDateTimeField22 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_IListDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_IListObjectField23 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_IListObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ISet_MessagePackObjectField24 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ISet_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ISetDateTimeField25 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ISetDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ISetObjectField26 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ISetObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_List_MessagePackObjectField27 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_List_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ListDateTimeField28 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ListDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ListObjectField29 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ListObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ObservableCollection_MessagePackObjectField30 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ObservableCollection_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ObservableCollectionDateTimeField31 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ObservableCollectionDateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_ObservableCollectionObjectField32 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_ObservableCollectionObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_StringKeyedCollection_DateTimeField33 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_StringKeyedCollection_DateTimeField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_StringKeyedCollection_MessagePackObjectField34 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_StringKeyedCollection_MessagePackObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
            this._methodBaseComplexTypeGenerated_get_StringKeyedCollection_ObjectField35 = typeof(MsgPack.Serialization.ComplexTypeGenerated).GetMethod("get_StringKeyedCollection_ObjectField", (System.Reflection.BindingFlags.Instance 
                            | (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)), null, new System.Type[0], null);
        }
        
        protected internal override void PackToCore(MsgPack.Packer packer, MsgPack.Serialization.ComplexTypeGeneratedEnclosure objectTree) {
            packer.PackMapHeader(2);
            this._serializer0.PackTo(packer, "Name");
            this._serializer0.PackTo(packer, objectTree.Name);
            this._serializer0.PackTo(packer, "Nested");
            this._serializer1.PackTo(packer, objectTree.Nested);
        }
        
        protected internal override MsgPack.Serialization.ComplexTypeGeneratedEnclosure UnpackFromCore(MsgPack.Unpacker unpacker) {
            MsgPack.Serialization.ComplexTypeGeneratedEnclosure result = default(MsgPack.Serialization.ComplexTypeGeneratedEnclosure);
            result = new MsgPack.Serialization.ComplexTypeGeneratedEnclosure();
            if (unpacker.IsArrayHeader) {
                int unpacked = default(int);
                int itemsCount = default(int);
                itemsCount = MsgPack.Serialization.UnpackHelpers.GetItemsCount(unpacker);
                string nullable = default(string);
                if ((unpacked < itemsCount)) {
                    nullable = MsgPack.Serialization.UnpackHelpers.UnpackStringValue(unpacker, typeof(MsgPack.Serialization.ComplexTypeGeneratedEnclosure), "System.String Name");
                }
                if (((nullable == null) 
                            == false)) {
                    result.Name = nullable;
                }
                unpacked = (unpacked + 1);
                MsgPack.Serialization.ComplexTypeGenerated nullable0 = default(MsgPack.Serialization.ComplexTypeGenerated);
                if ((unpacked < itemsCount)) {
                    if ((unpacker.Read() == false)) {
                        throw MsgPack.Serialization.SerializationExceptions.NewMissingItem(1);
                    }
                    if (((unpacker.IsArrayHeader == false) 
                                && (unpacker.IsMapHeader == false))) {
                        nullable0 = this._serializer1.UnpackFrom(unpacker);
                    }
                    else {
                        MsgPack.Unpacker disposable = default(MsgPack.Unpacker);
                        disposable = unpacker.ReadSubtree();
                        try {
                            nullable0 = this._serializer1.UnpackFrom(disposable);
                        }
                        finally {
                            if (((disposable == null) 
                                        == false)) {
                                disposable.Dispose();
                            }
                        }
                    }
                }
                if (((nullable0 == null) 
                            == false)) {
                    result.Nested = nullable0;
                }
                unpacked = (unpacked + 1);
            }
            else {
                int itemsCount0 = default(int);
                itemsCount0 = MsgPack.Serialization.UnpackHelpers.GetItemsCount(unpacker);
                for (int i = 0; (i < itemsCount0); i = (i + 1)) {
                    string key = default(string);
                    string nullable1 = default(string);
                    nullable1 = MsgPack.Serialization.UnpackHelpers.UnpackStringValue(unpacker, typeof(MsgPack.Serialization.ComplexTypeGeneratedEnclosure), "MemberName");
                    if (((nullable1 == null) 
                                == false)) {
                        key = nullable1;
                    }
                    else {
                        throw MsgPack.Serialization.SerializationExceptions.NewNullIsProhibited("MemberName");
                    }
                    if ((key == "Nested")) {
                        MsgPack.Serialization.ComplexTypeGenerated nullable3 = default(MsgPack.Serialization.ComplexTypeGenerated);
                        if ((unpacker.Read() == false)) {
                            throw MsgPack.Serialization.SerializationExceptions.NewMissingItem(i);
                        }
                        if (((unpacker.IsArrayHeader == false) 
                                    && (unpacker.IsMapHeader == false))) {
                            nullable3 = this._serializer1.UnpackFrom(unpacker);
                        }
                        else {
                            MsgPack.Unpacker disposable0 = default(MsgPack.Unpacker);
                            disposable0 = unpacker.ReadSubtree();
                            try {
                                nullable3 = this._serializer1.UnpackFrom(disposable0);
                            }
                            finally {
                                if (((disposable0 == null) 
                                            == false)) {
                                    disposable0.Dispose();
                                }
                            }
                        }
                        if (((nullable3 == null) 
                                    == false)) {
                            result.Nested = nullable3;
                        }
                    }
                    else {
                        if ((key == "Name")) {
                            string nullable2 = default(string);
                            nullable2 = MsgPack.Serialization.UnpackHelpers.UnpackStringValue(unpacker, typeof(MsgPack.Serialization.ComplexTypeGeneratedEnclosure), "System.String Name");
                            if (((nullable2 == null) 
                                        == false)) {
                                result.Name = nullable2;
                            }
                        }
                    }
                }
            }
            return result;
        }
        
        private static T @__Conditional<T>(bool condition, T whenTrue, T whenFalse)
         {
            if (condition) {
                return whenTrue;
            }
            else {
                return whenFalse;
            }
        }
    }
}