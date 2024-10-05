using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowDamage : MonoBehaviour
{
    public float moveSpeed;
    public int damage;
    public Text showDamage;
    
    void Update()
    {
        showDamage.text = "-" + damage.ToString();
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        StartCoroutine(clear());
    }
    IEnumerator clear()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
