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
            string? imagePath = Console.ReadLine(); // Add '?' to make it nullable

            // Check if the user entered a path
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                Console.WriteLine("Please enter a valid image file path.");
                return;
            }

            // Check if the file exists
            if (!File.Exists(imagePath))
            {
                Console.WriteLine("Image file not found.");
                return;
            }

            // Initialize the Tesseract OCR engine
            string tessdataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");
            using (var engine = new TesseractEngine(tessdataPath, "eng", EngineMode.Default))
            {
                // Set the page segmentation mode to auto
                engine.DefaultPageSegMode = PageSegMode.Auto;

                // Set the image resolution to 300 DPI
                engine.SetVariable("user_defined_dpi", "300");

                // Create a Tesseract page for the image
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    if (img != null)
                    {
                        using (var page = engine.Process(img))
                        {
                            if (page != null)
                            {
                                // Get the recognized text
                                string text = page.GetText();

                                // Display the recognized text
                                Console.WriteLine("Recognized Text:");
                                Console.WriteLine(text);
                            }
                            else
                            {
                                Console.WriteLine("Failed to process the image.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failed to load the image.");
                    }
                }
            }

            // Wait for user input before closing the console window
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
