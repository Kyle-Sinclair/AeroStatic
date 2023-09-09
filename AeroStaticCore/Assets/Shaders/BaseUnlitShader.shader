Shader "NedMakesGames/MyLit" {
    
    /*A shader is more than just code that draws.
      A single shader is actually made of many — sometimes thousands — of smaller functions. 
      Unity can choose to run any of them depending on the situation. 
      They’re organized into several subdivisions. 
      The top-most subdivisions are known as subshaders. 
      Subshaders allow you to write different code for different render pipelines. 
      Unity automatically picks the correct subshader to use.
    */
    
    SubShader{
        // These tags are shared by all passes in this sub shader
        Tags{"RenderPipeline" = "UniversalPipeline"}

        
        Pass {
            Name "ForwardLit" // For debugging
            Tags{"LightMode" = "UniversalForward"} // Pass specific tags. 
            // "UniversalForward" tells Unity this is the main lighting pass of this shader
            
            
            
            HLSLPROGRAM // Begin HLSL code

            ENDHLSL
        }
        
        
    }


}