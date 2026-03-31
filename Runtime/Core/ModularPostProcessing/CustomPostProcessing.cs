using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class CustomPostProcessing : ScriptableObject
{
    [SerializeField] protected Shader shader;
    [SerializeField] protected string passName = "Default Post Processing";
    [SerializeField] protected RenderPassEvent injectionPoint = RenderPassEvent.AfterRenderingPostProcessing;

    
    public abstract void SendDataToShader(Material material, Matrix4x4 clipToView);
    public RenderPassEvent GetInjectionPoint() { return injectionPoint; }
    public string GetPassName() { return passName; }
    public Shader GetShader() { return shader; }
    
}
