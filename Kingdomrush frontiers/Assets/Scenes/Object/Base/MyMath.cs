using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scenes.Object.Base
{
    static internal class MyMath
    {
        static public float GetAngleRadZ(Vector4 Value)
        {
            Vector4 AngleCheck = Value;
            AngleCheck.Normalize();

            float Result = Mathf.Acos(AngleCheck.x);

            if (AngleCheck.y < 0)
            {
                Result = (2.0f * Mathf.PI) - Result;
            }

            return Result;
        }
        static public float GetAngleDegZ(Vector4 Value)
        {
            return GetAngleRadZ(Value) * Mathf.Rad2Deg;
        }
        static public Vector4 CentimeterToMeter(Vector4 Centimeter)
        {
            return new Vector4(Centimeter.x / 100, Centimeter.y / 100, Centimeter.z / 100, Centimeter.w);
        }


    }
}
