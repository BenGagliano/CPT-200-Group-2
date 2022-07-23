using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{

    public GameObject treeView;
    public GameObject loginView;

    public void MoveToScene()
    {
        StartCoroutine(waiter());

    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(7);
        loginView.gameObject.SetActive(false);
        treeView.gameObject.SetActive(true);
    }
}
