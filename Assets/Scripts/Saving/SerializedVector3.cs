using System;
using UnityEngine;

namespace RPG.Saving
{
    [Serializable]
     public struct SerializedVector3
     {
          public float x;
          public float y;
          public float z;

          public SerializedVector3(Vector3 vector)
          {
               x = vector.x;
               y = vector.y;
               z = vector.z;
          }
     }
}