// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Jiexi's shader with help of tanoshimi
//https://answers.unity.com/questions/543558/moving-vertices-in-shaders.html
Shader "VertexModifier"
 {
 
     Properties 
     {

     	 _Color("Color", Color) = (0.25,0.25,0.25,1)
         _MainTex ("Base (RGB)", 2D) = "white" {}
         _Amount ("Height", Float) = 0.0 //this will be incremented in script
         _xPos ("xPos", float) = 5.0 //just to test, these can be filled from script with desired values.
         _zPos ("zPos", float) = 3.0 //It might be wise to optimise this to a float2
         //_zPos ("zPos", float) = 3.0 //It might be wise to optimise this to a float2
     }
     SubShader 
     {
         Tags { "RenderType"="Opaque" }
         LOD 200
         
		CGPROGRAM
		// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members uv_MainTex)
		#pragma exclude_renderers d3d11
		//#pragma surface surf Lambert vertex:vert
		//#pragma vertex vert
		//#pragma fragment frag
		#pragma target 3.5



         sampler2D _MainTex;
         float _Amount;
         float _xPos;
         float _zPos;
         int _VertIndex;

         struct v2f 
         {
             float2 uv_MainTex;
             float4 pos : SV_POSITION;
         };


         v2f vert(float4 vertex: POSITION, uint vid: SV_VertexID)
         {
	          v2f o;
	          o.pos = UnityObjectToClipPos(vertex);
	          float f = (float)vid;
	          o.color = half4(sin(f/10), sin(f/100), sin(f/1000),0) * 0.5 + 0.5;
	          v.vertex.y = _Amount;
	          return o;
         }
         ENDCG
     } 
     FallBack "Diffuse"
}
        
         
		//void vert (inout appdata_full v) //, uint vid: SV_VertexID
		//{         
		//  float3 castToWorld = round(mul(unity_ObjectToWorld, v.vertex) );
		//
		//  if(castToWorld.x == 5.0 && castToWorld.y < _Amount && castToWorld.z == -1.0)
		//  {
		   
		//  }

		  //v.pos = UnityObjectToClipPos(vertex);
		  //float f = (float)vid;
		  //v.color = half4(sin(f/10),sin(f/100), sin(f/1000),0)*0.5+0.5;

		  //if(_VertIndex == vid)
		  //{
		  //
		  //}
		//   v.vertex.y = _Amount;
		//}

		 //void surf(Input IN, inout SurfaceOutput o) 
		 //{
		 //    half4 c = tex2D (_MainTex, IN.uv_MainTex);
		 //    o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
		 //    o.Alpha = c.a;
		 //}

		 //fixed4 frag(v2f i ): SV_Target{
		 //	return i.color;
		 //}
