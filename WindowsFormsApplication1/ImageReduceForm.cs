using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class ImageReduceForm : Form
    {
        public ImageReduceForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var key = this.fileName.Text;
            var ext = this.ext.Text;
            ext = "tif";

            var fileName = $"C:\\Users\\swang\\Pictures\\phone-photo\\{key}.{ext}";
            Bitmap b = new Bitmap(fileName);

            var newImage = new Bitmap(b.Width, b.Height);

            Graphics g = Graphics.FromImage((Image)newImage);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            g.DrawImage(b, 0, 0, b.Width, b.Height);

            g.Dispose();

            newImage.Save($"C:\\Users\\swang\\Pictures\\{key}-01.{ext}", ImageFormat.Jpeg);
        }


        private List<ImageFileInfo> ParseFolder(string folder)
        {
            var imageFileInfoList = new List<ImageFileInfo>();

            var di = new DirectoryInfo(folder);

            var files = di.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {

                var file = files[i];


                var fileInfo = new ImageFileInfo();
                fileInfo.FileName = file.FullName;
                fileInfo.AbsoluteFileName = file.Name;
                fileInfo.FileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FullName);
                fileInfo.Extension = file.Extension.ToUpper();
                fileInfo.AbsoluteDirectory = Path.GetDirectoryName(file.FullName);
                fileInfo.FileSize = (float)(file.Length / 1024);
                fileInfo.ProcessedFileName = Path.Combine(fileInfo.AbsoluteDirectory, fileInfo.FileNameWithoutExtension + "_processed_" + fileInfo.Extension);
                fileInfo.ProcessedFileAbsoluteName = Path.GetFileName(fileInfo.ProcessedFileName);

                Bitmap b = new Bitmap(fileInfo.FileName);
                var newImage = new Bitmap(b.Width, b.Height);

                fileInfo.Width = b.Width;
                fileInfo.Height = b.Height;


                Graphics g = Graphics.FromImage((Image)newImage);

                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                g.DrawImage(b, 0, 0, b.Width, b.Height);

                g.Dispose();

                var format = ImageFormat.Jpeg;
                if (fileInfo.Extension == ".BMP")
                {
                    format = ImageFormat.Bmp;
                }
                if (fileInfo.Extension == ".PMG")
                {
                    format = ImageFormat.Png;
                }
                if (fileInfo.Extension == ".TIF" || fileInfo.Extension == ".TIFF")
                {
                    format = ImageFormat.Tiff;
                }

                newImage.Save(fileInfo.ProcessedFileName, format);

                newImage.Dispose();
                b.Dispose();



                /// read processed file size 
                /// 
                var pFileInfo = new FileInfo(fileInfo.ProcessedFileName);
                fileInfo.ProcessedFileSize = (float)(pFileInfo.Length / 1024.00);
                fileInfo.PercentageSmall = (float)(((fileInfo.FileSize - fileInfo.ProcessedFileSize) / fileInfo.FileSize) * 100.00);

                imageFileInfoList.Add(fileInfo);
            }


            return imageFileInfoList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var folder = @"C:\Users\swang\Pictures\phone-photo";
            folder = @"C:\Users\swang\Pictures\phone-photo\tifs";

            var imageFileInfoList = ParseFolder(folder);

            var or = imageFileInfoList.OrderByDescending(i => i.FileSize).ToList();

            foreach (var item in or)
            {
                System.Diagnostics.Debug.WriteLine($"Origin FileName: {item.AbsoluteFileName}, orgin fileSize: {item.FileSize} KB,  processed fileName: {item.ProcessedFileAbsoluteName}, processed fileSize:{item.ProcessedFileSize} KB,    percentage smaller: {item.PercentageSmall} %");
            }
        }

        private void fileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void ImageReduceForm_Load(object sender, EventArgs e)
        {

        }
    }



    public class ImageFileInfo
    {
        public string AbsoluteDirectory { get; set; }
        public string AbsoluteFileName { get; set; }
        public string FileName { get; set; }
        public string FileNameWithoutExtension { get; set; }
        public string Extension { get; set; }
        public string ProcessedFileName { get; set; }
        public string ProcessedFileAbsoluteName { get; set; }

        public float FileSize { get; set; }
        public float ProcessedFileSize { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
        public float PercentageSmall
        {
            get; set;
        }
    }
}
