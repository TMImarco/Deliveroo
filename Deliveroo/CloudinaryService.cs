namespace Deliveroo;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IConfiguration config)
    {
        var account = new Account(
            config["Cloudinary:CloudName"],
            config["Cloudinary:ApiKey"],
            config["Cloudinary:ApiSecret"]
        );

        _cloudinary = new Cloudinary(account);
    }

    public string UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return null;

        using (var stream = file.OpenReadStream())
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream)
            };

            var result = _cloudinary.Upload(uploadParams);

            return result.SecureUrl.ToString(); // 🔥 QUESTO è quello che salvi nel DB
        }
    }

    private string GetThumbnailUrl(string publicId)
    {
        var transformation = new Transformation()
            .Width(150)
            .Height(150)
            .Crop("fill")
            .Quality("auto")
            .FetchFormat("auto");

        return _cloudinary.Api.UrlImgUp
            .Transform(transformation)
            .Secure(true)
            .BuildUrl(publicId);
    }
    
    public List<object> GetImmagini()
    {
        var tutte = new List<object>();
        string? nextCursor = null;

        do
        {
            var listParams = new ListResourcesParams()
            {
                ResourceType = ResourceType.Image,
                MaxResults = 500, // massimo per chiamata
                NextCursor = nextCursor // pagina successiva
            };

            var result = _cloudinary.ListResources(listParams);

            var batch = result.Resources
                .Select(r => (object)new
                {
                    thumbUrl = GetThumbnailUrl(r.PublicId),
                    url = r.SecureUrl.ToString(),
                    publicId = r.PublicId
                });

            tutte.AddRange(batch);

            nextCursor = result.NextCursor; // null = non ci sono altre pagine
        } while (nextCursor != null);

        return tutte;
    }
}