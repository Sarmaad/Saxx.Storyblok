﻿using System;
using System.Text.Json;

namespace Saxx.Storyblok.Converters
{
    public class StoryblokComponentConverter : System.Text.Json.Serialization.JsonConverter<StoryblokComponent>
    {
        public override StoryblokComponent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // performance is probably abysmal, but System.Text.Json does not support polymorphic deserialization very well
            // https://github.com/dotnet/corefx/issues/38650
            using var doc = JsonDocument.ParseValue(ref reader);
            if (doc.RootElement.TryGetProperty("component", out var componentElement))
            {
                var componentName = componentElement.GetString();
                if (!string.IsNullOrWhiteSpace(componentName))
                {
                    var componentMappings = StoryblokMappings.Mappings;

                    if (componentMappings.ContainsKey(componentName))
                    {
                        var mapping = componentMappings[componentName];
                        
                        return (StoryblokComponent) JsonSerializer.Deserialize(doc.RootElement.GetRawText(), mapping.Type, options);
                    }
                }
            }
            
            // don't call JsonSerializer.Deserizalize, because we'll get a stack overlow
            return new StoryblokComponent
            {
                Uuid = doc.RootElement.GetProperty("_uid").GetGuid(),
                Component = doc.RootElement.GetProperty("component").GetString()
            };
        }

        public override void Write(Utf8JsonWriter writer, StoryblokComponent value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}