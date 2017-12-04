// ******************************************************************
// Copyright (c) William Bradley
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using Mechanize;
using Mechanize.Forms.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Mechanize
{
    /// <summary>
    /// Tests Mechanize.NET with a Search Query.
    /// </summary>
    public static class TestGoogle
    {
        public static async Task Test()
        {
            using (var browser = new MechanizeBrowser())
            {
                var page = await browser.NavigateAsync("https://www.google.com/");
                if (page.IsHtml)
                {
                    var form = page.Forms["f"];
                    var queryfield = form.FindControl<ScalarControl>("q");

                    Console.WriteLine("Enter a Google Search Query: ");
                    queryfield.Value = Console.ReadLine();

                    var regsubmit = form.FindControl<SubmitControl>("btnK");

                    // Do you feel lucky, punk?
                    var feelingLucky = form.FindControl<SubmitControl>("btnI");

                    // Well do ya?
                    Console.WriteLine("Feeling lucky? (y/n)");
                    var request = Console.ReadKey();

                    // I gots to know
                    var isFeelingLucky = request.KeyChar == 'y';

                    var submitbutton = isFeelingLucky ? feelingLucky : regsubmit;
                    var newpage = await form.SubmitForm(submitbutton);

                    if (newpage.IsHtml)
                    {
                        if (isFeelingLucky)
                        {
                            OpenBrowser.Open(newpage.RequestInfo.EvaluatedAddress);
                        }
                        else
                        {
                            var results = newpage.Document.DocumentNode.Descendants().Where(item => item.HasClass("g")).ToList();
                            if (results.Count > 0) results.RemoveAt(results.Count - 1); //remove non-query result

                            if (results.Any())
                            {
                                Console.WriteLine("\nResults: \n");
                                foreach (var result in results)
                                {
                                    var elements = result.Descendants();
                                    var title = elements.FirstOrDefault(item => item.HasClass("r"))?.InnerText;

                                    var linkTag = elements.FirstOrDefault(item => item.Name == "cite");
                                    var link = linkTag?.InnerText;

                                    Console.WriteLine(link != null ? $"{title} ({link})" : title);
                                    Console.WriteLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("No Results");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The Query didn't return valid Html");
                    }
                }
                else
                {
                    Console.WriteLine("The Queried Address is not valid");
                }
            }
        }
    }
}