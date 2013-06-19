using System;
using System.Data;

namespace Rebuild.Utils
{
    public class DataReaderFieldInfo
    {
        internal DataReaderFieldInfo(int index, string name, Type fieldType)
        {
            Name = name;
            FieldType = fieldType;
            Index = index;
        }

        public Type FieldType { get; private set; }

        public int Index { get; private set; }

        public string Name { get; private set; }
    }
}
