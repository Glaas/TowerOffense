using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using NaughtyAttributes;
public class JsonCreator : MonoBehaviour
{
    public List<string> jsonList;

    [Button("CreateJson")]
    public void JsonCreate()
    {
        string sampleText = GenerateLoremIpsum();
       // JSONNode myNode = JSON.Parse();
        //write 1 paragraph of lorem ipsum

       // myNode.Add("name", null);
       // print(myNode);

        string GenerateLoremIpsum()
        {
            return "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        }
    }
}