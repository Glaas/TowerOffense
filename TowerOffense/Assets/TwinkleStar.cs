using UnityEngine;

public class TwinkleStar : MonoBehaviour
{
    public Material twinkleMaterial;
    [ColorUsageAttribute(true, true)]
    public Color twinkleColorBright;

    private void Update()
    {
        twinkleMaterial.SetColor("_EmissionColor", Color.Lerp(twinkleColorBright, Color.black, Mathf.PingPong(Time.time / 3, .4f)));
    }
}
