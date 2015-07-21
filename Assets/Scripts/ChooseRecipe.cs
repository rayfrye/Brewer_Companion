using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
public class RecipeInstance
{
	public float ogMeasured;
	public float fgMeasured;
	public float ogPrimary;
	public float ogSecondary;
	public float boilVolMeasured;
	public string notes;
	public float rating;
	public string description;
	public float carbVols;
	public float mashPH;
	public float runoffPH;
	public float runningGravity;
	public float grainSteepTime;
	public float grainSteepTemp;
	public bool includeStarter;
	public float starterSize;
	public bool stirPlate;
	public float oldVol;
	public float oldBoilVol;
	public float oldEfficiency;
	public float desiredIBU;
	public float desiredColor;
	public string colorAdjString;
	public float desiredOG;
	public bool rebalanceScale;

	[SerializeField]
	public Recipe recipe;
}

[System.Serializable]
public class Recipe
{
	public string recipeName;
	public string recipeType;
	public string brewerName;
	public string asstBrewerName;
	public float batchSize;
	public float boilSize;
	public float efficiency;
	public float boilTime;


	public List<RecipeFermentable> recipeFermentables = new List<RecipeFermentable>();
	public List<RecipeHop> recipeHops = new List<RecipeHop> ();

	[SerializeField]
	public Style style;
	[SerializeField]
	public Equipment equipment;
}

[System.Serializable]
public class Mash
{
	public string mashName;
	public float grainWeight;
	public float grainTemp;
	public float boilTemp;
	public float tunTemp;
	public float ph;
	public float spargeTemp;
	public int batch;
	public float batchPct;
	public int batchEven;
	public int batchDrain;
	public int mash39;
	public float tunDeadspace;
	public float biabVol;
	public int biab;
	public string notes;

	public int equipAdjust;
	public float tunVol;
	public float tunMass;
	public float tunHC;
}

[System.Serializable]
public class MashStep
{
	public string mashStepName;
	public int type;
	public float infusion;
	public float stepTemp;
	public float stepTime;
	public float riseTime;
	public float tunAddition;
	public float tunHC;
	public float tunVol;
	public float tunTemp;
	public float tuneMass;
	public float startTemp;
	public float grainTemp;
	public float starVol;
	public float grainWeight;
	public float infusionTemp;
	public float decoctionAmt;
}

[System.Serializable]
public class BaseGrain
{
	public string baseGrainName;
	public string origin;
	public string supplier;
	public int type;
	public int inRecipe;
	public float inventory;
	public float amount;
	public float color;
	public float @yield;
	public float lateExtract;
	public float percent;
	public int notFermentable;
	public int order;
	public float coarseFineDiff;
	public float moisture;
	public float diastaticPower;
	public float protein;
	public float ibuGalPerLb;
	public int addAfterBoil;
	public int recommendMash;
	public float maxInBatch;
	public string notes;
	public float boilTime;
	public double price;
	public string convertGrain;
}

[System.Serializable]
public class Carb
{
	public string carbName;
	public float temperature;
	public int type;
	public string primerName;
	public float carbRate;
	public string notes;
}

[System.Serializable]
public class Age
{
	public string ageName;
	public float primTemp;
	public float primEndTemp;
	public float secTemp;
	public float secEndTemp;
	public float tertTemp;
	public float tertEndTemp;
	public float endAgeTemp;
	public float endTempsSet;
	public float primDays;
	public float secDays;
	public float tertDays;
	public float age;
	public int type;
	public string notes;
}

[System.Serializable]
public class RecipeHop
{
	public int id;
	public float amount;
	public float time;
	public string use;
	public string type;
	public string displayAmount;
	public string displayTime;
	public float ibuContrib;
	public int order;
	public double price;

	[SerializeField]
	public Hop hop;
}

[System.Serializable]
public class Hop
{
	public string hopName;
	public string notes;
	public string origin;
	public float alpha;
	public float beta;
	public string form;
	public float HSI;
}

[System.Serializable]
public class RecipeFermentable
{
	public int id;
	public string type;
	public float amount;
	public float yield;
	public float color;
	public bool addAfterColor;
	public string supplier;
	public float coarseFineDiff;
	public float moisture;
	public float diastaticPower;
	public float protien;
	public float maxInBatch;
	public bool recommendMash;
	public float ibuGalPerLb;
	public string displayAmt;
	public string displayColor;

	[SerializeField]
	public Fermentable fermentable;
}

[System.Serializable]
public class Fermentable
{
	public string fermentableName;
	public string origin;
	public string notes;
	public string extractSubstitute;
	public float potential;
}

[System.Serializable]
public class RecipeYeast
{
	public int id;
	public float starterSize;
	public float amount;
	public float inventory;
	public double price;
	public int order;
	public int inRecipe;
	public float cells;

	[SerializeField]
	public Yeast yeast;
}

[System.Serializable]
public class Yeast
{
	public string yeastName;
	public string lab;
	public string productID;
	public int type;
	public int form;
	public string flOcculation;
	public float minAttenuation;
	public float maxAttenuation;
	public float minTemp;
	public float maxTemp;
	public bool useStarter;
	public bool addToSecondary;
	public int timesCultured;
	public int maxReuse;
	public string cultureDate;
	public string bestFor;
	public string notes;
}

