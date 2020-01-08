using System;


namespace Saxx.Storyblok
{
    public class StoryblokLink
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public LinkType LinkType { get; set; }
        public string FieldType { get; set; }
        public string CachedUrl { get; set; }
    }

    public enum LinkType
    {
        Url,
        Story
    }
}
