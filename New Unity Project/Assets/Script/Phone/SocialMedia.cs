using System.Collections.Generic;
using System.Linq;
using Script.DataObjects;
using TMPro;
using UnityEngine;

namespace Script
{
    public class SocialMedia : MonoBehaviour
    {
        public GameObject PostPrefab;
        public GameObject ReplyPrefab;
        private List<string> _tags;
        public string Filter;

        private void Start()
        {
            var posts_json = Resources.Load<TextAsset>("posts");
            var container = JsonUtility.FromJson<PostContainer>(posts_json.text);

            _tags = container.posts.SelectMany(x => x.tags).Distinct().ToList();

            foreach (Post post in container.posts)
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
            var dateField = postItem.DateField;
            var bodyField = postItem.BodyField;

            var bodyText = ReplaceTagsForLinks(post.text, postItem);

            authorField.GetComponent<TextMeshProUGUI>().SetText($"{post.author}");
            dateField.GetComponent<TextMeshProUGUI>().SetText($"{post.date}");
            bodyField.GetComponent<TextMeshProUGUI>().SetText(bodyText);

            foreach (var reply in post.replies)
            {
                InstantiateReply(reply, prefab);
            }
        }

        private string ReplaceTagsForLinks(string text, PostItem postItem)
        {
            foreach (var tag in _tags.Where(tag => text.Contains(tag)))
            {
                text = text.Contains($"{tag}s") 
                    ? text.Replace($"#{tag}s", $"<b><color=#9edcff><link={tag}>#{tag}s</link></color></b>") 
                    : text.Replace($"#{tag}", $"<b><color=#9edcff><link={tag}>#{tag}</link></color></b>");
            
                postItem.Tags.Add(tag);
            }

            return text;
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
}