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
    }
    private void Start()
    {

    }
    private void Update()
    {
        index = node.pos.ToString();
        tmpro.text = $"grid :{index}\n" +
        $"local space : {transform.localPosition}\n" +
        $"world space : " + transform.position;
    }

}
