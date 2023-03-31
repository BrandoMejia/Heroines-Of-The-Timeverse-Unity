Shader "MyShaders/BasicShader"
{
    Properties {
     _MainColor("Color Principal", Color) = (1,1,1,1)
    }
    SubShader{
        Pass
        {
            Material{
                Diffuse(1,1,1,1)
                Ambient(1,1,1,1)
                }
        }
    }
    FallBack "Diffuse"
}
