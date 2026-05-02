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
}