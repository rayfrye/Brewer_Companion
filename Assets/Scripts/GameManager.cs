using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public Timer timer;

	public ChooseRecipe chooseRecipe;
	public PreMash preMash;

	public BrewStep currentBrewStep;
	public enum BrewStep
	{
		chooseRecipe
		,premash
		,mash
		,mashout
		,sparge
		,boil
		,chill
		,pitchyeast
		,ferment
	}

	public GameObject recipeList;
	public Recipe currentRecipe;
	
	void Update()
	{
		doCurrentStep ();
	}

	void doCurrentStep()
	{
		switch (currentBrewStep) 
		{
			case BrewStep.chooseRecipe:
			{
				doChooseRecipe();
				break;
			}
			default:
			{
				break;
			}
		}
	}

	void doChooseRecipe()
	{
		//chooseRecipe
	}

	public void startPremash(Recipe recipe)
	{
		currentBrewStep = BrewStep.premash;
		recipeList.SetActive (false);
		currentRecipe = recipe;
	}
}
