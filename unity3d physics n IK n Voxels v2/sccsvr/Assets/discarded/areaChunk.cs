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
    public class areaChunk
    {
       
        //public SharpDX.Vector3 worldAreaPosition;
        //public areaChunk[,,] areaChunkList = new areaChunk[10, 10, 10];
        areaChunk areaChunker;

        public bigChunk[,,] bigChunker;

        public bigChunk[,,] bigChunkArray;

        //public bigChunk[,,] bigChunkerArray { get; set; }
        public areaChunk[,,] areaChunkList = new areaChunk[10, 10, 10];


        public areaChunk()
        {

            //counter++;
        }









        //public areaChunk areaChunkers { get; set; }

        /*public void setAreaChunkBigChunkArray(int x, int y, int z, bigChunk[,,] arrayOfBigChunk)
        {
            areaChunker.areaChunkList[x, y, z].bigChunkArray = arrayOfBigChunk;
        }*/



        /*public bigChunk this[int x, int y, int z] {
            get
            {
                return bigChunker[x,y,z];
                    
            } set

            {
                bigChunker[x,y,z] = this[x, y, z];
            }
        }*/


        /*public areaChunk(bigChunk[,,] arrayOfBigChunk,int x,int y, int z)
        {
            arrayOfBigChunk = new bigChunk[100, 100, 100];

            areaChunkList[x, y, z].bigChunkArray = arrayOfBigChunk;

        }*/

        public areaChunk this[int x, int y, int z]
        {

            get { return areaChunkList[x,y,z];}

            set { areaChunkList[x, y, z] = this[x, y, z]; }
        }


        /*public bigChunk[,,] this[int x, int y, int z]
        {
            get { return areaChunkList[x, y, z]; }

            //get { return _entry.oA.getVal3(_entry.listB[index]); }
            //set { _entry.oA.setVal3(_entry.listB[index], value); }

            set { areaChunkList[x, y, z] = this[x, y, z]; }
        }*/








        /*public int this[int index]
        {
            get { return _entry.oA.getVal3(_entry.listB[index]); }
            set { _entry.oA.setVal3(_entry.listB[index], value); }
        }*/




        //public string Type { get; set; }
        //public decimal Price { get; set; }
        //public int Amount { get; set; }


        /*public areaChunk()
        {
            //areaChunker = new areaChunk();
        }

        public areaChunk getAreaChunk(int x, int y, int z)
        {
            return areaChunkList[x, y, z];
        }

        public void setAreaChunk(int x, int y, int z)
        {
            areaChunkList[x, y, z] = new areaChunk();
        }

        public void assignBigChunk(int x, int y, int z,bigChunk[,,] bigChunk)
        {
            areaChunkList[x, y, z].bigChunkArray = bigChunk;
            //bigChunk = new bigChunk().bigChunkList
            //areaChunkList[x, y, z] = bigChunk;
        }

        public bigChunk getBigChunkInAreaChunk(int areaX, int areaY, int areaZ, int x, int y, int z)
        {
            /*if (areaChunker.bigChunkArray[x, y, z] != null)
            {
                return areaChunker.bigChunkArray[x, y, z];
            }

            return areaChunkList[areaX, areaY, areaZ].bigChunkArray[x, y, z];
        }

        public void setBigChunkInAreaChunk(int areaX, int areaY, int areaZ, int x, int y, int z)
        {
            areaChunkList[areaX, areaY, areaZ].bigChunkArray[x, y, z] = new bigChunk();
        }*/

        /*public areaChunk getAreaChunkWithPosition(int x, int y, int z)
        {
            return areaChunkList[x, y, z];
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
