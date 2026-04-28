namespace Portfolio_Backend.Helpers;

public class FileUploadHelper
{
    public static async Task<string> SaveFileAsync(
        IFormFile file,
        string rootPath,
        string folderName
    )
    {
        if (file == null || file.Length == 0)
            throw new Exception("Invalid file");

        var uploadsFolder = Path.Combine(rootPath, folderName);

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        var filePath = Path.Combine(uploadsFolder, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);

        await file.CopyToAsync(stream);

        return $"{folderName}/{fileName}";
    }
}