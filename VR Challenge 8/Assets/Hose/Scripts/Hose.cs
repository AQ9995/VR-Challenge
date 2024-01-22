using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class Hose : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshFilter meshFilter;

    [SerializeField] Transform startTransform, endTransform;
    [SerializeField] int segmentCount = 20;
    [SerializeField] float totalLength = 8;
    [SerializeField] float totalWeight = 8;

    [SerializeField] float radius = 0.05f;

    [SerializeField] int sides = 16;

    [SerializeField] float drag = 15f;
    [SerializeField] float angularDrag= 15f;

    [SerializeField] bool usephysics = false;

    Transform[] segments = new Transform[0];
    [SerializeField] Transform segmentsParent;
    private int prevSegmentCount;
    private float prevTotalLength;
    private float prevTotalWeight;
    private float prevDrag;
    private float prevAngularDrag;
    private float prevRadius;

    private MeshDataHose meshData;
    
    private Vector3[] vertices;
    private int[,] vertexIndicesMap;
    private Mesh mesh;
    private bool createTriangle;
    private int prevSides;

 
    void Start()
    {
        
        vertices = new Vector3[segmentCount * sides * 3 ];
        GenerateMesh(); 
    }

    
    void Update()
    {
        if(prevSegmentCount != segmentCount)
        {
            RemoveSegments();
            segments = new Transform[segmentCount];
            GenerateSegments();
            GenerateMesh();
        }
        
        prevSegmentCount = segmentCount;

        if (totalLength != prevTotalLength || totalWeight != prevTotalWeight || drag != prevDrag || angularDrag != prevAngularDrag)
        {
            UpdateHose();
            GenerateMesh();

        }

        if (sides != prevSides)
        {
            vertices = new Vector3[segmentCount * sides * 3];
            GenerateMesh();
        }
        prevSides = sides;

        prevTotalLength = totalLength;
        prevDrag = drag;
        prevTotalWeight = totalWeight;
        prevAngularDrag = angularDrag;
        
       
        if(prevRadius != radius && usephysics)
        {
            UdateRadius();
            GenerateMesh();
        }
        prevRadius = radius;
        UpdateMesh();


    }

 
    private void UdateRadius()
    {
        for(int i =0; i < segments.Length; i++)
        {
            SetRadiusSegment(segments[i], radius);
        }
    }

    void UpdateMesh()
    {
        GenerateVertices();
        meshFilter.mesh.vertices = vertices;
    }

    void GenerateMesh()
    {
        createTriangle = true;

        if (meshData == null)
        {
            meshData = new MeshDataHose(sides, segmentCount + 1, false);
        }
        else
        {
            meshData.ResetMesh(sides, segmentCount + 1, false);
        }

        GenerateIndicesMap();
        GenerateVertices();
        meshData.ProcessMesh();
        mesh = meshData.CreateMesh();

        meshFilter.sharedMesh = mesh;

        createTriangle = false;
    }
    private void GenerateIndicesMap()
    {
        vertexIndicesMap = new int[segmentCount + 1, sides +1];
        int meshVertexIndex = 0;
        for (int segmentIndex = 0; segmentIndex < segmentCount; segmentIndex++)
        {
            for(int sideIndex= 0 ; sideIndex < sides; sideIndex++)
            {

                vertexIndicesMap[segmentIndex, sideIndex] = meshVertexIndex;
                meshVertexIndex++;

            }
        }
    }
    private void GenerateVertices()
    {

        for (int i = 0; i < segments.Length; i++)
        {

            GenerateCircleVerticesAndTriangle(segments[i], i);
        }
    }
    private void GenerateCircleVerticesAndTriangle(Transform segmentTransform, int segmentIndex)
    {
        float angleDiff = 360 / sides;

        Quaternion diffRotation = Quaternion.FromToRotation(Vector3.forward,  segmentTransform.forward);

        for (int sideInsex = 0; sideInsex < sides; sideInsex++)
        {
            float angleRad = sideInsex * angleDiff * Mathf.Deg2Rad;
            float x = -1 * radius * Mathf.Cos(angleRad);
            float y = radius * Mathf.Sin(angleRad);

            Vector3 pointOffset = new(x, y, 0);

            Vector3 pointRotated = diffRotation * pointOffset;

            Vector3 pointRotatedAtCenterOfTransform = segmentTransform.position + pointRotated;

            int verticesIndex = (segmentIndex * sides) + sideInsex;
            vertices [verticesIndex] = pointRotatedAtCenterOfTransform;
            

            if (createTriangle)
            {

                meshData.AddVertex(pointRotatedAtCenterOfTransform, new (0, 0), verticesIndex);

                bool createThisTriangle = segmentIndex < segmentCount - 1;
                if (createThisTriangle)
                {
                    int currentIncrement = 1;
                    int a = vertexIndicesMap[segmentIndex, sideInsex];
                    int b = vertexIndicesMap[segmentIndex + currentIncrement, sideInsex];
                    int c = vertexIndicesMap[segmentIndex, sideInsex + currentIncrement];
                    int d = vertexIndicesMap[segmentIndex + currentIncrement, sideInsex + currentIncrement];

                    bool isLastGap = sideInsex == sides - 1;

                    if (isLastGap)
                    {
                        c = vertexIndicesMap[segmentIndex, 0];
                        d = vertexIndicesMap[segmentIndex + currentIncrement, 0];
                    }

                    meshData.AddTriangle(a, d, c);
                    meshData.AddTriangle(d, a, b);
                }
            }
        }
    }

    private void SetRadiusSegment(Transform transform, float radius)
    {
        SphereCollider spherCollider = transform.GetComponent<SphereCollider>();
        spherCollider.radius = radius;
    }
    private void UpdateHose()
    {
        for (int i = 0; i < segments.Length; i++)
        {
            if(i != 0)
            {
                updatLengthOnSegment(segments[i], totalLength / segmentCount);
            }
            updatWeightOnSegment(segments[i], totalWeight, drag, angularDrag);
        }
    }

    private void updatWeightOnSegment(Transform transform, float totalWehigt, float drag, float angularDrag)
    {
        Rigidbody rigidbody = transform.GetComponent<Rigidbody>(); 
        rigidbody.mass = totalWehigt / segmentCount;
        rigidbody.drag = drag;
        rigidbody.angularDrag = angularDrag;

    }
    private void updatLengthOnSegment(Transform transform, float v)
    {
        ConfigurableJoint joint = transform.GetComponent<ConfigurableJoint>();
        if (joint != null)
        {
            joint.connectedAnchor = Vector3.forward * totalLength / segmentCount;
        }
        {
            
        }
    }

    private void RemoveSegments()
    {
        

            for (int i = 0; i < segments.Length; i++)
            {
                if (segments[i] != null)
                {
                    Destroy(segments[i].gameObject);
                }
            }
       

    }

    private void OnDrawGizmos()
    {

        if (segments != null && vertices != null)
        {


            for (int i = 0; i < segments.Length; i++)
            {
                if (segments[i] != null)
                {
                    Gizmos.DrawWireSphere(segments[i].position, radius);
                }

            }

            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i] != null)
                {

                    Gizmos.DrawSphere(vertices[i], 0.2f);

                }
            }
        
        }
        
        
    }
    private void GenerateSegments()
    {
        JoinSegment(startTransform, null, true);
        Transform prevTransform = startTransform;

        Vector3 direction = (endTransform.position - startTransform.position);

        for (int i= 0; i < segmentCount; i++)
        {
            GameObject segment = new GameObject($"segment_{i}");
            segment.transform.SetParent(segmentsParent);
            segments[i] = segment.transform;

            Vector3 pos = prevTransform.position + (direction / segmentCount);
            segment.transform.position= pos;

            JoinSegment(segment.transform, prevTransform);

            prevTransform = segment.transform;
        }

        JoinSegment(endTransform, prevTransform, false, true);
    }

    private void JoinSegment(Transform current, Transform connectedTransform, bool isKinetic = false, bool isCloseConnected = false)
    {

        if (current.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rigidbody = current.AddComponent<Rigidbody>();
            rigidbody.isKinematic = isKinetic;
            rigidbody.mass = totalLength / totalWeight;
            rigidbody.drag = drag;
            rigidbody.angularDrag = angularDrag;
        }


        if (usephysics)
        {
            SphereCollider sphererCollider = current.AddComponent<SphereCollider>();
            sphererCollider.radius = radius;
        }


        if (connectedTransform != null)
        {
            ConfigurableJoint joint = current.GetComponent<ConfigurableJoint>();
            if (joint == null)
            {
                joint = current.AddComponent<ConfigurableJoint>();
            }
           

            joint.connectedBody = connectedTransform.GetComponent<Rigidbody>();

            joint.autoConfigureConnectedAnchor = false;
            if (isCloseConnected)
            {
                joint.connectedAnchor = Vector3.forward * 0.1f;
            }
            else
            {
                joint.connectedAnchor = Vector3.forward * (totalLength / segmentCount);
            }

            joint.xMotion = ConfigurableJointMotion.Locked;
            joint.yMotion = ConfigurableJointMotion.Locked;
            joint.zMotion = ConfigurableJointMotion.Locked;

            joint.angularXMotion = ConfigurableJointMotion.Free;
            joint.angularYMotion = ConfigurableJointMotion.Free;
            joint.angularZMotion = ConfigurableJointMotion.Limited;

            SoftJointLimit softJointLimit = new SoftJointLimit();
            softJointLimit.limit = 0;
            joint.angularZLimit = softJointLimit;

            JointDrive jointDrive = new JointDrive();
            jointDrive.positionDamper = 0;
            jointDrive.positionSpring = 0;
            joint.angularXDrive = jointDrive;
            joint.angularYZDrive = jointDrive;

        }

    }
}
