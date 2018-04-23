using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MatrixBlender))]
public class PerspectiveShifter : MonoBehaviour
{
    private Camera camera;
    
    private Matrix4x4 ortho, perspective;
    public float fov = 60f,
                        near = .3f,
                        far = 1000f,
                        orthographicSize = 50f;
    private float aspect;
    private MatrixBlender blender;
    private bool orthoOn;
    private static PerspectiveShifter _instance;

    void Start()
    {
        _instance = this;
        camera = GetComponent<Camera>();
        aspect = (float)Screen.width / (float)Screen.height;
        ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
        perspective = Matrix4x4.Perspective(fov, aspect, near, far);
        camera.projectionMatrix = ortho;
        orthoOn = true;
        blender = (MatrixBlender)GetComponent(typeof(MatrixBlender));
    }

    public static void ShiftToOrtho(float time) {
        _instance.blender.BlendToMatrix(_instance.ortho, time);
    }
    public static void ShiftToPerspective(float time)
    {
        _instance.blender.BlendToMatrix(_instance.perspective, time);
    }
}
