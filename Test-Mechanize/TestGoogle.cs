using Mechanize;
using Mechanize.Forms.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Mechanize
{
    public static class TestGoogle
    {
        public static async Task Test()
        {
            using (var browser = new WebBrowser())
            {
                var page = await browser.NavigateAsync("https://www.google.com/");
                if (page.IsHtml)
                {
                    var form = page.Forms["f"];
                    var queryfield = form.FindControl<TextInputControl>("q");

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