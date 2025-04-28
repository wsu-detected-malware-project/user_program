namespace func.Invest {
    public class GetFile{
            public static IEnumerable<string> GetFilesSafely(string rootDirectory, string[] searchPatterns) {
            Stack<string> directories = new Stack<string>();
            directories.Push(rootDirectory);

            while (directories.Count > 0) {
                string currentDir = directories.Pop();

                try {
                    foreach (string subDir in Directory.GetDirectories(currentDir)) {
                        directories.Push(subDir);
                    }
                }
                catch { continue; }

                foreach (string pattern in searchPatterns) {
                    string[] files = Array.Empty<string>();

                    try { files = Directory.GetFiles(currentDir, pattern); }
                    catch { continue; }

                    foreach (string file in files) {
                        yield return file;
                    }
                }
            }
        }
    }
}