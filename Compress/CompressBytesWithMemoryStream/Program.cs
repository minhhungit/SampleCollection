using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace CompressBytesWithMemoryStream
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string OriginalText = @"Mây buồn trôi mãi, trôi về nơi xa
Mây cũng tiếc nuối tình chúng ta những ngày qua
Mây buồn tha thiết, áng mây trôi đi lặng lẽ
Cuộc tình ngày nào nay thôi cũng trùng xa mãi.

Còn yêu nhau nữa không, trái tim em như lặng câm
Khi cất tiếng hát là nỗi đau chia lìa nhau
Em buồn biết mấy, biết ta xa nhau từ đây
Yêu em, yêu em mà sao vẫn cứ gian dối.

Giờ anh đi mãi xa, xa thật xa nơi chân trời 
Tình yêu đó sẽ mãi chỉ là bóng mây trôi vào đêm
Mây buồn mây khóc, em buồn em khóc 
Em không tin ta sẽ vang xa nhau từ đây.

Ngoài kia mưa đã rơi như giọt nước mắt không lời 
Tình yêu đó sẽ mãi chỉ là giấc mơ như ngày thơ 
Thôi đừng xa cách, thôi tình đã mất, 
Em quay lưng cho nước mắt dâng tràn đôi mi.

(...)

Ngàn ngôi sao sáng kia, nay dường như đã không còn 
Vì anh đã nỡ xoá hết ngàn chứng nhân duyên tình ta.
Mây buồn đâu nữa, nay dường như đã, 
Em tan đi trong nỗi xót xa riêng mình em.

Người ơi em muốn tin, tin tình yêu anh chân thành
Dù em cố giấu nước mắt ngàn thứ tha khi còn yêu 
Yêu người tha thiết, nhưng người đâu biết
Em không tin ta đã cách xa ngày hôm qua";

            // Convert the text into bytes
            byte[] DataBytes = Encoding.Unicode.GetBytes(OriginalText);
            Console.WriteLine("Original data is {0} bytes", DataBytes.Length);

            // Compress it
            byte[] Compressed = Compress(DataBytes);
            Console.WriteLine("Compressed data is {0} bytes", Compressed.Length);

            // Decompress it
            byte[] Decompressed = Decompress(Compressed);
            Console.WriteLine("Decompressed data is {0} bytes", Decompressed.Length);

            // Convert it back to text
            string DecompressedText = Encoding.Unicode.GetString(Decompressed);
            Console.WriteLine("Is the decompressed text the same as the original? {0}", DecompressedText == OriginalText);

            Console.WriteLine("\n------------------");
            // Print it out
            Console.WriteLine("And here is the decompressed text:\n");
            Console.WriteLine(DecompressedText);

            Console.ReadKey();
        }

        public static string Decompress(string input)
        {
            byte[] compressed = Convert.FromBase64String(input);
            byte[] decompressed = Decompress(compressed);
            return Encoding.UTF8.GetString(decompressed);
        }

        public static string Compress(string input)
        {
            byte[] encoded = Encoding.UTF8.GetBytes(input);
            byte[] compressed = Compress(encoded);
            return Convert.ToBase64String(compressed);
        }

        public static byte[] Decompress(byte[] input)
        {
            using (var source = new MemoryStream(input))
            {
                byte[] lengthBytes = new byte[4];
                source.Read(lengthBytes, 0, 4);

                var length = BitConverter.ToInt32(lengthBytes, 0);
                using (var decompressionStream = new GZipStream(source, CompressionMode.Decompress))
                {
                    var result = new byte[length];
                    decompressionStream.Read(result, 0, length);
                    return result;
                }
            }
        }

        public static byte[] Compress(byte[] input)
        {
            using (var result = new MemoryStream())
            {
                var lengthBytes = BitConverter.GetBytes(input.Length);
                result.Write(lengthBytes, 0, 4);

                using (var compressionStream = new GZipStream(result,
                    CompressionMode.Compress))
                {
                    compressionStream.Write(input, 0, input.Length);
                    compressionStream.Flush();

                }
                return result.ToArray();
            }
        }
    }
}
