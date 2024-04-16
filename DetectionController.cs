using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
    public string _tagTargetDetection = "Player";

    public List<Collider2D> detectedObjs = new List<Collider2D>(); // Lista de objetos detectados

    // M�todo chamado quando um objeto entra na �rea de colis�o deste objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == _tagTargetDetection)
        {
        
            detectedObjs.Add(collision);
        }
    }

    // M�todo chamado quando um objeto sai da �rea de colis�o deste objeto
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _tagTargetDetection)
        {
            
            detectedObjs.Remove(collision);
        }
    }
}
