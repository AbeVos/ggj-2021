using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message
{
    public string date;
    public string author;
    public string text;
}

[System.Serializable]
public class Post
{
    public string date;
    public string author;
    public string text;
    public List<string> tags;
    // public Message[] replies;
}

[System.Serializable]
public class PostContainer
{
    public Post[] posts;
}

public class SocMed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {

        }

        TextAsset posts_json = Resources.Load<TextAsset>("posts");
        PostContainer container = JsonUtility.FromJson<PostContainer>(posts_json.text);

        foreach (Post post in container.posts)
        {
            Debug.Log(post.text);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
