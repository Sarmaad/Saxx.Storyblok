using System;
using System.Text.Json;

namespace Saxx.Storyblok.Converters
{
    public class StoryblokLinkConverter : System.Text.Json.Serialization.JsonConverter<StoryblokLink>
    {
        public override StoryblokLink Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);

            doc.RootElement.GetProperty("id").TryGetGuid(out var id);
            if (!Enum.TryParse(doc.RootElement.GetProperty("linktype").GetString(), true, out LinkType linkType))
            {
                linkType = LinkType.Url;
            }
            
            return new StoryblokLink
            {
                Id = id,
                Url = doc.RootElement.GetProperty("url").GetString(),
                LinkType = linkType,
                FieldType = doc.RootElement.GetProperty("fieldtype").GetString(),
                CachedUrl = doc.RootElement.GetProperty("cached_url").GetString()
            };
        }

        public override void Write(Utf8JsonWriter writer, StoryblokLink value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
