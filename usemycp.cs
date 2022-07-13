using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;



public class usemycp : MonoBehaviour
{
    public Text output;
    public string urlImg;
    public Image img,img1,img2,img3;
    public string urlImg1;
    string user;
    string jsonData;
    string title1;
    string aa;
    string re;
    string cp;
    string couponid;
    

    void Start()
    {   
        user = PlayerPrefs.GetString("id1");
        re = PlayerPrefs.GetString("rec");
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ2ZXJzaW9uIjoiMC4xLjAiLCJpYXQiOjE2MzA0NzgyMjB9.U2aJ-AvIVAj8hGcVAc8u-1U07UxW6FainC8Se83max8");


        string url = "https://arapi.huajaiit.com/api/coupon/used/" + user + "/1/novalue";
        Debug.Log(url);
        WWWForm form = new WWWForm();

        form.AddField("receivedId", re);
        form.AddField("user_id", user);

        byte[] rawData = form.data;

        WWW www = new WWW(url, rawData, headers);
        yield return www;
        jsonData = www.text;
        JSONNode jsonNode = SimpleJSON.JSON.Parse(jsonData);
        Debug.Log("jsonNode: " + jsonNode.ToString());

        if (!string.IsNullOrEmpty(www.error))
        {
            aa = (jsonNode.ToString().Replace("\"", "").Replace("{", "").Replace("message:", "").Replace("}", ""));
            output.text = (aa);
            img.enabled=false;
		    img1.enabled=false;
            img2.enabled=false;
		    img3.enabled=false;
        }
        else
        {
            Image[] imgr ={img,img1,img2,img3};
            for(int i=0;i<4;i++)
			{   
				if(i<jsonNode.Count)
				    {
                    cp = jsonNode[i]["couponCode"].ToString();
                    PlayerPrefs.SetString("coupon", cp);

                    couponid = cp.Replace("\"", "");
                    Debug.Log(couponid);

                    string getJSONImg1 = jsonNode[i]["couponCode"].ToString();
                    string replaceQuote1 = ("https://arapi.huajaiit.com/static/" + couponid + ".png");

                    Debug.Log(replaceQuote1);
                    urlImg1 = replaceQuote1.Replace("\\", "");
                    WWW www_img1 = new WWW(urlImg1);
                    yield return www_img1;
                    imgr[i].sprite = Sprite.Create(www_img1.texture, new Rect(0, 0, www_img1.texture.width, www_img1.texture.height), new Vector2(0, 0));
                    imgr[i].enabled=true;
                    }else{
					 Destroy(imgr[i].gameObject);
                }
           }
        }
    }
}
