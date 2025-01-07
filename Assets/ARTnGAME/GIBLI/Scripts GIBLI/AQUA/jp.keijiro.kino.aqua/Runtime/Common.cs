using UnityEngine;
using UnityEngine.Rendering;

namespace Artngame.GIBLI.Kino.Aqua {

#region Internal classes

static class CommonAssets
{
    static Texture2D LoadNoiseTexture()
      => Resources.Load<Texture2D>("KinoAquaNoise");

    static Texture2D _noiseTexture;

    public static Texture2D NoiseTexture
      => _noiseTexture = _noiseTexture ?? LoadNoiseTexture();
}

static class ShaderIDs
{
    public static int EffectParams1 = Shader.PropertyToID("_EffectParams1");
    public static int EffectParams2 = Shader.PropertyToID("_EffectParams2");
    public static int EdgeColor = Shader.PropertyToID("_EdgeColor");
    public static int FillColor = Shader.PropertyToID("_FillColor");
    public static int Iteration = Shader.PropertyToID("_Iteration");
    public static int MainTex = Shader.PropertyToID("_MainTex");
    public static int NoiseTexture = Shader.PropertyToID("_NoiseTexture");
    public static int OverlayOpacity = Shader.PropertyToID("_OverlayOpacity");
    public static int OverlayTexture = Shader.PropertyToID("_OverlayTexture");
}

#endregion

#region Public classes

public enum OverlayMode { Off, Multiply, Overlay, Screen }

public static class ShaderHelper
{
    public static void SetProperties
      (Material material,
       Texture inputTexture,
       float opacity,
       Color edgeColor,
       float edgeContrast,
       Color fillColor,
       float blurWidth,
       float blurFrequency,
       float hueShift,
       float interval,
       int iteration)
    {
        var bfreq = Mathf.Exp((blurFrequency - 0.5f) * 6);

        material.SetVector(ShaderIDs.EffectParams1,
          new Vector4(opacity, interval,blurWidth, bfreq));

        material.SetVector(ShaderIDs.EffectParams2,
          new Vector2(edgeContrast, hueShift));

        material.SetColor(ShaderIDs.EdgeColor, edgeColor);
        material.SetColor(ShaderIDs.FillColor, fillColor);
        material.SetInt(ShaderIDs.Iteration, iteration);

        material.SetTexture(ShaderIDs.MainTex, inputTexture);
        material.SetTexture(ShaderIDs.NoiseTexture, CommonAssets.NoiseTexture);
    }

    public static void SetOverlayProperties
      (Material material, OverlayMode mode, Texture texture, float opacity)
    {
        if (mode == OverlayMode.Multiply)
            material.EnableKeyword("KINO_AQUA_MULTIPLY");
        else
            material.DisableKeyword("KINO_AQUA_MULTIPLY");

        if (mode == OverlayMode.Overlay)
            material.EnableKeyword("KINO_AQUA_OVERLAY");
        else
            material.DisableKeyword("KINO_AQUA_OVERLAY");

        if (mode == OverlayMode.Screen)
            material.EnableKeyword("KINO_AQUA_SCREEN");
        else
            material.DisableKeyword("KINO_AQUA_SCREEN");

        if (mode == OverlayMode.Off)
            material.EnableKeyword("KINO_AQUA_OFF");
        else
            material.DisableKeyword("KINO_AQUA_OFF");

        material.SetTexture(ShaderIDs.OverlayTexture, texture);
        material.SetFloat(ShaderIDs.OverlayOpacity, opacity);
    }
}

#endregion

} // namespace Kino.Aqua
