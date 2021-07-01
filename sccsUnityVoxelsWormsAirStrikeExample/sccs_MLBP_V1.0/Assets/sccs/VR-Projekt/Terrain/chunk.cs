using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Text;
using UnityEngine;


public class chunk
{
    private static chunk chunker;
    private int width = 10;
    private int height = 10;
    private int depth = 10;
    //public byte[] map;
    private byte[,,] map;
    private float planeSize = 0.1f;
    private int seed = 3420;

    private byte block;

    private Vector3[] positions;
    private Vector3[] normals;
    private Vector2[] textureCoordinates;
    private int[] triangleIndices;

    private int counterVertexTop = 0;
    private int counterVertexBottom = 0;
    private int counterVertexRight = 0;
    private int counterVertexLeft = 0;
    private int counterVertexFront = 0;
    private int counterVertexBack = 0;

    private int vertzIndex = 0;
    private int trigsIndex = 0;

    private int detailScale = 5;
    private int heightScale = 5;

    private Vector3 forward = new Vector3(0, 0, 1);
    private Vector3 back = new Vector3(0, 0, -1);
    private Vector3 right = new Vector3(1, 0, 0);
    private Vector3 left = new Vector3(-1, 0, 0);
    private Vector3 up = new Vector3(0, 1, 0);
    private Vector3 down = new Vector3(0, -1, 0);

    private smallChunker chuking;

    ///bigChunk chunkero;
    smallChunker[,,] smallChunker = new smallChunker[1, 1, 1];

    public static int countingArrayOfChunks = 0;

    Mesh mesh;
    MeshFilter meshFilt;
    MeshRenderer meshRend;
    GameObject currentChunk;
    private Texture mat;


    public meshData startBuildingArray(Vector3 currentPosition, Vector3 fakePos)
    {
        //Console.WriteLine("yo000");
        map = new byte[width, height, depth];

        for (int x = 0; x < width; x++)
        {
            float noiseX = Math.Abs(((float)(x * planeSize + currentPosition.x + seed) / detailScale) * heightScale);

            for (int y = 0; y < height; y++)
            {
                float noiseY = Math.Abs(((float)(y * planeSize + currentPosition.y + seed) / detailScale) * heightScale);

                for (int z = 0; z < depth; z++)
                {
                    float noiseZ = Math.Abs(((float)(z * planeSize + currentPosition.z + seed) / detailScale) * heightScale);

                    //float noiser = Noise.Generate(noiseX, noiseY, noiseZ);

                    float temporaryY = 10f;
                    float temporaryZ = 10f;
                    float temporaryX = 10f;

                    temporaryY *= Mathf.PerlinNoise((x * planeSize + currentPosition.x + seed) / detailScale, (z * planeSize + currentPosition.z + seed) / detailScale) * heightScale;
                    temporaryX *= Mathf.PerlinNoise((y * planeSize + currentPosition.y + seed) / detailScale, (z * planeSize + currentPosition.z + seed) / detailScale) * heightScale;
                    temporaryZ *= Mathf.PerlinNoise((x * planeSize + currentPosition.x + seed) / detailScale, (y * planeSize + currentPosition.y + seed) / detailScale) * heightScale;

                    float size0 = (1 / planeSize) * currentPosition.y;
                    temporaryY -= size0;

                    float size1 = (1 / planeSize) * currentPosition.x;
                    temporaryX -= size1;

                    float size2 = (1 / planeSize) * currentPosition.z;
                    temporaryZ -= size2;

                    /*if ((int)Math.Round(temporaryY) >= y && (int)Math.Round(temporaryX) >= x && (int)Math.Round(temporaryZ) >= z)
                    {
                        map[x, y, z] = 1;
                    }*/

                    if (currentPosition.y < 1)
                    {
                        map[x, 0, z] = 1;
                    }


                    if ((int)Math.Round(temporaryY) >= y)
                    {
                        map[x, y, z] = 1;
                    }

                    block = map[x, y, z];
                    if (block == 0) continue;
                    {
                        calculateNumberOfVertex(x, y, z);
                    }
                }
            }
        }









        positions = new Vector3[counterVertexTop * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4];
        //normals = new Vector3[counterVertexTop * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4];
        //textureCoordinates = new Vector2[counterVertexTop * 4 + counterVertexBottom * 4 + counterVertexRight * 4 + counterVertexLeft * 4 + counterVertexFront * 4 + counterVertexBack * 4];
        triangleIndices = new int[counterVertexTop * 6 + counterVertexBottom * 6 + counterVertexRight * 6 + counterVertexLeft * 6 + counterVertexFront * 6 + counterVertexBack * 6];

        Regenerate(currentPosition);





        //currentChunk = new GameObject();
        //mesh = new Mesh();
        //mesh.Clear();
        //currentChunk.AddComponent<MeshFilter>().mesh = mesh;

        //string texture = "Assets/Resources/Textures/green";
        //mat = Resources.Load(texture, typeof(Texture)) as Texture;
        //currentChunk.AddComponent<MeshRenderer>().material.mainTexture = mat;
        //mesh.vertices = positions.ToArray();
        //mesh.triangles = triangleIndices.ToArray();
        ///mesh.RecalculateNormals();
        //currentChunk.transform.position = position;
        if (positions.Length > 0)
        {
            /*int xx = (int)(chunkBig.currentChunkPos.x / 60);
            int yy = (int)(chunkBig.currentChunkPos.y / 10);
            int zz = (int)(chunkBig.currentChunkPos.z / 60);

            chunkBig.chunkBig.smallChunkerList[xx, yy, zz].map = map;*/

            return new meshData(fakePos, currentPosition, positions.Length, positions, triangleIndices);
        }
        else
        {
            return new meshData(fakePos, currentPosition, 0, positions, triangleIndices);
        }
    }

