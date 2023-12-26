using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeTemplateUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;

    [SerializeField] private Animator recipeTemplate;

    [SerializeField] private SingleIconUI singleIcon;

    private List<SingleIconUI> singleIconList = new List<SingleIconUI>();

    public void SetNewRecipe(RecipeSO recipeSO)
    {
        title.text = recipeSO.Name;

        foreach (var item in singleIconList)
        {
            item.gameObject.SetActive(false);
        }


        for (int i = 0; i < recipeSO.kitchenObjectSOList.Count; i++)
        {

            if (i >= singleIconList.Count)
            {
                var clone = Instantiate(singleIcon, singleIcon.transform.parent);
                singleIconList.Add(clone);
            }
            singleIconList[i].SetKitchenObjectSO(recipeSO.kitchenObjectSOList[i]);

            // TODO: 後で
            //recipeTemplate.Play("LeftRight", 0);
        }

        this.gameObject.SetActive(true);
    }
}
