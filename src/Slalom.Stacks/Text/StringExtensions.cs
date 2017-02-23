using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Slalom.Stacks.Text
{
    /// <summary>
    /// Contains extensions for <see cref="String"/> objects.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Compresses the specified string instance.
        /// </summary>
        /// <param name="instance">The instance to compress.</param>
        /// <returns>Returns the compressed string.</returns>
        public static string Compress(this string instance)
        {
            var bytes = Encoding.UTF8.GetBytes(instance);

            using (var inStream = new MemoryStream(bytes))
            {
                using (var outStream = new MemoryStream())
                {
                    using (var zip = new GZipStream(outStream, CompressionMode.Compress))
                    {
                        inStream.CopyTo(zip);
                    }

                    return Convert.ToBase64String(outStream.ToArray());
                }
            }
        }

        /// <summary>
        /// Resizes the specified string.
        /// </summary>
        /// <param name="text">The specified string.</param>
        /// <param name="count">The desired length.</param>
        /// <param name="pad">The pad character if needed.</param>
        /// <returns>A String of the specified length.</returns>
        public static string Resize(this string text, int count, char pad)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            return new string(text.Take(count).ToArray()).PadRight(count, pad);
        }

        /// <summary>
        /// Returns a copy of the string in camel case.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>Returns a copy of the string in camel case.</returns>
        public static string ToCamelCase(this string instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            return instance.Substring(0, 1).ToLowerInvariant() + instance.Substring(1);
        }

        /// <summary>
        /// Returns a copy of the string delimited with the specified delimiter.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>Returns a copy of the string with the added delimiter.</returns>
        /// <exception cref="System.ArgumentNullException">instance</exception>
        public static string ToDelimited(this string instance, string delimiter)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            return string.Concat(instance.Select((x, i) => i > 0 && char.IsUpper(x) ? delimiter + x.ToString() : x.ToString())).ToLowerInvariant();
        }

        /// <summary>
        /// Returns a copy of the string in pascal case.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>Returns a copy of the string in pascal case.</returns>
        public static string ToPascalCase(this string instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            return instance.Substring(0, 1).ToUpperInvariant() + instance.Substring(1);
        }

        /// <summary>
        /// Returns a copy of the string in snake case.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>Returns a copy of the string in snake case.</returns>
        public static string ToSnakeCase(this string instance)
        {
            return instance.ToDelimited("-");
        }

        /// <summary>
        /// Uncompresses the specified string instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>Returns an uncompressed string.</returns>
        public static string Uncompress(this string instance)
        {
            var bytes = Convert.FromBase64String(instance);
            using (var inStream = new MemoryStream(bytes))
            {
                using (var outStream = new MemoryStream())
                {
                    using (var zip = new GZipStream(inStream, CompressionMode.Decompress))
                    {
                        zip.CopyTo(outStream);
                    }

                    return Encoding.UTF8.GetString(outStream.ToArray());
                }
            }
        }
    }
}