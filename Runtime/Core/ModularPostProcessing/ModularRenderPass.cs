using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
using UnityEngine.Rendering.Universal;

public class ModularRenderPass : ScriptableRenderPass
{
    private CustomPostProcessing postProcessing;
    private Material material;

    public ModularRenderPass(CustomPostProcessing postProcessing)
    {
        this.postProcessing = postProcessing;
        material = new Material(postProcessing.GetShader());
        renderPassEvent = postProcessing.GetInjectionPoint();
    }

    public void CleanUp()
    {
        Material.Destroy(material);
    }

    public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
    {
        if (!material) { return; }
        var resourceData = frameData.Get<UniversalResourceData>();
        var cameraData = frameData.Get<UniversalCameraData>();

        if (resourceData.isActiveTargetBackBuffer) { return; }

        var src = resourceData.activeColorTexture; // Grab the camera's color texture

        var destDesc = renderGraph.GetTextureDesc(src);
        destDesc.name = postProcessing.GetPassName();
        destDesc.depthBufferBits = 0; // TODO: Figure out what this does

        TextureHandle dest = renderGraph.CreateTexture(destDesc);

        Matrix4x4 clipToView = GL.GetGPUProjectionMatrix(cameraData.camera.projectionMatrix, true).inverse;
        postProcessing.SendDataToShader(material, clipToView);
        int numPasses = material.passCount; // Get the number of material passes to then handle blitting

        //for (int i = 0; i < numPasses; i++)
        //{
        //
        //}

        RenderGraphUtils.BlitMaterialParameters para = new(src, dest, material, 0);
        renderGraph.AddBlitPass(para, passName: postProcessing.GetPassName());
        resourceData.cameraColor = dest;
    }

    struct PassData
    {
        public TextureHandle source;
        public TextureHandle destination;
        public TextureHandle gbuffer0;
        public Material material;
        public Matrix4x4 clipToView;
    }
}
