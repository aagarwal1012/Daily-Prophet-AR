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

#ifndef VUFORIA_VB_RGB_INCLUDED
#define VUFORIA_VB_RGB_INCLUDED

struct v2f {
    float4  pos : SV_POSITION;
    float2  uv : TEXCOORD0;
};

sampler2D _MainTex;
float4 _MainTex_ST;

v2f vuforiaConvertRGBVert(appdata_base v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

    return o;
}

half4 vuforiaConvertRGBFrag(v2f i) : COLOR
{
    half4 c = tex2D(_MainTex, i.uv);

    c.rgb = c.rgb;
    c.a = 1.0;

    return c;
}
#endif //VUFORIA_VB_RGB_INCLUDED