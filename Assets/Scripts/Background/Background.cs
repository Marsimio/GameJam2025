using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public float speed;
    
    public Renderer bgRenderer;
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
