using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject PostPrefab;

    void Start()
    {
        TextAsset posts_json = Resources.Load<TextAsset>("posts");
        PostContainer container = JsonUtility.FromJson<PostContainer>(posts_json.text);

        foreach (Post post in container.posts)
        {
            InstantiatePost(post);
        }
    }

    void InstantiatePost(Post post)
    {
        var authorField = PostPrefab.GetComponent<PostItem>().AuthorField;
        var bodyField = PostPrefab.GetComponent<PostItem>().BodyField;
        var tagsField = PostPrefab.GetComponent<PostItem>().TagsField;

        authorField.GetComponent<Text>().text = post.author;
        bodyField.GetComponent<Text>().text = post.text;
        tagsField.GetComponent<Text>().text = TagsToString(post.tags);

        var prefab = Instantiate(PostPrefab, gameObject.transform);
        prefab.transform.parent = gameObject.transform;
    }

    string TagsToString(List<string> tags)
    {
        var tagsText = "";
        foreach (var tag in tags)
        {
            tagsText = $"{tagsText} #{tag}";
        }
        return tagsText;
    }
}
