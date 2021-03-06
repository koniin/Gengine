﻿using System;
using Microsoft.Xna.Framework;

namespace Gengine.MathUtils {
    public static class Calc2D {
        public static Vector2 GetRightPointingAngledPoint(int degrees) {
            double angle = System.Math.PI*degrees/180.0;
            double sinAngle = System.Math.Sin(angle);
            double cosAngle = System.Math.Cos(angle);

            float xAngle = (float) sinAngle;
            float yAngle = (float) cosAngle;

            return new Vector2(xAngle, yAngle);
        }

        public static Vector2 GetRandomDirection(Random random) {
            double azimuth = random.NextDouble() * 2 * System.Math.PI;
            return new Vector2((float) System.Math.Cos(azimuth), (float) System.Math.Sin(azimuth));
        }

        public static Vector2 GetAngledPoint(int degrees) {
            double angle = System.Math.PI*degrees/180.0;
            double sinAngle = System.Math.Sin(angle);
            double cosAngle = System.Math.Cos(angle);

            double xAngle = sinAngle;
            double yAngle = cosAngle;

            return new Vector2((float) xAngle, (float) yAngle);
        }
    }
}
