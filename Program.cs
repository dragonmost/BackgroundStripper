// See https://aka.ms/new-console-template for more information
using System.Drawing;

string executionPath;
if (args.Length <= 0)
{
    executionPath = Path.GetDirectoryName(Environment.ProcessPath);
}
else
{
    executionPath = args[0];
}

string exportPath = Path.Combine(executionPath, "result");
Directory.CreateDirectory(exportPath);

foreach (string file in Directory.GetFiles(executionPath))
{
    if (Path.GetExtension(file) is ".jpg" or ".png")
    {
        string filename = Path.GetFileName(file);
        Console.WriteLine($"Cropping: {filename}");

        Bitmap image = new Bitmap(file);
        image.Clone(FindInnerPhotoBoundingBox(image, Color.White), image.PixelFormat).Save(Path.Combine(exportPath, filename));
    }
    else
    {
        Console.WriteLine($"Skipping: {Path.GetFileName(file)}");
    }
}

Rectangle FindInnerPhotoBoundingBox(Bitmap image, Color backgroundColor)
{
    Rectangle boundingBox = Rectangle.Empty;
    var argb = backgroundColor.ToArgb();

    // Loop through each pixel in the image
    for (int x = 0; x < image.Width; x++)
    {
        for (int y = 0; y < image.Height; y++)
        {
            Color pixelColor = image.GetPixel(x, y);

            // Check if the pixel is part of the inner photo (not the background)
            if (pixelColor.ToArgb() != argb)
            {
                // Update the bounding box as we encounter non-background pixels
                if (boundingBox == Rectangle.Empty)
                {
                    boundingBox = new Rectangle(x, y, 1, 1);
                }
                else
                {
                    boundingBox = Rectangle.Union(boundingBox, new Rectangle(x, y, 1, 1));
                }
            }
        }
    }

    return boundingBox;
}