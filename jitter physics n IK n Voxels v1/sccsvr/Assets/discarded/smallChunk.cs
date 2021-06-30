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
    public class smallChunk
    {
        public Vector3 worldPosition;
        private int width = 10;
        private int height = 1;
        private int depth = 10;
        public byte[,,] map;
        smallChunk chuk;

        public Vector3[] positionz;
        public Vector3[] normalz;
        public Vector2[] textureCoordinatez;
        public int[] triangleIndicez;
        //public MeshObjectNode meshObjNode;
        //public SceneNodeVisual3D sceneNoderVisual3d;

        public static smallChunk[] arrayOfSmallChunks = new smallChunk[2 * 2 * 2];

        public static int counter = 0;      

        public smallChunk()
        {
            //arrayOfSmallChunks[counter++] = this;
        }

        public void whatever(Vector3[] positions, Vector3[] normals, Vector2[] textureCoordinates, int[] triangleIndices, int x, int y, int z)
        {
            //threadInfinite.bigChunko.smallChunkerList[x, y, z].positionz = positions;
            //threadInfinite.bigChunko.smallChunkerList[x, y, z].normalz = normals;
            //threadInfinite.bigChunko.smallChunkerList[x, y, z].textureCoordinatez = textureCoordinates;
            //threadInfinite.bigChunko.smallChunkerList[x, y, z].triangleIndicez = triangleIndices;
        }




        //public smallChunk[,,] chunkSmallList = new smallChunk[10, 100, 10];

        /*public smallChunk(Vector3 worldPos, int x, int y, int z)
        {
            chunkSmallList[x, y, z] = this;
            chunkSmallList[x, y, z].worldPosition = worldPos;
        }

        public smallChunk getSmallChunkTop(int x, int y, int z)
        {
            return chunkSmallList[x, y + 1, z];
        }

        public byte GetByte(int x, int y, int z)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
            {
                return 0;
            }
            return this.map[x, y, z];
            //return map[x + width * (y + depth * z)];
        }*/
    }
}
