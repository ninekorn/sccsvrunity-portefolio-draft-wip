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
    public class bigChunk
    {
        public Vector3 worldAreaPosition;
        public smallChunk[,,] smallChunkerList;

        areaChunk areaChunker;

        public bigChunk[,,] bigChunker;

        public bigChunk[,,] bigChunkArray;

        //public bigChunk[,,] bigChunkerArray { get; set; }
        //public areaChunk[,,] areaChunkList = new areaChunk[10, 10, 10];

        //public areaChunk areaChunkers { get; set; }

        /*public void setAreaChunkBigChunkArray(int x, int y, int z, bigChunk[,,] arrayOfBigChunk)
        {
            areaChunker.areaChunkList[x, y, z].bigChunkArray = arrayOfBigChunk;
        }*/




        /*public bigChunk getBigChunk(float xi, float yi, float zi)
        {
            int x = (int)xi;
            int y = (int)yi;
            int z = (int)zi;

            if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= width) || (z >= width))
            {
                return null;
            }
            return bigchunk[x, y, z];
        }*/
    }
}
