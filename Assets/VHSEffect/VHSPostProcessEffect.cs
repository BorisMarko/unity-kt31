using UnityEngine;
using UnityEngine.Video;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/GlitchEffect")]
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(VideoPlayer))]
public class VHSPostProcessEffect : MonoBehaviour
{
    public Shader shader;
    public VideoClip VHSClip;
    public float noiseIntensity = 0.1f;

    private float _yScanline;
    private float _xScanline;
    private Material _material = null;
    private VideoPlayer _player;

    void Awake()
    {
        _player = GetComponent<VideoPlayer>();
        _player.isLooping = true;
        _player.renderMode = VideoRenderMode.APIOnly;
        _player.audioOutputMode = VideoAudioOutputMode.None;
        _player.clip = VHSClip;
        _player.Play();

        _material = new Material(shader);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!_material)
        {
            _material = new Material(shader);
        }

        _material.SetTexture("_VHSTex", _player.texture);

        _yScanline += Time.deltaTime * 0.01f * noiseIntensity;
        _xScanline -= Time.deltaTime * 0.1f * noiseIntensity;

        if (_yScanline >= 1)
        {
            _yScanline = Random.value * noiseIntensity;
        }
        if (_xScanline <= 0 || Random.value < 0.05)
        {
            _xScanline = Random.value * noiseIntensity;
        }

        _material.SetFloat("_yScanline", _yScanline);
        _material.SetFloat("_xScanline", _xScanline);
        Graphics.Blit(source, destination, _material);
    }


    protected void OnDisable()
    {
        if (_material)
        {
            DestroyImmediate(_material);
        }
    }
}
