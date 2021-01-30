using System.Collections.Generic;
using System.Linq;
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
    private List<string> _tags;

    void Start()
    {
        TextAsset posts_json = Resources.Load<TextAsset>("posts");
        PostContainer container = JsonUtility.FromJson<PostContainer>(posts_json.text);

        _tags = container.posts.SelectMany(x => x.tags).ToList();

        foreach (Post post in container.posts)
        {
            InstantiatePost(post);
        }
    }

    void InstantiatePost(Post post)
    {
        var authorField = PostPrefab.GetComponent<PostItem>().AuthorField;
        var bodyField = PostPrefab.GetComponent<PostItem>().BodyField;

        authorField.GetComponent<TextMeshProUGUI>().SetText($"{post.author} - {post.date}");

        var bodyText = ReplaceTagsForLinks(post.text);
        bodyField.GetComponent<TextMeshProUGUI>().SetText(bodyText);

        var prefab = Instantiate(PostPrefab, gameObject.transform);
        prefab.transform.SetParent(gameObject.transform, false);

        foreach (var reply in post.replies)
        {
            //InstantiateReply(reply, prefab);
        }
    }

    string ReplaceTagsForLinks(string text)
    {
        var punctuation = text.Where(char.IsPunctuation).Distinct().ToArray();
        var words = text.Split().Select(x => x.Trim(punctuation)).ToList();
        var output = new List<string>();

        foreach (var word in words)
        {
            if (_tags.Contains(word))
            {
                output.Add($"<color=#1E90FF><link={word}>{word}</link></color>"); //todo: decent color implementation
            }
            else
            {
                output.Add(word);
            }
        }
        return string.Join(" ", output);
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
