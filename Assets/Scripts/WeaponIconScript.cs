using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIconScript : MonoBehaviour {
    public List<GameObject> weaponList = new List<GameObject>();
    //int lastSelected = 0;
    bool wasDisabled;
    [SerializeField]
    Image weaponImage;
    IEnumerator waitToDisable;
    // Use this for initialization
    private void Start()
    {
        UpdateWeaponList();
        waitToDisable = WaitToDisable();
    }
    public void UpdateWeaponList ()
    {
        weaponList.Clear();
        int i = 0;
        foreach (Transform t in gameObject.transform)
        {
            weaponList.Add(t.gameObject);
            if (i > 3)
                t.gameObject.SetActive(false);
            i++;
        }
    }

    public void CycleWeapon(int currentSelection, int direction)
    {
        int temp = currentSelection;
       // lastSelected = currentSelection;
        foreach (GameObject go in weaponList)
        {
            go.transform.localScale = new Vector3(1f,1f,1f);
            go.transform.SetSiblingIndex(temp % weaponList.Count);
            if (temp % weaponList.Count == 1)
            {
                go.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                weaponImage.sprite = go.transform.Find("Image").GetComponent<Image>().sprite;
                go.GetComponent<Animator>().Play("WeaponIconSelect",0,0);
            }
            if (temp % weaponList.Count >= 3)
                go.SetActive(false);
            else
                go.SetActive(true);
            temp +=direction;
            if (temp < 0)
                temp = weaponList.Count-1;
        }
        StopCoroutine(waitToDisable);
        waitToDisable = WaitToDisable();
        StartCoroutine(waitToDisable);
        if (wasDisabled)
            GetComponent<Animator>().Play("WeaponIconsEnabled", 0, 0);
        wasDisabled = false;
        //  weaponList[currentSelection].transform.SetSiblingIndex(1);
    }
    IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(3f);
        wasDisabled = true;
        GetComponent<Animator>().Play("WeaponIconsDisabled",0,0);
    }
}
