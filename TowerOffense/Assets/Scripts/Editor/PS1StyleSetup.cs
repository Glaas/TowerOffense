//This code was written by Sebastien "Glaas" Decuyper
//https://sebdec.net/aboutme.html
//s.decuyper314@gmail.com
//If you have any suggestions, bugs to report, ideas for improvement, always feel free to contact me !

//This is mostly an automation of this AWESOME tutorial by Max Turnbull @beakfriends 
//https://beakfriends.medium.com/tutorial-easy-lo-fi-pixel-goodness-in-unity-dc8fc999d2de
//THANK YOU SO MUCH MAX

//To use : Create a c# script called "PS1StyleSetup" and place it anywhere in your Assets folder. Once that's done, you should have a "Tools" menu at the top of your Unity editor, next to 
//Component and Window. Go to Tools > SebEssentials > Setup PS1 Style

//Feel free to get rid of the namepace, it isn't relevant if you're using only this script. It refers to the tools library I'm building at https://github.com/Glaas/SebEssentials

using UnityEngine;
using UnityEditor;

namespace SebEssentials
{
    public class PS1StyleSetup : UnityEditor.ScriptableWizard
    {
        public enum AspectRatio
        {
            [System.ComponentModel.Description("4/3 (recommended)")]
            _4_3,
            [System.ComponentModel.Description("16/9")]
            _16_9,
            [System.ComponentModel.Description("16/10")]
            _16_10
        }
        [Tooltip("Your fave aspect ratio ! RECOMMENDED : 4/3 for maximum nostalgia goodness !")]
        public AspectRatio aspectRatio = AspectRatio._4_3;
        [Tooltip("The double camera thing ! WARNING: will delete every pre-existing camera in the scene !")]
        public bool SetUpCameras = true;
        [Tooltip("Create a render texture and a material, and put them together in a neat little folder called \"PS1Setup\" in your Assets folder.")]
        public bool CreateRenderTexture = true;
        [Tooltip("Disable Anti-aliasing for all quality settings. So if you forget to set it up on your camera, you're still good !")]
        public bool TurnOffAntiAliasingInQualitySettings = true;

        [MenuItem("Tools/SebEssentials/Setup PS1 style")]
        static void CreateWizard()
        {
            ScriptableWizard.DisplayWizard<PS1StyleSetup>("PS1 stylezzz", "Low-rez for the win !");
        }

        private void OnWizardCreate()
        {
            SetUpCamerasFunc();
            if (TurnOffAntiAliasingInQualitySettings) QualitySettings.antiAliasing = 1;
        }

        void SetUpCamerasFunc()
        {
            foreach (Camera cam in FindObjectsOfType<Camera>()) //Destroys all cameras
            {
                Destroy(cam.gameObject);
            }
            //Creates the 2 Cameras needed for the setup
            GameObject mainCamGO = new GameObject();
            GameObject RDtextCamGO = new GameObject();

            //Creates the quad with the texture being projected on
            GameObject textQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            //Make it a child of the correct camera
            textQuad.transform.SetParent(RDtextCamGO.transform);
            //Give it the correct aspect ratio and place it in front of the camera

            switch (aspectRatio)
            {
                case AspectRatio._4_3:
                    textQuad.transform.localScale = new Vector3(4, 3, 0);
                    break;
                case AspectRatio._16_9:
                    textQuad.transform.localScale = new Vector3(16, 9, 0);
                    break;
                case AspectRatio._16_10:
                    textQuad.transform.localScale = new Vector3(16, 10, 0);
                    break;
                default:
                    throw new System.NotImplementedException();
            }

            textQuad.transform.position = new Vector3(0, 0, 1);
            //Send them into the uncaring void below
            RDtextCamGO.transform.position = new Vector3(-5000, -5000, 0);
            //Let's get rid of that "New Game Object" name nonsense >:)
            mainCamGO.name = "MainCamera";
            RDtextCamGO.name = "RenderTextCam";

            //Giving them actual cameras would be nice
            Camera mainCam = mainCamGO.AddComponent<Camera>();
            Camera RdTextCam = RDtextCamGO.AddComponent<Camera>();

            //Gimme those crunchy pixels
            mainCam.allowMSAA = false;

            //Just to be sure the default values are not garbo
            RdTextCam.clearFlags = CameraClearFlags.Depth;
            RdTextCam.backgroundColor = Color.black;
            RdTextCam.orthographic = true;
            RdTextCam.allowMSAA = false;

            if (!CreateRenderTexture) return; //But why :'(

            //I still recommend 4/3 ! It's not too late !!!
            int height, width;
            switch (aspectRatio)
            {
                case AspectRatio._4_3:
                    width = 320;
                    height = 240;
                    RdTextCam.orthographicSize = 1.5f;
                    break;
                case AspectRatio._16_9:
                    width = 426;
                    height = 240;
                    RdTextCam.orthographicSize = 4.5f;
                    break;
                case AspectRatio._16_10:
                    width = 384;
                    height = 240;
                    RdTextCam.orthographicSize = 5f;
                    break;
                default:
                    throw new System.NotImplementedException();
            }
            RenderTexture rd = new RenderTexture(width, height, 24);
            rd.antiAliasing = 1;
            rd.useMipMap = false;
            //I always forget this one urgh
            rd.filterMode = FilterMode.Point;

            //Create a folder to put the things that we need it. Keep it organized !
            AssetDatabase.CreateFolder("Assets", "PS1Setup");
            AssetDatabase.CreateAsset(rd, "Assets/PS1Setup/RenderTexture.renderTexture");
            Material rdMat = new Material(Shader.Find("Unlit/Texture"));
            AssetDatabase.CreateAsset(rdMat, "Assets/PS1Setup/renderTextureMaterial.mat");
            rdMat.mainTexture = (RenderTexture)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(rd), rd.GetType());

            //Assign all the things and we're done !
            mainCam.targetTexture = rd;
            textQuad.GetComponent<MeshRenderer>().material = rdMat;
        }
    }
}