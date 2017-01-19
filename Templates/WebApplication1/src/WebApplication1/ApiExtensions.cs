using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1
{
    public static class ApiExtensions
    {
        public static async Task<string> ReadAsStringAsync(this Stream request)
        {
            using (var stream = new MemoryStream())
            {
                await request.CopyToAsync(stream);

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}