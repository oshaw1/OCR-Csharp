using System;
using System.Drawing;
using Tesseract;
using System.IO;

namespace TextRecognition
{
    class Program
    {
        static void Main(string[] args)
        {
            // Prompt the user to select an image file
            Console.Write("Enter the path to the image file: ");
            string imagePath = Console.ReadLine();

            // Read the image file into memory as a byte array
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            // Initialize a memory stream from the image byte array
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                // Initialize the Tesseract OCR engine
                using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                {
                    // Set the page segmentation mode to auto
                    engine.DefaultPageSegMode = PageSegMode.Auto;

                    // Set the image resolution to 300 DPI
                    engine.SetVariable("user_defined_dpi", "300");

                    // Create a Tesseract page for the image
                    using (var page = engine.Process(Pix.LoadFromMemory(imageBytes)))
                    {
                        // Get the recognized text
                        string text = page.GetText();

                        // Display the recognized text
                        Console.WriteLine("Recognized Text:");
                        Console.WriteLine(text);
                    }
                }
            }

            // Wait for user input before closing the console window
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
