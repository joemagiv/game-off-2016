using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Typewriter : MonoBehaviour {
	
	public float timeBetweenNextText;

	
	public Text textBox;
	
	public GameObject logo;
	public GameObject gameStartUI;
	
	public Animator tvAnimator;
	
	private MusicManager musicManager;
	
	
     //Store all your text in this string array
	private string[] goatText = new string[]{"In 1949 used car salesman Earl 'Madman' Muntz became fascinated with television sets.",
	 "A self-taught electrical engineer he would tear apart a set until it stopped working and would then replace the last piece before it broke.", 
	 "Through this process (which would later be known as 'Muntzing') he discovered many components of modern sets were unnecessary.", 
	 "He then began selling his stripped down 'Muntz TVs' for less than $100, a fraction of the cost of mainstream competitors.",
	 "Incidentally, he coined and popularized the term 'TV'. Primarily because 'Television' would not fit on a airplane banner.",
	 "This new affordability helped increase the widespread adoption of TV across America, and the rest is broadcast history..." };
	 int currentlyDisplayingText = 0;
 
	 void Start() {
	 	gameStartUI.SetActive(false);
	 	musicManager = FindObjectOfType<MusicManager>().GetComponent<MusicManager>();
	 }
	
	
	 
	 void Awake () {
		 Invoke("DismissLogo",5f);
	 }
	
	void DismissLogo(){
		tvAnimator.SetTrigger("TVquickFuzz");
		logo.SetActive(false);
		Invoke("StartText",1.5f);
	}
	
	void StartText(){
		StartCoroutine(AnimateText());
		Invoke("NextText01",timeBetweenNextText);
	}
 
	 void NextText01(){
	 	SkipToNextText();
	 	Invoke("NextText02",timeBetweenNextText);
	 }
 
	void NextText02(){
	 	SkipToNextText();
		Invoke("NextText03",timeBetweenNextText);
	}
	
	void NextText03(){
		SkipToNextText();
		Invoke("NextText04",timeBetweenNextText);
	}
	
	void NextText04(){
		SkipToNextText();
		Invoke("NextText05",timeBetweenNextText);
	}
	
	void NextText05(){
		SkipToNextText();
		Invoke("PretitlePause",timeBetweenNextText);
	}
	
	void PretitlePause(){
		tvAnimator.SetTrigger("TVquickFuzz");
		textBox.text = "";
		Invoke("TitleDisplay",2.7f);
		
	}
	
	void TitleDisplay(){
		
		textBox.alignment = TextAnchor.UpperCenter;
		textBox.fontSize = 140;
		textBox.text = "Muntz";
		Invoke("GameStartUIDisplay", 4f);
	}
	
	void GameStartUIDisplay(){
		gameStartUI.SetActive(true);
		musicManager.StartMainGameMusic();
	}
	
	public void StartGame(){
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.buildIndex+1);
	}
	
	//void NextText07(){
	//	SkipToNextText();
	//	Invoke("NextText07",timeBetweenNextText);
	//}
 
     //This is a function for a button you press to skip to the next text
 public void SkipToNextText(){
	 StopAllCoroutines();
	 currentlyDisplayingText++;
    //If we've reached the end of the array, do anything you want. I just restart the example text
	 if (currentlyDisplayingText>goatText.Length) {
		 currentlyDisplayingText=0;
	 }
	 StartCoroutine(AnimateText());
 }
 
    //Note that the speed you want the typewriter effect to be going at is the yield waitforseconds (in my case it's 1 letter for every      0.03 seconds, replace this with a public float if you want to experiment with speed in from the editor)
 IEnumerator AnimateText(){
	 
	 for (int i = 0; i < (goatText[currentlyDisplayingText].Length+1); i++)
	 {
		 textBox.text = goatText[currentlyDisplayingText].Substring(0, i);
		 yield return new WaitForSeconds(.03f);
	 }
 }
}
