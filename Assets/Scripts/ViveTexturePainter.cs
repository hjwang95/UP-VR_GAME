/// <summary>
/// CodeArtist.mx 2015
/// This is the main class of the project, its in charge of raycasting to a model and place brush prefabs infront of the canvas camera.
/// If you are interested in saving the painted texture you can use the method at the end and should save it to a file.
/// </summary>

using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Valve.VR;

public class ViveTexturePainter : MonoBehaviour
{
    public const float MAX_DISTANCE = 1.0f;

    public SteamVR_Action_Single squeeze = null;
    public static GameObject spray;
    public static Color brushColor; //The selected color

    public GameObject brushContainer; //The cursor that overlaps the model and our container for the brushes painted
    public Camera canvasCam;  //The camera that looks at the model, and the camera that looks at the canvas.
    public Sprite cursorPaint; // Cursor for the differen functions 
    public RenderTexture canvasTexture; // Render Texture that looks at our Base Texture and the painted brushes
    public Material baseMaterial; // The material of our base texture (Were we will save the painted texture)

    

    float brushSize = 1.0f; //The size of our brush

    int brushCounter = 0, MAX_BRUSH_COUNT = 1000; //To avoid having millions of brushes
    bool saving = false; //Flag to check if we are saving the texture

    public static event EventHandler spraying;

    private void Awake()
    {
    }

    void Update()
    {
        float squeezeValue = squeeze.GetAxis(SteamVR_Input_Sources.Any);
        if (squeezeValue > 0)
        {
            //Debug.Log("Trigger in painter: " + squeezeValue);
           
            DoAction();
        }
    }

    //The main action, instantiates a brush or decal entity at the clicked position on the UV map
    void DoAction()
    {
        if (saving)
            return;
        Vector3 uvWorldPosition = Vector3.zero;
        if (HitTestUVPosition(ref uvWorldPosition))
        {
            GameObject brushObj;

            brushColor.a = 1f / Mathf.Exp(brushSize * 150); // Brushes have alpha to have a merging effect when painted over.

            //Paint mode 
            brushObj = (GameObject)Instantiate(Resources.Load("TexturePainter-Instances/BrushEntity")); //Paint a brush
            brushObj.GetComponent<SpriteRenderer>().color = brushColor; //Set the brush color

            brushObj.transform.parent = brushContainer.transform; //Add the brush to our container to be wiped later
            brushObj.transform.localPosition = uvWorldPosition; //The position of the brush (in the UVMap)
            brushObj.transform.localScale = Vector3.one * brushSize;//The size of the brush
            
            OnSpraying();
        }
        brushCounter++; //Add to the max brushes
        if (brushCounter >= MAX_BRUSH_COUNT)
        { //If we reach the max brushes available, flatten the texture and clear the brushes
           // saving = true;
            //Invoke("SaveTexture", 0.1f);
        }
    }
    
    //Returns the position on the texuremap according to a hit in the mesh collider
    bool HitTestUVPosition(ref Vector3 uvWorldPosition)
    {
        RaycastHit hit;
        if (spray != null)
        {
            Ray cursorRay = new Ray(spray.transform.position, -spray.transform.forward);
            if (Physics.Raycast(cursorRay, out hit, MAX_DISTANCE))
            {
                brushSize = (hit.distance / MAX_DISTANCE) * .01f;

                MeshCollider meshCollider = hit.collider as MeshCollider;
                if (meshCollider == null || meshCollider.sharedMesh == null)
                    return false;
                Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
                uvWorldPosition.x = pixelUV.x - canvasCam.orthographicSize;//To center the UV on X
                uvWorldPosition.y = pixelUV.y - canvasCam.orthographicSize;//To center the UV on Y
                uvWorldPosition.z = 0.0f;
                return true;
            }
        }
        return false;

    }

    protected virtual void OnSpraying()
    {
        if (spraying != null)
        {
            spraying(this, EventArgs.Empty);
        }
    }

    //Sets the base material with a our canvas texture, then removes all our brushes
    void SaveTexture()
    {
        brushCounter = 0;
        System.DateTime date = System.DateTime.Now;
        RenderTexture.active = canvasTexture;
        Texture2D tex = new Texture2D(canvasTexture.width, canvasTexture.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, canvasTexture.width, canvasTexture.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null;
        baseMaterial.mainTexture = tex; //Put the painted texture as the base
        foreach (Transform child in brushContainer.transform)
        {//Clear brushes
            Destroy(child.gameObject);
        }
        //StartCoroutine ("SaveTextureToFile"); //Do you want to save the texture? This is your method!
        Invoke("ShowCursor", 0.1f);
    }
    //Show again the user cursor (To avoid saving it to the texture)
    void ShowCursor()
    {
        saving = false;
    }

    ////////////////// PUBLIC METHODS //////////////////

    
    public void SetBrushSize(float newBrushSize)
    { //Sets the size of the cursor brush or decal
        brushSize = newBrushSize;
        //brushCursor.transform.localScale = Vector3.one * brushSize;
    }

    ////////////////// OPTIONAL METHODS //////////////////

#if !UNITY_WEBPLAYER
    IEnumerator SaveTextureToFile(Texture2D savedTexture)
    {
        brushCounter = 0;
        string fullPath = System.IO.Directory.GetCurrentDirectory() + "\\UserCanvas\\";
        System.DateTime date = System.DateTime.Now;
        string fileName = "CanvasTexture.png";
        if (!System.IO.Directory.Exists(fullPath))
            System.IO.Directory.CreateDirectory(fullPath);
        var bytes = savedTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(fullPath + fileName, bytes);
        Debug.Log("<color=orange>Saved Successfully!</color>" + fullPath + fileName);
        yield return null;
    }
#endif
}
