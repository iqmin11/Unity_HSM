using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scenes.Object.Base
{
    internal class MyDeserializer
    {
        private Array Data;
        UInt64 Offset = 0;

        public void ReadFile(byte[] File)
        {
            Data = File;
        }

        public void Read(ref int Value)
        {
            byte[] intBytes = BitConverter.GetBytes(Value);
            Read(intBytes, sizeof(int));
            Value = BitConverter.ToInt32(intBytes);
        }

        public void Read(ref float Value)
        {
            byte[] floatBytes = BitConverter.GetBytes(Value);
            Read(floatBytes, sizeof(float));
            Value = BitConverter.ToSingle(floatBytes);
        }

        //void Read(string Value)
        //{

        //}

        public void Read(Array Dest, int Size)
        {
            Buffer.BlockCopy(Data, (int)Offset, Dest, 0, Size);
            Offset += (UInt64)Size;
        }

    }
}
