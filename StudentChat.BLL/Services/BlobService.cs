using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using StudentChat.BLL.Configuration;
using StudentChat.BLL.Services.Contracts;
using StudentChat.DAL.Repositories.Contracts;

namespace StudentChat.BLL.Services;

public class BlobService : IBlobService
{
    private readonly BlobEnvironmentConfiguration _envirovment;
    private readonly string _keyCrypt;
    private readonly string _blobPath;
    private readonly IUnitOfWork _unitOfWork;

    public BlobService(IOptions<BlobEnvironmentConfiguration> environment, IUnitOfWork unitOfWork = null)
    {
        _envirovment = environment.Value;
        _keyCrypt = _envirovment.BlobStoreKey;
        _blobPath = _envirovment.BlobStorePath;
        _unitOfWork = unitOfWork;
    }

    public MemoryStream FindFileInStorageAsMemoryStream(string name)
    {
        string[] splitedName = name.Split('.');

        byte[] decodedBytes = DecryptFile(splitedName[0], splitedName[1]);

        var image = new MemoryStream(decodedBytes);

        return image;
    }

    public string FindFileInStorageAsBase64(string name)
    {
        string[] splitedName = name.Split('.');

        byte[] decodedBytes = DecryptFile(splitedName[0], splitedName[1]);

        string base64 = Convert.ToBase64String(decodedBytes);

        return base64;
    }

    public string SaveFileInStorage(string base64, string name, string extension)
    {
        byte[] imageBytes = Convert.FromBase64String(base64);
        string createdFileName = $"{DateTime.Now}{name}"
            .Replace(" ", "_")
            .Replace(".", "_")
            .Replace(":", "_");

        string hashBlobStorageName = HashFunction(createdFileName);

        Directory.CreateDirectory(_blobPath);
        EncryptFile(imageBytes, extension, hashBlobStorageName);

        return $"{hashBlobStorageName}.{extension}";
    }

    public void SaveFileInStorageBase64(string base64, string name, string extension)
    {
        byte[] imageBytes = Convert.FromBase64String(base64);
        Directory.CreateDirectory(_blobPath);
        EncryptFile(imageBytes, extension, name);
    }

    public void DeleteFileInStorage(string name)
    {
        File.Delete($"{_blobPath}{name}");
    }

    public string UpdateFileInStorage(
        string previousBlobName,
        string base64Format,
        string newBlobName,
        string extension)
    {
        DeleteFileInStorage(previousBlobName);

        string hashBlobStorageName = SaveFileInStorage(
        base64Format,
        newBlobName,
        extension);

        return hashBlobStorageName;
    }

    public async Task CleanBlobStorage()
    {
        var base64Files = GetAllBlobNames();

        var existingFilesInDatabase = await _unitOfWork.FileBaseRepository.GetAllAsync();

        List<string> existingMedia = new();
        existingMedia.AddRange(existingFilesInDatabase.Select(img => img.BlobName));

        var filesToRemove = base64Files.Except(existingMedia).ToList();

        foreach (var file in filesToRemove)
        {
            Console.WriteLine($"Deleting {file}...");
            DeleteFileInStorage(file);
        }
    }

    private IEnumerable<string> GetAllBlobNames()
    {
        var paths = Directory.EnumerateFiles(_blobPath);

        return paths.Select(p => Path.GetFileName(p));
    }

    private string HashFunction(string createdFileName)
    {
        using (var hash = SHA256.Create())
        {
            Encoding enc = Encoding.UTF8;
            byte[] result = hash.ComputeHash(enc.GetBytes(createdFileName));
            return Convert.ToBase64String(result).Replace('/', '_');
        }
    }

    private void EncryptFile(byte[] imageBytes, string type, string name)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(_keyCrypt);

        byte[] iv = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(iv);
        }

        byte[] encryptedBytes;
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.Key = keyBytes;
            aes.IV = iv;
            ICryptoTransform encryptor = aes.CreateEncryptor();
            encryptedBytes = encryptor.TransformFinalBlock(imageBytes, 0, imageBytes.Length);
        }

        byte[] encryptedData = new byte[encryptedBytes.Length + iv.Length];
        Buffer.BlockCopy(iv, 0, encryptedData, 0, iv.Length);
        Buffer.BlockCopy(encryptedBytes, 0, encryptedData, iv.Length, encryptedBytes.Length);
        File.WriteAllBytes($"{_blobPath}{name}.{type}", encryptedData);
    }

    private byte[] DecryptFile(string fileName, string type)
    {
        byte[] encryptedData = File.ReadAllBytes($"{_blobPath}{fileName}.{type}");
        byte[] keyBytes = Encoding.UTF8.GetBytes(_keyCrypt);

        byte[] iv = new byte[16];
        Buffer.BlockCopy(encryptedData, 0, iv, 0, iv.Length);

        byte[] decryptedBytes;
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.Key = keyBytes;
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor();
            decryptedBytes = decryptor.TransformFinalBlock(encryptedData, iv.Length, encryptedData.Length - iv.Length);
        }

        return decryptedBytes;
    }
}