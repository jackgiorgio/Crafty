using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Discoveries : MonoBehaviour {

	#region Singleton

	public static Discoveries instance;

	private void Awake()
	{
		instance = this;
	}

	#endregion

	public GameObject discoverUI;
	public Animator discoverAnimator;
	public Image icon;
	public TextMeshProUGUI itemName;
	public TextMeshProUGUI description;
	public TextMeshProUGUI title;
	public Image background;

	public List<Item> discoveredItems;
	public List<Item> nextToDiscover;

	public Color standardBackgroundColor;

	private bool isDiscovering = false;

	private void Start()
	{
		discoveredItems = new List<Item>();
	}

	public bool HasDiscovered (Item item)
	{
		return discoveredItems.Contains(item);
	}

	public void Discover (Item item)
	{
		if (isDiscovering)
		{
			nextToDiscover.Add(item);
		} else
		{
			OpenDiscoveryPanel(item);
		}
	
		discoveredItems.Add(item);
	}

	public void OpenDiscoveryPanel (Item item)
	{
		if (!discoverUI.activeSelf)
			discoverUI.SetActive(true);

		Debug.Log(item.name);
		discoverAnimator.SetBool("isOpen", true);

		if (item.customSound == "")
		{
			int rndm = Random.Range(0, 2);
			if (rndm == 0)
				AudioManager.instance.Play("HappyChord01");
			else if (rndm == 1)
				AudioManager.instance.Play("HappyChord02");
			else
				AudioManager.instance.Play("HappyChord03");
		} else
		{
			AudioManager.instance.Play(item.customSound);
		}

		isDiscovering = true;

		icon.sprite = item.icon;
		itemName.text = item.name;
		description.text = item.discoveryText;

		if (item.discoveryTitle == "")
		{
			title.text = "CRAFTED!";
		} else
		{
			title.text = item.discoveryTitle;
		}

		if (item.customColor != Color.black)
		{
			background.color = item.customColor;
		} else
		{
			background.color = standardBackgroundColor;
		}
	}

	public void CloseDiscoveryPanel ()
	{
		AudioManager.instance.Play("Click");

		if (nextToDiscover.Count > 0)
		{
			OpenDiscoveryPanel(nextToDiscover[0]);
			nextToDiscover.RemoveAt(0);
		} else
		{
			isDiscovering = false;
			discoverAnimator.SetBool("isOpen", false);
		}
	}

}