public class ChooseRecipe : MonoBehaviour 
{
	public GameManager gameManager;

	public List<Recipe> recipes;
	public List<Fermentable> fermentables;
	public List<Hop> hops;	

	public Transform recipeContentView;
	public GameObject btn_Recipe;

	void Start()
	{
		testXML ();
		allocateRecipeIDs ();
		drawRecipes ();
	}

	void testXML()
	{
		//Create new XMLDocument object
		XmlDocument xml = new XmlDocument ();
		//Load XML content into a string from file. Forget why I had to do this but it was important.
		//Somethign to do with the XML version or something
		string content = System.IO.File.ReadAllText( Application.dataPath + @"/Resources/XML/Pale Lager Pilsner.bsmx" );
		//Remove &ouml; from text. For some reason Unity was having trouble handling this.
		content = content.Replace ("&ouml;", "").Replace ("&#39;","'");
		//Convert the string to XML
		xml.LoadXml( content );

		//print (xml.DocumentElement.SelectSingleNode ("/Selections/Data/Recipe/F_R_NAME").InnerText);


		//Recipe class should contain everything a recipe needs.
		Recipe newRecipe = new Recipe();
		XmlNode recipeNode = xml.DocumentElement.SelectSingleNode ("/Selections/Data/Recipe");
		newRecipe.recipeName = recipeNode["F_R_NAME"].InnerText;
		newRecipe.brewerName = recipeNode ["F_R_BREWER"].InnerText;
		newRecipe.asstBrewerName = recipeNode ["F_R_ASST_BREWER"].InnerText;
//		newRecipe.batchSize = float.Parse (recipeNode ["BATCH_SIZE"].InnerText);
//		newRecipe.boilSize = float.Parse (recipeNode ["BOIL_SIZE"].InnerText);
//		newRecipe.boilTime = float.Parse (recipeNode ["BOIL_TIME"].InnerText);
//		newRecipe.efficiency = float.Parse (recipeNode ["EFFICIENCY"].InnerText);
		/*
		//Loop through each Hop used.
		foreach (XmlNode node in xml.DocumentElement.SelectNodes ("/RECIPES/RECIPE/HOPS/HOP")) 
		{
			//There are 2 Hop classes, Hop and RecipeHop
			//Hop is all generic information about the hop - name, origin, alpha, etc...
			//RecipeHop is any specific information about the recipe's instance of the hop - amount, time added, type, etc...
			RecipeHop newRecipeHop = new RecipeHop();
			newRecipeHop.amount = float.Parse (node["AMOUNT"].InnerText);
			newRecipeHop.use = node["USE"].InnerText;
			newRecipeHop.time = float.Parse (node["TIME"].InnerText);
			newRecipeHop.type = node["TYPE"].InnerText;
			newRecipeHop.displayAmount = node["DISPLAY_AMOUNT"].InnerText;
			newRecipeHop.displayTime = node["DISPLAY_TIME"].InnerText;

			Hop newHop = new Hop();
			//Add this hop to the RecipeHop
			newRecipeHop.hop = newHop;
			newHop.hopName = node["NAME"].InnerText;
			newHop.origin = node["ORIGIN"].InnerText;
			newHop.alpha = float.Parse (node["ALPHA"].InnerText);
			newHop.beta = float.Parse (node["BETA"].InnerText);
			newHop.notes = node["NOTES"].InnerText;
			newHop.form = node["FORM"].InnerText;
			newHop.HSI = float.Parse (node["HSI"].InnerText);

			//Add the RecipeHop to the list of hops for newRecipe
			newRecipe.recipeHops.Add(newRecipeHop);
		}
	
		//Add newRecipe to the list of all recipes.
		recipes.Add (newRecipe);
		*/
	}

	void allocateRecipeIDs()
	{
		foreach (Recipe recipe in recipes) 
		{
			foreach(RecipeFermentable recipeFermentable in recipe.recipeFermentables)
			{
				recipeFermentable.fermentable = fermentables[recipeFermentable.id];
			}

			foreach(RecipeHop recipeHop in recipe.recipeHops)
			{
				recipeHop.hop = hops[recipeHop.id];
			}
		}
	}

	void drawRecipes()
	{
		foreach (Recipe recipe in recipes) 
		{
			GameObject newBtn = Instantiate (btn_Recipe) as GameObject;
			newBtn.transform.SetParent (recipeContentView);

			Btn_RecipeList btn_recipeList = newBtn.GetComponent<Btn_RecipeList>();
			btn_recipeList.txt_RecipeName.text = recipe.recipeName;
			btn_recipeList.txt_BatchSize.text = "Batch Size: " + recipe.batchSize + " gallons";
			string fermentableBill = "Fermentable Bill:\n";
			foreach(RecipeFermentable recipeFermentable in recipe.recipeFermentables)
			{
				fermentableBill += recipeFermentable.fermentable.fermentableName;// + "(" + recipeFermentable.ferm + "lbs.),";
			}
			btn_recipeList.txt_FermentableBill.text = fermentableBill;
			btn_recipeList.btn_StartBrew.onClick.AddListener(() => gameManager.startPremash(recipe));
		}
	}

	
}
