using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Utils
{
    public class Utils
    {
        public static float GetHorizontalScreenBounds()
        {
            if (Camera.main == null)
            {
                return 0;
            }

            float screenWidth = (Camera.main.orthographicSize * 2) * Camera.main.aspect;
            return screenWidth / 2;
        }
    }
}
