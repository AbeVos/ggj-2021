using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public Message[] replies;
}

[System.Serializable]
public class PostContainer
{
    public Post[] posts;
}

public class SocMed : MonoBehaviour
{
    public GameObject PostPrefab;
    public GameObject ReplyPrefab;
    private string _postFilter;

    void Start()
    {
        TextAsset posts_json = Resources.Load<TextAsset>("posts");
        PostContainer container = JsonUtility.FromJson<PostContainer>(posts_json.text);

        foreach (Post post in container.posts)
        {
            InstantiatePost(post);
        }
    }

    private void Update()
    {

    }

    void InstantiatePost(Post post)
    {
        var authorField = PostPrefab.GetComponent<PostItem>().AuthorField;
        var bodyField = PostPrefab.GetComponent<PostItem>().BodyField;

        authorField.GetComponent<TextMeshProUGUI>().SetText($"{post.author} - {post.date}");
        bodyField.GetComponent<TextMeshProUGUI>().SetText(post.text);

        var prefab = Instantiate(PostPrefab, gameObject.transform);
        prefab.transform.parent = gameObject.transform;

        foreach (var reply in post.replies)
        {
            //InstantiateReply(reply, prefab);
        }
    }

    void InstantiateReply(Message reply, GameObject parent)
    {
        var authorField = PostPrefab.GetComponent<PostItem>().AuthorField;
        var bodyField = PostPrefab.GetComponent<PostItem>().BodyField;

        authorField.GetComponent<Text>().text = $"{reply.author} - {reply.date}";
        bodyField.GetComponent<Text>().text = reply.text;

        var prefab = Instantiate(ReplyPrefab, parent.transform);
        prefab.transform.parent = parent.transform;

    }
}
