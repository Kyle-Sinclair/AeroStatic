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

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

                struct Attributes {
	                float3 positionOS : POSITION; // Position in object space
                    };
				struct Interpolators {
					// This value should contain the position in clip space (which is similar to a position on screen)
					// when output from the vertex function. It will be transformed into pixel position of the current
					// fragment on the screen when read from the fragment function
				float4 positionCS : SV_POSITION;
				};
            Interpolators Vertex(Attributes input) {
            	Interpolators output;
            	
			// These helper functions, found in URP/ShaderLib/ShaderVariablesFunctions.hlsl
			// transform object space values into world and clip space
			VertexPositionInputs posnInputs = GetVertexPositionInputs(input.positionOS);
			output.positionCS = posnInputs.positionCS;
			return output;
	// Pass position and orientation data to the fragment function
}
            ENDHLSL
        }
        
        
    }


}