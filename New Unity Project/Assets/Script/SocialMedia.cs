using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

public class SocialMedia : MonoBehaviour
{
    public GameObject PostPrefab;
    public GameObject ReplyPrefab;
    private List<string> _tags;
    public string Filter;

    void Start()
    {
        TextAsset posts_json = Resources.Load<TextAsset>("posts");
        PostContainer container = JsonUtility.FromJson<PostContainer>(posts_json.text);

        _tags = container.posts.SelectMany(x => x.tags).ToList();

        var orderedPosts = container.posts.OrderByDescending(x => x.date).ToList();
        foreach (Post post in orderedPosts)
        {
            InstantiatePost(post);
        }
    }

    private void Update()
    {
        FilterPosts();
    }

    private void FilterPosts()
    {
        foreach (Transform child in transform)
        {
            if (Filter == string.Empty)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                var postItem = child.GetComponent<PostItem>();
                if (postItem != null)
                {
                    var shouldShow = postItem.Tags.Contains(Filter);
                    child.gameObject.SetActive(shouldShow);
                }
            }
        }
    }

    private void InstantiatePost(Post post)
    {
        var prefab = Instantiate(PostPrefab, gameObject.transform);
        prefab.transform.SetParent(gameObject.transform, false);

        var postItem = prefab.GetComponent<PostItem>();
        var authorField = postItem.AuthorField;
        var bodyField = postItem.BodyField;

        var bodyText = ReplaceTagsForLinks(post.text, postItem);

        authorField.GetComponent<TextMeshProUGUI>().SetText($"{post.author} - {post.date}");
        bodyField.GetComponent<TextMeshProUGUI>().SetText(bodyText);

        foreach (var reply in post.replies)
        {
            InstantiateReply(reply, prefab);
        }
    }

    private string ReplaceTagsForLinks(string text, PostItem postItem)
    {
        var punctuation = text.Where(char.IsPunctuation).Distinct().ToArray();
        var words = text.Split().Select(x => x.Trim(punctuation)).ToList();
        var output = new List<string>();

        foreach (var word in words)
        {
            if (_tags.Contains(word))
            {
                postItem.Tags.Add(word);
                output.Add($"<color=#1E90FF><link={word}>{word}</link></color>"); //todo: decent color implementation
            }
            else
            {
                output.Add(word);
            }
        }
        return string.Join(" ", output);
    }

    private void InstantiateReply(Message reply, GameObject parent)
    {
        var authorField = ReplyPrefab.GetComponent<PostItem>().AuthorField;
        var bodyField = ReplyPrefab.GetComponent<PostItem>().BodyField;

        authorField.GetComponent<TextMeshProUGUI>().text = $"{reply.author} - {reply.date}";
        bodyField.GetComponent<TextMeshProUGUI>().text = reply.text;

        var prefab = Instantiate(ReplyPrefab, parent.transform);
        prefab.transform.SetParent(parent.transform);
    }
}
