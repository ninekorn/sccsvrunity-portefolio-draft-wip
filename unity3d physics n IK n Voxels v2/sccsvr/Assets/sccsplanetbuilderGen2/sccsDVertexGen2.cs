using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class sccsDVertex
{

    [StructLayout(LayoutKind.Sequential)]
    public struct DVertex
    {
        public Vector3 position;
        public Vector2 texture;
        public Vector4 color;
        public Vector3 normal;
    };
}
