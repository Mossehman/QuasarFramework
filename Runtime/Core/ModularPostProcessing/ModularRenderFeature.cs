using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; 

public class ModularRenderFeature : ScriptableRendererFeature
{
    [SerializeField] private List<CustomPostProcessing> postProcessingEffects = new();

    private List<ModularRenderPass> renderPasses = new();

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (renderPasses.Count == 0) return;

        foreach (ModularRenderPass pass in renderPasses) renderer.EnqueuePass(pass);
    }

    public override void Create()
    {
        if (postProcessingEffects.Count == 0) { return; }
        foreach (CustomPostProcessing postProcessingEffect in postProcessingEffects)
        {
            if (postProcessingEffect == null || postProcessingEffect.GetShader() == null) continue;
            ModularRenderPass renderPass = new ModularRenderPass(postProcessingEffect);
            renderPasses.Add(renderPass);
        }
    }
}
