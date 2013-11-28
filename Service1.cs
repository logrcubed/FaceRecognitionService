using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using FaceDetectionLibrary;
using System.Net.Mail;


namespace FaceRecogService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ImageUploadService : IImageUpload
    {
        public void FileUpload(string fileName, Stream fileStream)
        {

            FileStream fileToupload = new FileStream("C:\\ImageProcessing\\Images\\Destination\\" + fileName, FileMode.Create);

            byte[] bytearray = new byte[5000000];
            int bytesRead, totalBytesRead = 0;
            fileToupload.Position = 0;

            do
            {
                bytesRead = fileStream.Read(bytearray, 0, bytearray.Length);
                totalBytesRead += bytesRead;
            } while (bytesRead > 0);

            fileToupload.Write(bytearray, 0, totalBytesRead);
            fileToupload.Close();
            fileToupload.Dispose();

            //Task.Run(() => { ImageUploadService.ImageDetectAsync(fileName); });
            //ImageUploadService.ImageDetectAsync(fileName);

            //ImageUploadService.sendMailModule();
        }

        private static void ImageDetectAsync(string fileName)
        {
            string destination_filename = "C:\\ImageProcessing\\Images\\Destination\\" + fileName;

            // facedetection!
            byte[] bytes = Encoding.ASCII.GetBytes(destination_filename);
            unsafe
            {
                FaceDetection fd = new FaceDetection();
                fixed (byte* p = bytes)
                {
                    sbyte* sp = (sbyte*)p;
                    fd.detect(sp);
                }
            }
        }

        private static void sendMailModule()
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("Rpi_ImageRecognitionService@gmail.com");
            mail.To.Add("trilokdude@gmail.com");
            mail.Subject = "Test Mail - 1";
            mail.Body = "Mail with Image as attachment";

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment("C:\\ImageProcessing\\Images\\Detected\\image.jpg");
            mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("trilokdude@gmail.com", "blr560001");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }   
    }
}
