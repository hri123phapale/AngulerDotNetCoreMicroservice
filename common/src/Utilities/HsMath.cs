using System;

namespace Blogposts.Common.Utilities
{
    public static class HsMath
    {
        /// <summary>
        /// Clamps a value between min and max
        /// </summary>
        public static int Clamp(int num, int min = 0, int max = 100)
        {
            return Math.Max(min, Math.Min(max, num));
        }

        /// <summary>
        /// Clamp a double value between min and max
        /// </summary>
        public static double Clamp(double num, double min = 0, double max = 100)
        {
            return Math.Max(min, Math.Min(max, num));
        }

        /// <summary>
        /// Clamp float between min and max values
        /// </summary>
        public static float Clamp(float num, float min = 0, float max = 100)
        {
            return Math.Max(min, Math.Min(max, num));
        }

        /// <summary>
        /// Gets remainder that is non-negative, even if the first number is negative
        /// </summary>
        public static float EuclideanModulo(float a, float b)
        {
            return (a % b + b) % b;
        }

        /// <summary>
        /// Gets remainder that is non-negative, even if the first number is negative
        /// </summary>
        public static float EuclideanModulo(int a, int b)
        {
            return (a % b + b) % b;
        }
    }
}