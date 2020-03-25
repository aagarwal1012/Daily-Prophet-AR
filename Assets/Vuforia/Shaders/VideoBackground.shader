/*===============================================================================
Copyright 2019 PTC Inc.

Licensed under the Apache License, Version 2.0 (the "License"); you may not
use this file except in compliance with the License. You may obtain a copy of
the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
CONDITIONS OF ANY KIND, either express or implied. See the License for the
specific language governing permissions and limitations under the License.
===============================================================================*/

Shader "Custom/VideoBackground" {
    // Used to render the Vuforia Video Background

    Properties
    {
        [NoScaleOffset] _MainTex("Texture", 2D) = "white" {}
        [NoScaleOffset] _UVTex1("UV Texture 1", 2D) = "white" {}
        [NoScaleOffset] _UVTex2("UV Texture 2", 2D) = "white" {}
    }

    SubShader
    {
        Tags {"Queue" = "geometry-11" "RenderType" = "opaque" }
        Pass {
            ZWrite Off
            Cull Off
            Lighting Off

            CGPROGRAM

        //Custom Shaderwords for different texture formats
        #pragma multi_compile VUFORIA_RGB VUFORIA_YUVNV12 VUFORIA_YUVNV21 VUFORIA_YUV420P VUFORIA_YUVYV12

        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"
        #include "VuforiaVideoBackground.cginc"

        v2f vert(appdata_base v)
        {
            return vuforiaConvertRGBVert(v);
        }

        half4 frag(v2f i) : COLOR
        {
            half4 c = vuforiaConvertRGBFrag(i);

#ifdef UNITY_COLORSPACE_GAMMA
            return c;
#else
            return half4(GammaToLinearSpace(c.rgb), c.a);
#endif	
        }

        ENDCG
    }
    }
    Fallback "Legacy Shaders/Diffuse"
}
