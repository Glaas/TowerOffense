using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayInfo : MonoBehaviour
{
    public Node node;
    public TextMeshProUGUI tmpro;
    public string index;

    private void Awake()
    {
        node = GetComponent<Node>();
        tmpro = GetComponentInChildren<TextMeshProUGUI>();
        index = node.pos.ToString();
    }
    private void Update()
    {
        tmpro.text = $"grid :{index}\n" +
        $"local space : {transform.localPosition}\n" +
        $"world space : " + transform.position;
    }

}
