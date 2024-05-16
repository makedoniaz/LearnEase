namespace LearnEase.Extensions
{
    public static class StreamExtensions
    {
        public static async Task<string> ReadAsStringAsync(this Stream stream)
        {
            using StreamReader reader = new(stream);
            var bodyAsString = await reader.ReadToEndAsync();

            return bodyAsString;
        }
    }
}