using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Utils
{
    public class Utils
    {
        /// <summary>
        /// Get the value for the horizontal bounds in world space
        /// </summary>
        /// <returns></returns>
        public static float GetHorizontalScreenBounds()
        {
            if (Camera.main == null)
            {
                return 0;
            }

            float screenWidth = (Camera.main.orthographicSize * 2) * Camera.main.aspect;
            return screenWidth / 2;
        }

        /// <summary>
        /// Return the given number as a positive number
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float Pos(float x)
        {
            return Mathf.Sqrt(x * x);
        }
    }
}
