using System;
using System.IO;

namespace AK
{
    public class Image : IDisposable
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        // TODO
        #if PLATFORM_ANDROID

        internal Android.Graphics.Bitmap AndroidBitmap;

        private Image(Android.Graphics.Bitmap bitmap)
        {
            AndroidBitmap = bitmap;
            Width = bitmap.Width;
            Height = bitmap.Height;
        }

        public static Image FromStream(Stream stream)
        {
            var bitmap = Android.Graphics.BitmapFactory.DecodeStream(stream);
            return new Image(bitmap);
        }

        public static Image FromFile(string filename)
        {
            var bytes = GG.GGFile.GetBytes(filename);
            var bitmap = Android.Graphics.BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
            return new Image(bitmap);
        }

        public virtual void Dispose(bool disposing)
        {
            GG.GGUtils.Delete(ref AndroidBitmap, it => it.Recycle());
        }

        #elif PLATFORM_IOS

        internal UIKit.UIImage NativeImage;

        private Image(UIKit.UIImage image)
        {
            NativeImage = image;
            Width = (int)image.Size.Width;
            Height = (int)image.Size.Height;
        }

        public static Image FromStream(Stream stream)
        {
            var memoryStream = stream as MemoryStream;
            if (memoryStream == null)
            {
                memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
            }
            var data = Foundation.NSData.FromArray(memoryStream.ToArray());
            return new Image(new UIKit.UIImage(data));
        }

        public static Image FromFile(string filename)
        {
            var bytes = GG.GGFile.GetBytes(filename);
            var data = Foundation.NSData.FromArray(bytes);
            return new Image(new UIKit.UIImage(data));
        }

        public virtual void Dispose(bool disposing)
        {
            NativeImage = null;
        }

        #endif

        public virtual void Dispose(bool disposing)
        {
            
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}

