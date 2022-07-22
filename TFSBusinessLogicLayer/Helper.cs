using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechArch.DataHandler.GeneralSettings;
using TechArchDataHandler.FileHandler;
using TechArchDataHandler.General;

namespace TFSBusinessLogicLayer
{
    public static class Helper
    {
        static string path = "";
        public static OutputHandler CreateFolder(string folderName, string rootFileTypeFolder = "")
        {
            try
            {
                if (string.IsNullOrEmpty(rootFileTypeFolder))
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "TFS", folderName);

                    //to avoid overwriting check if folder already exist, if Exist: do nothing 
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
                else
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "TFS", rootFileTypeFolder, folderName);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }

                
                return new OutputHandler { IsErrorOccured = false, Message = "Folder Created Successfully" };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        public static OutputHandler SaveFileFromByteByPath(byte[] fileInBytes, string fileName, string path)
        {
            var fileLocation = ""; //variable for storing image to be set to the database url field
            try
            {
                if (fileInBytes.Length > 0)
                {
                    if (File.Exists(path))
                    {
                        return new OutputHandler
                        {
                            IsErrorOccured = true,
                            IsErrorKnown = true,
                            Message = "Filename already Exist, to avoid overwriting existing files please choose another name for the file"
                        };
                    }

                    File.WriteAllBytes(path, fileInBytes);
                    //fileLocation = Path.Combine(path, fileName);
                    fileLocation = Path.Combine(path);
                }

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

            return new OutputHandler
            {
                IsErrorOccured = false,
                Result = fileLocation
            };
        }

        public static OutputHandler DeleteFolder(string folderName, string rootFileTypeFolder = "")
        {
            try
            {
                if (string.IsNullOrEmpty(rootFileTypeFolder))
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "TFS", folderName);
                }
                else
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "TFS", rootFileTypeFolder, folderName);
                }
                // path = Path.Combine(Directory.GetCurrentDirectory(), "TFS", folderName);
                var isFolderEmpty = IsFolderEmpty(folderName, rootFileTypeFolder);

                if (isFolderEmpty)
                {
                    Directory.Delete(path);
                    return new OutputHandler { IsErrorOccured = false };
                }
                else
                {
                    return new OutputHandler { IsErrorOccured = true, Message = "The File Type you are trying to Delete has a folder which still has files in it, as such it cannot be deleted" };
                }

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        public static bool IsFolderEmpty(string folderName, string rootFileTypeFolder = "")
        {
            if (string.IsNullOrEmpty(rootFileTypeFolder))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "TFS", folderName);
            }
            else
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "TFS", rootFileTypeFolder, folderName);
            }
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        public static OutputHandler RenameFolder(string folderName)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "TFS", folderName);
                // var fullpath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                Directory.CreateDirectory(path);
                return new OutputHandler { };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        public static async Task<OutputHandler> ConvertFileToByte(string fileLocation)
        {
            try
            {
                using FileStream stream = new(fileLocation, FileMode.Open, FileAccess.Read);

                byte[] byteFile = await File.ReadAllBytesAsync(Path.Combine(Directory.GetCurrentDirectory(), fileLocation));

                stream.Read(byteFile, 0, Convert.ToInt32(stream.Length));
                stream.Close();
                return new OutputHandler
                {
                    Result = byteFile
                };
            }
            catch (Exception ex)
            {
                return new OutputHandler
                {
                    Message = ex.Message,
                    IsErrorOccured = true
                };
            }
        }
    }
}
