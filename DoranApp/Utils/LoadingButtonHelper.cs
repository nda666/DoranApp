using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DoranApp.Utils;

public static class LoadingButtonHelper
{
    public static void SetLoadingState(Button button, bool isLoading)
    {
        // Load the image from resources
        Image originalImage = Properties.Resources.icons8_loading;

        // Resize the image to 18x18
        Image resizedImage = ResizeImage(originalImage, new Size(18, 18));

        if (isLoading)
        {
            // Set the resized image as button's background image
            button.Image = resizedImage;
            button.ImageAlign = ContentAlignment.MiddleLeft;
            button.TextImageRelation = TextImageRelation.ImageBeforeText;
            // Adjust button properties
            button.BackgroundImageLayout = ImageLayout.Center; // Center the image within the button
            // button.Text = "Mohon tunggu..."; // Set text

            // Disable the button
            button.Enabled = false;
        }
        else
        {
            // Clear background image and reset text
            button.Image = null;
            // button.Text = string.Empty;
            button.TextImageRelation = TextImageRelation.Overlay;
            // Enable the button
            button.Enabled = true;
        }
    }

    private static Image ResizeImage(Image image, Size newSize)
    {
        Bitmap newImage = new Bitmap(newSize.Width, newSize.Height);

        using (Graphics graphics = Graphics.FromImage(newImage))
        {
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;

            graphics.DrawImage(image, 0, 0, newSize.Width, newSize.Height);
        }

        return newImage;
    }
}