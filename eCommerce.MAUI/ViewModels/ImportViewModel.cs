using Amazon.Library.Models;
using Amazon.Library.Services;
using eCommerce.Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.MAUI.ViewModels
{
    public class ImportViewModel
    {
        public string FilePath { get; set; }

        public async void ImportFile(Stream? stream = null)
        {
            StreamReader? sr = null;
            try
            {
                if (stream == null) { sr = new StreamReader(FilePath); }
                else
                {
                    sr = new StreamReader(stream);
                }

            }
            catch (Exception ex)
            {

            }
            string line = string.Empty;
            var products = new List<ProductDTO>();
            try
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue; // Skip empty or whitespace lines

                    var tokens = line.Split('|');

                    // Ensure the expected number of tokens is present
                    if (tokens.Length < 6)
                    {
                        Console.WriteLine("Skipping line due to incorrect format or missing fields.");
                        continue;
                    }

                    try
                    {
                        var product = new ProductDTO
                        {
                            Name = tokens[0],
                            Description = tokens[1],
                            Price = decimal.Parse(tokens[2]),
                            MarkDown = int.Parse(tokens[3]),
                            IsBogo = bool.Parse(tokens[4]),
                            Quantity = int.Parse(tokens[5])
                        };

                        await InventoryServiceProxy.Current.AddOrUpdate(product);
                    }
                    catch (Exception ex)
                    {
                        // Handle specific errors related to product creation or adding
                        Console.WriteLine($"Error processing product line: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, possibly logging the error
                Console.WriteLine($"General error: {ex.Message}");
            }
            finally
            {
                sr?.Dispose(); // Ensure the StreamReader is properly disposed of
            }



        }
    }
}