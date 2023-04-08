using UnityEditor;
using UnityEngine;

class WebGLBuilder 
{
    static void build() 
    {
        string[] scenes = {"Assets/soy.unity"};
        
        string pathToDeploy = "unitybuild/soyboy/";
        
        BuildPipeline.BuildPlayer(scenes, pathToDeploy, BuildTarget.WebGL, BuildOptions.None);
    }
}
