using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Xml;
using UnityEngine;

namespace OculusProjektV0
{
    public class chunkPosSmall
    {
        public Vector3 worldAreaPosition;
        public smallChunk[,,] smallChunkerList;
        public chunkPosSmall[,,] chunkPositions;
        public int x;
        public int y;
        public int z;
        public byte[,,] map;
        int width = 60;
        int height = 10;
        int depth = 60;



        public chunkPosSmall(Vector3 position, int x, int y, int z)
        {
            worldAreaPosition = position;
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public byte GetByte(int x, int y, int z)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
            {
                return 0;
            }
            return map[x, y, z];
            //return map[x + width * (y + depth * z)];
        }
    }
}
