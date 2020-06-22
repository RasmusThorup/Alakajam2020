using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayableArea : MonoBehaviour
{
    public bool generateMesh;
    public Vector4 gameAreaEdges;
    public MeshFilter meshFilter;
    public Vector2 gameAreaSize;
    public Transform sphereAreaEdges;
    Transform[] trailTrans = new Transform[4];
    TrailRenderer[] trailRenders = new TrailRenderer[4];
    Transform trailTopTrans;
    TrailRenderer trailTop;
    Transform trailBotTrans;
    TrailRenderer trailBot;
    float oldAspect;
    private Camera mainCam;
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    Vector2[] uv;
    Vector2 offset = new Vector2(0,0);
    Vector2 padding = new Vector2(0,0);

    public LineRenderer lineRenderer;

    [BoxGroup("Mesh Shrink")]
    [ReadOnly]
    public bool meshIsShrink;
    [BoxGroup("Mesh Shrink")]
    [ReadOnly]
    public bool meshIsSphere;
    [BoxGroup("Mesh Shrink")]
    public Vector2 shrinkAmount = new Vector2(5, 5);
    [BoxGroup("Mesh Shrink")]
    public float shrinkTransitionTime = 5;
    [BoxGroup("Mesh Shrink")]
    public float shrinkActiveTime = 10;
    [BoxGroup("Mesh Shrink")]
    public AnimationCurve shrinkCurve;
    [BoxGroup("Mesh Shrink")]
    public float maxEdgeWidth = 0.2f;
    [BoxGroup("Mesh Shrink")]
    public Vector2 sphereGameAreaCenter = new Vector2(0,0);
    [BoxGroup("Mesh Shrink")]
    public Vector2 sphereGameAreaRadiusMinMax = new Vector2(20,40);
    [BoxGroup("Mesh Shrink")]
    public float spinSpeed = 3;
    [BoxGroup("Mesh Shrink")]
    [ReadOnly]
    public float currentSphereGameAreaRadius;

    //-- Ugly singleton
    public static PlayableArea Instance;
    private void Awake()
    {
        Instance = this;
        lineRenderer.startWidth = 0;
        lineRenderer.endWidth = 0;
        currentSphereGameAreaRadius = sphereGameAreaRadiusMinMax.y;

        meshIsSphere = false;
        meshIsShrink = false;

        sphereAreaEdges.localPosition = sphereGameAreaCenter;

        for (int i = 0; i < trailTrans.Length; i++)
        {
            trailTrans[i] = sphereAreaEdges.GetChild(i);
            trailRenders[i] = trailTrans[i].GetComponent<TrailRenderer>();
        }

        foreach (var trailRender in trailRenders)
        {
            trailRender.emitting = false;
        }

        trailTrans[0].localPosition = new Vector3(0,sphereGameAreaRadiusMinMax.y,0);
        trailTrans[1].localPosition = new Vector3(0,-sphereGameAreaRadiusMinMax.y,0);
        trailTrans[2].localPosition = new Vector3(sphereGameAreaRadiusMinMax.y,0,0);
        trailTrans[3].localPosition = new Vector3(-sphereGameAreaRadiusMinMax.y,0,0);
    }
    //--

    void Start()
    {
        mesh = new Mesh();
        meshFilter.mesh = mesh;

        triangles = new int[]
        {
            0,1,3,
            1,2,3
        };

        mainCam = Camera.main;
    }

    void Update()
    {
        if (meshIsSphere)
        {
            //spin the game area
            sphereAreaEdges.Rotate(0, 0, spinSpeed*Time.deltaTime, Space.Self);
        }
    }

    void LateUpdate()
    {
        if (!meshIsShrink)
        {
            if (oldAspect == mainCam.aspect) //Early out if aspect hasn't changed
                return;
        }

        float height = 2.0f * Mathf.Tan(0.5f * mainCam.fieldOfView * Mathf.Deg2Rad)* 40.0f;
        float width = height * mainCam.aspect;

        gameAreaSize = new Vector2(width-padding.x,height-padding.y);
        
        float x = gameAreaSize.x*0.5f;
        float y = gameAreaSize.y*0.5f;
        gameAreaEdges = new Vector4(-x+offset.x,x+offset.x,-y+offset.y,y+offset.y);
        
        oldAspect = mainCam.aspect;
        GameManager.Instance.gameAreaEdges = gameAreaEdges;

        vertices = new Vector3[]
        {
            new Vector3 (gameAreaEdges.x,gameAreaEdges.z,0),
            new Vector3 (gameAreaEdges.x,gameAreaEdges.w,0),
            new Vector3 (gameAreaEdges.y,gameAreaEdges.w,0),
            new Vector3 (gameAreaEdges.y,gameAreaEdges.z,0)
        };

        lineRenderer.SetPositions(vertices);

        if (!generateMesh)
            return;


        uv = new Vector2[vertices.Length];
        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = new Vector2(vertices[i].y, vertices[i].x);
        }

        UpdateMesh();
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.uv = uv;
    }

    [Button]
    public void ShrinkArea()
    {
        if (meshIsShrink)
            return;
        meshIsShrink = true;
        StartCoroutine(ShrinkArea_Coroutine());
    }

    IEnumerator ShrinkArea_Coroutine()
    {
        yield return pTween.To(shrinkTransitionTime, 0, 1, t =>{
            float a = shrinkCurve.Evaluate(t);
            padding = shrinkAmount*a;

            float width = Mathf.Clamp(t*2,0,maxEdgeWidth);
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
        });

        yield return new WaitForSeconds(shrinkActiveTime);

        yield return pTween.To(shrinkTransitionTime, 1, 0, t =>{
            float a = shrinkCurve.Evaluate(t);
            padding = shrinkAmount*a;

            float width = Mathf.Clamp(t*2,0,maxEdgeWidth);
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
        });

        meshIsShrink = false;
        yield break;
    }

    [Button]
    public void SphericalArea()
    {
        if(meshIsSphere)
            return;
        meshIsSphere = true;
        
        foreach (var trailRender in trailRenders)
        {
            trailRender.emitting = true;
        }

        StartCoroutine(SphericalArea_Coroutine());
    }

    IEnumerator SphericalArea_Coroutine()
    {
        yield return pTween.To(shrinkTransitionTime, 1, 0, t =>{
            float a = shrinkCurve.Evaluate(t);            
            currentSphereGameAreaRadius = Mathf.Lerp(sphereGameAreaRadiusMinMax.x, sphereGameAreaRadiusMinMax.y, a);

            trailTrans[0].localPosition = new Vector3(0,currentSphereGameAreaRadius,0);
            trailTrans[1].localPosition = new Vector3(0,-currentSphereGameAreaRadius,0);
            trailTrans[2].localPosition = new Vector3(currentSphereGameAreaRadius,0,0);
            trailTrans[3].localPosition = new Vector3(-currentSphereGameAreaRadius,0,0);
            /*
            float scale = Mathf.Lerp(40, 90, a);
            sphereAreaEdges.transform.localScale = new Vector3(scale, scale, 5);
            */
        });

        yield return new WaitForSeconds(shrinkActiveTime);

        yield return pTween.To(shrinkTransitionTime, 0, 1, t =>{
            float a = shrinkCurve.Evaluate(t);            
            currentSphereGameAreaRadius = Mathf.Lerp(sphereGameAreaRadiusMinMax.x, sphereGameAreaRadiusMinMax.y, a);

            trailTrans[0].localPosition = new Vector3(0,currentSphereGameAreaRadius,0);
            trailTrans[1].localPosition = new Vector3(0,-currentSphereGameAreaRadius,0);
            trailTrans[2].localPosition = new Vector3(currentSphereGameAreaRadius,0,0);
            trailTrans[3].localPosition = new Vector3(-currentSphereGameAreaRadius,0,0);
            /*
            float scale = Mathf.Lerp(40, 90, a);
            sphereAreaEdges.transform.localScale = new Vector3(scale, scale, 5);
            */
        });

        meshIsSphere = false;

        foreach (var trailRender in trailRenders)
        {
            trailRender.emitting = false;
        }

        yield break;
    }
}
