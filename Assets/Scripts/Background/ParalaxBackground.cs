using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    Material material;
    private float distance;
    
    [Range(0f, 0.5f)] public float speed = 0.2f;
    
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }
    
    void Update()
    {
        distance += speed * Time.deltaTime;
        material.SetTextureOffset("_MainTex", Vector2.right * distance);
    }
}
