using Newtonsoft.Json;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenAI_API.Images
{
    public class ImageGenerationModel
    {
        public enum Version {

            Dalle2,
            Dalle3

        }

        public Version Current { get; set; }

        public ImageGenerationModel(Version model)
        {
            Current = model;
        }

        internal class ImageGenerationModelConverter : JsonConverter<ImageGenerationModel>
        {
            public override ImageGenerationModel ReadJson(JsonReader reader, Type objectType, ImageGenerationModel existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                string stringValue = (string)reader.Value;
               
                
                return stringValue switch
                {
                    "dall-e-2" => new ImageGenerationModel(Version.Dalle2),
                    "dall-e-3" => new ImageGenerationModel(Version.Dalle3),
                    _ => throw new JsonSerializationException($"Unexpected value when converting to ImageGenerationModel: {stringValue}")
                };
            }

            public override void WriteJson(JsonWriter writer, ImageGenerationModel value, JsonSerializer serializer)
            {

                string stringValue = value.Current switch
                {
                    Version.Dalle2 => "dall-e-2",
                    Version.Dalle3 => "dall-e-3",
                    _ => throw new ArgumentException("Invalid enum value", nameof(value)),
                };
                writer.WriteValue(stringValue);
            }
        }
    }
}
