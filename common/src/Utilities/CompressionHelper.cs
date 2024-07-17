using SharpCompress.Compressors.BZip2;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Blogposts.Common.Utilities
{
    /// <summary>
    /// Helper class that handles compression and decompression
    /// </summary>
    public static class CompressionHelper
    {
        #region Public methods

        /// <summary>
        /// Transforms the given string to a byte array and calls the compress function
        /// Then it converts it to base 64 encoding
        /// </summary>
        /// <param name="input">The string to be compressed</param>
        /// <param name="compressionType">The compression algorithm</param>
        /// <returns>The compressed string</returns>
        public static string Compress(string input,
            CompressionType compressionType = CompressionType.GZIP)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            byte[] encoded = Encoding.UTF8.GetBytes(input);

            byte[] compressed = compressionType switch
            {
                CompressionType.GZIP => CompressGzip(encoded),
                CompressionType.BZIP2 => CompressBzip2(encoded),
                _ => CompressGzip(encoded)
            };

            return Convert.ToBase64String(compressed);
        }

        /// <summary>
        /// Takes the compressed string and converts it to a byte array
        /// Then it calls the respective method to decompress it
        /// </summary>
        /// <param name="input">The compressed string</param>
        /// <returns>The de compressed string</returns>
        public static string Decompress(string input,
            CompressionType compressionType = CompressionType.GZIP)
        {
            if (string.IsNullOrEmpty(input) || !IsBase64String(input))
            {
                return null;
            }

            byte[] compressed = Convert.FromBase64String(input);

            byte[] decompressed = null;

            decompressed = compressionType switch
            {
                CompressionType.GZIP => DecompressGzip(compressed),
                CompressionType.BZIP2 => DecompressBzip2(compressed),
                _ => DecompressGzip(compressed)
            };

            return Encoding.UTF8.GetString(decompressed);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Checks if the given string is base 64 encoded string
        /// </summary>
        /// <param name="input">The string to be checked</param>
        /// <returns>True if the string is base 64</returns>
        private static bool IsBase64String(string input)
        {
            var buffer = new Span<byte>(new byte[input.Length]);
            return Convert.TryFromBase64String(input, buffer, out int bytesParsed);
        }

        /// <summary>
        /// Compresses a byte array and returns the compressed object
        /// </summary>
        /// <param name="input">The byte array to be compressed</param>
        /// <returns>The compressed byte array</returns>
        private static byte[] CompressBzip2(byte[] input)
        {
            using (var result = new MemoryStream())
            {
                using (var gZipStream = new BZip2Stream(result, SharpCompress.Compressors.CompressionMode.Compress, false))
                {
                    gZipStream.Write(input, 0, input.Length);
                    gZipStream.Flush();
                }
                return result.ToArray();
            }
        }

        /// <summary>
        /// Decompresses a byte array
        /// </summary>
        /// <param name="input">The byte array to be decompressed</param>
        /// <returns>The byte array</returns>
        private static byte[] DecompressBzip2(byte[] input)
        {
            using (var inputStream = new MemoryStream(input))
            using (var gZipStream = new BZip2Stream(inputStream, SharpCompress.Compressors.CompressionMode.Decompress, false))
            using (var outputStream = new MemoryStream())
            {
                gZipStream.CopyTo(outputStream);
                return outputStream.ToArray();
            }
        }

        /// <summary>
        /// Compresses a byte array and returns the compressed object
        /// </summary>
        /// <param name="input">The byte array to be compressed</param>
        /// <returns>The compressed byte array</returns>
        private static byte[] CompressGzip(byte[] input)
        {
            using (var result = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(result, CompressionMode.Compress))
                {
                    gZipStream.Write(input, 0, input.Length);
                    gZipStream.Flush();
                }

                return result.ToArray();
            }
        }

        /// <summary>
        /// Decompresses a byte array
        /// </summary>
        /// <param name="input">The byte array to be decompressed</param>
        /// <returns>The byte array</returns>
        private static byte[] DecompressGzip(byte[] input)
        {
            using (var inputStream = new MemoryStream(input))
            using (var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            using (var outputStream = new MemoryStream())
            {
                gZipStream.CopyTo(outputStream);
                return outputStream.ToArray();
            }
        }

        #endregion
    }

    public enum CompressionType : byte
    {
        GZIP,
        BZIP2
    }
}