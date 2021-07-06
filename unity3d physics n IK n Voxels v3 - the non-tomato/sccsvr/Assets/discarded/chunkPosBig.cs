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
    public class chunkPosBig
    {
        public Vector3 worldAreaPosition;
        public chunkPosSmall[,,] smallChunkerList;
        public chunkPosBig[,,] chunkPositions;
    }
}
