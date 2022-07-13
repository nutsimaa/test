using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;



public class showcp: MonoBehaviour
{
   
    string user;
    string jsonData;
    public string urlImg;
    public Image img;
	public string urlImg1;
    string cp;
    string couponid;
    string message;
    string re;
   

    void Start()
    { 
        user=PlayerPrefs.GetString("id1");
        re=PlayerPrefs.GetString("rec");
        
        StartCoroutine(Upload());
    } 

    IEnumerator Upload()
    {Dictionary<string, string> headers = new Dictionary<string,string>();
        headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ2ZXJzaW9uIjoiMC4xLjAiLCJpYXQiOjE2MzA0NzgyMjB9.U2aJ-AvIVAj8hGcVAc8u-1U07UxW6FainC8Se83max8");
        
       
        string url = "https://arapi.huajaiit.com/api/coupon/pedding/"+user+"/1/novalue";
        
        WWWForm form = new WWWForm();
       
        form.AddField("_id",re);
        form.AddField("couponCode","00000000");

        byte[] rawData = form.data;

        WWW www = new WWW(url, rawData, headers);
        yield return www;
        jsonData = www.text;
		JSONNode jsonNode = SimpleJSON.JSON.Parse(jsonData);
      
        message = jsonNode.ToString();
        Debug.Log(message);
              
            string getJSONImg1 = jsonNode[0]["couponCode"].ToString();
            string replaceQuote1 =("https://arapi.huajaiit.com/static/"+getJSONImg1.Replace("\"","")+".png");
            
			
            Debug.Log(replaceQuote1);
			urlImg1 = replaceQuote1.Replace("\\", "");
			WWW www_img1 = new WWW(urlImg1);
			yield return www_img1;
			img.sprite = Sprite.Create(www_img1.texture, new Rect(0, 0, www_img1.texture.width, www_img1.texture.height), new Vector2(0, 0));

            
        

    
        if(!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
        else
        {
             
             
         
          Debug.Log("Compleate!");
          

           
        }
    }

    

   
}
