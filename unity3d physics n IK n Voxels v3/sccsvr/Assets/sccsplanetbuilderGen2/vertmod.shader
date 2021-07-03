Shader "Unlit/VertexID"
{    
	 Properties 
     {
     	 _Color("Color", Color) = (0.25,0.25,0.25,1)
         //_MainTex ("Base (RGB)", 2D) = "white" {}
         _Amount("Weight", Float) = 0.0 //this will be incremented in script
         //_VertIndex ("indexOfVert", int) = 0 //this will be incremented in script
         _VertPos("VertPos", Color) = (0,0,0,1)
         //_xPos ("xPos", float) = 5.0 //just to test, these can be filled from script with desired values.
         //_zPos ("zPos", float) = 3.0 //It might be wise to optimise this to a float2
         //_zPos ("zPos", float) = 3.0 //It might be wise to optimise this to a float2
     }

    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            //#pragma target 3.5
            //#pragma surface surf Standard fullforwardshadows addshadow //vertex:vert

			//sampler2D _MainTex;
			float _Amount;
			float4 _VertPos;

            struct v2f {
                fixed4 color : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert (
                float4 vertex : POSITION, // vertex position input
                uint vid : SV_VertexID // vertex ID, needs to be uint
                )
            {
                //v2f o;
                //o.pos = vertex;
                //o.pos.y = _Amount;		
                //if((int)(_VertPos.x) == (int)(o.pos.x)&&(int)(_VertPos.y) == (int)(o.pos.y)&&(int)(_VertPos.z) == (int)(o.pos.z) ) //(int)_VertIndex == (int)vid // 
                //{ 
             	//	o.pos.y = _Amount;
                //}
                //
				//o.pos = UnityObjectToClipPos(o.pos);
                //float f = (float)vid;
                //o.color = half4(sin(f/10),sin(f/100),sin(f/1000),0) * 0.5 + 0.5;          
                //return o;
            }


            //void vert (appdata_full v) {
          	//	v.vertex.xyz += v.normal * _Amount;
      		//}
      		void vert (inout appdata_full v, out Input o)
      		{
            	UNITY_INITIALIZE_OUTPUT(Input,o);
            	v.vertex += 10;
			}



            fixed4 frag (v2f i) : SV_Target
            {
                return i.color;
            }
            ENDCG
        }
    }
}