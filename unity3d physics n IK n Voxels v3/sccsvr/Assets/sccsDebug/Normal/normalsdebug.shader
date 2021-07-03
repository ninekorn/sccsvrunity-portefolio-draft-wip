﻿// Upgrade NOTE: replaced 'PositionFog()' with transforming position into clip space.
// Upgrade NOTE: replaced 'V2F_POS_FOG' with 'float4 pos : SV_POSITION'

Shader "sccsDebug/normalsdebug" {
  Properties { }  
  SubShader {
    Pass {
      CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members normal)
#pragma exclude_renderers d3d11
        #pragma vertex vert
        #pragma fragment frag

        #pragma fragmentoption ARB_fog_exp2
        #pragma fragmentoption ARB_precision_hint_fastest

        #include "UnityCG.cginc"
        #include "AutoLight.cginc"

        struct v2f {
          float4 pos : SV_POSITION;
          float3 normal : COLOR;
        };  

        v2f vert(appdata_tan v) {
          v2f o;
          o.pos = UnityObjectToClipPos (v.vertex);
          o.normal = (v.normal + 1) * 0.5;
          return o;
        }
        
        half4 frag(v2f i) : COLOR {
          i.normal = normalize(i.normal);
          return half4(i.normal.rgb, 1);
        }
      ENDCG
    }
  }
}
