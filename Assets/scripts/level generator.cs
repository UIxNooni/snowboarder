using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class levelgenerator : MonoBehaviour
{
    public SpriteShapeController spriteshapecontroller;
    [Range(3f, 100f)] public int levellength = 50;
    [Range(1f, 50f)] public float xmultiplier = 2f;
    [Range(1f, 50f)] public float ymultiplier = 2f;
    [Range(0f, 1f)] public float curvesmoothness = 0.5f;
    public float noisestep = 0.5f;
    public float bottom = 10f;
    private Vector3 lastpos;


    private void OnValidate()
    {
        spriteshapecontroller.spline.Clear();
        for (int i = 0; i < levellength; i++)
        {
            lastpos = transform.position + new Vector3(i * xmultiplier, Mathf.PerlinNoise(0, i * noisestep) * ymultiplier);
            spriteshapecontroller.spline.InsertPointAt(i, lastpos);

            if (i != 0 && i != levellength - 1)
            {
                spriteshapecontroller.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                spriteshapecontroller.spline.SetLeftTangent(i, Vector3.left * xmultiplier * curvesmoothness);
                spriteshapecontroller.spline.SetRightTangent(i, Vector3.right * xmultiplier * curvesmoothness);

            }
        }

        spriteshapecontroller.spline.InsertPointAt(levellength, new Vector3(lastpos.x, transform.position.y - bottom));
        spriteshapecontroller.spline.InsertPointAt(levellength + 1, new Vector3(transform.position.x, transform.position.y - bottom));
    }

}