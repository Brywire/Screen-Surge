using Raylib_cs;

namespace ScreenSurge
{
    public class ResourceManager
    {
        private static ResourceManager instance;
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private string resourceDirectory;

        // Error code
        private ResourceManager(string resourceDirectory)
        {
            if (!Directory.Exists(resourceDirectory))
            {
                throw new DirectoryNotFoundException($"Resource directory not found: {resourceDirectory}");
            }
            this.resourceDirectory = resourceDirectory;
        }

        public static ResourceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new InvalidOperationException("ResourceManager has not been initialized. Call Initialize first.");
                }
                return instance;
            }
        }

        public static void Initialize(string resourceDirectory)
        {
            instance = new ResourceManager(resourceDirectory);
        }

        public Texture2D LoadTexture(string textureName)
        {
            // Check if the texture is already loaded
            if (textures.ContainsKey(textureName))
            {
                return textures[textureName];
            }

            // Construct the full path to the texture file
            string texturePath = Path.Combine(resourceDirectory, textureName);

            // Load the texture
            Texture2D texture = Raylib.LoadTexture(texturePath);

            // Store the texture in the dictionary
            textures.Add(textureName, texture);

            return texture;
        }

        public void UnloadTexture(string textureName)
        {
            // Check if the texture is loaded
            if (textures.ContainsKey(textureName))
            {
                // Unload the texture
                Raylib.UnloadTexture(textures[textureName]);

                // Remove the texture from the dictionary
                textures.Remove(textureName);
            }
        }

        public void UnloadAllTextures()
        {
            foreach (var texture in textures.Values)
            {
                Raylib.UnloadTexture(texture);
            }
            textures.Clear();
        }
    }
}