    public void calculateNumberOfVertex(int x, int y, int z)
    {

        //TOPFACE
        if (IsTransparent(x, y + 1, z))
        {
            counterVertexTop += 1;

        }
        //LEFTFACE
        if (IsTransparent(x - 1, y, z))
        {
            counterVertexLeft += 1;
        }
        //RIGHTFACE
        if (IsTransparent(x + 1, y, z))
        {
            counterVertexRight += 1;
        }
        //FRONTFACE
        if (IsTransparent(x, y, z - 1))
        {
            counterVertexFront += 1;
        }
        //BACKFACE
        if (IsTransparent(x, y, z + 1))
        {
            counterVertexBack += 1;
        }
        //BOTTOMFACE
        if (IsTransparent(x, y - 1, z))
        {
            counterVertexBottom += 1;
        }
    }
    public void Regenerate(Vector3 currentPosition)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    block = map[x, y, z];

                    if (block == 0) continue;
                    {
                        DrawBrick(x, y, z, currentPosition);
                    }
                }
            }
        }


    }

    //chunkPosBig chunkbig;

    public void DrawBrick(int x, int y, int z, Vector3 currentPosition)
    {

        Vector3 start = new Vector3(x * planeSize, y * planeSize, z * planeSize);
        Vector3 offset1, offset2;

        //int xx = (int)(chunkBig.currentChunkPos.x);
        //int yy = (int)(chunkBig.currentChunkPos.y);
        //int zz = (int)(chunkBig.currentChunkPos.z);


        /*if (endlessTerrain.terrainChunkDictionary.ContainsKey(new Vector3(currentPosition.x, currentPosition.y+0.1f, currentPosition.z)))
        {
            UnityEngine.Debug.Log("yo");
        }*/





        /*if (y == 0)
        {
            //chunkBig[]
            var yo = chunkBig.chunkBig.smallChunkerList[xx, yy+1, zz];
            if (yo != null)
            {
                if (yo.GetByte(x, 0, z) == 0)
                {
                    //TOPFACE
                    if (IsTransparent(x, y + 1, z))
                    {
                        offset1 = forward * planeSize;
                        offset2 = right * planeSize;
                        createTopFace(start + up * planeSize, offset1, offset2);
                        vertzIndex += 4;
                        trigsIndex += 6;
                    }
                }
            }
        }*/







        //chunker[smallChunk.worldPosition.X, smallChunk.worldPosition.Y, smallChunk.worldPosition.Z]

        //smallChunk chjuklfjwl = InfiniteUniverse.getChunk(posChunkTopX, posChunkTopY, posChunkTopZ);

        /*if (y == 0)
        {
            var smallChunkTop = smallChunk.chunkSmallList[posChunkTopX, posChunkTopY+1, posChunkTopZ];
            /*if (smallChunkTop == null)
            {
                //if (yoman.GetByte(x, 0, z) == 0)
                //{
                //TOPFACE
                if (IsTransparent(x, y + 1, z))
                {
                    offset1 = forward * planeSize;/
                    offset2 = right * planeSize;
                    createTopFace(start + up * planeSize, offset1, offset2);
                    vertzIndex += 4;
                    trigsIndex += 6;
                }
                //}
            }

            if (smallChunkTop != null)
            {
                if (smallChunk.getSmallChunkTop(posChunkTopX, posChunkTopY, posChunkTopZ).GetByte(x, 0, z) == 0)
                {
                    //TOPFACE
                    if (IsTransparent(x, y + 1, z))
                    {
                        offset1 = forward * planeSize;
                        offset2 = right * planeSize;
                        createTopFace(start + up * planeSize, offset1, offset2);
                        vertzIndex += 4;
                        trigsIndex += 6;
                    }
                }
            }
        }*/

























        /* if (y == height-1 && infini.getBigChunk(new Vector3(currentPosition.X, currentPosition.Y-10, currentPosition.Z), chunker) != null)
         {
             var chuk = infini.getBigChunk(new Vector3(currentPosition.X, currentPosition.Y - 10, currentPosition.Z), chunker);

             if (chuk.chunkerList[xPoser,yPoser,zPoser].GetByte(x, y, width - 1) == 0)
             {
                 //TOPFACE
                 if (IsTransparent(x, y + 1, z))
                 {
                     offset1 = forward * planeSize;
                     offset2 = right * planeSize;
                     createTopFace(start + up * planeSize, offset1, offset2);
                     vertzIndex += 4;
                     trigsIndex += 6;
                 }
             }
         }*/
        /*else if (y != 0)
        {
            //TOPFACE
            if (IsTransparent(x, y + 1, z))
            {
                offset1 = forward * planeSize;
                offset2 = right * planeSize;
                createTopFace(start + up * planeSize, offset1, offset2);
                vertzIndex += 4;
                trigsIndex += 6;
            }
        }*/




        /*//FRONTFACE
        if (y == 0 && test.getChunk(transform.position.x, transform.position.y, transform.position.z - 1) != null)
        {
            mainChunk chuk = test.getChunk(transform.position.x, transform.position.y, transform.position.z - 1);

            if (chuk.chunker.GetComponent<chunku>().GetByte(x, y, width - 1) == 0)
            {
                //TOPFACE
                if (IsTransparent(x, y + 1, z))
                {
                    offset1 = forward * planeSize;
                    offset2 = right * planeSize;
                    createTopFace(start + up * planeSize, offset1, offset2);
                    vertzIndex += 4;
                    trigsIndex += 6;
                }                
            }
        }

        else if (y != 0)
        {
            //TOPFACE
            if (IsTransparent(x, y + 1, z))
            {
                offset1 = forward * planeSize;
                offset2 = right * planeSize;
                createTopFace(start + up * planeSize, offset1, offset2);
                vertzIndex += 4;
                trigsIndex += 6;
            }       
        }*/

















        //TOPFACE
        if (IsTransparent(x, y + 1, z))
        {
            offset1 = forward * planeSize;
            offset2 = right * planeSize;
            createTopFace(start + up * planeSize, offset1, offset2);
            vertzIndex += 4;
            trigsIndex += 6;
        }

        //LEFTFACE
        if (IsTransparent(x - 1, y, z))
        {
            offset1 = back * planeSize;
            offset2 = down * planeSize;
            createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);
            vertzIndex += 4;
            trigsIndex += 6;
        }

        //RIGHTFACE
        if (IsTransparent(x + 1, y, z))
        {
            offset1 = up * planeSize;
            offset2 = forward * planeSize;
            createRightFace(start + right * planeSize, offset1, offset2);
            vertzIndex += 4;
            trigsIndex += 6;
        }
        //FRONTFACE
        if (IsTransparent(x, y, z - 1))
        {
            offset1 = left * planeSize;
            offset2 = up * planeSize;
            createFrontFace(start + right * planeSize, offset1, offset2);
            vertzIndex += 4;
            trigsIndex += 6;
        }
        //BACKFACE
        if (IsTransparent(x, y, z + 1))
        {
            offset1 = right * planeSize;
            offset2 = up * planeSize;
            createBackFace(start + forward * planeSize, offset1, offset2);
            vertzIndex += 4;
            trigsIndex += 6;
        }
        //BOTTOMFACE
        if (IsTransparent(x, y - 1, z))
        {
            offset1 = right * planeSize;
            offset2 = forward * planeSize;
            createBottomFace(start, offset1, offset2);
            vertzIndex += 4;
            trigsIndex += 6;
        }
    }
    private void createTopFace(Vector3 start, Vector3 offset1, Vector3 offset2)
    {
        positions[0 + vertzIndex] = start;
        positions[1 + vertzIndex] = start + offset1;
        positions[2 + vertzIndex] = start + offset2;
        positions[3 + vertzIndex] = start + offset1 + offset2;

        /*normals[0 + vertzIndex] = new Vector3(-1, 1, 0);
        normals[1 + vertzIndex] = new Vector3(-1, 1, 0);
        normals[2 + vertzIndex] = new Vector3(-1, 1, 0);
        normals[3 + vertzIndex] = new Vector3(-1, 1, 0);


        textureCoordinates[0 + vertzIndex] = new Vector2(1f, 1f);
        textureCoordinates[1 + vertzIndex] = new Vector2(1f, 1f);
        textureCoordinates[2 + vertzIndex] = new Vector2(1f, 1f);
        textureCoordinates[3 + vertzIndex] = new Vector2(1f, 1f);*/

        triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
        triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
        triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
        triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
    }



    private void createBottomFace(Vector3 start, Vector3 offset1, Vector3 offset2)
    {
        positions[0 + vertzIndex] = start;
        positions[1 + vertzIndex] = start + offset1;
        positions[2 + vertzIndex] = start + offset2;
        positions[3 + vertzIndex] = start + offset1 + offset2;

        /*normals[0 + vertzIndex] = new Vector3(0, 1, -1);
        normals[1 + vertzIndex] = new Vector3(0, 1, -1);
        normals[2 + vertzIndex] = new Vector3(0, 1, -1);
        normals[3 + vertzIndex] = new Vector3(0, 1, -1);

        textureCoordinates[0 + vertzIndex] = new Vector2(0f, 1f);
        textureCoordinates[1 + vertzIndex] = new Vector2(0f, 1f);
        textureCoordinates[2 + vertzIndex] = new Vector2(0f, 1f);
        textureCoordinates[3 + vertzIndex] = new Vector2(0f, 1f);*/

        triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
        triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
        triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
        triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
    }


    private void createFrontFace(Vector3 start, Vector3 offset1, Vector3 offset2)
    {
        positions[0 + vertzIndex] = start;
        positions[1 + vertzIndex] = start + offset1;
        positions[2 + vertzIndex] = start + offset2;
        positions[3 + vertzIndex] = start + offset1 + offset2;

        /*normals[0 + vertzIndex] = new Vector3(-1, 0, 0);
        normals[1 + vertzIndex] = new Vector3(-1, 0, 0);
        normals[2 + vertzIndex] = new Vector3(-1, 0, 0);
        normals[3 + vertzIndex] = new Vector3(-1, 0, 0);

        textureCoordinates[0 + vertzIndex] = new Vector2(1f, 0f);
        textureCoordinates[1 + vertzIndex] = new Vector2(1f, 1f);
        textureCoordinates[2 + vertzIndex] = new Vector2(1f, 0f);
        textureCoordinates[3 + vertzIndex] = new Vector2(0f, 1f);*/

        triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
        triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
        triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
        triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
    }
    private void createBackFace(Vector3 start, Vector3 offset1, Vector3 offset2)
    {
        positions[0 + vertzIndex] = start;
        positions[1 + vertzIndex] = start + offset1;
        positions[2 + vertzIndex] = start + offset2;
        positions[3 + vertzIndex] = start + offset1 + offset2;

        /*normals[0 + vertzIndex] = new Vector3(0, 0, -1);
        normals[1 + vertzIndex] = new Vector3(0, 0, -1);
        normals[2 + vertzIndex] = new Vector3(0, 0, -1);
        normals[3 + vertzIndex] = new Vector3(0, 0, -1);

        textureCoordinates[0 + vertzIndex] = new Vector2(1f, 1f);
        textureCoordinates[1 + vertzIndex] = new Vector2(1f, 0f);
        textureCoordinates[2 + vertzIndex] = new Vector2(1f, 1f);
        textureCoordinates[3 + vertzIndex] = new Vector2(0f, 1f);*/

        triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
        triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
        triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
        triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
    }

    private void createRightFace(Vector3 start, Vector3 offset1, Vector3 offset2)
    {
        positions[0 + vertzIndex] = start;
        positions[1 + vertzIndex] = start + offset1;
        positions[2 + vertzIndex] = start + offset2;
        positions[3 + vertzIndex] = start + offset1 + offset2;

        /* normals[0 + vertzIndex] = new Vector3(-1, 0, -1);
         normals[1 + vertzIndex] = new Vector3(-1, 0, -1);
         normals[2 + vertzIndex] = new Vector3(-1, 0, -1);
         normals[3 + vertzIndex] = new Vector3(-1, 0, -1);

         textureCoordinates[0 + vertzIndex] = new Vector2(1f, 0f);
         textureCoordinates[1 + vertzIndex] = new Vector2(1f, 0f);
         textureCoordinates[2 + vertzIndex] = new Vector2(1f, 0f);
         textureCoordinates[3 + vertzIndex] = new Vector2(0f, 1f);*/

        triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
        triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
        triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
        triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
    }

    private void createleftFace(Vector3 start, Vector3 offset1, Vector3 offset2)
    {
        positions[0 + vertzIndex] = start;
        positions[1 + vertzIndex] = start + offset1;
        positions[2 + vertzIndex] = start + offset2;
        positions[3 + vertzIndex] = start + offset1 + offset2;

        /*normals[0 + vertzIndex] = new Vector3(-1, 1, -1);
        normals[1 + vertzIndex] = new Vector3(-1, 1, -1);
        normals[2 + vertzIndex] = new Vector3(-1, 1, -1);
        normals[3 + vertzIndex] = new Vector3(-1, 1, -1);

        textureCoordinates[0 + vertzIndex] = new Vector2(0f, 0f);
        textureCoordinates[1 + vertzIndex] = new Vector2(0f, 0f);
        textureCoordinates[2 + vertzIndex] = new Vector2(0f, 0f);
        textureCoordinates[3 + vertzIndex] = new Vector2(0f, 0f);*/

        triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
        triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
        triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
        triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
    }

    public bool IsTransparent(int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= depth)) return true;
        {
            return map[x, y, z] == 0;
            //return map[x + width * (y + depth * z)] == 0;
        }
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
    /*public bigChunk getBigChunk(float xi, float yi, float zi)
    {
        int x = (int)xi;
        int y = (int)yi;
        int z = (int)zi;

        if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= width) || (z >= width))
        {
            return null;
        }
        return bigFuckingChunk[x, y, z];
    }*/
}
